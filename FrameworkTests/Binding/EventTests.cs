using System.ComponentModel;
using Microsoft.Xna.Framework;
using PropertyChanged.SourceGenerator;
using StardewModdingAPI;
using StardewUI.Events;
using StardewUI.Widgets;
using Xunit.Abstractions;

namespace StardewUI.Framework.Tests.Binding;

public partial class EventTests(ITestOutputHelper output) : BindingTests(output)
{
    class EventTestModel
    {
        public int Delta { get; } = 5;
        public List<Item> Items { get; set; } = [];

        public void IncrementItem(string id, int delta, SButton button)
        {
            var item = Items.Find(item => item.Id == id);
            Assert.NotNull(item);
            int multiplier = button == SButton.ControllerX ? 5 : 1;
            item.Quantity += delta * multiplier;
        }

        public class Item(string id)
        {
            public string Id { get; } = id;
            public int Quantity { get; set; }
        }
    }

    [Fact]
    public void WhenViewRaisesEvent_InvokesBoundMethod()
    {
        string markup =
            @"<lane>
                <frame *repeat={Items}>
                    <button click=|^IncrementItem(Id, ^Delta, $Button)| />
                </frame>
            </lane>";
        var model = new EventTestModel() { Items = [new("foo"), new("bar"), new("baz")] };
        var tree = BuildTreeFromMarkup(markup, model);

        var rootView = Assert.IsType<Lane>(tree.Views.SingleOrDefault());
        var bazFrame = Assert.IsType<Frame>(rootView.Children[2]);
        var bazButton = Assert.IsType<Button>(bazFrame.Content);

        bazButton.OnClick(new(Vector2.Zero, SButton.ControllerA));

        Assert.Equal(5, model.Items[2].Quantity);

        bazButton.OnClick(new(Vector2.Zero, SButton.ControllerX));

        Assert.Equal(30, model.Items[2].Quantity);
    }

    class ManyToOneEventTestModel
    {
        public Outer? Data { get; set; }
        public List<(string, int)> Results { get; } = [];
        public int Value { get; set; }

        public void AddResult(string s, int i)
        {
            Results.Add((s, i));
        }

        public record Outer(int Value, string Name, Inner Inner1, Inner Inner2);

        public record Inner(int Value);
    }

    // Our goal with this test is simply to be as punishing as possible in terms of caching behavior and ambiguity
    // around scoping and argument types. We use the same event bound to the same handler, but always on different
    // views, with context that might be different or the same, arguments that might be bound or literal, and
    // ambiguous names all over the place (everything is intentionally "Value").
    [Fact]
    public void WhenSameEventBoundForManyViews_InvokesWithSeparateArgs()
    {
        string markup =
            @"<frame *context={Data} click=|AddResult(""frame"", Value)|>
                <lane click=|^AddResult(Name, Value)|>
                    <panel>
                        <frame *context={Inner1}>
                            <image click=|^^AddResult(""image1"", Value)| />
                        </frame>
                        <frame *context={Inner2}>
                            <image click=|~ManyToOneEventTestModel.AddResult(""image2"", Value)| />
                        </frame>
                    </panel>
                    <button click=|^AddResult(""button"", ""999"")| />
                </lane>
            </frame>";
        var model = new ManyToOneEventTestModel { Data = new(10, "nameFromData", new(50), new(51)), Value = 3 };
        var tree = BuildTreeFromMarkup(markup, model);
        var frame = Assert.IsType<Frame>(tree.Views.SingleOrDefault());
        var lane = Assert.IsType<Lane>(frame.Content);
        var panel = Assert.IsType<Panel>(lane.Children[0]);
        var image1 = Assert.IsType<Image>(Assert.IsType<Frame>(panel.Children[0]).Content);
        var image2 = Assert.IsType<Image>(Assert.IsType<Frame>(panel.Children[1]).Content);
        var button = Assert.IsType<Button>(lane.Children[1]);

        var dummyEventArgs = new ClickEventArgs(Vector2.Zero, SButton.ControllerA);
        image1.OnClick(dummyEventArgs);
        button.OnClick(dummyEventArgs);
        panel.OnClick(dummyEventArgs); // Should do nothing, no event bound
        frame.OnClick(dummyEventArgs);
        lane.OnClick(dummyEventArgs);
        image2.OnClick(dummyEventArgs);
        button.OnClick(dummyEventArgs);

        Assert.Equal(
            [("image1", 50), ("button", 999), ("frame", 3), ("nameFromData", 10), ("image2", 51), ("button", 999)],
            model.Results
        );
    }

    class OptionalParamsEventTestModel
    {
        public List<(string, int)> Results { get; } = [];

        public void AddResult(string name, int value, int toAdd = 0, bool alsoAddOne = false)
        {
            Results.Add((name, value + toAdd + (alsoAddOne ? 1 : 0)));
        }
    }

    [Fact]
    public void WhenEventBoundWithOptionalParameters_InvokesWithDefinedArgs()
    {
        string markup =
            @"<panel>
                <image click=|AddResult(""image1"", ""10"", ""5"", ""true"")| />
                <image click=|AddResult(""image2"", ""20"", ""3"")| />
                <button click=|AddResult(""button"", ""30"")| />
            </panel>";
        var model = new OptionalParamsEventTestModel();
        var tree = BuildTreeFromMarkup(markup, model);
        var panel = Assert.IsType<Panel>(tree.Views.SingleOrDefault());
        var image1 = Assert.IsType<Image>(panel.Children[0]);
        var image2 = Assert.IsType<Image>(panel.Children[1]);
        var button = Assert.IsType<Button>(panel.Children[2]);

        var dummyEventArgs = new ClickEventArgs(Vector2.Zero, SButton.ControllerA);
        image1.OnClick(dummyEventArgs);
        image2.OnClick(dummyEventArgs);
        button.OnClick(dummyEventArgs);

        Assert.Equal([("image1", 16), ("image2", 23), ("button", 30)], model.Results);
    }

    partial class NoParamsEventTestModel : INotifyPropertyChanged
    {
        public float PreviousMoney { get; private set; }

        [Notify]
        private float money;

        public void HandleChange()
        {
            PreviousMoney = Money;
        }
    }

    [Fact]
    public void WhenEventBoundWithNoParameters_InvokesWithReceiverOnly()
    {
        string markup = @"<slider min=""50"" max=""200"" value={<>Money} value-change=|HandleChange()| />";
        var model = new NoParamsEventTestModel() { Money = 100 };
        var tree = BuildTreeFromMarkup(markup, model);
        var slider = Assert.IsType<Slider>(tree.Views.SingleOrDefault());

        slider.Value = 150;

        Assert.Equal(100, model.PreviousMoney);
    }

    partial class TabsTestModel : INotifyPropertyChanged
    {
        public enum Tab
        {
            One,
            Two,
            Three,
        };

        public string HeaderText { get; set; } = "";
        public string Page1Text { get; set; } = "";
        public string Page2Text { get; set; } = "";
        public string Page3Text { get; set; } = "";

        [Notify]
        private Tab selectedTab;

        public void ChangeTab(Tab tab)
        {
            SelectedTab = tab;
        }
    }

    [Fact]
    public void WhenEventHandlerChangesModel_UpdatesViewContent()
    {
        string markup =
            @"<lane orientation=""vertical"" horizontal-content-alignment=""middle"">
                <banner background-border-thickness=""48,0"" padding=""12"" text={HeaderText} />
                <lane orientation=""horizontal"" horizontal-content-alignment=""middle"">
                    <button text=""Tab 1"" click=|ChangeTab(""One"")|/>
                    <button text=""Tab 2"" click=|ChangeTab(""Two"")|/>
                    <button text=""Tab 3"" click=|ChangeTab(""Three"")|/>
                </lane>
                <frame *switch={SelectedTab} layout=""200px 200px"" margin=""0,16,0,0"" padding=""32,24"">
                    <label *case=""One"" text={Page1Text} />
                    <label *case=""Two"" text={Page2Text} />
                    <label *case=""Three"" text={Page3Text} />
                </frame>
            </lane>";
        var model = new TabsTestModel()
        {
            HeaderText = "Tabbed Menu",
            Page1Text = "This is the first page.",
            Page2Text = "This is the second page.",
            Page3Text = "This is the third page.",
        };
        var tree = BuildTreeFromMarkup(markup, model);

        var root = Assert.IsType<Lane>(tree.Views.SingleOrDefault());
        var banner = Assert.IsType<Banner>(root.Children[0]);
        Assert.Equal("Tabbed Menu", banner.Text);
        var tabsLane = Assert.IsType<Lane>(root.Children[1]);
        var tab2Button = Assert.IsType<Button>(tabsLane.Children[1]);
        var tab3Button = Assert.IsType<Button>(tabsLane.Children[2]);
        var contentFrame = Assert.IsType<Frame>(root.Children[2]);

        var label = Assert.IsType<Label>(contentFrame.Content);
        Assert.Equal("This is the first page.", label.Text);

        var dummyEventArgs = new ClickEventArgs(Vector2.Zero, SButton.ControllerA);
        tab2Button.OnClick(dummyEventArgs);
        tree.Update();

        label = Assert.IsType<Label>(contentFrame.Content);
        Assert.Equal("This is the second page.", label.Text);

        tab3Button.OnClick(dummyEventArgs);
        tree.Update();

        label = Assert.IsType<Label>(contentFrame.Content);
        Assert.Equal("This is the third page.", label.Text);
    }

    class BubbleTestModel
    {
        public int Counter { get; private set; }

        public void HandleVoid()
        {
            Counter++;
        }

        public bool HandleWithResult(bool result)
        {
            Counter++;
            return result;
        }
    }

    [Fact]
    public void WhenEventHandlerReturnsVoid_AllowsBubbling()
    {
        string markup =
            @"<frame click=|HandleVoid()|>
                <button layout=""10px 10px"" click=|HandleVoid()| />
            </frame>";
        var model = new BubbleTestModel();
        var tree = BuildTreeFromMarkup(markup, model);

        var root = Assert.IsType<Frame>(tree.Views.SingleOrDefault());
        root.Measure(new(100, 100)); // Otherwise event won't dispatch to children.
        root.OnClick(new(new(5, 5), SButton.ControllerA));

        Assert.Equal(2, model.Counter);
    }

    [Fact]
    public void WhenEventHandlerReturnsFalse_AllowsBubbling()
    {
        string markup =
            @"<frame click=|HandleVoid()|>
                <button layout=""10px 10px"" click=|HandleWithResult(""false"")| />
            </frame>";
        var model = new BubbleTestModel();
        var tree = BuildTreeFromMarkup(markup, model);

        var root = Assert.IsType<Frame>(tree.Views.SingleOrDefault());
        root.Measure(new(100, 100));
        root.OnClick(new(new(5, 5), SButton.ControllerA));

        Assert.Equal(2, model.Counter);
    }

    [Fact]
    public void WhenEventHandlerReturnsTrue_PreventsBubbling()
    {
        string markup =
            @"<frame click=|HandleVoid()|>
                <button layout=""10px 10px"" click=|HandleWithResult(""true"")| />
            </frame>";
        var model = new BubbleTestModel();
        var tree = BuildTreeFromMarkup(markup, model);

        var root = Assert.IsType<Frame>(tree.Views.SingleOrDefault());
        root.Measure(new(100, 100));
        root.OnClick(new(new(5, 5), SButton.ControllerA));

        Assert.Equal(1, model.Counter);
    }
}
