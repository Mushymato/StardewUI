using System.ComponentModel;
using PropertyChanged.SourceGenerator;
using StardewUI.Widgets;
using Xunit.Abstractions;

namespace StardewUI.Framework.Tests.Binding;

public partial class OutletTests(ITestOutputHelper output) : BindingTests(output)
{
    partial class MultipleOutletsModel : INotifyPropertyChanged
    {
        public string ContentText { get; set; } = "";
        public string HeaderCollapsedText { get; set; } = "";
        public string HeaderExpandedText { get; set; } = "";
        public bool IsCollapsed => !IsExpanded;

        [Notify]
        private bool isExpanded;
    }

    [Fact]
    public void WhenMultipleOutletsTargeted_SetsViewPerOutlet()
    {
        string markup =
            @"<expander is-expanded={<>IsExpanded}>
                <frame *outlet=""header"" *if={IsExpanded}>
                    <label text={:HeaderExpandedText} />
                </frame>
                <frame *outlet=""header"" *if={IsCollapsed}>
                    <label text={:HeaderCollapsedText} />
                </frame>
                <panel>
                    <label text={:ContentText} />
                </panel>
            </expander>";
        var model = new MultipleOutletsModel
        {
            HeaderCollapsedText = "Expand",
            HeaderExpandedText = "Collapse",
            ContentText = "Content Text",
        };
        var tree = BuildTreeFromMarkup(markup, model);

        var expander = Assert.IsType<Expander>(tree.Views.SingleOrDefault());
        var header = Assert.IsType<Frame>(expander.Header);
        var headerLabel = Assert.IsType<Label>(header.Content);
        Assert.Equal("Expand", headerLabel.Text);
        var content = Assert.IsType<Panel>(expander.Content);
        var contentLabel = Assert.IsType<Label>(content.Children[0]);
        Assert.Equal("Content Text", contentLabel.Text);

        model.IsExpanded = true;
        tree.Update();

        header = Assert.IsType<Frame>(expander.Header);
        headerLabel = Assert.IsType<Label>(header.Content);
        Assert.Equal("Collapse", headerLabel.Text);
    }
}
