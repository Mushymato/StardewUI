﻿using Microsoft.Xna.Framework;
using StardewValley;

namespace StardewUI;

/// <summary>
/// Layout widget for a sectioned menu including a scrollable content area.
/// </summary>
/// <remarks>
/// The primary content is always kept centered, with optional title, footer and sidebar (e.g. for
/// navigation) available to decorate the view. Many if not most menus can be fully represented with
/// this layout, as long as they do not have built-in subnavigation such as top-level tabs.
/// </remarks>
public class ScrollableFrameView : WrapperView
{
    /// <summary>
    /// The primary content, which displays inside the menu frame and is clipped/scrollable.
    /// </summary>
    public IView? Content
    {
        get => contentContainer?.Content;
        set
        {
            var _ = Root; // Ensure view created
            contentContainer.Content = value;
        }
    }

    /// <summary>
    /// Optional footer to display below the <see cref="Content"/>.
    /// </summary>
    /// <remarks>
    /// Footer layout can be any arbitrary size and will not push up the <see cref="Content"/>.
    /// However, footers wider than the <c>Content</c> may cause problems.
    /// </remarks>
    public IView? Footer
    {
        get => footerContainer?.Children.FirstOrDefault();
        set
        {
            var _ = Root; // Ensure view created
            footerContainer.Children = value is not null ? [value] : [];
        }
    }

    /// <summary>
    /// Layout parameters to apply to the frame surrounding the <see cref="Content"/>.
    /// </summary>
    /// <remarks>
    /// Determines the size of the scrollable area and should generally be one of the
    /// <see cref="LayoutParameters.FixedSize"/> overloads, or at least have a fixed
    /// <see cref="LayoutParameters.Height"/>.
    /// </remarks>
    public LayoutParameters FrameLayout
    {
        get => contentFrame?.Layout ?? default;
        set
        {
            var _ = Root; // Ensure view created
            contentFrame.Layout = value;
        }
    }

    /// <summary>
    /// Optional content to display to the left of the <see cref="Content"/> frame.
    /// </summary>
    /// <remarks>
    /// Typically used for navigation or other contextual info. Max width is constrained to
    /// <see cref="SidebarWidth"/> regardless of layout.
    /// </remarks>
    public IView? Sidebar
    {
        get => sidebarContainer?.Children[0];
        set
        {
            var _ = Root; // Ensure view created
            sidebarContainer.Children = value is not null ? [value] : [];
        }
    }

    /// <summary>
    /// Maximum width of the sidebar area.
    /// </summary>
    /// <remarks>
    /// To keep the primary content centered, the same dimension must be applied to the scrollbar's
    /// container, so the width must be specified ahead of time. This acts as a maximum width; the
    /// actual sidebar does not have to fill this space (it will be right-aligned in that case), but
    /// larger views may clip or overflow.
    /// </remarks>
    public int SidebarWidth
    {
        get => sidebarWidth;
        set
        {
            if (sidebarWidth == value)
            {
                return;
            }
            sidebarWidth = value;
            if (IsViewCreated)
            {
                sidebarContainer.Layout = new()
                {
                    Width = Length.Px(sidebarWidth),
                    Height = Length.Content(),
                };
                scrollbar.Layout = new()
                {
                    Width = Length.Px(sidebarWidth),
                    Height = Length.Stretch(),
                };
            }
        }
    }

    /// <summary>
    /// Title to display above the <see cref="Content"/>.
    /// </summary>
    /// <remarks>
    /// All titles are displayed as a <see cref="Banner"/>.
    /// </remarks>
    public string? Title
    {
        get => banner?.Text;
        set
        {
            var _ = Root; // Ensure view created
            banner.Text = value ?? "";
            banner.Visibility = !string.IsNullOrEmpty(value) ? Visibility.Visible : Visibility.Hidden;
        }
    }

    private int sidebarWidth;

    // Initialized in CreateView
    private Banner banner = null!;
    private ScrollContainer contentContainer = null!;
    private Frame contentFrame = null!;
    private Panel footerContainer = null!;
    private Scrollbar scrollbar = null!;
    private Panel sidebarContainer = null!;
    private Lane scrollingLayout = null!;

    public override bool Measure(Vector2 availableSize)
    {
        var wasDirty = base.Measure(availableSize);
        if (wasDirty)
        {
            footerContainer.Margin = new(Top: (int)MathF.Ceiling(scrollingLayout.OuterSize.Y));
        }
        return wasDirty;
    }

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
        banner = new Banner()
        {
            Layout = LayoutParameters.FitContent(),
            Margin = new(Top: -85),
            Padding = new(12),
            Background = UiSprites.BannerBackground,
            BackgroundBorderThickness =
                (UiSprites.BannerBackground.FixedEdges ?? Edges.NONE)
                * (UiSprites.BannerBackground.SliceSettings?.Scale ?? 1),
            Visibility = Visibility.Hidden,
        };
        contentContainer = new ScrollContainer()
        {
            Name = "ContentScrollContainer",
            Peeking = 16,
            ScrollStep = 64,
            Layout = LayoutParameters.Fill(),
        };
        contentFrame = new Frame()
        {
            Name = "ContentFrame",
            Background = UiSprites.MenuBackground,
            Border = UiSprites.MenuBorder,
            BorderThickness = UiSprites.MenuBorderThickness,
            Margin = new(Top: -20),
            Content = contentContainer,
        };
        sidebarContainer = new Panel()
        {
            Layout = new() { Width = Length.Px(sidebarWidth), Height = Length.Content() },
            HorizontalContentAlignment = Alignment.End,
        };
        scrollbar = new Scrollbar(
            UiSprites.SmallUpArrow,
            UiSprites.SmallDownArrow,
            UiSprites.ScrollBarTrack,
            UiSprites.VerticalScrollThumb
        )
        {
            Name = "ContentPageScroll",
            Layout = new() { Width = Length.Px(sidebarWidth), Height = Length.Stretch() },
            Margin = new(Top: 10, Bottom: 20),
        };
        scrollbar.Container = contentContainer;
        scrollingLayout = new Lane()
        {
            Name = "ScrollableFrameScrollingLayout",
            Layout = LayoutParameters.FitContent(),
            Children = [sidebarContainer, contentFrame, scrollbar],
            ZIndex = 1,
        };
        footerContainer = new Panel()
        {
            Name = "ScrollableFrameFooter",
            Layout = LayoutParameters.FitContent(),
        };
        return new Panel()
        {
            Name = "ScrollableFrameContentLayout",
            Layout = LayoutParameters.FitContent(),
            HorizontalContentAlignment = Alignment.Middle,
            Children = [banner, scrollingLayout, footerContainer],
        };
    }
}
