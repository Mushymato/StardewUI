namespace StardewUI.Framework.Sources;

/// <summary>
/// Value source that always provides a null/default value, and does not allow writing.
/// </summary>
/// <remarks>
/// Can be used in place of a real <see cref="IValueSource{T}"/> when no data is available, e.g. when a complex binding
/// is attempted when a <c>null</c> value is at the root, and therefore the destination type cannot be determined.
/// </remarks>
/// <typeparam name="T">The return type of the context property.</typeparam>
public class NullValueSource<T> : IValueSource<T>
{
    /// <summary>
    /// Immutable default instance of a <see cref="NullValueSource{T}"/>.
    /// </summary>
    public static readonly NullValueSource<T> Instance = new();

    /// <inheritdoc />
    public bool CanRead => true;

    /// <inheritdoc />
    public bool CanWrite => false;

    /// <inheritdoc />
    public string DisplayName => "(none)";

    /// <inheritdoc />
    public T? Value
    {
        get => default;
        set { }
    }

    /// <inheritdoc />
    public Type ValueType => typeof(T);

    object? IValueSource.Value
    {
        get => null;
        set { }
    }

    private NullValueSource() { }

    /// <inheritdoc />
    public bool Update(bool force = false)
    {
        return false;
    }
}
