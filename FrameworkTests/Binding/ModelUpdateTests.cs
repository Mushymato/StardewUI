using StardewUI.Framework.Binding;
using Xunit.Abstractions;

namespace StardewUI.Framework.Tests.Binding;

public partial class ModelUpdateTests(ITestOutputHelper output) : BindingTests(output)
{
    class UpdateOuterModel
    {
        public UpdateInnerModel Inner { get; set; } = new();
        public int UpdateCount { get; set; }

        public void Update()
        {
            UpdateCount++;
        }
    }

    class UpdateInnerModel
    {
        public TimeSpan ElapsedTotal { get; set; }

        public void Update(TimeSpan elapsed)
        {
            ElapsedTotal += elapsed;
        }
    }

    [Fact]
    public void WhenContextHasUpdateMethod_RunsEachTick()
    {
        // The markup deliberately attaches to the `Inner` context twice in order to verify that there isn't a duplicate
        // update tick; it should be one per context, not one per context *binding*.
        string markup =
            @"<panel>
                <lane>
                    <frame *if=""true"" *context={Inner}>
                        <button text=""Cancel"" />
                    </frame>
                    <frame *if=""true"" *context={Inner}>
                        <button text=""OK"" />
                    </frame>
                </lane>
            </panel>";
        var model = new UpdateOuterModel();
        var tree = BuildTreeFromMarkup(markup, model);

        // Initial update, no time elapsed (see below).
        Assert.Equal(1, model.UpdateCount);
        Assert.Equal(TimeSpan.Zero, model.Inner.ElapsedTotal);

        // Since there is no actual game loop running in these tests, we have to reset the tracker manually.
        ContextUpdateTracker.Instance.Reset();
        tree.Update(TimeSpan.FromMilliseconds(50));
        // This should be ignored because the tracker hasn't reset.
        tree.Update(TimeSpan.FromMilliseconds(60));

        Assert.Equal(2, model.UpdateCount);
        Assert.Equal(TimeSpan.FromMilliseconds(50), model.Inner.ElapsedTotal);

        // Make double-plus sure that reset actually resets
        ContextUpdateTracker.Instance.Reset();
        tree.Update(TimeSpan.FromMilliseconds(40));

        Assert.Equal(3, model.UpdateCount);
        Assert.Equal(TimeSpan.FromMilliseconds(90), model.Inner.ElapsedTotal);
    }

    class UpdateInvalidReturnModel
    {
        public int UpdateCount { get; set; }

        public bool Update()
        {
            UpdateCount++;
            return true;
        }
    }

    [Fact]
    public void WhenUpdateMethodHasInvalidReturnType_IgnoresForTick()
    {
        string markup = @"<label text=""Hello"" />";
        var model = new UpdateInvalidReturnModel();
        var tree = BuildTreeFromMarkup(markup, model);

        // BuildTreeFromMarkup would fire the first update anyway, but this adds a little more certainty.
        tree.Update();

        Assert.Equal(0, model.UpdateCount);
    }

    class UpdateInvalidArgsModel
    {
        public int UpdateCount { get; set; }

        public void Update(TimeSpan elapsed, int arg)
        {
            UpdateCount++;
        }
    }

    [Fact]
    public void WhenUpdateMethodHasInvalidArgumentTypes_IgnoresForTick()
    {
        string markup = @"<label text=""Hello"" />";
        var model = new UpdateInvalidArgsModel();
        var tree = BuildTreeFromMarkup(markup, model);

        // BuildTreeFromMarkup would fire the first update anyway, but this adds a little more certainty.
        tree.Update();

        Assert.Equal(0, model.UpdateCount);
    }
}
