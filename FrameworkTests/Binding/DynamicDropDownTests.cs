using System.ComponentModel;
using PropertyChanged.SourceGenerator;
using StardewUI.Framework.Views;
using Xunit.Abstractions;

namespace StardewUI.Framework.Tests.Binding;

public partial class DynamicDropDownTests(ITestOutputHelper output) : BindingTests(output)
{
    partial class DropDownTestModel : INotifyPropertyChanged
    {
        public Func<object, string> FormatItem { get; } = item => $"Item {item}";

        [Notify]
        private List<object> items = [];

        [Notify]
        private object selectedItem = 3;
    }

    [Fact]
    public void WhenModelIsConvertible_BindsWithConversion()
    {
        string markup = @"<dropdown options={Items} selected-option={<>SelectedItem} option-format={FormatItem} />";
        var model = new DropDownTestModel { Items = [3, 7, 15] };
        var tree = BuildTreeFromMarkup(markup, model);

        var dropdown = Assert.IsType<DynamicDropDownList>(tree.Views.SingleOrDefault());
        dropdown.SelectedIndex = 1;
        tree.Update();

        Assert.Equal(7, model.SelectedItem);
    }

    enum DropdownTestEnum
    {
        Foo,
        Bar,
        Baz,
        Qux,
    }

    partial class TypedDropdownTestModel : INotifyPropertyChanged
    {
        [Notify]
        private List<DropdownTestEnum> items = [];

        [Notify]
        private Func<DropdownTestEnum, string>? itemFormat;

        [Notify]
        private int selectedIndex;

        [Notify]
        private DropdownTestEnum selectedItem;
    }

    [Fact]
    public void WhenDynamicDropdownTypesAreConsistent_BindsAndUpdates()
    {
        string markup =
            @"<dropdown options={Items}
                        selected-index={<>SelectedIndex}
                        selected-option={<>SelectedItem}
                        option-format={ItemFormat}
            />";
        var model = new TypedDropdownTestModel()
        {
            Items = [DropdownTestEnum.Foo, DropdownTestEnum.Bar, DropdownTestEnum.Baz],
        };
        var tree = BuildTreeFromMarkup(markup, model);

        var dropdown = Assert.IsType<DynamicDropDownList>(tree.Views.SingleOrDefault());
        Assert.Equal(0, dropdown.SelectedIndex);
        Assert.Equal(0, model.SelectedIndex);
        Assert.Equal(DropdownTestEnum.Foo, dropdown.SelectedOption?.Value);
        Assert.Equal(DropdownTestEnum.Foo, model.SelectedItem);
        Assert.Equal("Foo", dropdown.SelectedOptionText);

        model.SelectedIndex = 1;
        tree.Update();

        Assert.Equal(1, dropdown.SelectedIndex);
        Assert.Equal(1, model.SelectedIndex);
        Assert.Equal(DropdownTestEnum.Bar, dropdown.SelectedOption?.Value);
        Assert.Equal(DropdownTestEnum.Bar, model.SelectedItem);
        Assert.Equal("Bar", dropdown.SelectedOptionText);

        model.SelectedItem = DropdownTestEnum.Baz;
        tree.Update();

        Assert.Equal(2, dropdown.SelectedIndex);
        Assert.Equal(2, model.SelectedIndex);
        Assert.Equal(DropdownTestEnum.Baz, dropdown.SelectedOption?.Value);
        Assert.Equal(DropdownTestEnum.Baz, model.SelectedItem);
        Assert.Equal("Baz", dropdown.SelectedOptionText);

        model.ItemFormat = v => new string(v.ToString().ToLower().Reverse().ToArray());
        model.SelectedItem = DropdownTestEnum.Qux;
        model.Items = [DropdownTestEnum.Baz, DropdownTestEnum.Qux, DropdownTestEnum.Foo];
        tree.Update();

        Assert.Equal(1, dropdown.SelectedIndex);
        Assert.Equal(1, model.SelectedIndex);
        Assert.Equal(DropdownTestEnum.Qux, dropdown.SelectedOption?.Value);
        Assert.Equal(DropdownTestEnum.Qux, model.SelectedItem);
        Assert.Equal("xuq", dropdown.SelectedOptionText);
    }
}
