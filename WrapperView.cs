﻿using Microsoft.Xna.Framework;

namespace StardewUI;

/// <inheritdoc cref="WrapperView{T}"/>
public abstract class WrapperView : WrapperView<IView> { }

/// <summary>
/// Base class for "app views" with potentially complex hierarchy using a single root view.
/// </summary>
/// <remarks>
/// <para>
/// This implements all the boilerplate of an <see cref="IView"/> without having to actually implement a totally custom
/// <see cref="View"/>; instead, it delegates all functionality to the inner (root) <see cref="IView"/>.
/// </para>
/// <para>
/// The typical use case is for what is often called "Components", "Layouts", "User Controls", etc., in which a class
/// defines both the view hierarchy and an API for interacting with the view and underlying data at the same time. The
/// top-level layout is created in <see cref="CreateView"/>, and child views can be added on creation or at any later
/// time. More importantly, since the subclass decides what children to create, it can also store references to those
/// children for the purposes of updating the UI, responding to events, etc.
/// </para>
/// <para>
/// Wrapper views can be composed like any other views, or used in a <see cref="ViewMenu"/>.
/// </para>
/// </remarks>
/// <typeparam name="T">Type of view being wrapped.</typeparam>
public abstract class WrapperView<T> : IView where T : IView
{
    public event EventHandler<ClickEventArgs>? Click;
    public event EventHandler<PointerEventArgs>? Drag;
    public event EventHandler<PointerEventArgs>? DragStart;
    public event EventHandler<PointerEventArgs>? DragEnd;
    public event EventHandler<PointerEventArgs>? PointerEnter;
    public event EventHandler<PointerEventArgs>? PointerLeave;
    public event EventHandler<WheelEventArgs>? Wheel;

    public Bounds ActualBounds => Root.ActualBounds;
    public bool IsFocusable => Root.IsFocusable;
    public LayoutParameters Layout { get => Root.Layout; set => Root.Layout = value; }
    public string Name { get => Root.Name; set => Root.Name = value; }
    public Vector2 OuterSize => Root.OuterSize;
    public Tags Tags { get; set; } = new();
    public string Tooltip { get => Root.Tooltip; set => Root.Tooltip = value; }
    public Visibility Visibility { get => Root.Visibility; set => Root.Visibility = value; }
    public int ZIndex { get => Root.ZIndex; set => Root.ZIndex = value; }

    protected bool IsViewCreated => root.IsValueCreated;
    protected T Root => root.Value;

    private readonly Lazy<T> root;

    public WrapperView()
    {
        root = new(() =>
        {
            var view = CreateView();
            view.Click += View_Click;
            view.Drag += View_Drag;
            view.DragEnd += View_DragEnd;
            view.DragStart += View_DragStart;
            view.PointerEnter += View_PointerEnter;
            view.PointerLeave += View_PointerLeave;
            view.Wheel += View_Wheel;
            return view;
        });
    }

    // View methods

    public virtual void Draw(ISpriteBatch b)
    {
        Root.Draw(b);
    }

    public virtual FocusSearchResult? FocusSearch(Vector2 position, Direction direction)
    {
        return Root.FocusSearch(position, direction);
    }

    public virtual ViewChild? GetChildAt(Vector2 position)
    {
        return Root.GetChildAt(position);
    }

    public virtual Vector2? GetChildPosition(IView childView)
    {
        return Root.GetChildPosition(childView);
    }

    public virtual IEnumerable<ViewChild> GetChildren()
    {
        return Root.GetChildren();
    }

    public virtual bool IsDirty()
    {
        return Root.IsDirty();
    }

    public virtual bool Measure(Vector2 availableSize)
    {
        return Root.Measure(availableSize);
    }

    public virtual void OnClick(ClickEventArgs e)
    {
        Root.OnClick(e);
    }

    public virtual void OnDrag(PointerEventArgs e)
    {
        Root.OnDrag(e);
    }

    public virtual void OnDrop(PointerEventArgs e)
    {
        Root.OnDrop(e);
    }

    public virtual void OnPointerMove(PointerMoveEventArgs e)
    {
        Root.OnPointerMove(e);
    }

    public virtual void OnWheel(WheelEventArgs e)
    {
        Root.OnWheel(e);
    }

    public virtual bool ScrollIntoView(IEnumerable<ViewChild> path, out Vector2 distance)
    {
        return Root.ScrollIntoView(path, out distance);
    }

    /// <summary>
    /// Creates and returns the root view.
    /// </summary>
    protected abstract T CreateView();

    private void View_Click(object? sender, ClickEventArgs e)
    {
        Click?.Invoke(this, e);
    }

    private void View_Drag(object? sender, PointerEventArgs e)
    {
        Drag?.Invoke(this, e);
    }

    private void View_DragEnd(object? sender, PointerEventArgs e)
    {
        DragEnd?.Invoke(this, e);
    }

    private void View_DragStart(object? sender, PointerEventArgs e)
    {
        DragStart?.Invoke(this, e);
    }

    private void View_PointerEnter(object? sender, PointerEventArgs e)
    {
        PointerEnter?.Invoke(this, e);
    }

    private void View_PointerLeave(object? sender, PointerEventArgs e)
    {
        PointerLeave?.Invoke(this, e);
    }

    private void View_Wheel(object? sender, WheelEventArgs e)
    {
        Wheel?.Invoke(this, e);
    }
}