using System.ComponentModel;
using PropertyChanged.SourceGenerator;
using StardewUI.Widgets;
using Xunit.Abstractions;

namespace StardewUI.Framework.Tests.Binding;

public partial class ConditionalTests(ITestOutputHelper output) : BindingTests(output)
{
    partial class ConditionalBindingTestModel : INotifyPropertyChanged
    {
        [Notify]
        private bool firstLineVisible;

        [Notify]
        private bool secondLineVisible;

        [Notify]
        private bool thirdLineVisible;
    }

    [Fact]
    public void WhenConditionalBindingBecomesTrue_AddsView()
    {
        string markup =
            @"<lane>
                <label *if={FirstLineVisible} text=""First Line"" />
                <label text=""Second Line"" />
                <label *if={ThirdLineVisible} text=""Third Line"" />
            </lane>";
        var model = new ConditionalBindingTestModel { FirstLineVisible = true };
        var tree = BuildTreeFromMarkup(markup, model);

        var root = Assert.IsType<Lane>(tree.Views.SingleOrDefault());
        Assert.Collection(
            root.Children,
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal("First Line", label.Text);
            },
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal("Second Line", label.Text);
            }
        );

        model.ThirdLineVisible = true;
        tree.Update();
        Assert.Collection(
            root.Children,
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal("First Line", label.Text);
            },
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal("Second Line", label.Text);
            },
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal("Third Line", label.Text);
            }
        );
    }

    [Fact]
    public void WhenConditionalBindingBecomesFalse_RemovesView()
    {
        string markup =
            @"<lane>
                <label *if={FirstLineVisible} text=""First Line"" />
                <label *if={SecondLineVisible} text=""Second Line"" />
                <label *if=""false"" text=""Third Line"" />
            </lane>";
        var model = new ConditionalBindingTestModel { FirstLineVisible = true, SecondLineVisible = true };
        var tree = BuildTreeFromMarkup(markup, model);

        var root = Assert.IsType<Lane>(tree.Views.SingleOrDefault());
        Assert.Collection(
            root.Children,
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal("First Line", label.Text);
            },
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal("Second Line", label.Text);
            }
        );

        model.FirstLineVisible = false;
        tree.Update();
        Assert.Collection(
            root.Children,
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal("Second Line", label.Text);
            }
        );
    }

    [Fact]
    public void WhenConditionalBindingIsNegated_InvertsCondition()
    {
        string markup =
            @"<lane>
                <label *!if={FirstLineVisible} text=""First Line"" />
                <label *!if={SecondLineVisible} text=""Second Line"" />
            </lane>";
        var model = new ConditionalBindingTestModel { FirstLineVisible = true, SecondLineVisible = false };
        var tree = BuildTreeFromMarkup(markup, model);

        var root = Assert.IsType<Lane>(tree.Views.SingleOrDefault());
        var label = (Label)Assert.Single(root.Children);
        Assert.Equal("Second Line", label.Text);

        model.FirstLineVisible = false;
        model.SecondLineVisible = true;
        tree.Update();

        label = (Label)Assert.Single(root.Children);
        Assert.Equal("First Line", label.Text);
    }

    partial class SwitchCaseLiteralTestModel : INotifyPropertyChanged
    {
        [Notify]
        private int whichItem;
    }

    [Fact]
    public void WhenCaseMatchesDirectChildLiteral_RendersView()
    {
        string markup =
            @"<lane *switch={WhichItem}>
                <label text=""Always"" />
                <label *case=""1"" text=""Item 1"" />
                <label *case=""2"" text=""Item 2"" />
                <label *case=""3"" text=""Item 3"" />
            </lane>";
        var model = new SwitchCaseLiteralTestModel() { WhichItem = 3 };
        var tree = BuildTreeFromMarkup(markup, model);

        var root = Assert.IsType<Lane>(tree.Views.SingleOrDefault());
        Assert.Collection(
            root.Children,
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal("Always", label.Text);
            },
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal("Item 3", label.Text);
            }
        );

        model.WhichItem = 2;
        tree.Update();
        Assert.Collection(
            root.Children,
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal("Always", label.Text);
            },
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal("Item 2", label.Text);
            }
        );
    }

    [Fact]
    public void WhenCaseUsedInSingleChildLayout_RendersOneChild()
    {
        string markup =
            @"<frame *switch={WhichItem}>
                <label *case=""1"" text=""Item 1"" />
                <label *case=""2"" text=""Item 2"" />
                <label *case=""3"" text=""Item 3"" />
            </frame>";
        var model = new SwitchCaseLiteralTestModel() { WhichItem = 3 };
        var tree = BuildTreeFromMarkup(markup, model);

        var root = Assert.IsType<Frame>(tree.Views.SingleOrDefault());
        var label = Assert.IsType<Label>(root.Content);
        Assert.Equal("Item 3", label.Text);

        model.WhichItem = 2;
        tree.Update();
        label = Assert.IsType<Label>(root.Content);
        Assert.Equal("Item 2", label.Text);
    }

    partial class SwitchCaseBindingTestModel : INotifyPropertyChanged
    {
        public enum Selection
        {
            Foo,
            Bar,
        };

        [Notify]
        private Selection current = Selection.Foo;

        [Notify]
        private Selection first = Selection.Foo;

        [Notify]
        private Selection second = Selection.Bar;
    }

    [Fact]
    public void WhenCaseMatchesDirectChildBinding_RendersView()
    {
        string markup =
            @"<lane *switch={Current}>
                <label *case={First} text=""Item 1"" />
                <label *case={Second} text=""Item 2"" />
            </lane>";
        var model = new SwitchCaseBindingTestModel();
        var tree = BuildTreeFromMarkup(markup, model);

        var root = Assert.IsType<Lane>(tree.Views.SingleOrDefault());
        Assert.Collection(
            root.Children,
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal("Item 1", label.Text);
            }
        );

        model.Current = SwitchCaseBindingTestModel.Selection.Bar;
        tree.Update();
        Assert.Collection(
            root.Children,
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal("Item 2", label.Text);
            }
        );
    }

    [Fact]
    public void WhenCaseMatchesIndirectChildBinding_RendersView()
    {
        string markup =
            @"<lane *switch={Current}>
                <frame layout=""24px 24px"">
                    <label *case=""Foo"" text=""Item 1"" />
                    <label *case=""Bar"" text=""Item 2"" />
                </frame>
            </lane>";
        var model = new SwitchCaseBindingTestModel() { Current = SwitchCaseBindingTestModel.Selection.Bar };
        var tree = BuildTreeFromMarkup(markup, model);

        var rootView = Assert.IsType<Lane>(tree.Views.SingleOrDefault());
        var frame = Assert.IsType<Frame>(rootView.Children[0]);
        var label = Assert.IsType<Label>(frame.Content);
        Assert.Equal("Item 2", label.Text);

        model.Current = SwitchCaseBindingTestModel.Selection.Foo;
        tree.Update();
        label = Assert.IsType<Label>(frame.Content);
        Assert.Equal("Item 1", label.Text);
    }
}
