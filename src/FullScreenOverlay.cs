﻿using Microsoft.Xna.Framework;
using StardewValley;

namespace StardewUI;

/// <summary>
/// Base class for an overlay meant to take up the full screen.
/// </summary>
public abstract class FullScreenOverlay : IOverlay
{
    /// <inheritdoc />
    /// <remarks>
    /// Full-screen overlays always have a <c>null</c> parent.
    /// </remarks>
    public IView? Parent => null;

    /// <inheritdoc />
    /// <remarks>
    /// Full-screen overlays should generally stretch to the entire viewport dimensions, but are middle-aligned in case
    /// of a discrepancy.
    /// </remarks>
    public Alignment HorizontalAlignment => Alignment.Middle;

    /// <inheritdoc />
    /// <remarks>
    /// Full-screen overlays should generally stretch to the entire viewport dimensions, but are middle-aligned in case
    /// of a discrepancy.
    /// </remarks>
    public Alignment HorizontalParentAlignment => Alignment.Middle;

    /// <inheritdoc />
    /// <remarks>
    /// Full-screen overlays should generally stretch to the entire viewport dimensions, but are middle-aligned in case
    /// of a discrepancy.
    /// </remarks>
    public Alignment VerticalAlignment => Alignment.Middle;

    /// <inheritdoc />
    /// <remarks>
    /// Full-screen overlays should generally stretch to the entire viewport dimensions, but are middle-aligned in case
    /// of a discrepancy.
    /// </remarks>
    public Alignment VerticalParentAlignment => Alignment.Middle;

    /// <inheritdoc />
    public Vector2 ParentOffset => Vector2.Zero;

    /// <inheritdoc />
    public float DimmingAmount { get; set; } = 0.8f;

    /// <inheritdoc />
    /// <remarks>
    /// The view provided in a full-screen overlay is a dimming frame with the content view inside.
    /// </remarks>
    public IView View => overlayView.Value;

    /// <inheritdoc />
    public event EventHandler<EventArgs>? Close;

    private readonly Lazy<IView> overlayView;

    public FullScreenOverlay()
    {
        overlayView = new(() => CreateOverlayView());
    }

    /// <inheritdoc />
    public void OnClose()
    {
        Game1.playSound("bigDeSelect");
        Close?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Creates the content view that will be displayed as an overlay.
    /// </summary>
    protected abstract IView CreateView();

    private IView CreateOverlayView()
    {
        return new Frame()
        {
            Layout = LayoutParameters.FitContent(),
            HorizontalContentAlignment = Alignment.Middle,
            VerticalContentAlignment = Alignment.Middle,
            Content = CreateView(),
        };
    }
}
