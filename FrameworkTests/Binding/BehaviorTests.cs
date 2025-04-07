using System.ComponentModel;
using PropertyChanged.SourceGenerator;
using StardewUI.Framework.Behaviors;
using StardewUI.Widgets;
using Xunit.Abstractions;

namespace StardewUI.Framework.Tests.Binding;

public partial class BehaviorTests(ITestOutputHelper output) : BindingTests(output)
{
    class FreshMaker : ViewBehavior<Label, int?>
    {
        private string freshness = "";
        private string originalText = "";

        public override void Update(TimeSpan elapsed)
        {
            if (!string.IsNullOrEmpty(freshness) && !View.Text.StartsWith(freshness))
            {
                View.Text = freshness + ' ' + originalText;
            }
        }

        // This is very much a simplified test implementation that isn't meant to resemble the real thing.
        // A real behavior would have to be a lot more careful about the text being changed *after* attaching, and be
        // able to revert to the "most recent" view properties prior to the behavior actually taking effect.
        protected override void OnAttached()
        {
            originalText = View.Text;
        }

        protected override void OnNewData(int? previousData)
        {
            freshness = Data switch
            {
                1 => "Fresh",
                2 => "Fresher",
                3 => "Freshest",
                _ => "",
            };
        }
    }

    [Fact]
    public void WhenBehaviorCreatedWithLiteral_InvokesBehavior()
    {
        BehaviorFactory.Register<FreshMaker>("fresh");

        string markup = @"<label text=""Artichoke"" +fresh=""1"" />";
        var tree = BuildTreeFromMarkup(markup, new());

        var label = Assert.IsType<Label>(tree.Views.SingleOrDefault());
        Assert.Equal("Fresh Artichoke", label.Text);
    }

    partial class BehaviorTestModel : INotifyPropertyChanged
    {
        [Notify]
        private int freshness;
    }

    [Fact]
    public void WhenBehaviorCreatedWithBinding_AndBoundValueUpdates_UpdatesBehaviorData()
    {
        BehaviorFactory.Register<FreshMaker>("fresh");

        string markup = @"<label text=""Clam"" +fresh={{Freshness}} />";
        var model = new BehaviorTestModel() { Freshness = 2 };
        var tree = BuildTreeFromMarkup(markup, model);

        var label = Assert.IsType<Label>(tree.Views.SingleOrDefault());
        Assert.Equal("Fresher Clam", label.Text);

        model.Freshness = 3;
        tree.Update();
        Assert.Equal("Freshest Clam", label.Text);
    }

    [Fact]
    public void WhenBehaviorAddedToUnsupportedViewType_IgnoresBehavior()
    {
        BehaviorFactory.Register<FreshMaker>("fresh");

        string markup = @"<button text=""Hello"" +fresh=""1"" />";
        var tree = BuildTreeFromMarkup(markup, new());

        var button = Assert.IsType<Button>(tree.Views.SingleOrDefault());
        Assert.Equal("Hello", button.Text);
    }
}
