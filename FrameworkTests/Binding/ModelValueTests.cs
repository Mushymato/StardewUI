using System.ComponentModel;
using Microsoft.Xna.Framework;
using PropertyChanged.SourceGenerator;
using StardewUI.Widgets;
using Xunit.Abstractions;

namespace StardewUI.Framework.Tests.Binding;

public partial class ModelValueTests(ITestOutputHelper output) : BindingTests(output)
{
    partial class InputBindingTestModel : INotifyPropertyChanged
    {
        [Notify]
        private Color color;

        [Notify]
        private string name = "";
    }

    [Fact]
    public void WhenInputBinding_AndModelChanges_UpdatesView()
    {
        string markup = @"<label max-lines=""1"" color={Color} text={Name} />";
        var model = new InputBindingTestModel() { Name = "Test text", Color = Color.Blue };
        var tree = BuildTreeFromMarkup(markup, model);

        var label = Assert.IsType<Label>(tree.Views.SingleOrDefault());
        Assert.Equal(1, label.MaxLines);
        Assert.Equal("Test text", label.Text);
        Assert.Equal(Color.Blue, label.Color);

        model.Name = "New text";
        tree.Update();
        Assert.Equal("New text", label.Text);
    }

    partial class OutputBindingTestModel : INotifyPropertyChanged
    {
        [Notify]
        private bool @checked;

        [Notify]
        private Vector2 size;
    }

    [Fact]
    public void WhenOutputBinding_AndViewChanges_UpdatesModel()
    {
        string markup = @"<checkbox layout=""200px 20px"" is-checked={>Checked} outer-size={>Size} />";
        var model = new OutputBindingTestModel { Checked = false, Size = Vector2.Zero };
        var tree = BuildTreeFromMarkup(markup, model);

        // Initial bind should generally not cause immediate output sync, because we assume the view isn't completely
        // stable or fully initialized yet.
        Assert.False(model.Checked);
        Assert.Equal(Vector2.Zero, model.Size);

        var checkbox = Assert.IsType<CheckBox>(tree.Views.SingleOrDefault());
        checkbox.Measure(new Vector2(1000, 1000));
        tree.Update();
        Assert.False(model.Checked);
        Assert.Equal(new Vector2(200, 20), model.Size);

        checkbox.IsChecked = true;
        tree.Update();
        Assert.True(model.Checked);
        Assert.Equal(new Vector2(200, 20), model.Size);
    }

    [Fact]
    public void WhenInOutBinding_AndViewOrModelChanges_UpdatesOtherEnd()
    {
        string markup = @"<checkbox is-checked={<>Checked} />";
        var model = new OutputBindingTestModel { Checked = true };
        var tree = BuildTreeFromMarkup(markup, model);

        var checkbox = Assert.IsType<CheckBox>(tree.Views.SingleOrDefault());
        Assert.True(model.Checked);
        Assert.True(checkbox.IsChecked);

        // No changes, nothing should happen here.
        tree.Update();
        Assert.True(model.Checked);
        Assert.True(checkbox.IsChecked);

        // Simulate click to uncheck
        checkbox.IsChecked = false;
        tree.Update();
        Assert.False(model.Checked);

        // Now the context is updated from some other source
        model.Checked = true;
        tree.Update();
        Assert.True(checkbox.IsChecked);
    }

    [Fact]
    public void WhenOneTimeBinding_UpdatesFirstTimeOnly()
    {
        string markup =
            @"<lane>
                <label text={Name} />
                <label text={<:Name} />
            </lane>";
        var model = new InputBindingTestModel() { Name = "First" };
        var tree = BuildTreeFromMarkup(markup, model);

        var lane = Assert.IsType<Lane>(tree.Views.SingleOrDefault());
        var label1 = Assert.IsType<Label>(lane.Children[0]);
        var label2 = Assert.IsType<Label>(lane.Children[1]);

        Assert.Equal("First", label1.Text);
        Assert.Equal("First", label2.Text);

        model.Name = "Second";
        tree.Update();

        Assert.Equal("Second", label1.Text);
        Assert.Equal("First", label2.Text);
    }

    class FieldBindingTestModel
    {
        public string Name = "";
        public string Description = "";
    }

    [Fact]
    public void WhenModelContainsFields_BindsInitialValues()
    {
        string markup = @"<label text={Name} tooltip={Description} />";
        var model = new FieldBindingTestModel() { Name = "Foo", Description = "Bar" };
        var tree = BuildTreeFromMarkup(markup, model);

        var label = Assert.IsType<Label>(tree.Views.SingleOrDefault());

        Assert.Equal("Foo", label.Text);
        Assert.Equal("Bar", label.Tooltip);
    }
}
