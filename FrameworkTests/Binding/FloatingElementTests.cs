using System.ComponentModel;
using Microsoft.Xna.Framework;
using PropertyChanged.SourceGenerator;
using StardewUI.Widgets;
using Xunit.Abstractions;

namespace StardewUI.Framework.Tests.Binding;

public partial class FloatingElementTests(ITestOutputHelper output) : BindingTests(output)
{
    [Fact]
    public void WhenSimpleNodeHasFloatAttribute_AddsFloatingElement()
    {
        string markup =
            @"<panel>
                <label text=""foo"" />
                <label *float=""above"" text=""bar"" />
                <label text=""baz"" />
                <label *float=""before; -10, 4"" text=""quux"" />
            </panel>";
        var tree = BuildTreeFromMarkup(markup, new());

        var panel = Assert.IsType<Panel>(tree.Views.SingleOrDefault());
        Assert.Collection(
            panel.Children,
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal("foo", label.Text);
            },
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal("baz", label.Text);
            }
        );
        Assert.Collection(
            panel.FloatingElements,
            fe =>
            {
                // Floating positions end up as function delegates, so the only way to verify them is to actually
                // compute the final position against some dummy sizes.
                Assert.Equal(new Vector2(0, -24), fe.Position.GetOffset(new Vector2(80, 24), new Vector2(200, 50)));
                var label = Assert.IsType<Label>(fe.View);
                Assert.Equal("bar", label.Text);
            },
            fe =>
            {
                Assert.Equal(new Vector2(-90, 4), fe.Position.GetOffset(new Vector2(80, 24), new Vector2(200, 50)));
                var label = Assert.IsType<Label>(fe.View);
                Assert.Equal("quux", label.Text);
            }
        );
    }

    partial class ConditionalFloatModel : INotifyPropertyChanged
    {
        [Notify]
        private bool showFloat;
    }

    [Fact]
    public void WhenConditionalNodeHasFloatAttribute_AddsOrRemovesFloatingElement()
    {
        string markup =
            @"<panel>
                <label *float=""after"" *if={ShowFloat} text=""foo"" />
            </panel>";
        var model = new ConditionalFloatModel();
        var tree = BuildTreeFromMarkup(markup, model);

        var panel = Assert.IsType<Panel>(tree.Views.SingleOrDefault());
        Assert.Empty(panel.Children);
        Assert.Empty(panel.FloatingElements);

        model.ShowFloat = true;
        tree.Update();

        Assert.Empty(panel.Children);
        var fe = Assert.Single(panel.FloatingElements);
        Assert.Equal(new Vector2(200, 0), fe.Position.GetOffset(new Vector2(80, 24), new Vector2(200, 50)));
        var label = Assert.IsType<Label>(fe.View);
        Assert.Equal("foo", label.Text);
    }

    class RepeatingFloatModel
    {
        public IReadOnlyList<Badge> Badges { get; set; } = [];

        public class Badge(string text, Func<Vector2, Vector2, Vector2> position)
        {
            public Func<Vector2, Vector2, Vector2> Position => position;
            public string Text => text;
        }
    }

    [Fact]
    public void WhenRepeatingNodeHasFloatAttribute_AddsAllAsFloatingElements()
    {
        string markup =
            @"<panel>
                <label *repeat={Badges} *float={Position} text={Text} />
            </panel>";
        var model = new RepeatingFloatModel()
        {
            Badges =
            [
                new("foo", (floatSize, parentSize) => new(parentSize.X - floatSize.X, 0)),
                new("bar", (floatSize, parentSize) => new(parentSize.X - floatSize.X, 20)),
                new("baz", (floatSize, parentSize) => new(parentSize.X - floatSize.X, 40)),
            ],
        };
        var tree = BuildTreeFromMarkup(markup, model);

        var panel = Assert.IsType<Panel>(tree.Views.SingleOrDefault());
        Assert.Collection(
            panel.FloatingElements,
            fe =>
            {
                Assert.Equal(new Vector2(120, 0), fe.Position.GetOffset(new Vector2(80, 24), new Vector2(200, 50)));
                var label = Assert.IsType<Label>(fe.View);
                Assert.Equal("foo", label.Text);
            },
            fe =>
            {
                Assert.Equal(new Vector2(120, 20), fe.Position.GetOffset(new Vector2(80, 24), new Vector2(200, 50)));
                var label = Assert.IsType<Label>(fe.View);
                Assert.Equal("bar", label.Text);
            },
            fe =>
            {
                Assert.Equal(new Vector2(120, 40), fe.Position.GetOffset(new Vector2(80, 24), new Vector2(200, 50)));
                var label = Assert.IsType<Label>(fe.View);
                Assert.Equal("baz", label.Text);
            }
        );
    }
}
