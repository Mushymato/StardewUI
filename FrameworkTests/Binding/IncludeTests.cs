using System.Collections.ObjectModel;
using System.ComponentModel;
using PropertyChanged.SourceGenerator;
using StardewUI.Framework.Dom;
using StardewUI.Graphics;
using StardewUI.Widgets;
using Xunit.Abstractions;

namespace StardewUI.Framework.Tests.Binding;

public partial class IncludeTests(ITestOutputHelper output) : BindingTests(output)
{
    partial class SingleIncludeModel : INotifyPropertyChanged
    {
        [Notify]
        private string assetName = "";

        [Notify]
        private string labelText = "";

        [Notify]
        private Sprite? sprite;
    }

    [Fact]
    public void WhenIncludedViewBindsToTranslation_UsesResolutionScopeForViewDocument()
    {
        ResolutionScope.AddTranslation("OuterKey", "Foo");
        var includedDocument = Document.Parse(@"<label text={#IncludedKey} />");
        var includedScope = new FakeResolutionScope();
        includedScope.AddTranslation("IncludedKey", "Bar");
        ResolutionScopeFactory.AddForDocument(includedDocument, includedScope);
        AssetCache.Put("IncludedView", includedDocument);

        string markup =
            @"<lane>
                <label text={#OuterKey} />
                <include name=""IncludedView"" />
            </lane>";
        var tree = BuildTreeFromMarkup(markup, new());

        var lane = Assert.IsType<Lane>(tree.Views.SingleOrDefault());
        var outerLabel = Assert.IsType<Label>(lane.Children[0]);
        Assert.Equal("Foo", outerLabel.Text);
        var innerLabel = Assert.IsType<Label>(lane.Children[1]);
        Assert.Equal("Bar", innerLabel.Text);
    }

    [Fact]
    public void WhenIncludedDataChanges_UpdatesView()
    {
        AssetCache.Put("LabelView", Document.Parse(@"<label text={LabelText} />"));
        string markup =
            @"<frame>
                <include name=""LabelView"" />
            </frame>";
        var model = new SingleIncludeModel() { LabelText = "Foo" };
        var tree = BuildTreeFromMarkup(markup, model);

        var rootView = Assert.IsType<Frame>(tree.Views.SingleOrDefault());
        var label = Assert.IsType<Label>(rootView.Content);
        Assert.Equal("Foo", label.Text);

        model.LabelText = "Bar";
        tree.Update();

        Assert.Equal(label, rootView.Content);
        Assert.Equal("Bar", label.Text);
    }

    [Fact]
    public void WhenIncludedNameChanges_ReplacesView()
    {
        AssetCache.Put("LabelView", Document.Parse(@"<label text={LabelText} />"));
        AssetCache.Put("ImageView", Document.Parse(@"<image sprite={Sprite} />"));
        string markup =
            @"<frame>
                <include name={AssetName} />
            </frame>";
        var model = new SingleIncludeModel()
        {
            AssetName = "LabelView",
            LabelText = "Foo",
            Sprite = UiSprites.SmallGreenPlus,
        };
        var tree = BuildTreeFromMarkup(markup, model);

        var rootView = Assert.IsType<Frame>(tree.Views.SingleOrDefault());
        var label = Assert.IsType<Label>(rootView.Content);
        Assert.Equal("Foo", label.Text);

        model.AssetName = "ImageView";
        tree.Update();

        var image = Assert.IsType<Image>(rootView.Content);
        Assert.Equal(UiSprites.SmallGreenPlus, image.Sprite);
    }

    [Fact]
    public void WhenIncludedAssetExpires_ReloadsAndReplacesView()
    {
        AssetCache.Put("IncludedView", Document.Parse(@"<label text={LabelText} />"));
        string markup =
            @"<frame>
                <include name=""IncludedView"" />
            </frame>";
        var model = new SingleIncludeModel() { LabelText = "Foo", Sprite = UiSprites.ButtonDark };
        var tree = BuildTreeFromMarkup(markup, model);

        var rootView = Assert.IsType<Frame>(tree.Views.SingleOrDefault());
        var label = Assert.IsType<Label>(rootView.Content);
        Assert.Equal("Foo", label.Text);

        AssetCache.Put("IncludedView", Document.Parse(@"<image sprite={Sprite} tooltip={LabelText} />"));
        tree.Update();

        var image = Assert.IsType<Image>(rootView.Content);
        Assert.Equal(UiSprites.ButtonDark, image.Sprite);
        Assert.Equal("Foo", image.Tooltip);
    }

    partial class NestedIncludeModel : INotifyPropertyChanged
    {
        [Notify]
        private InnerData inner = new();

        public partial class InnerData
        {
            [Notify]
            private string labelText = "";
        }
    }

    [Fact]
    public void WhenIncludedExplicitContextChanges_UpdatesView()
    {
        AssetCache.Put("LabelView", Document.Parse(@"<label text={LabelText} />"));
        string markup =
            @"<frame>
                <include name=""LabelView"" *context={Inner} />
            </frame>";
        var model = new NestedIncludeModel() { Inner = new() { LabelText = "Foo" } };
        var tree = BuildTreeFromMarkup(markup, model);

        var rootView = Assert.IsType<Frame>(tree.Views.SingleOrDefault());
        var label = Assert.IsType<Label>(rootView.Content);
        Assert.Equal("Foo", label.Text);

        model.Inner = new() { LabelText = "Bar" };
        tree.Update();

        Assert.Equal(label, rootView.Content);
        Assert.Equal("Bar", label.Text);
    }

    partial class RepeatingIncludeModel : INotifyPropertyChanged
    {
        [Notify]
        private ObservableCollection<InnerData> items = [];

        [Notify]
        private int maxLines;

        public partial class InnerData
        {
            [Notify]
            private string labelText = "";
        }
    }

    [Fact]
    public void WhenIncludedImplicitContextChanges_UpdatesView()
    {
        AssetCache.Put("LabelView", Document.Parse(@"<label max-lines={^MaxLines} text={LabelText} />"));
        string markup =
            @"<lane>
                <include name=""LabelView"" *repeat={Items} />
            </lane>";
        var model = new RepeatingIncludeModel()
        {
            Items = [new() { LabelText = "Foo" }, new() { LabelText = "Bar" }],
            MaxLines = 3,
        };
        var tree = BuildTreeFromMarkup(markup, model);

        var rootView = Assert.IsType<Lane>(tree.Views.SingleOrDefault());
        Assert.Collection(
            rootView.Children,
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal(3, label.MaxLines);
                Assert.Equal("Foo", label.Text);
            },
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal(3, label.MaxLines);
                Assert.Equal("Bar", label.Text);
            }
        );

        model.Items[0] = new() { LabelText = "Baz" };
        model.Items[1] = new() { LabelText = "Quux" };
        model.Items.Add(new() { LabelText = "Meep" });
        tree.Update();

        Assert.Collection(
            rootView.Children,
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal(3, label.MaxLines);
                Assert.Equal("Baz", label.Text);
            },
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal(3, label.MaxLines);
                Assert.Equal("Quux", label.Text);
            },
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal(3, label.MaxLines);
                Assert.Equal("Meep", label.Text);
            }
        );
    }
}
