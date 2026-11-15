using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StardewUI.Data;

/// <summary>
/// 
/// </summary>
public class ListSlice : IList, INotifyCollectionChanged, INotifyPropertyChanged, IDisposable
{
    /// <summary>
    /// Creates a new slice of an existing list.
    /// </summary>
    public ListSlice(IList source)
    {
        Source = source;
    }

    /// <summary>
    /// The source collection that this is a slice of.
    /// </summary>
    public IList Source
    {
        get;
        set
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            if (field != value)
            {
                DetachHandlers(field);
                field = value;
                AttachHandlers(field);

                if (TryChangeCount(count, out var offsetChanged))
                {
                    OnPropertyChanged(nameof(Count));

                    if (offsetChanged)
                        OnPropertyChanged(nameof(Offset));
                }

                OnPropertyChanged();
                OnCollectionChanged();
            }
        }
    }

    /// <inheritdoc/>
    /// <remarks>
    /// Trying to set values outside the range of the source collection will silently fail.
    /// For setting values, it is better to use <see cref="TrySet"/> instead.
    /// </remarks>
    public object? this[int index]
    {
        get =>
            index >= Count ? throw new IndexOutOfRangeException(nameof(index)) :
            index + Offset >= Source.Count ? null : Source[index + Offset];

        set
        {
            if (index >= Count)
                throw new IndexOutOfRangeException(nameof(index));

            var ind = index + Offset;
            if (ind < Source.Count)
                Source[ind] = value;
        }
    }

    /// <summary>
    /// Determines how many elements the offset slice can extend past the end of the source collection.
    /// </summary>
    public int Margin
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                OnPropertyChanged();

                if (TryChangeCount(count, out var offsetChanged))
                {
                    OnPropertyChanged(nameof(Count));

                    if (offsetChanged)
                        OnPropertyChanged(nameof(Offset));

                    OnCollectionChanged();
                }
            }
        }
    }

    /// <summary>
    /// Whether or not items should be iterated past the end of the source collection.
    /// </summary>
    public bool ShowOverflow
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                OnPropertyChanged();
                OnCollectionChanged();
            }
        }
    }

    /// <summary>
    /// Attempts to set a value in the underlying collection.
    /// </summary>
    /// <remarks>
    /// Will not throw exceptions, instead returning false if the index is out of range or could not be added.
    /// </remarks>
    public bool TrySet(int index, object? value)
    {
        if (index >= Count || index + Offset > Source.Count || index < 0)
            return false;

        if (index + Offset == Source.Count)
            return Source.Add(value) >= 0;

        Source[index + Offset] = value;
        return true;
    }

    /// <inheritdoc/>
    public bool IsFixedSize => false;

    /// <inheritdoc/>
    public bool IsReadOnly => Source.IsReadOnly;

    /// <inheritdoc/>
    public int Count
    {
        get => count;
        set
        {
            if (TryChangeCount(value, out var offsetChanged))
            {
                OnPropertyChanged();

                if (offsetChanged)
                    OnPropertyChanged(nameof(Offset));

                OnCollectionChanged();
            }
        }
    }

    /// <inheritdoc/>
    public int Offset
    {
        get => offset;
        set
        {
            if (TryChangeOffset(value))
            {
                OnPropertyChanged();
                OnCollectionChanged();
            }
        }
    }

    /// <inheritdoc/>
    public bool IsSynchronized => Source.IsSynchronized;

    /// <inheritdoc/>
    public object SyncRoot => Source.SyncRoot;

    /// <inheritdoc/>
    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    /// <inheritdoc/>
    public event PropertyChangedEventHandler? PropertyChanged;

    private int offset;
    private int count;

    /// <inheritdoc/>
    public int Add(object? value)
    {
        var ind = Source.Add(value);
        return ind < 0 ? ind : ind - Offset;
    }

    /// <inheritdoc/>
    public void Clear()
    {
        Source.Clear();
    }

    /// <inheritdoc/>
    public bool Contains(object? value)
    {
        return IndexOf(value) >= 0;
    }

    /// <summary>Do not use, not implemented.</summary>
    /// <exception cref="NotImplementedException"></exception>
    public void CopyTo(Array array, int index)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public IEnumerator GetEnumerator()
    {
        return new SliceEnumerator(Source, Offset, Count, ShowOverflow ? Margin : 0);
    }

    /// <inheritdoc/>
    public int IndexOf(object? value)
    {
        for (int i = 0; i < Count; i++)
        {
            if (i + Offset >= Source.Count)
                return -1;

            if ((Source[i + Offset] is object o && o.Equals(value)) || value is null)
                return i;
        }
        return -1;
    }

    /// <inheritdoc/>
    public void Insert(int index, object? value)
    {
        if (index + Offset < Source.Count)
            Source.Insert(index + Offset, value);
        else
            Source.Add(value);
    }

    /// <inheritdoc/>
    public void Remove(object? value)
    {
        Source.Remove(value);
    }

    /// <inheritdoc/>
    public void RemoveAt(int index)
    {
        Source.RemoveAt(index + Offset);
    }

    private bool TryChangeCount(int value, out bool offsetChanged)
    {
        offsetChanged = false;
        value = Math.Clamp(value, 0, Source.Count + Margin);
        if (count != value)
        {
            count = value;
            offsetChanged = TryChangeOffset(offset);
            return true;
        }

        return false;
    }

    private bool TryChangeOffset(int value)
    {
        value = Math.Clamp(value, 0, Source.Count + Margin - Count);
        if (offset != value)
        {
            offset = value;
            return true;
        }
        return false;
    }

    private void SourcePropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IList.Count))
        {
            if (TryChangeCount(count, out bool offsetChanged))
            {
                OnPropertyChanged(nameof(Count));

                if (offsetChanged)
                    OnPropertyChanged(nameof(Offset));

                OnCollectionChanged();
            }
        }
    }

    private void SourceItemsChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        OnCollectionChanged(e);
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        if (propertyName != null)
            PropertyChanged?.Invoke(this, new(propertyName));
    }

    private void OnCollectionChanged(NotifyCollectionChangedEventArgs? ev = null)
    {
        CollectionChanged?.Invoke(this, ev ?? new(NotifyCollectionChangedAction.Reset));
    }

    private void AttachHandlers(IList l)
    {
        if (l is INotifyCollectionChanged c)
            c.CollectionChanged += SourceItemsChanged;

        if (l is INotifyPropertyChanged p)
            p.PropertyChanged += SourcePropertyChanged;
    }

    private void DetachHandlers(IList l)
    {
        if (l is INotifyCollectionChanged c)
            c.CollectionChanged -= SourceItemsChanged;

        if (l is INotifyPropertyChanged p)
            p.PropertyChanged -= SourcePropertyChanged;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        DetachHandlers(Source);
        GC.SuppressFinalize(this);
    }

    private class SliceEnumerator(IList source, int offset, int count, int limit) : IEnumerator
    {
        public object? Current { get; private set; }
        private int index = offset - 1;

        public bool MoveNext()
        {
            index++;
            if (index >= offset + count)
                return false;

            if (index >= source.Count)
                if (index - source.Count < limit)
                    Current = null;
                else
                    return false;
            else
                Current = source[index];

            return true;
        }

        public void Reset()
        {
            index = offset - 1;
        }
    }
}
