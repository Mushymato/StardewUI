using System.ComponentModel;
using Microsoft.Xna.Framework;
using PropertyChanged.SourceGenerator;
using StardewModdingAPI;
using StardewUI.Layout;
using StardewUI.Widgets;
using Xunit.Abstractions;

namespace StardewUI.Framework.Tests.Binding;

public partial class TemplateTests(ITestOutputHelper output) : BindingTests(output)
{
    partial class TemplateBindingModel : INotifyPropertyChanged
    {
        [Notify]
        private bool option1;

        [Notify]
        private bool option2;
    }

    [Fact]
    public void WhenBoundWithTemplateNodes_ExpandsTemplates()
    {
        string markup =
            @"<template name=""heading"">
                <label color=""red"" text={&text} />
            </template>

            <lane orientation=""vertical"">
                <heading text=""Heading 1"" />
                <row title=""Option 1""><checkbox is-checked={<>Option1}/></row>
                <heading text=""Heading 2"" />
                <row title=""Option 2""><checkbox is-checked={<>Option2}/></row>
            </lane>

            <template name=""row"">
                <lane vertical-content-alignment=""middle"">
                    <label layout=""400px content"" text={&title} />
                    <outlet />
                </lane>
            </template>";
        var model = new TemplateBindingModel() { Option1 = true };
        var tree = BuildTreeFromMarkup(markup, model);

        var root = Assert.IsType<Lane>(tree.Views.SingleOrDefault());
        Assert.Equal(Orientation.Vertical, root.Orientation);
        CheckBox option1Checkbox = null!;
        CheckBox option2Checkbox = null!;
        Assert.Collection(
            root.Children,
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal(Color.Red, label.Color);
                Assert.Equal("Heading 1", label.Text);
            },
            child =>
            {
                var row = Assert.IsType<Lane>(child);
                Assert.Equal(Alignment.Middle, row.VerticalContentAlignment);
                Assert.Collection(
                    row.Children,
                    child =>
                    {
                        var label = Assert.IsType<Label>(child);
                        Assert.Equal(Length.Px(400), label.Layout.Width);
                        Assert.Equal(Length.Content(), label.Layout.Height);
                        Assert.Equal("Option 1", label.Text);
                    },
                    child =>
                    {
                        option1Checkbox = Assert.IsType<CheckBox>(child);
                        Assert.True(option1Checkbox.IsChecked);
                    }
                );
            },
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal(Color.Red, label.Color);
                Assert.Equal("Heading 2", label.Text);
            },
            child =>
            {
                var row = Assert.IsType<Lane>(child);
                Assert.Equal(Alignment.Middle, row.VerticalContentAlignment);
                Assert.Collection(
                    row.Children,
                    child =>
                    {
                        var label = Assert.IsType<Label>(child);
                        Assert.Equal(Length.Px(400), label.Layout.Width);
                        Assert.Equal(Length.Content(), label.Layout.Height);
                        Assert.Equal("Option 2", label.Text);
                    },
                    child =>
                    {
                        option2Checkbox = Assert.IsType<CheckBox>(child);
                        Assert.False(option2Checkbox.IsChecked);
                    }
                );
            }
        );

        option1Checkbox.IsChecked = false;
        tree.Update();
        Assert.False(model.Option1);

        model.Option2 = true;
        tree.Update();
        Assert.True(option2Checkbox.IsChecked);
    }

    partial class TemplateBindingEventOuterModel
    {
        public string Arg1 { get; private set; } = "";
        public int Arg2 { get; private set; }
        public string Arg3 { get; private set; } = "";
        public TemplateBindingEventInnerModel? Inner { get; set; }

        public void Handle(string arg1, int arg2, string arg3)
        {
            Arg1 = arg1;
            Arg2 = arg2;
            Arg3 = arg3;
        }
    }

    partial class TemplateBindingEventInnerModel
    {
        public int Id { get; set; }
    }

    [Fact]
    public void WhenBoundWithTemplateNodes_ExpandsEventHandlerArguments()
    {
        // It might seem silly that we're passing in the same "Count" property that's already in the model as an event
        // argument, but it's just for testing the template binding.
        string markup =
            @"<panel *context={Inner}>
                <foo bar=""abc"" id={Id} />
            </panel>

            <template name=""foo"">
                <button click=|^Handle(""dummy"", &id, &bar)| />
            </template>";
        var model = new TemplateBindingEventOuterModel() { Inner = new() { Id = 38 } };
        var tree = BuildTreeFromMarkup(markup, model);

        var panel = Assert.IsType<Panel>(tree.Views.SingleOrDefault());
        var button = Assert.IsType<Button>(panel.Children.SingleOrDefault());
        button.OnClick(new(Vector2.Zero, SButton.ControllerA));

        Assert.Equal("dummy", model.Arg1);
        Assert.Equal(38, model.Arg2);
        Assert.Equal("abc", model.Arg3);
    }

    [Fact]
    public void WhenTemplateInvocationHasStructuralAttributes_AppliesToExpandedNodes()
    {
        string markup =
            @"<lane>
                <foo *repeat={this} color=""blue"" />
            </lane>

            <template name=""foo"">
                <label text={this} color={&color} />
            </template>";
        var model = new List<string> { "aaa", "bbb", "ccc" };
        var tree = BuildTreeFromMarkup(markup, model);

        var lane = Assert.IsType<Lane>(tree.Views.SingleOrDefault());
        Assert.Collection(
            lane.Children,
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal("aaa", label.Text);
                Assert.Equal(Color.Blue, label.Color);
            },
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal("bbb", label.Text);
                Assert.Equal(Color.Blue, label.Color);
            },
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal("ccc", label.Text);
                Assert.Equal(Color.Blue, label.Color);
            }
        );
    }

    [Fact]
    public void WhenTemplateHasTemplateBoundStructuralAttribute_ExpandsValue()
    {
        string markup =
            @"<lane>
                <foo show-baz={ShowBaz} />
            </lane>

            <template name=""foo"">
                <label text=""bar"" />
                <label *if={&show-baz} text=""baz"" />
            </template>";
        var model = new { ShowBaz = true };
        var tree = BuildTreeFromMarkup(markup, model);

        var lane = Assert.IsType<Lane>(tree.Views.SingleOrDefault());
        Assert.Collection(
            lane.Children,
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal("bar", label.Text);
            },
            child =>
            {
                var label = Assert.IsType<Label>(child);
                Assert.Equal("baz", label.Text);
            }
        );
    }

    [Fact]
    public void WhenTemplateReferencesOtherTemplate_ExpandsInOrder()
    {
        string markup =
            @"<lane orientation=""vertical"">
                <foo color=""green"" text1=""A"" text2=""B"" text3=""C"" />
                <foo color=""yellow"" text1=""X"" text2=""Y"" text3=""Z"" />
            </lane>

            <template name=""foo"">
                <bar>
                    <baz color={&color} text={&text1} />
                    <baz color=""Black"" text={&text2} />
                    <baz *outlet=""quux"" color={&color} text={&text3} />
                </bar>
            </template>

            <template name=""bar"">
                <outlet />
                <outlet name=""quux"" />
                <spacer />
            </template>

            <template name=""baz"">
                <label color={&color} text={&text} />
            </template>";
        var tree = BuildTreeFromMarkup(markup, new());

        var root = Assert.IsType<Lane>(tree.Views.SingleOrDefault());
        Assert.Equal(Orientation.Vertical, root.Orientation);
        Assert.Collection(
            root.Children,
            child => AssertLabel(Color.Green, "A", child),
            child => AssertLabel(Color.Black, "B", child),
            child => AssertLabel(Color.Green, "C", child),
            child => Assert.IsType<Spacer>(child),
            child => AssertLabel(Color.Yellow, "X", child),
            child => AssertLabel(Color.Black, "Y", child),
            child => AssertLabel(Color.Yellow, "Z", child),
            child => Assert.IsType<Spacer>(child)
        );

        static void AssertLabel(Color color, string text, IView view)
        {
            var label = Assert.IsType<Label>(view);
            Assert.Equal(color, label.Color);
            Assert.Equal(text, label.Text);
        }
    }
}
