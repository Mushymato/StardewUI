using StardewUI.Widgets;
using Xunit.Abstractions;

namespace StardewUI.Framework.Tests.Binding;

public partial class TranslationValueTests(ITestOutputHelper output) : BindingTests(output)
{
    [Fact]
    public void WhenBoundToTranslation_UpdatesWithTranslationValue()
    {
        ResolutionScope.AddTranslation("TranslationKey", "Hello");
        string markup = @"<label text={#TranslationKey} />";
        var tree = BuildTreeFromMarkup(markup, new());

        var label = Assert.IsType<Label>(tree.Views.SingleOrDefault());

        Assert.Equal("Hello", label.Text);
    }
}
