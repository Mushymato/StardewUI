using StardewUI.Data;
using StardewUI.Layout;
using System.Collections;
using System.ComponentModel;

namespace StardewUI.Widgets;

/// <summary>
/// A subset of <see cref="Grid"/> which supports virtualized scrolling.
/// </summary>
public partial class ScrollGrid : ComponentView<Grid>, IScrollable
{
    /// <summary>
    /// The source collection, if any.
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

    /// <inheritdoc/>
    public Orientation Orientation => View.PrimaryOrientation.Swap();

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
            if (field != value)
            {
                field = value;
                OnPropertyChanged(nameof(ScrollStep));
            }
        }
    }

    /// <inheritdoc/>
    public float DragStep
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                OnPropertyChanged(nameof(DragStep));
            }
        }
    }

    /// <inheritdoc/>
    public event EventHandler? ScrollChanged;

    private bool suppressOffsetChange = false;
    private Scrollbar scrollbar = null!;

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
    protected override Grid CreateView()
    {
        var grid = new Grid();
        // TODO
        scrollbar = new Scrollbar()
        {
            Container = this
        };
        return grid;
    }

    /// <inheritdoc/>
    public override void Dispose()
    {
        DisposeSlice();
        base.Dispose();
        GC.SuppressFinalize(this);
    }
}
