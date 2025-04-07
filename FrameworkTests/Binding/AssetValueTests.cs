using System.ComponentModel;
using PropertyChanged.SourceGenerator;
using StardewUI.Widgets;
using Xunit.Abstractions;

namespace StardewUI.Framework.Tests.Binding;

public partial class AssetValueTests(ITestOutputHelper output) : BindingTests(output)
{
    partial class AssetBindingModel : INotifyPropertyChanged
    {
        [Notify]
        private string assetName = "";
    }

    [Fact]
    public void WhenAssetInputBinding_BindsInitialAsset()
    {
        // Asset + model bindings are most likely to be used for images, but those are much more difficult to test as
        // they require game content to be loaded, so we make do with just another string.
        AssetCache.Put("LabelText", "Some Text");

        string markup = @"<label text={@<AssetName} />";
        var model = new AssetBindingModel { AssetName = "LabelText" };
        var tree = BuildTreeFromMarkup(markup, model);

        var label = Assert.IsType<Label>(tree.Views.SingleOrDefault());
        Assert.Equal("Some Text", label.Text);
    }

    [Fact]
    public void WhenAssetInputBinding_UpdatesWhenNameChanges()
    {
        AssetCache.Put("LabelText1", "First Text");
        AssetCache.Put("LabelText2", "Second Text");

        string markup = @"<label text={@<AssetName} />";
        var model = new AssetBindingModel { AssetName = "LabelText1" };
        var tree = BuildTreeFromMarkup(markup, model);
        model.AssetName = "LabelText2";
        tree.Update();

        var label = Assert.IsType<Label>(tree.Views.SingleOrDefault());
        Assert.Equal("Second Text", label.Text);
    }

    [Fact]
    public void WhenAssetInputBinding_UpdatesWhenAssetChanges()
    {
        AssetCache.Put("LabelText", "First Text");

        string markup = @"<label text={@<AssetName} />";
        var model = new AssetBindingModel { AssetName = "LabelText" };
        var tree = BuildTreeFromMarkup(markup, model);
        AssetCache.Put("LabelText", "Second Text");
        tree.Update();

        var label = Assert.IsType<Label>(tree.Views.SingleOrDefault());
        Assert.Equal("Second Text", label.Text);
    }

    [Fact]
    public void WhenAssetOneTimeBinding_BindsInitialAsset()
    {
        // Asset + model bindings are most likely to be used for images, but those are much more difficult to test as
        // they require game content to be loaded, so we make do with just another string.
        AssetCache.Put("LabelText", "Some Text");

        string markup = @"<label text={@:AssetName} />";
        var model = new AssetBindingModel { AssetName = "LabelText" };
        var tree = BuildTreeFromMarkup(markup, model);

        var label = Assert.IsType<Label>(tree.Views.SingleOrDefault());
        Assert.Equal("Some Text", label.Text);
    }

    [Fact]
    public void WhenAssetOneTimeBinding_IgnoresUpdateWhenNameChanges()
    {
        AssetCache.Put("LabelText1", "First Text");
        AssetCache.Put("LabelText2", "Second Text");

        string markup = @"<label text={@:AssetName} />";
        var model = new AssetBindingModel { AssetName = "LabelText1" };
        var tree = BuildTreeFromMarkup(markup, model);
        model.AssetName = "LabelText2";
        tree.Update();

        var label = Assert.IsType<Label>(tree.Views.SingleOrDefault());
        Assert.Equal("First Text", label.Text);
    }

    [Fact]
    public void WhenAssetOneTimeBinding_UpdatesWhenAssetChanges()
    {
        AssetCache.Put("LabelText", "First Text");

        string markup = @"<label text={@:AssetName} />";
        var model = new AssetBindingModel { AssetName = "LabelText" };
        var tree = BuildTreeFromMarkup(markup, model);
        AssetCache.Put("LabelText", "Second Text");
        tree.Update();

        var label = Assert.IsType<Label>(tree.Views.SingleOrDefault());
        Assert.Equal("Second Text", label.Text);
    }
}
