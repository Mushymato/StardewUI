using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using static StardewValley.Menus.CharacterCustomization;

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
        get;
        set
        {
            if (field != value)
            {
                field = value;
                OnPropertyChanged();
            }
        }
    }

    /// <inheritdoc/>
    public int Offset
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

    /// <inheritdoc/>
    public bool IsSynchronized => Source.IsSynchronized;

    /// <inheritdoc/>
    public object SyncRoot => Source.SyncRoot;

    /// <inheritdoc/>
    public event NotifyCollectionChangedEventHandler? CollectionChanged;

    /// <inheritdoc/>
    public event PropertyChangedEventHandler? PropertyChanged;

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
        return new SliceEnumerator(Source, Offset, Count);
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

    private void SourcePropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IList.Count))
        {
            if (Count >= Source.Count)
                Offset = 0;
            else
                Offset = Math.Clamp(Offset, 0, Source.Count - Count);
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

    private class SliceEnumerator(IList source, int offset, int count) : IEnumerator
    {
        public object? Current { get; private set; }
        private int index = offset - 1;

        public bool MoveNext()
        {
            index++;
            if (index >= offset + count)
                return false;

            Current = index > source.Count ? null : source[index];
            return true;
        }

        public void Reset()
        {
            index = offset - 1;
        }
    }
}
