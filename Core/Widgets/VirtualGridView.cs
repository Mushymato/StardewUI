using StardewUI.Data;
using StardewUI.Events;
using StardewUI.Graphics;
using StardewUI.Layout;
using System.Collections;
using System.ComponentModel;

namespace StardewUI.Widgets;

/// <summary>
/// Provides a virtually scrollable grid with a scrollbar.
/// </summary>
public partial class VirtualGridView : ComponentView<ScrollableGrid>, IFloatContainer
{
    /// <summary>
    /// The source collection, to be virtualized, if any.
    /// </summary>
    public IList? Items
    {
        get => View.Items;
        set => View.Items = value;
    }

    /// <summary>
    /// The items from the source collection that are currently visible
    /// </summary>
    public ListSlice? VisibleItems => View.VisibleItems;

    /// <summary>
    /// The number of items from the collection that should be visible at a time
    /// </summary>
    public int VisibleCount
    {
        get => View.VisibleCount;
        set => View.VisibleCount = value;
    }

    /// <summary>
    /// Whether out-of-bounds directional inputs should scroll the grid if possible
    /// </summary>
    public bool Wrap
    {
        get => View.Wrap;
        set => View.Wrap = value;
    }

    /// <summary>
    /// Child views to display in this layout, arranged according to the <see cref="ItemLayout"/>.
    /// </summary>
    public IList<IView> Children
    {
        get => View.Children;
        set => View.Children = value;
    }

    /// <summary>
    /// Specifies how to align the entire grid when the combined length of all columns is not exactly equal to the
    /// grid's layout length.
    /// </summary>
    /// <remarks>
    /// Applies only to the grid's <see cref="PrimaryOrientation"/> axis, which is the axis affected by
    /// <see cref="ItemLayout"/>; the secondary axis does not require or support grid-level alignment because it can
    /// already be made <see cref="Length.Content"/>-sized.
    /// </remarks>
    public Alignment GridAlignment
    {
        get => View.GridAlignment;
        set => View.GridAlignment = value;
    }

    /// <summary>
    /// Specifies how to align each child <see cref="IView"/> horizontally within its respective cell, i.e. if the view
    /// is narrower than the cell's width.
    /// </summary>
    public Alignment HorizontalItemAlignment
    {
        get => View.HorizontalItemAlignment;
        set => View.HorizontalItemAlignment = value;
    }

    /// <summary>
    /// The layout for items (cells) in this grid.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Layouts are relative to the <see cref="PrimaryOrientation"/>. <see cref="GridItemLayout.Count"/> specifies the
    /// number of columns when <see cref="Orientation.Horizontal"/>, and number of rows when
    /// <see cref="Orientation.Vertical"/>; similarly, <see cref="GridItemLayout.Length"/> specifies the column width
    /// when horizontal and row height when vertical. The other dimension is determined by the individual item's own
    /// <see cref="LayoutParameters"/>.
    /// </para>
    /// <para>
    /// Note that this affects the <i>limits</i> for individual items, not necessarily their exact size. Children may be
    /// smaller than the cells that contain them, and if so are positioned according to the
    /// <see cref="HorizontalItemAlignment"/> and <see cref="VerticalItemAlignment"/>.
    /// </para>
    /// </remarks>
    public GridItemLayout ItemLayout
    {
        get => View.ItemLayout;
        set => View.ItemLayout = value;
    }

    /// <summary>
    /// Spacing between the edges of adjacent columns (<see cref="Vector2.X"/>) and rows (<see cref="Vector2.Y"/>).
    /// </summary>
    /// <remarks>
    /// Setting this is roughly equivalent to specifying the same <see cref="View.Margin"/> on each child, except that
    /// it will not add extra space before the first item or after the last item.
    /// </remarks>
    public Vector2 ItemSpacing
    {
        get => View.ItemSpacing;
        set => View.ItemSpacing = value;
    }

    /// <summary>
    /// Specifies how to align each child <see cref="IView"/> vertically within its respective cell, i.e. if the view
    /// is shorter than the cell's height.
    /// </summary>
    public Alignment VerticalItemAlignment
    {
        get => View.VerticalItemAlignment;
        set => View.VerticalItemAlignment = value;
    }

    /// <summary>
    /// The number of items in the primary orientation (i.e. in the row or colume), updated after measure.
    /// This value is updated on measure and read-only, it is utilized in focus search.
    /// </summary>
    public int PrimaryItemCount => View.PrimaryItemCount;

    /// <inheritdoc />
    public IList<FloatingElement> FloatingElements
    {
        get => View.FloatingElements;
        set => View.FloatingElements = value;
    }

    /// <summary>
    /// The <see cref="Scrollbar.DownSprite" /> used for the scrollbar.
    /// </summary>
    public Sprite? ScrollbarDownSprite
    {
        get => scrollbar.DownSprite;
        set => scrollbar.DownSprite = value;
    }

    /// <summary>
    /// The <see cref="Scrollbar.Margin" /> of the scrollbar.
    /// </summary>
    public Edges ScrollbarMargin
    {
        get => scrollbar.Margin;
        set => scrollbar.Margin = value;
    }

    /// <summary>
    /// The <see cref="Scrollbar.ForcedVisibility" /> of the scrollbar.
    /// </summary>
    public Visibility? ScrollbarVisibility
    {
        get => scrollbar.ForcedVisibility;
        set => scrollbar.ForcedVisibility = value;
    }

    /// <summary>
    /// The <see cref="Scrollbar.ThumbSprite" /> used for the scrollbar.
    /// </summary>
    public Sprite? ScrollbarThumbSprite
    {
        get => scrollbar.ThumbSprite;
        set => scrollbar.ThumbSprite = value;
    }

    /// <summary>
    /// The <see cref="Scrollbar.TrackSprite" /> used for the scrollbar.
    /// </summary>
    public Sprite? ScrollbarTrackSprite
    {
        get => scrollbar.TrackSprite;
        set => scrollbar.TrackSprite = value;
    }

    /// <summary>
    /// The <see cref="Scrollbar.UpSprite" /> used for the scrollbar.
    /// </summary>
    public Sprite? ScrollbarUpSprite
    {
        get => scrollbar.UpSprite;
        set => scrollbar.UpSprite = value;
    }

    /// <summary>
    /// The <see cref="ScrollContainer.ScrollStep" /> of the scrollable view.
    /// <c>(unofficial-mushymato)</c>
    /// </summary>
    public float ScrollStep
    {
        get => scrollbar.Container?.ScrollStep ?? 32f;
        set => scrollbar.Container?.ScrollStep = value;
    }

    /// <summary>
    /// The <see cref="Scrollbar.Progress" /> of the scrollable view.
    /// <c>(unofficial-mushymato)</c>
    /// </summary>
    public float Progress
    {
        get => scrollbar.Progress;
        set => scrollbar.Progress = value;
    }

    /// <summary>
    /// Specifies the axis that items are added to before wrapping.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <see cref="Orientation.Horizontal"/> means children are added from left to right, and when reaching the edge or
    /// max column count, start over at the beginning of the next row. <see cref="Orientation.Vertical"/> means children
    /// flow from top to bottom, and when reaching the bottom, wrap to the top of the next column.
    /// </para>
    /// <para>
    /// Also affects which dimension is fixed and which is potentially unbounded. Horizontally-oriented grids have a
    /// fixed width and can grow to any height (if <see cref="LayoutParameters.Height"/> is set to
    /// <see cref="Length.Content"/>). Vertically-oriented grids are the opposite, having a fixed height and growing to
    /// an arbitrary width.
    /// </para>
    /// </remarks>
    public Orientation PrimaryOrientation
    {
        get => View.PrimaryOrientation;
        set => View.PrimaryOrientation = value;
    }

    /// <summary>
    /// The <see cref="View.Draggable" /> of the scrollable view.
    /// If enabled, this view can be dragged (e.g. touch screen controls)
    /// <c>(unofficial-mushymato)</c>
    /// </summary>
    public bool DragToScroll
    {
        get => (scrollbar.Container as View)?.Draggable ?? false;
        set => (scrollbar.Container as View)?.Draggable = value;
    }

    // Initialized in CreateView
    private Scrollbar scrollbar = null!;
    private static readonly HashSet<string> forwardProps = [
        nameof(PrimaryItemCount), nameof(ScrollStep), nameof(Progress), nameof(Children),
        nameof(GridAlignment), nameof(HorizontalItemAlignment), nameof(FloatingElements),
        nameof(Items), nameof(VisibleItems), nameof(VisibleCount), nameof(Wrap)
    ];

    /// <inheritdoc />
    public override void OnWheel(WheelEventArgs e)
    {
        base.OnWheel(e);
        if (e.Handled || scrollbar.Container is not { } container)
        {
            return;
        }
        switch (e.Direction)
        {
            case Direction.North when container.Orientation == Orientation.Vertical:
            case Direction.West when container.Orientation == Orientation.Horizontal:
                e.Handled = container.ScrollBackward();
                break;
            case Direction.South when container.Orientation == Orientation.Vertical:
            case Direction.East when container.Orientation == Orientation.Horizontal:
                e.Handled = container.ScrollForward();
                break;
        }
        if (e.Handled)
        {
            Game1.playSound("shiny4");
        }
    }

    /// <inheritdoc />
    protected override ScrollableGrid CreateView()
    {
        var grid = new ScrollableGrid();
        scrollbar = new Scrollbar()
        {
            Layout = new() { Height = Length.Stretch() },
            Margin = new(Left: 32, Bottom: -8),
            Container = grid,
        };

        FloatingPosition scrollFloat =
            grid.PrimaryOrientation is Orientation.Vertical ?
            FloatingPosition.BelowParent : 
            FloatingPosition.AfterParent;

        grid.FloatingElements.Add(new(scrollbar, scrollFloat));
        grid.PropertyChanged += OnPropertyChanged;
        scrollbar.PropertyChanged += OnPropertyChanged;
        return grid;
    }

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(PrimaryOrientation))
        {
            if (View.FloatingElements.FirstOrDefault(e => e.View == scrollbar) is FloatingElement scrollFloat)
                View.FloatingElements.Remove(scrollFloat);
            
            View.FloatingElements.Add(new(scrollbar,
                PrimaryOrientation is Orientation.Vertical ?
                FloatingPosition.BelowParent :
                FloatingPosition.AfterParent
            ));

            OnPropertyChanged(nameof(PrimaryOrientation));
        }
        else if (e.PropertyName is string name && forwardProps.Contains(name))
        {
            OnPropertyChanged(name);
        }
    }
}
