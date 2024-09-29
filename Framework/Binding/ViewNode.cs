﻿using System.Reflection;
using StardewUI.Framework.Descriptors;
using StardewUI.Framework.Dom;

namespace StardewUI.Framework.Binding;

/// <summary>
/// Internal structure of a view node, encapsulating dependencies required for data binding and lazy creation/updates.
/// </summary>
/// <param name="viewFactory">Factory for creating views, based on their tag names.</param>
/// <param name="viewBinder">Binding service used to create <see cref="IViewBinding"/> instances that detect changes to
/// data or assets and propagate them to the bound <see cref="IView"/>.</param>
/// <param name="element">Element data for this node.</param>
/// <param name="childNodes">Children of this node.</param>
public class ViewNode(
    IViewFactory viewFactory,
    IViewBinder viewBinder,
    SElement element,
    IEnumerable<IViewNode> childNodes
) : IViewNode
{
    public IReadOnlyList<IViewNode> ChildNodes { get; } = childNodes.ToList();

    public object? Context
    {
        get => context;
        set
        {
            if (value == context)
            {
                return;
            }
            context = value;
            wasContextChanged = true;
        }
    }

    public IReadOnlyList<IView> Views => view is not null ? [view] : [];

    private IViewBinding? binding;
    private IChildrenBinder? childrenBinder;
    private object? context;
    private IView? view;
    private bool wasContextChanged;

    public bool Update()
    {
        bool wasChanged = false;
        if (view is null)
        {
            view ??= viewFactory.CreateView(element.Tag);
            var viewDescriptor = viewBinder.GetDescriptor(view);
            childrenBinder = ReflectionChildrenBinder.FromViewDescriptor(viewDescriptor);
        }
        if (wasContextChanged)
        {
            wasChanged = true;
            binding?.Dispose();
            binding = null;
        }
        if (binding is not null)
        {
            wasChanged |= binding.Update();
        }
        else
        {
            // Don't require explicit update because IViewBinder.Bind always does an initial forced update.
            binding = viewBinder.Bind(view, element, context);
            wasChanged = true;
        }
        bool wasChildViewChanged = false;
        foreach (var childNode in ChildNodes)
        {
            if (wasContextChanged)
            {
                childNode.Context = context;
            }
            // Even though Views is an IReadOnlyList<IView>, that does not make it an immutable list. If we want to
            // reliably detect changes, we have to account for the possibility of the list being modified in situ.
            var previousViews = new List<IView>(childNode.Views);
            childNode.Update();
            wasChildViewChanged |= !childNode.Views.SequenceEqual(previousViews);
        }
        if (wasChildViewChanged)
        {
            UpdateViewChildren();
        }
        wasContextChanged = false;
        return wasChanged;
    }

    private void UpdateViewChildren()
    {
        if (view is null)
        {
            return;
        }
        var children = ChildNodes.SelectMany(node => node.Views).Where(view => view is not null).Cast<IView>().ToList();
        if (childrenBinder is not null)
        {
            childrenBinder.SetChildren(view, children);
        }
        else if (children.Count > 0)
        {
            throw new BindingException(
                $"Cannot bind {children.Count} children to view type {view.GetType().Name} because it does not "
                    + "define any publicly writable child/children property."
            );
        }
    }

    interface IChildrenBinder
    {
        void SetChildren(IView view, List<IView> children);
    }

    static class ReflectionChildrenBinder
    {
        private static readonly Dictionary<Type, IChildrenBinder?> cache = [];
        private static readonly MethodInfo multipleMethod = typeof(ReflectionChildrenBinder).GetMethod(
            nameof(Multiple),
            BindingFlags.Static | BindingFlags.NonPublic
        )!;
        private static readonly MethodInfo singleMethod = typeof(ReflectionChildrenBinder).GetMethod(
            nameof(Single),
            BindingFlags.Static | BindingFlags.NonPublic
        )!;

        public static IChildrenBinder? FromViewDescriptor(IViewDescriptor viewDescriptor)
        {
            if (!cache.TryGetValue(viewDescriptor.TargetType, out var childrenDescriptor))
            {
                if (viewDescriptor.TryGetChildrenProperty(out var childrenProperty))
                {
                    var binder = CreateBinder(viewDescriptor.TargetType, childrenProperty);
                    cache[viewDescriptor.TargetType] = childrenDescriptor = binder;
                }
                else
                {
                    cache[viewDescriptor.TargetType] = childrenDescriptor = null;
                }
            }
            return childrenDescriptor;
        }

        private static IChildrenBinder CreateBinder(Type viewType, IPropertyDescriptor childrenProperty)
        {
            var enumerableChildType = childrenProperty
                .ValueType.GetInterfaces()
                .Where(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                .Select(t => t.GetGenericArguments()[0])
                .Where(t => typeof(IView).IsAssignableFrom(t))
                .FirstOrDefault();
            var factoryMethod = enumerableChildType is not null
                ? multipleMethod.MakeGenericMethod([viewType, enumerableChildType, childrenProperty.ValueType])
                : singleMethod.MakeGenericMethod([viewType, childrenProperty.ValueType]);
            return (IChildrenBinder)factoryMethod.Invoke(null, [childrenProperty])!;
        }

        private static ReflectionChildrenBinder<TView, TChildren> Multiple<TView, TChild, TChildren>(
            IPropertyDescriptor<TChildren> property
        )
            where TChildren : IEnumerable<TChild>
            where TView : IView
        {
            return new((view, children) => property.SetValue(view, children!), true);
        }

        private static ReflectionChildrenBinder<TView, TChild> Single<TView, TChild>(
            IPropertyDescriptor<TChild?> property
        )
            where TView : IView
            where TChild : notnull
        {
            return new((view, child) => property.SetValue(view, child), false);
        }
    }

    class ReflectionChildrenBinder<TView, TChildren>(Action<TView, TChildren?> setChildren, bool allowsMultiple)
        : IChildrenBinder
        where TView : IView
    {
        public void SetChildren(IView view, List<IView> children)
        {
            if (allowsMultiple)
            {
                setChildren((TView)view, (TChildren)(object)children);
            }
            else
            {
                if (children.Count > 1)
                {
                    throw new BindingException(
                        $"Cannot bind {children.Count} children to view type {typeof(TView).Name} because it only "
                            + "supports a single child/content view."
                    );
                }
                var child = children.Count > 0 ? children[0] : null;
                setChildren((TView)view, (TChildren?)(object?)child);
            }
        }
    }
}
