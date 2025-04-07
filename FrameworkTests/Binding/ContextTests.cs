using System.ComponentModel;
using Microsoft.Xna.Framework;
using PropertyChanged.SourceGenerator;
using StardewUI.Framework.Binding;
using StardewUI.Graphics;
using StardewUI.Layout;
using StardewUI.Widgets;
using Xunit.Abstractions;

namespace StardewUI.Framework.Tests.Binding;

public partial class ContextTests(ITestOutputHelper output) : BindingTests(output)
{
    [Fact]
    public void WhenRootContextChanged_UpdatesViewTree()
    {
        AssetCache.Put("Mods/TestMod/TestSprite", UiSprites.SmallTrashCan);

        string markup =
            @"<lane orientation=""vertical"" horizontal-content-alignment=""middle"" vertical-content-alignment=""end"">
                <image scale=""3.5"" sprite={@Mods/TestMod/TestSprite} />
                <label max-lines=""2"" text={HeaderText} />
            </lane>";
        var tree = BuildTreeFromMarkup(markup, new { HeaderText = "" });

        var root = Assert.IsType<Lane>(tree.Views.SingleOrDefault());
        Assert.Equal(Orientation.Vertical, root.Orientation);
        Assert.Equal(Alignment.Middle, root.HorizontalContentAlignment);
        Assert.Equal(Alignment.End, root.VerticalContentAlignment);
        Assert.Collection(
            root.Children,
            child =>
            {
                var image = Assert.IsType<Image>(child);
                Assert.Equal(3.5f, image.Scale);
                Assert.Equal(UiSprites.SmallTrashCan, image.Sprite);
            },
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal(2, label.MaxLines);
                Assert.Equal("", label.Text);
            }
        );

        var model = new { HeaderText = "Some text" };
        tree.Context = BindingContext.Create(model);
        tree.Update();

        Assert.Equal("Some text", ((Label)root.Children[1]).Text);
    }

    partial class ContextTestModel : INotifyPropertyChanged
    {
        public partial class OuterData : INotifyPropertyChanged
        {
            [Notify]
            private MiddleData? middle;
        }

        public partial class MiddleData : INotifyPropertyChanged
        {
            [Notify]
            private InnerData? inner;
        }

        public partial class InnerData : INotifyPropertyChanged
        {
            [Notify]
            private string text = "";
        }

        [Notify]
        private OuterData? outer;
    }

    [Fact]
    public void WhenAnyContextChanges_UpdatesAllContextModifiers()
    {
        string markup =
            @"<frame *context={Outer}>
                <panel>
                    <panel *context={Middle}>
                        <lane *context={Inner}>
                            <label text={Text} />
                        </lane>
                    </panel>
                </panel>
            </frame>";
        var model = new ContextTestModel();
        var tree = BuildTreeFromMarkup(markup, model);

        var outerFrame = Assert.IsType<Frame>(tree.Views.SingleOrDefault());
        var outerPanel = Assert.IsType<Panel>(outerFrame.Content);
        var middlePanel = Assert.IsType<Panel>(outerPanel.Children.FirstOrDefault());
        var innerLane = Assert.IsType<Lane>(middlePanel.Children.FirstOrDefault());
        var label = Assert.IsType<Label>(innerLane.Children.FirstOrDefault());

        Assert.Equal("", label.Text);

        model.Outer = new() { Middle = new() { Inner = new() { Text = "foo" } } };
        tree.Update();

        Assert.Equal("foo", label.Text);

        model.Outer.Middle = new() { Inner = new() { Text = "bar" } };
        tree.Update();

        Assert.Equal("bar", label.Text);

        model.Outer.Middle.Inner = new() { Text = "baz" };
        tree.Update();

        Assert.Equal("baz", label.Text);
    }

    partial class ContextWalkingTestModel : INotifyPropertyChanged
    {
        [Notify]
        private int maxLines;

        [Notify]
        private List<ItemData> items = [];

        public partial class ItemData : INotifyPropertyChanged
        {
            [Notify]
            private Color color = Color.White;

            [Notify]
            private ItemInnerData? inner;
        }

        public partial class ItemInnerData : INotifyPropertyChanged
        {
            [Notify]
            private string text = "";
        }
    }

    [Fact]
    public void WhenAncestorContextChanges_UpdatesDescendantsWithDistanceRedirects()
    {
        string markup =
            @"<lane orientation=""vertical"">
                <lane *repeat={Items}>
                    <frame *context={Inner}>
                        <label max-lines={^^MaxLines} color={^Color} text={Text} />
                    </frame>
                </lane>
            </lane>";
        var model = new ContextWalkingTestModel()
        {
            MaxLines = 1,
            Items =
            [
                new()
                {
                    Color = Color.White,
                    Inner = new() { Text = "Item 1" },
                },
                new() { Color = Color.Aqua },
            ],
        };
        var tree = BuildTreeFromMarkup(markup, model);

        var rootView = Assert.IsType<Lane>(tree.Views.SingleOrDefault());
        Assert.Collection(
            rootView.Children,
            child =>
            {
                var lane = Assert.IsType<Lane>(child);
                var frame = Assert.IsType<Frame>(lane.Children.SingleOrDefault());
                var label = Assert.IsType<Label>(frame.Content);
                Assert.Equal(1, label.MaxLines);
                Assert.Equal(Color.White, label.Color);
                Assert.Equal("Item 1", label.Text);
            },
            child =>
            {
                var lane = Assert.IsType<Lane>(child);
                var frame = Assert.IsType<Frame>(lane.Children.SingleOrDefault());
                Assert.IsType<Label>(frame.Content);
                // It's not useful to assert anything about the label here, because we didn't set the Inner data and
                // therefore it doesn't have a context. One of the quirks of this system is that the context cannot be
                // walked unless the current context is set; thus even the ^ and ^^ attributes do nothing at this point.
            }
        );

        model.MaxLines = 2;
        model.Items[1].Inner = new() { Text = "Item 2" };
        tree.Update();

        Assert.Collection(
            rootView.Children,
            child =>
            {
                var lane = Assert.IsType<Lane>(child);
                var frame = Assert.IsType<Frame>(lane.Children.SingleOrDefault());
                var label = Assert.IsType<Label>(frame.Content);
                Assert.Equal(2, label.MaxLines);
                Assert.Equal(Color.White, label.Color);
                Assert.Equal("Item 1", label.Text);
            },
            child =>
            {
                var lane = Assert.IsType<Lane>(child);
                var frame = Assert.IsType<Frame>(lane.Children.SingleOrDefault());
                var label = Assert.IsType<Label>(frame.Content);
                Assert.Equal(2, label.MaxLines);
                Assert.Equal(Color.Aqua, label.Color);
                Assert.Equal("Item 2", label.Text);
            }
        );

        model.Items[0].Color = Color.Yellow;
        model.Items[0].Inner!.Text = "Item 3";
        model.Items[1].Color = Color.Lime;
        tree.Update();

        Assert.Collection(
            rootView.Children,
            child =>
            {
                var lane = Assert.IsType<Lane>(child);
                var frame = Assert.IsType<Frame>(lane.Children.SingleOrDefault());
                var label = Assert.IsType<Label>(frame.Content);
                Assert.Equal(2, label.MaxLines);
                Assert.Equal(Color.Yellow, label.Color);
                Assert.Equal("Item 3", label.Text);
            },
            child =>
            {
                var lane = Assert.IsType<Lane>(child);
                var frame = Assert.IsType<Frame>(lane.Children.SingleOrDefault());
                var label = Assert.IsType<Label>(frame.Content);
                Assert.Equal(2, label.MaxLines);
                Assert.Equal(Color.Lime, label.Color);
                Assert.Equal("Item 2", label.Text);
            }
        );
    }

    [Fact]
    public void WhenAncestorContextChanges_UpdatesDescendantsWithTypeRedirects()
    {
        string markup =
            @"<lane orientation=""vertical"">
                <lane *repeat={Items}>
                    <frame *context={Inner}>
                        <label max-lines={~ContextWalkingTestModel.MaxLines} color={~ItemData.Color} text={Text} />
                    </frame>
                </lane>
            </lane>";
        var model = new ContextWalkingTestModel()
        {
            MaxLines = 1,
            Items =
            [
                new()
                {
                    Color = Color.White,
                    Inner = new() { Text = "Item 1" },
                },
                new() { Color = Color.Aqua },
            ],
        };
        var tree = BuildTreeFromMarkup(markup, model);

        var rootView = Assert.IsType<Lane>(tree.Views.SingleOrDefault());
        Assert.Collection(
            rootView.Children,
            child =>
            {
                var lane = Assert.IsType<Lane>(child);
                var frame = Assert.IsType<Frame>(lane.Children.SingleOrDefault());
                var label = Assert.IsType<Label>(frame.Content);
                Assert.Equal(1, label.MaxLines);
                Assert.Equal(Color.White, label.Color);
                Assert.Equal("Item 1", label.Text);
            },
            child =>
            {
                var lane = Assert.IsType<Lane>(child);
                var frame = Assert.IsType<Frame>(lane.Children.SingleOrDefault());
                Assert.IsType<Label>(frame.Content);
                // It's not useful to assert anything about the label here, because we didn't set the Inner data and
                // therefore it doesn't have a context. One of the quirks of this system is that the context cannot be
                // walked unless the current context is set; thus even the ^ and ^^ attributes do nothing at this point.
            }
        );

        model.MaxLines = 2;
        model.Items[0].Color = Color.Yellow;
        model.Items[0].Inner!.Text = "Item 3";
        model.Items[1].Color = Color.Lime;
        model.Items[1].Inner = new() { Text = "Item 2" };
        tree.Update();

        Assert.Collection(
            rootView.Children,
            child =>
            {
                var lane = Assert.IsType<Lane>(child);
                var frame = Assert.IsType<Frame>(lane.Children.SingleOrDefault());
                var label = Assert.IsType<Label>(frame.Content);
                Assert.Equal(2, label.MaxLines);
                Assert.Equal(Color.Yellow, label.Color);
                Assert.Equal("Item 3", label.Text);
            },
            child =>
            {
                var lane = Assert.IsType<Lane>(child);
                var frame = Assert.IsType<Frame>(lane.Children.SingleOrDefault());
                var label = Assert.IsType<Label>(frame.Content);
                Assert.Equal(2, label.MaxLines);
                Assert.Equal(Color.Lime, label.Color);
                Assert.Equal("Item 2", label.Text);
            }
        );
    }
}
