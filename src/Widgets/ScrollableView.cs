﻿using StardewValley;

namespace StardewUI;

/// <summary>
/// Provides a content container and accompanying scrollbar.
/// </summary>
/// <remarks>
/// <para>
/// This does not add any extra UI elements aside from the scrollbar, like <see cref="ScrollableFrameView"/> does, and
/// is more suitable for highly customized menus.
/// </para>
/// <para>
/// Currently supports only vertically-scrolling content.
/// </para>
/// </remarks>
public class ScrollableView : WrapperView
{
    /// <summary>
    /// The content to make scrollable.
    /// </summary>
    public IView? Content
    {
        get => container?.Content;
        set => RequireView(() => container).Content = value;
    }

    /// <summary>
    /// Amount of extra distance above/below scrolled content; see <see cref="ScrollContainer.Peeking"/>.
    /// </summary>
    public float Peeking
    {
        get => container?.Peeking ?? 0;
        set => RequireView(() => container).Peeking = value;
    }

    // Initialized in CreateView
    private ScrollContainer container = null!;
    private Scrollbar scrollbar = null!;

    public override void OnWheel(WheelEventArgs e)
    {
        if (e.Handled || scrollbar.Container is not ScrollContainer container)
        {
            return;
        }
        switch (e.Direction)
        {
            case Direction.North when container.Orientation == Orientation.Vertical:
            case Direction.West when container.Orientation == Orientation.Horizontal:
                e.Handled = container.ScrollBackward();
                break;
            case Direction.South when container.Orientation == Orientation.Vertical:
            case Direction.East when container.Orientation == Orientation.Horizontal:
                e.Handled = container.ScrollForward();
                break;
        }
        if (e.Handled)
        {
            Game1.playSound("shwip");
        }
    }

    protected override IView CreateView()
    {
        container = new ScrollContainer() { Peeking = 16, ScrollStep = 64 };
        scrollbar = new Scrollbar()
        {
            Layout = new() { Width = Length.Px(32), Height = Length.Stretch() },
            Margin = new(Left: 32, Bottom: -8),
            Container = container,
        };
        container.FloatingElements.Add(new(scrollbar, FloatingPosition.AfterParent));
        return container;
    }
}
