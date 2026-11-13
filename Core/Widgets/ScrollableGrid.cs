using StardewUI.Data;
using StardewUI.Input;
using StardewUI.Layout;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace StardewUI.Widgets;

/// <summary>
/// A subset of <see cref="Grid"/> which provides a virtual 'slice' of a backing collection, and a modifiable offset.
/// </summary>
/// <remarks>
/// <para>
/// Does not provide its own scroll bar; scrolling UI and behavior can be controlled via adding a
/// <see cref="Scrollbar"/> to any other part of the UI.
/// </para>
/// <para>
/// Unlike <see cref="ScrollContainer"/>, scrolling is virtualized, making it suitable for large collections.
/// </para>
/// <para>
/// It does not create or change its own children, so it is the user's responsibility to bind the visible slice
/// to actual views. This can be achieved in markup using a repeat directive.
/// </para>
/// </remarks>
public partial class ScrollableGrid : Grid, IScrollable
{
    /// <summary>
    /// Default constructor
    /// </summary>
    public ScrollableGrid()
    {
        PropertyChanged += OwnPropertyChanged;
    }

    private void OwnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(PrimaryItemCount))
        {
            scrollStep = scrollRowCount * PrimaryItemCount;
            OnPropertyChanged(nameof(ScrollStep));
            OnPropertyChanged(nameof(ScrollSize));
            OnScrollChanged();
        }
    }

    private readonly ListSlice itemSlice = new()
    {
        Count = 1
    };

    /// <summary>
    /// The source collection, to be virtualized, if any.
    /// </summary>
    public IList<object>? Items
    {
        get => itemSlice.Source;
        set
        {
            if (value != itemSlice.Source)
            {
                itemSlice.Source = value;
                OnSourceChanged();
            }
        }
    }

    /// <summary>
    /// The items from the source collection that are currently visible
    /// </summary>
    public ObservableCollection<object> VisibleItems => itemSlice.Slice;

    /// <summary>
    /// The number of items from the collection that should be visible at a time
    /// </summary>
    public int VisibleCount
    {
        get => itemSlice.Count;
        set
        {
            value = Math.Max(0, value);
            if (itemSlice.Count != value)
            {
                itemSlice.Count = value;
                RecalculateScrollProgress();
                OnPropertyChanged(nameof(VisibleCount));
                OnPropertyChanged(nameof(ScrollSize));
                OnScrollChanged();
            }
        }
    }

    /// <summary>
    /// Whether out-of-bounds directional inputs should scroll the grid if possible
    /// </summary>
    public bool Wrap
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                OnPropertyChanged(nameof(Wrap));
            }
        }
    }

    /// <inheritdoc/>
    public Orientation Orientation => PrimaryOrientation.Swap();

    /// <inheritdoc/>
    public float Progress { get; set; }

    /// <inheritdoc/>
    public float ScrollSize
    {
        get
        {
            if (itemSlice.Source != null)
            {
                float whole = Math.DivRem(
                    Math.Max(0, itemSlice.Source.Count - itemSlice.Count),
                    Math.Max(1, PrimaryItemCount),
                    out int rem
                );

                if (rem != 0)
                    whole++;

                return whole * PrimaryItemCount;
            }

            return 0;
        }
    }

    /// <inheritdoc/>
    public float ScrollOffset
    {
        get => itemSlice.Offset;
        set => TrySetOffset(value);
    }

    /// <inheritdoc/>
    public float ScrollStep
    {
        get => scrollStep;
        set
        {
            int row = (int)(value + .5f);
            value = row * PrimaryItemCount;
            if (scrollRowCount != row || value != scrollStep)
            {
                scrollStep = value;
                scrollRowCount = row;
                OnPropertyChanged(nameof(ScrollStep));
            }
        }
    }

    /// <inheritdoc/>
    public float DragStep => 1f / Math.Max(1f, secondaryStartPositions.Count is 0 ? ContentBounds.Size.Y : secondaryStartPositions[0]);

    /// <inheritdoc/>
    public event EventHandler? ScrollChanged;

    private bool suppressOffsetChange = false;
    private int scrollRowCount = 1;
    private float scrollStep = 1f;

    /// <inheritdoc/>
    public bool ScrollBackward()
        => TrySetOffset(ScrollOffset - ScrollStep);

    /// <inheritdoc/>
    public bool ScrollForward()
        => TrySetOffset(ScrollOffset + ScrollStep);

    /// <inheritdoc/>
    public void SetScrollOffsetNoDirty(float scrollOffsetValue)
    {
        suppressOffsetChange = true;
        itemSlice.Offset = (int)scrollOffsetValue;
        suppressOffsetChange = false;
    }

    private bool TrySetOffset(float value)
    {
        var val = Math.Clamp((int)value, 0, (int)ScrollSize) / PrimaryItemCount * PrimaryItemCount;
        var old = itemSlice.Offset;

        if (val == old)
            return false;

        itemSlice.Offset = val;
        if (!suppressOffsetChange)
        {
            RecalculateScrollProgress();
            OnPropertyChanged(nameof(ScrollOffset));
            OnScrollChanged();
        }
        return old != itemSlice.Offset;
    }

    private void RecalculateScrollProgress()
    {
        var size = ScrollSize;
        Progress = size is 0 ? 0 : Math.Clamp(ScrollOffset / size, 0, 1f);
    }

    private void OnScrollChanged()
    {
        ScrollChanged?.Invoke(this, EventArgs.Empty);
    }

    private void OnSourceChanged()
    {
        OnPropertyChanged(nameof(Items));
        OnPropertyChanged(nameof(ScrollSize));
        OnPropertyChanged(nameof(ScrollOffset));
        OnScrollChanged();
    }

    private void DisposeSlice()
    {
        itemSlice.Source = null;
    }

    /// <inheritdoc/>
    protected override FocusSearchResult? FindFocusableDescendant(Vector2 contentPosition, Direction direction)
    {
        return base.FindFocusableDescendant(contentPosition, direction) ?? TryWrapEdge(contentPosition, direction);
    }

    /// <inheritdoc/>
    protected override void OnDispose()
    {
        DisposeSlice();
        base.OnDispose();
    }

    private FocusSearchResult? TryWrapEdge(Vector2 contentPosition, Direction direction)
    {
        if (!Wrap || direction.GetOrientation() == PrimaryOrientation)
            return null;

        int ord = direction switch
        {
            Direction.North => -1,
            Direction.South => 1,
            Direction.East => 1,
            Direction.West => -1,
            _ => 0
        };

        if (ord is 0)
            return null;

        var contentChildIndex = GetChildIndexAt(GetCellAt(contentPosition));
        if (contentChildIndex < 0 || contentChildIndex >= childPositions.Count)
            return null;

        if (!TrySetOffset(ScrollOffset + ord * PrimaryItemCount))
            return null;

        Vector2 fakePosition = direction switch
        {
            Direction.North => new(contentPosition.X, ActualBounds.Top - 1),
            Direction.South => new(contentPosition.X, ActualBounds.Bottom + 1),
            Direction.East => new(ActualBounds.Right + 1, contentPosition.Y),
            Direction.West => new(ActualBounds.Left - 1, contentPosition.Y),
            _ => Vector2.Zero
        };
        return childPositions[contentChildIndex].FocusSearch(fakePosition, direction.Reverse());
    }

    /// <inheritdoc/>
    protected override void OnMeasure(Vector2 availableSize)
    {
        base.OnMeasure(availableSize);
        Vector2 limits = Layout.GetLimits(availableSize);
        int visCount = (int)(MathF.Floor(limits.Y / MaxSecondaryHeight) * PrimaryItemCount);
        visCount = Math.Max(visCount, 1);
        VisibleCount = visCount;
    }
}
