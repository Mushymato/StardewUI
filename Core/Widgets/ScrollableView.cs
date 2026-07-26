using System.ComponentModel;
using StardewUI.Events;
using StardewUI.Graphics;
using StardewUI.Layout;

namespace StardewUI.Widgets;

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
public partial class ScrollableView : ComponentView<ScrollContainer>, IFloatContainer
{
    /// <summary>
    /// The content to make scrollable.
    /// </summary>
    public IView? Content
    {
        get => View.Content;
        set => View.Content = value;
    }

    /// <inheritdoc />
    public IList<FloatingElement> FloatingElements
    {
        get => View.FloatingElements;
        set => View.FloatingElements = value;
    }

    /// <summary>
    /// Amount of extra distance above/below scrolled content; see <see cref="ScrollContainer.Peeking"/>.
    /// </summary>
    public float Peeking
    {
        get => View.Peeking;
        set => View.Peeking = value;
    }

    /// <summary>
    /// The <see cref="Scrollbar.DownSprite" /> used for the scrollbar.
    /// </summary>
    public Sprite? ScrollbarDownSprite
    {
        get => scrollbar.DownSprite;
        set => scrollbar.DownSprite = value;
    }

    /// <summary>
    /// The <see cref="Scrollbar.Margin" /> of the scrollbar.
    /// </summary>
    public Edges ScrollbarMargin
    {
        get => scrollbar.Margin;
        set => scrollbar.Margin = value;
    }

    /// <summary>
    /// The <see cref="Scrollbar.ForcedVisibility" /> of the scrollbar.
    /// </summary>
    public Visibility? ScrollbarVisibility
    {
        get => scrollbar.ForcedVisibility;
        set => scrollbar.ForcedVisibility = value;
    }

    /// <summary>
    /// The <see cref="Scrollbar.ThumbSprite" /> used for the scrollbar.
    /// </summary>
    public Sprite? ScrollbarThumbSprite
    {
        get => scrollbar.ThumbSprite;
        set => scrollbar.ThumbSprite = value;
    }

    /// <summary>
    /// The <see cref="Scrollbar.TrackSprite" /> used for the scrollbar.
    /// </summary>
    public Sprite? ScrollbarTrackSprite
    {
        get => scrollbar.TrackSprite;
        set => scrollbar.TrackSprite = value;
    }

    /// <summary>
    /// The <see cref="Scrollbar.UpSprite" /> used for the scrollbar.
    /// </summary>
    public Sprite? ScrollbarUpSprite
    {
        get => scrollbar.UpSprite;
        set => scrollbar.UpSprite = value;
    }

    /// <summary>
    /// The <see cref="ScrollContainer.ScrollStep" /> of the scrollable view.
    /// <c>(unofficial-mushymato)</c>
    /// </summary>
    public float ScrollStep
    {
        get => scrollbar.Container?.ScrollStep ?? 32f;
        set => scrollbar.Container?.ScrollStep = value;
    }

    /// <summary>
    /// The <see cref="Scrollbar.Progress" /> of the scrollable view.
    /// <c>(unofficial-mushymato)</c>
    /// </summary>
    public float Progress
    {
        get => scrollbar.Progress;
        set => scrollbar.Progress = value;
    }

    // Initialized in CreateView
    private Scrollbar scrollbar = null!;

    /// <inheritdoc />
    public override void OnWheel(WheelEventArgs e)
    {
        if (e.Handled || scrollbar.Container is not { } container)
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

    /// <inheritdoc />
    protected override ScrollContainer CreateView()
    {
        var container = new ScrollContainer() { Peeking = 16, ScrollStep = 64 };
        scrollbar = new Scrollbar()
        {
            Layout = new() { Height = Length.Stretch() },
            Margin = new(Left: 32, Bottom: -8),
            Container = container,
        };
        container.FloatingElements.Add(new(scrollbar, FloatingPosition.AfterParent));
        scrollbar.PropertyChanged += OnPropertyChanged;
        return container;
    }

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(Scrollbar.Progress))
            OnPropertyChanged(nameof(Progress));
    }

    /// <summary>
    /// Convienence function that resets scrolling then
    /// calls <see cref="IView.ScrollIntoView(IEnumerable{ViewChild}, out Vector2)"/>
    /// on the <see cref="ScrollContainer"/> of this view,
    /// using a known <seealso cref="ViewChild"/> of <see cref="ScrollContainer.Content"/>.
    ///
    /// This achieves effect of scrolling to a particular child outside of <see cref="IView.FocusSearch(Vector2, Direction)"/>.
    /// </summary>
    /// <param name="child">Target child to scroll to.</param>
    /// <param name="distance">Final scrolled distance</param>
    /// <returns></returns>
    public bool ContainerScrollIntoView(ViewChild child, out Vector2 distance)
    {
        distance = default;
        scrollbar.Container?.ScrollOffset = 0;
        return scrollbar.Container?.ScrollIntoView([child], out distance) ?? false;
    }
}
