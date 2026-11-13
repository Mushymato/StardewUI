using System.Collections.ObjectModel;
using System.ComponentModel;

namespace StardewUI.Data;

/// <summary>
/// A helper class that manages a ObservableCollection that represents a slice of a list.
/// </summary>
public sealed class ListSlice()
{
    /// <summary>
    /// List slice, updated depending on <see cref="Source"/>, <see cref="Offset"/>, and <see cref="Count"/>.
    /// This Slice may contain null only if the Slice would exceed the bounds of the underlying <see cref="Source"/> list.
    /// </summary>
    public ObservableCollection<object> Slice { get; private set; } = [];

    /// <summary>
    /// Source list to populate <see cref="Slice"/> with.
    /// </summary>
    public IList<object>? Source
    {
        get => field;
        set
        {
            if (field != value)
            {
                field = value;
                Slice.Clear();
                if (field == null)
                {
                    Offset = 0;
                    Count = 0;
                    return;
                }
                int sourceLen = field.Count;
                for (int i = 0; i < Count; ++i)
                {
                    int ind = Offset + i;
                    Slice.Add(ind < sourceLen ? field[ind] : null!);
                }
            }
        }
    } = null;

    /// <summary>
    /// Start index of slice
    /// </summary>
    public int Offset
    {
        get => field;
        set
        {
            value = Math.Max(0, value);
            if (field != value)
            {
                if (Source != null)
                {
                    int delta = value - field;
                    int sourceLen = Source.Count;
                    if (delta < 0)
                    {
                        for (int i = 0; i < -delta; ++i)
                        {
                            int ind = field - i - 1;
                            Slice.Insert(0, ind < sourceLen ? Source[ind] : null!);
                            Slice.RemoveAt(Slice.Count - 1);
                        }
                    }
                    else if (delta > 0)
                    {
                        for (int i = 0; i < delta; ++i)
                        {
                            int ind = field + Count + i;
                            Slice.Add(ind < sourceLen ? Source[ind] : null!);
                            Slice.RemoveAt(0);
                        }
                    }
                }
                field = value;
            }
        }
    } = 0;

    /// <summary>
    /// Length of slice
    /// </summary>
    public int Count
    {
        get => field;
        set
        {
            value = Math.Max(0, value);
            if (field != value)
            {
                if (Source != null)
                {
                    int delta = value - field;
                    int sourceLen = Source.Count;
                    if (delta < 0)
                    {
                        for (int i = 0; i < -delta; ++i)
                        {
                            Slice.RemoveAt(Slice.Count - 1);
                        }
                    }
                    else if (delta > 0)
                    {
                        for (int i = 0; i < delta; ++i)
                        {
                            int ind = Offset + field + i;
                            Slice.Add(ind < sourceLen ? Source[ind] : null!);
                        }
                    }
                }
                field = value;
            }
        }
    } = 0;
}
