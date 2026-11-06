using StardewUI.Data;
using StardewUI.Input;
using StardewUI.Layout;
using System.Collections;
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
    /// The source collection, to be virtualized, if any.
    /// </summary>
    public IList? Items
    {
        get => VisibleItems?.Source;
        set
        {
            if (value is null)
            {
                if (VisibleItems == null)
                    return;

                DisposeSlice();
            }
            else
            {
                if (VisibleItems is ListSlice l)
                {
                    l.Source = value;
                    return;
                }

                ListSlice items = new(value);
                items.PropertyChanged += ItemsPropertyChanged;
                VisibleItems = items;
            }

            OnSourceChanged();
        }
    }

    /// <summary>
    /// The items from the source collection that are currently visible
    /// </summary>
    public ListSlice? VisibleItems { get; private set; }

    /// <summary>
    /// The number of items from the collection that should be visible at a time
    /// </summary>
    public int VisibleCount
    {
        get => VisibleItems is ListSlice l ? l.Count : 0;
        set
        {
            if (VisibleItems is ListSlice l)
                l.Count = value;
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
    public float ScrollSize => VisibleItems is ListSlice l ? Math.Max(0, l.Source.Count - l.Count) : 0;

    /// <inheritdoc/>
    public float ScrollOffset
    {
        get => VisibleItems is ListSlice l ? l.Offset : 0;
        set
        {
            if (VisibleItems is ListSlice l)
                l.Offset = (int)value;
        }
    }

    /// <inheritdoc/>
    public float ScrollStep
    {
        get;
        set
        {
            value = MathF.Max(0f, MathF.Round(value));
            if (field != value)
            {
                field = value;
                OnPropertyChanged(nameof(ScrollStep));
            }
        }
    } = 1f;

    /// <inheritdoc/>
    public float DragStep => 1f / Math.Max(1f, secondaryStartPositions.Count is 0 ? ContentBounds.Size.Y : secondaryStartPositions[0]);

    /// <inheritdoc/>
    public event EventHandler? ScrollChanged;

    private bool suppressOffsetChange = false;

    /// <inheritdoc/>
    public bool ScrollBackward()
        => TrySetOffset(ScrollOffset - ScrollStep);

    /// <inheritdoc/>
    public bool ScrollForward()
        => TrySetOffset(ScrollOffset + ScrollStep);

    /// <inheritdoc/>
    public void SetScrollOffsetNoDirty(float scrollOffsetValue)
    {
        if (VisibleItems is ListSlice l)
        {
            suppressOffsetChange = true;
            l.Offset = (int)scrollOffsetValue;
            suppressOffsetChange = false;
        }
    }

    private bool TrySetOffset(float value)
    {
        var old = ScrollOffset;
        ScrollOffset = value;
        return old != ScrollOffset;
    }

    private void OnScrollChanged()
    {
        ScrollChanged?.Invoke(this, EventArgs.Empty);
    }

    private void OnSourceChanged()
    {
        OnPropertyChanged(nameof(Items));
        OnPropertyChanged(nameof(VisibleCount));
        OnPropertyChanged(nameof(ScrollSize));
        OnPropertyChanged(nameof(ScrollOffset));
        OnScrollChanged();
    }

    private void DisposeSlice()
    {
        if (VisibleItems is ListSlice slice)
        {
            slice.PropertyChanged -= ItemsPropertyChanged;
            slice.Dispose();
            VisibleItems = null;
        }
    }

    private void ItemsPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(ListSlice.Count):
                OnPropertyChanged(nameof(VisibleCount));
                OnPropertyChanged(nameof(ScrollSize));
                OnScrollChanged();
                break;

            case nameof(ListSlice.Offset):
                if (!suppressOffsetChange)
                {
                    OnPropertyChanged(nameof(ScrollOffset));
                    OnScrollChanged();
                }
                break;

            case nameof(ListSlice.Source):
                OnSourceChanged();
                break;
        }
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

        if (!TrySetOffset(ScrollOffset + ord))
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
}
