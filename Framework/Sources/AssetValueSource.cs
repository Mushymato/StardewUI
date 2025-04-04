using StardewUI.Framework.Content;

namespace StardewUI.Framework.Sources;

/// <summary>
/// Value source that looks up an asset registered with SMAPI's content manager.
/// </summary>
/// <typeparam name="T">The type of asset to retrieve.</typeparam>
/// <param name="cache">Asset cache used to obtain current value/status.</param>
/// <param name="nameSource">Secondary source that provides the name of the asset as it would be supplied to SMAPI in
/// <see cref="StardewModdingAPI.IGameContentHelper.Load{T}(string)"/>.</param>
public class AssetValueSource<T>(IAssetCache cache, IValueSource<string> nameSource) : IValueSource<T>, IDisposable
    where T : notnull
{
    /// <inheritdoc />
    public bool CanRead => true;

    /// <inheritdoc />
    public bool CanWrite => false;

    /// <inheritdoc />
    public string DisplayName => $"Asset@{nameSource.Value}";

    /// <inheritdoc />
    public T? Value
    {
        get => cacheEntry is not null ? cacheEntry.Asset : default;
        set => throw new NotSupportedException($"Writing to an {typeof(AssetValueSource<>).Name} is not supported.");
    }

    object? IValueSource.Value
    {
        get => Value;
        set => Value = value is not null ? (T)value : default;
    }

    /// <inheritdoc />
    public Type ValueType => typeof(T);

    private IAssetCacheEntry<T>? cacheEntry;

    /// <summary>
    /// Initializes an <see cref="AssetValueSource{T}"/> instance using a constant asset name.
    /// </summary>
    /// <param name="cache">Asset cache used to obtain current value/status.</param>
    /// <param name="name">The asset name/path as it would be supplied to SMAPI in
    /// <see cref="StardewModdingAPI.IGameContentHelper.Load{T}(string)"/>.</param>
    public AssetValueSource(IAssetCache cache, string name)
        : this(cache, new ConstantValueSource<string>(name)) { }

    /// <inheritdoc />
    public void Dispose()
    {
        // It's unnecessary to null this for resource/memory-management purposes, but we want to prevent any subsequent
        // bindings from potentially seeing (incorrectly) that the entry is still valid.
        cacheEntry = null;
        GC.SuppressFinalize(this);
    }

    /// <inheritdoc />
    /// <returns><c>true</c> if the underlying asset expired since the last update; <c>false</c> if the
    /// <see cref="Value"/> was still current.</returns>
    public bool Update(bool force = false)
    {
        bool nameChanged = nameSource.Update(force);
        if (force || nameChanged || cacheEntry is null || !cacheEntry.IsValid)
        {
            cacheEntry = !string.IsNullOrEmpty(nameSource.Value) ? cache.Get<T>(nameSource.Value) : null;
            return true;
        }
        return false;
    }
}
