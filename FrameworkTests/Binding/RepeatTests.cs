using System.Collections.ObjectModel;
using System.ComponentModel;
using PropertyChanged.SourceGenerator;
using StardewUI.Widgets;
using Xunit.Abstractions;

namespace StardewUI.Framework.Tests.Binding;

public partial class RepeatTests(ITestOutputHelper output) : BindingTests(output)
{
    partial class RepeatingElement : INotifyPropertyChanged
    {
        [Notify]
        private string name = "";
    }

    partial class RepeatingModel : INotifyPropertyChanged
    {
        [Notify]
        private List<RepeatingElement> items = [];
    }

    [Fact]
    public void WhenRepeating_RendersViewPerElement()
    {
        string markup =
            @"<lane>
                <label *repeat={Items} text={Name} />
            </lane>";
        var model = new RepeatingModel()
        {
            Items = [new() { Name = "Foo" }, new() { Name = "Bar" }, new() { Name = "Baz" }],
        };
        var tree = BuildTreeFromMarkup(markup, model);

        var root = Assert.IsType<Lane>(tree.Views.SingleOrDefault());
        Assert.Collection(
            root.Children,
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal("Foo", label.Text);
            },
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal("Bar", label.Text);
            },
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal("Baz", label.Text);
            }
        );
    }

    [Fact]
    public void WhenRepeating_PropagatesResolutionScope()
    {
        ResolutionScope.AddTranslation("RepeatTranslationKey", "Hello");
        string markup = @"<label *repeat={Items} text={#RepeatTranslationKey} />";
        var model = new RepeatingModel()
        {
            Items = [new() { Name = "Foo" }, new() { Name = "Bar" }, new() { Name = "Baz" }],
        };
        var tree = BuildTreeFromMarkup(markup, model);

        Assert.Collection(
            tree.Views,
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal("Hello", label.Text);
            },
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal("Hello", label.Text);
            },
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal("Hello", label.Text);
            }
        );
    }

    [Fact]
    public void WhenRepeating_AndItemChanges_UpdatesView()
    {
        string markup =
            @"<lane>
                <label *repeat={Items} text={Name} />
            </lane>";
        var model = new RepeatingModel()
        {
            Items = [new() { Name = "Foo" }, new() { Name = "Bar" }, new() { Name = "Baz" }],
        };
        var tree = BuildTreeFromMarkup(markup, model);

        model.Items[1].Name = "Quux";
        tree.Update();

        var root = Assert.IsType<Lane>(tree.Views.SingleOrDefault());
        Assert.Collection(
            root.Children,
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal("Foo", label.Text);
            },
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal("Quux", label.Text);
            },
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal("Baz", label.Text);
            }
        );
    }

    [Fact]
    public void WhenRepeating_AndCollectionChanges_RebuildsAllViews()
    {
        string markup =
            @"<lane>
                <label *repeat={Items} text={Name} />
            </lane>";
        var model = new RepeatingModel()
        {
            Items = [new() { Name = "Foo" }, new() { Name = "Bar" }, new() { Name = "Baz" }],
        };
        var tree = BuildTreeFromMarkup(markup, model);

        model.Items = [new() { Name = "abc" }, new() { Name = "def" }, new() { Name = "xyz" }];
        tree.Update();

        var root = Assert.IsType<Lane>(tree.Views.SingleOrDefault());
        Assert.Collection(
            root.Children,
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal("abc", label.Text);
            },
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal("def", label.Text);
            },
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal("xyz", label.Text);
            }
        );
    }

    partial class RepeatingObservableModel : INotifyPropertyChanged
    {
        [Notify]
        private ObservableCollection<RepeatingElement> items = [];
    }

    [Fact]
    public void WhenRepeating_AndNotifyingCollectionChanges_UpdatesAffectedViews()
    {
        string markup =
            @"<lane>
                <label *repeat={Items} text={Name} />
            </lane>";
        var model = new RepeatingObservableModel()
        {
            Items = [new() { Name = "Foo" }, new() { Name = "Bar" }, new() { Name = "Baz" }],
        };
        var tree = BuildTreeFromMarkup(markup, model);

        model.Items.Move(0, 1);
        model.Items.Insert(0, new() { Name = "Quux" });
        model.Items.Insert(2, new() { Name = "Plip" });
        tree.Update();

        var root = Assert.IsType<Lane>(tree.Views.SingleOrDefault());
        Assert.Collection(
            root.Children,
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal("Quux", label.Text);
            },
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal("Bar", label.Text);
            },
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal("Plip", label.Text);
            },
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal("Foo", label.Text);
            },
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal("Baz", label.Text);
            }
        );

        model.Items.RemoveAt(2);
        model.Items.RemoveAt(0);
        model.Items.RemoveAt(2);
        tree.Update();
        Assert.Collection(
            root.Children,
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal("Bar", label.Text);
            },
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal("Foo", label.Text);
            }
        );
    }
}
