using System.ComponentModel;
using StardewUI.Framework.Binding;
using StardewUI.Framework.Descriptors;

namespace StardewUI.Framework.Sources;

/// <summary>
/// Value source that obtains its value from a property path, starting from some root data and traversing the list of
/// properties to reach the final value.
/// </summary>
/// <typeparam name="T">The return type of the context property, i.e. at the end of the path.</typeparam>
public class ContextPathValueSource<T> : IValueSource<T>, IDisposable
{
    /// <inheritdoc />
    public bool CanRead => properties[^1].CanRead;

    /// <inheritdoc />
    public bool CanWrite => properties[^1].CanWrite;

    /// <inheritdoc />
    public string DisplayName =>
        path[0]?.Data is { } rootData
            ? rootData.GetType().Name + "." + string.Join('.', properties.Select(p => p.Name))
            : "(none)";

    /// <inheritdoc />
    public T? Value
    {
        get => actualSource is not null ? actualSource.Value : default;
        set
        {
            if (actualSource is null && dirtyIndex >= 0)
            {
                // If the binding is configured for output-only, or if it is in/out and some elements along the path
                // have changed, then the `actualSource` may not be valid anymore and we need to make it point to the
                // correct context again.
                Update();
            }
            if (actualSource is not null)
            {
                actualSource.Value = value;
            }
        }
    }

    /// <inheritdoc />
    public Type ValueType => typeof(T);

    object? IValueSource.Value
    {
        get => Value;
        set => Value = value is not null ? (T)value : default;
    }

    private readonly bool allowUpdates;
    private int dirtyIndex = 0;
    private readonly Node?[] path;
    private readonly IPropertyDescriptor[] properties;
    private readonly BindingContext? rootContext;

    private IValueSource<T>? actualSource;

    /// <summary>
    /// Initializes a new <see cref="ContextPathValueSource{T}"/> using the specified context and property sequence.
    /// </summary>
    /// <param name="context">Context used for the data binding.</param>
    /// <param name="properties">Array of properties to traverse, in order. Equivalent to a nested C# accessor
    /// expression such as "Foo.Bar.Baz".</param>
    /// <param name="allowUpdates">Whether or not to allow <see cref="Update"/> to read a new value. <c>false</c>
    /// prevents all updates and makes the source read only one time.</param>
    /// <exception cref="ArgumentException">Thrown when the <paramref name="properties"/> array is empty.</exception>
    public ContextPathValueSource(BindingContext? context, IPropertyDescriptor[] properties, bool allowUpdates = true)
    {
        if (properties.Length == 0)
        {
            throw new ArgumentException("Context path has no elements.", nameof(properties));
        }
        this.properties = properties;
        this.allowUpdates = allowUpdates;
        rootContext = context;
        path = new Node[properties.Length - 1];
        if (context?.Data is { } rootData)
        {
            var rootNode = new Node(rootData, properties[0], 0);
            if (allowUpdates)
            {
                if (
                    rootData is not INotifyPropertyChanged
                    && properties.All(p =>
                        p.IsField
                        || p.IsAutoProperty
                        || DescriptorFactory
                            .GetObjectDescriptor(p.DeclaringType, lazy: true)
                            .SupportsChangeNotifications
                    )
                )
                {
                    Logger.LogOnce(
                        $"Binding to path '{DisplayName}' may receive no updates or inconsistent updates because all "
                            + "properties along the path are either fields or auto-properties, and no types along the "
                            + "path implement INotifyPropertyChanged. "
                            + ContextPropertyValueSource<T>.OneTimeBindingTip,
                        LogLevel.Warn
                    );
                }
                if (rootNode.Listen())
                {
                    rootNode.Change += Node_Change;
                }
            }
            path[0] = rootNode;
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (actualSource is IDisposable disposable)
        {
            disposable.Dispose();
        }
        actualSource = null;
        foreach (var node in path)
        {
            node?.Dispose();
        }
        Array.Clear(path);
        GC.SuppressFinalize(this);
    }

    /// <inheritdoc />
    public bool Update(bool force = false)
    {
        if (force)
        {
            dirtyIndex = 0;
        }
        if (dirtyIndex < 0)
        {
            return actualSource?.Update() ?? false;
        }
        if (actualSource is IDisposable disposable)
        {
            disposable.Dispose();
        }
        // Actual rebuild starts at the index following the one that was changed, since a change means that the node's
        // target property changed, not the node's own context.
        dirtyIndex++;
        // Path omits the last property, and the dirty index after incrementing here may be past the end of the list.
        // This is expected, and when it occurs, we still want to execute the rest of the logic, i.e. taking the value
        // at the end of the path (which excludes the final segment) and recreate the `actualSource`.
        for (int i = dirtyIndex; i < path.Length; i++)
        {
            if (path[i] is { } node)
            {
                node.Dispose();
            }
            path[i] = null;
        }
        Array.Clear(path, dirtyIndex, path.Length - dirtyIndex);
        var currentNode = path[dirtyIndex - 1];
        while (dirtyIndex < path.Length)
        {
            var currentValue = currentNode?.GetValue();
            var nextNode = currentValue is not null ? new Node(currentValue, properties[dirtyIndex], dirtyIndex) : null;
            if (nextNode is null)
            {
                break;
            }
            if (allowUpdates && nextNode.Listen())
            {
                nextNode.Change += Node_Change;
            }
            currentNode = path[dirtyIndex] = nextNode;
            dirtyIndex++;
        }
        if (path[^1] is { } lastNode)
        {
            if (lastNode.GetValue() is { } sourceData)
            {
                var sourceContext = BindingContext.Create(sourceData, rootContext);
                actualSource = new ContextPropertyValueSource<T>(sourceContext, properties[^1].Name, allowUpdates);
                actualSource.Update(force: true);
            }
            else
            {
                actualSource = null;
            }
        }
        dirtyIndex = -1;
        return true;
    }

    private void Node_Change(object? sender, EventArgs e)
    {
        if (sender is not Node node)
        {
            return;
        }
        dirtyIndex = Math.Max(dirtyIndex, node.Index);
    }

    private class Node(object data, IPropertyDescriptor property, int index) : IDisposable
    {
        public event EventHandler<EventArgs>? Change;

        public object Data => data;
        public int Index => index;

        public void Dispose()
        {
            if (data is INotifyPropertyChanged npc)
            {
                npc.PropertyChanged -= Data_PropertyChanged;
            }
        }

        public object? GetValue()
        {
            return data is not null ? property.GetUntypedValue(data) : null;
        }

        public bool Listen()
        {
            if (data is INotifyPropertyChanged npc)
            {
                npc.PropertyChanged += Data_PropertyChanged;
                return true;
            }
            return false;
        }

        private void Data_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == property.Name)
            {
                Change?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
