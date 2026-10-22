using System.ComponentModel;
using StardewUI.Events;
using StardewUI.Layout;

namespace StardewUI.Widgets;

/// <summary>
/// Describes content that can be scrolled with a scrollbar.
/// <c>(unofficial-mushymato)</c>
/// </summary>
public interface IScrollable : INotifyPropertyChanged
{
	/// <summary>
	/// The orientation, i.e. the direction of scrolling.
	/// </summary>
	/// <remarks>
	/// A single <see cref="IScrollable"/> can only scroll in one direction. If content needs to scroll both
	/// horizontally and vertically, a nested <see cref="IScrollable"/> can be used.
	/// </remarks>
	public Orientation Orientation { get; }

	/// <summary>
	/// The progress of scrollbar, calculated from intended scroll offset before clamp divided by size.
	/// </summary>
	public float Progress { get; set; }

	/// <summary>
	/// The maximum amount which can be scrolled without exceeding the content bounds.
	/// </summary>
	public float ScrollSize { get; }

	/// <summary>
	/// The current scroll position along the <see cref="Orientation"/> axis.
	/// </summary>
	public float ScrollOffset { get; set; }

	/// <summary>
	/// Default scroll distance when calling <see cref="ScrollForward"/> or <see cref="ScrollBackward"/>. Does not
	/// prevent directly setting the scroll position via <see cref="ScrollOffset"/>.
	/// </summary>
	public float ScrollStep { get; set; }

	/// <summary>
	/// Event raised when any aspect of the scrolling changes.
	/// </summary>
	/// <remarks>
	/// This tracks changes to the <see cref="ScrollOffset"/> but also the <see cref="ScrollSize"/>, even if the offset
	/// has not changed. <see cref="ScrollStep"/> is not included.
	/// </remarks>
	public event EventHandler? ScrollChanged;

	/// <summary>
	/// See <see cref="View.DragStart"/>
	/// </summary>
	public event EventHandler<PointerEventArgs>? DragStart;

	/// <summary>
	/// See <see cref="View.Drag"/>
	/// </summary>
	public event EventHandler<PointerEventArgs>? Drag;

	/// <summary>
	/// See <see cref="View.DragEnd"/>
	/// </summary>
	public event EventHandler<PointerEventArgs>? DragEnd;

	/// <summary>
	/// Set <see cref="ScrollOffset"/> without marking it as dirty.
	/// Using this means <see cref="ScrollChanged"/> will not be raised.
	/// </summary>
	public void SetScrollOffsetNoDirty(float scrollOffsetValue);

	/// <summary>
	/// Scrolls backward (up or left) by the distance configured in <see cref="ScrollStep"/>.
	/// </summary>
	public bool ScrollBackward();

	/// <summary>
	/// Scrolls forward (down or right) by the distance configured in <see cref="ScrollStep"/>.
	/// </summary>
	public bool ScrollForward();
}