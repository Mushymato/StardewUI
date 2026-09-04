---
title: Slider
description: A horizontal track with draggable thumb (button) for choosing a numeric value in a range.
search:
    boost: 0.002
---

<link rel="stylesheet" href="/StardewUI/stylesheets/reference.css" />

/// html | div.api-reference

# Class Slider

!!! tip "Framework View"

    The tag name of this view is `<slider>`.
## Definition

<div class="api-definition" markdown>

Namespace: [StardewUI.Widgets](index.md)  
Assembly: StardewUI.dll  

</div>

A horizontal track with draggable thumb (button) for choosing a numeric value in a range.

```cs
[StardewUI.GenerateDescriptor]
public class Slider : StardewUI.Widgets.ComponentView
```

**Inheritance**  
[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) ⇦ [DecoratorView&lt;T&gt;](decoratorview-1.md) ⇦ [ComponentView&lt;T&gt;](componentview-1.md) ⇦ [ComponentView](componentview.md) ⇦ Slider

## Members

### Constructors

 | Name | Description |
| --- | --- |
| [Slider()](#slider) |  | 

### Properties

 | Name | Description |
| --- | --- |
| [ActualBounds](decoratorview-1.md#actualbounds)<br/>`actual-bounds` | The bounds of this view relative to the origin (0, 0).<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [BackgroundSprite](#backgroundsprite)<br/>`background-sprite` | Background or track sprite, if not using the default. | 
| [ClipOrigin](decoratorview-1.md#cliporigin)<br/>`clip-origin` | Origin position for the [ClipSize](../iview.md#clipsize).<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [ClipSize](decoratorview-1.md#clipsize)<br/>`clip-size` | Size of the clipping rectangle, outside which content will not be displayed.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [ContentBounds](decoratorview-1.md#contentbounds)<br/>`content-bounds` | The true bounds of this view's content; i.e. [ActualBounds](../iview.md#actualbounds) excluding margins.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [FloatingBounds](decoratorview-1.md#floatingbounds)<br/>`floating-bounds` | Contains the bounds of all floating elements in this view tree, including the current view and all descendants.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [FocusableTag](decoratorview-1.md#focusabletag)<br/>`focusable-tag` | A string tag that identifies this view for use in [FocusOnTaggedView(string)](../framework/imenucontroller.md#focusontaggedviewstring).<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [HoveredSubject](decoratorview-1.md#hoveredsubject)<br/>`hovered-subject` | When Lookup Anything (Pathoschild.LookupAnything) is loaded, this Object or NPC subject is given to lookup anything for it's menu. `(unofficial-mushymato)`<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [Interval](#interval)<br/>`interval` | The interval of which [Value](slider.md#value) should be a multiple. Affects which values will be hit while dragging. | 
| [IsFocusable](decoratorview-1.md#isfocusable)<br/>`is-focusable` | Whether or not the view can receive controller focus, i.e. the stick/d-pad controlled cursor can move to this view. Not generally applicable for mouse controls.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [IsWithinScrollBounds](decoratorview-1.md#iswithinscrollbounds)<br/>`is-within-scroll-bounds` | Whether this view is currently within the scrolling bounds, updated during [Measure(Vector2)](../iview.md#measurevector2).<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [ItemSpan](decoratorview-1.md#itemspan)<br/>`item-span` | Item span that defines how many cells this item takes up when it's the child of a grid. Span only considers the primary orientation and cannot exceed one row/col When this is -1, take the remainder of the row/col. `(unofficial-mushymato)`<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [Layout](decoratorview-1.md#layout)<br/>`layout` | The current layout parameters, which determine how [Measure(Vector2)](../iview.md#measurevector2) will behave.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [Max](#max)<br/>`max` | The maximum value allowed for [Value](slider.md#value). | 
| [Min](#min)<br/>`min` | The minimum value allowed for [Value](slider.md#value). | 
| [Name](decoratorview-1.md#name)<br/>`name` | Simple name for this view, used in log/debug output; does not affect behavior.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [Opacity](decoratorview-1.md#opacity)<br/>`opacity` | Opacity (alpha level) of the view.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [OuterSize](decoratorview-1.md#outersize)<br/>`outer-size` | The true computed layout size resulting from a single [Measure(Vector2)](../iview.md#measurevector2) pass.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [PointerEventsEnabled](decoratorview-1.md#pointereventsenabled)<br/>`pointer-events-enabled` | Whether this view should receive pointer events like [Click](../iview.md#click) or [Drag](../iview.md#drag).<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [PointerStyle](decoratorview-1.md#pointerstyle)<br/>`pointer-style` | Pointer style to use when this view is hovered.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [ScreenRead](decoratorview-1.md#screenread)<br/>`screen-read` | When a screen reader mod (shoaib.stardewaccess) is loaded, this element will be announced by the screen reader using this value. `(unofficial-mushymato)`<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [ScrollWithChildren](decoratorview-1.md#scrollwithchildren)<br/>`scroll-with-children` | If set to an axis, specifies that when any child of the view is scrolled into view (using [ScrollIntoView(IEnumerable&lt;ViewChild&gt;, Vector2)](../iview.md#scrollintoviewienumerableviewchild-vector2)), then this entire view should be scrolled along with it.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [Tags](decoratorview-1.md#tags)<br/>`tags` | The user-defined tags for this view.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [ThumbSize](#thumbsize)<br/>`thumb-size` | Override for the thumb/button size, recommended when using a custom [ThumbSprite](slider.md#thumbsprite). | 
| [ThumbSprite](#thumbsprite)<br/>`thumb-sprite` | Sprite for the thumb/button, if not using the default. | 
| [Tooltip](decoratorview-1.md#tooltip)<br/>`tooltip` | Tooltip data to display on hover, if any.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [TrackWidth](#trackwidth)<br/>`track-width` | Width of the track bar. | 
| [Transform](decoratorview-1.md#transform)<br/>`transform` | Local transformation to apply to this view, including any children and floating elements.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [TransformOrigin](decoratorview-1.md#transformorigin)<br/>`transform-origin` | Relative origin position for any [Transform](../iview.md#transform) on this view.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [Value](#value)<br/>`value` | The current value. | 
| [ValueColor](#valuecolor)<br/>`value-color` | Color of the value text to render, if overriding the default text color. | 
| [ValueFormat](#valueformat)<br/>`value-format` | Specifies how to format the [Value](slider.md#value) in the label text. | 
| [View](componentview-1.md#view)<br/>`view` | <span class="muted" markdown>(Inherited from [ComponentView&lt;T&gt;](componentview-1.md))</span> | 
| [Visibility](decoratorview-1.md#visibility)<br/>`visibility` | Drawing visibility for this view.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [ZIndex](decoratorview-1.md#zindex)<br/>`z-index` | Z order for this view within its direct parent. Higher indices draw later (on top).<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 

### Methods

 | Name | Description |
| --- | --- |
| [ContainsPoint(Vector2)](decoratorview-1.md#containspointvector2) | Checks if a given point, relative to the view's origin, is within its bounds.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [CreateView()](#createview) | Creates and returns the root view.<br><span class="muted" markdown>(Overrides [ComponentView&lt;T&gt;](componentview-1.md).[CreateView()](componentview-1.md#createview))</span> | 
| [Dispose()](decoratorview-1.md#dispose) | <span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [Draw(ISpriteBatch)](decoratorview-1.md#drawispritebatch) | Draws the content for this view.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [FocusSearch(Vector2, Direction)](#focussearchvector2-direction) | Finds the next focusable component in a given direction that does _not_ overlap with a current position.<br><span class="muted" markdown>(Overrides [DecoratorView&lt;T&gt;](decoratorview-1.md).[FocusSearch(Vector2, Direction)](decoratorview-1.md#focussearchvector2-direction))</span> | 
| [GetChildAt(Vector2, Boolean, Boolean)](decoratorview-1.md#getchildatvector2-bool-bool) | Finds the child at a given position.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [GetChildPosition(IView)](decoratorview-1.md#getchildpositioniview) | Computes or retrieves the position of a given direct child.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [GetChildren(Boolean)](decoratorview-1.md#getchildrenbool) | Gets the current children of this view.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [GetChildrenAt(Vector2)](decoratorview-1.md#getchildrenatvector2) | Finds all children at a given position.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [GetDefaultFocusChild()](decoratorview-1.md#getdefaultfocuschild) | Gets the direct child that should contain cursor focus when a menu or overlay containing this view is first opened.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [HasOutOfBoundsContent()](decoratorview-1.md#hasoutofboundscontent) | Checks if the view has content or elements that are all or partially outside the [ActualBounds](../iview.md#actualbounds).<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [IsDirty()](decoratorview-1.md#isdirty) | Checks whether or not the view is dirty - i.e. requires a new layout with a full [Measure(Vector2)](../iview.md#measurevector2).<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [IsVisible(Vector2?)](decoratorview-1.md#isvisiblevector2) | Checks if the view is effectively visible, i.e. if it has anything to draw.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [Measure(Vector2)](decoratorview-1.md#measurevector2) | Performs layout on this view, updating its [OuterSize](../iview.md#outersize), [ActualBounds](../iview.md#actualbounds) and [ContentBounds](../iview.md#contentbounds), and arranging any children in their respective positions.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [OnButtonPress(ButtonEventArgs)](decoratorview-1.md#onbuttonpressbuttoneventargs) | Called when a button press is received while this view is in the focus path.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [OnButtonRepeat(ButtonEventArgs)](decoratorview-1.md#onbuttonrepeatbuttoneventargs) | Called when a button press is first received, and at recurring intervals thereafter, for as long as the button is held and this view remains in the focus path.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [OnClick(ClickEventArgs)](decoratorview-1.md#onclickclickeventargs) | Called when a click is received within this view's bounds.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [OnDrag(PointerEventArgs)](decoratorview-1.md#ondragpointereventargs) | Called when the view is being dragged (mouse moved while left button held).<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [OnDrop(PointerEventArgs)](decoratorview-1.md#ondroppointereventargs) | Called when the mouse button is released after at least one [OnDrag(PointerEventArgs)](../iview.md#ondragpointereventargs).<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [OnLayout()](#onlayout) | Runs whenever layout occurs as a result of the UI elements changing.<br><span class="muted" markdown>(Overrides [DecoratorView&lt;T&gt;](decoratorview-1.md).[OnLayout()](decoratorview-1.md#onlayout))</span> | 
| [OnPointerMove(PointerMoveEventArgs)](decoratorview-1.md#onpointermovepointermoveeventargs) | Called when a pointer movement related to this view occurs.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [OnPropertyChanged(PropertyChangedEventArgs)](decoratorview-1.md#onpropertychangedpropertychangedeventargs) | Raises the [PropertyChanged](decoratorview-1.md#propertychanged) event.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [OnPropertyChanged(string)](decoratorview-1.md#onpropertychangedstring) | Raises the [PropertyChanged](decoratorview-1.md#propertychanged) event.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [OnUpdate(TimeSpan)](decoratorview-1.md#onupdatetimespan) | Runs on every update tick.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [OnWheel(WheelEventArgs)](decoratorview-1.md#onwheelwheeleventargs) | Called when a wheel event is received within this view's bounds.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [RegisterDecoratedProperty&lt;TValue&gt;(DecoratedProperty&lt;T, TValue&gt;)](decoratorview-1.md#registerdecoratedpropertytvaluedecoratedpropertyt-tvalue) | Registers a [DecoratedProperty&lt;T, TValue&gt;](decoratorview-1.decoratedproperty-1.md).<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [ScrollIntoView(IEnumerable&lt;ViewChild&gt;, Vector2)](decoratorview-1.md#scrollintoviewienumerableviewchild-vector2) | Attempts to scroll the specified target into view, including all of its ancestors, if not fully in view.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [UpdateParentScrollingBounds(Bounds)](decoratorview-1.md#updateparentscrollingboundsbounds) | Propagate new scrolling bounds to this view and it's children<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 

### Events

 | Name | Description |
| --- | --- |
| [ButtonPress](decoratorview-1.md#buttonpress) | Event raised when any button on any input device is pressed.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [ButtonRepeat](decoratorview-1.md#buttonrepeat) | Event raised when a button is being held while the view is in focus, and has been held long enough since the initial [ButtonPress](../iview.md#buttonpress) or the previous `ButtonRepeat` to trigger a repeated press.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [Click](decoratorview-1.md#click) | Event raised when the view receives a click initiated from any button.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [Drag](decoratorview-1.md#drag) | Event raised when the view is being dragged using the mouse.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [DragEnd](decoratorview-1.md#dragend) | Event raised when mouse dragging is stopped, i.e. when the button is released. Always raised after the last [Drag](../iview.md#drag), and only once per drag operation.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [DragStart](decoratorview-1.md#dragstart) | Event raised when mouse dragging is first activated. Always raised before the first [Drag](../iview.md#drag), and only once per drag operation.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [LeftClick](decoratorview-1.md#leftclick) | Event raised when the view receives a click initiated from the left mouse button, or the controller's action button (A).<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [PointerEnter](decoratorview-1.md#pointerenter) | Event raised when the pointer enters the view.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [PointerLeave](decoratorview-1.md#pointerleave) | Event raised when the pointer exits the view.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [PointerMove](decoratorview-1.md#pointermove) | Event raised when the pointer moves within the view.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [PropertyChanged](decoratorview-1.md#propertychanged) | <span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [RightClick](decoratorview-1.md#rightclick) | Event raised when the view receives a click initiated from the right mouse button, or the controller's tool-use button (X).<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [ValueChange](#valuechange) | Event raised when the [Value](slider.md#value) changes. | 
| [Wheel](decoratorview-1.md#wheel) | Event raised when the scroll wheel moves.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 

## Details

### Constructors

#### Slider()



```cs
public Slider();
```

-----

### Properties

#### BackgroundSprite

Background or track sprite, if not using the default.

```cs
public StardewUI.Graphics.Sprite BackgroundSprite { get; set; }
```

##### Property Value

[Sprite](../graphics/sprite.md)

-----

#### Interval

The interval of which [Value](slider.md#value) should be a multiple. Affects which values will be hit while dragging.

```cs
public float Interval { get; set; }
```

##### Property Value

[Single](https://learn.microsoft.com/en-us/dotnet/api/system.single)

-----

#### Max

The maximum value allowed for [Value](slider.md#value).

```cs
public float Max { get; set; }
```

##### Property Value

[Single](https://learn.microsoft.com/en-us/dotnet/api/system.single)

-----

#### Min

The minimum value allowed for [Value](slider.md#value).

```cs
public float Min { get; set; }
```

##### Property Value

[Single](https://learn.microsoft.com/en-us/dotnet/api/system.single)

-----

#### ThumbSize

Override for the thumb/button size, recommended when using a custom [ThumbSprite](slider.md#thumbsprite).

```cs
public Microsoft.Xna.Framework.Vector2? ThumbSize { get; set; }
```

##### Property Value

[Nullable](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<[Vector2](https://docs.monogame.net/api/Microsoft.Xna.Framework.Vector2.html)>

-----

#### ThumbSprite

Sprite for the thumb/button, if not using the default.

```cs
public StardewUI.Graphics.Sprite ThumbSprite { get; set; }
```

##### Property Value

[Sprite](../graphics/sprite.md)

-----

#### TrackWidth

Width of the track bar.

```cs
public float TrackWidth { get; set; }
```

##### Property Value

[Single](https://learn.microsoft.com/en-us/dotnet/api/system.single)

-----

#### Value

The current value.

```cs
public float Value { get; set; }
```

##### Property Value

[Single](https://learn.microsoft.com/en-us/dotnet/api/system.single)

-----

#### ValueColor

Color of the value text to render, if overriding the default text color.

```cs
public Microsoft.Xna.Framework.Color? ValueColor { get; set; }
```

##### Property Value

[Nullable](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<[Color](https://docs.monogame.net/api/Microsoft.Xna.Framework.Color.html)>

-----

#### ValueFormat

Specifies how to format the [Value](slider.md#value) in the label text.

```cs
public Func<System.Single, string> ValueFormat { get; set; }
```

##### Property Value

[Func](https://learn.microsoft.com/en-us/dotnet/api/system.func-2)<[Single](https://learn.microsoft.com/en-us/dotnet/api/system.single), [string](https://learn.microsoft.com/en-us/dotnet/api/system.string)>

-----

### Methods

#### CreateView()

Creates and returns the root view.

```cs
protected override StardewUI.IView CreateView();
```

##### Returns

[IView](../iview.md)

-----

#### FocusSearch(Vector2, Direction)

Finds the next focusable component in a given direction that does _not_ overlap with a current position.

```cs
public override StardewUI.Input.FocusSearchResult FocusSearch(Microsoft.Xna.Framework.Vector2 position, StardewUI.Direction direction);
```

##### Parameters

**`position`** &nbsp; [Vector2](https://docs.monogame.net/api/Microsoft.Xna.Framework.Vector2.html)  
The current cursor position, relative to this view. May have dimensions that are negative or outside the view bounds, indicating that the cursor is not currently within the view.

**`direction`** &nbsp; [Direction](../direction.md)  
The direction of cursor movement.

##### Returns

[FocusSearchResult](../input/focussearchresult.md)

  The next focusable view reached by moving in the specified `direction`, or `null` if there are no focusable descendants that are possible to reach in that direction.

##### Remarks

If `position` is out of bounds, it does not necessarily mean that the view should return `null`; the expected result depends on the `direction` also. The base case is when the focus position is already in bounds, and in this case a view should return whichever view can be reached by moving from the edge of that view along a straight line in the specified `direction`. However, focus search is recursive and the result should reflect the "best" candidate for focus if the cursor were to move _into_ this view's bounds. For example, in a 1D horizontal layout the rules might be: 

  - If the `direction` is [East](../direction.md#east), and the position's X value is negative, then the result should the leftmost focusable child, regardless of Y value.
  - If the direction is [South](../direction.md#south), and the X position is within the view's horizontal bounds, and the Y value is negative or greater than the view's height, then result should be whichever child intersects with that X position.
  - If the direction is [West](../direction.md#west) and the X position is negative, or the direction is [East](../direction.md#east) and the X position is greater than the view's width, then the result should be `null` as there is literally nothing the view knows about in that direction.

 There are no strict rules for how a view performs focus search, but in general it is assumed that a view implementation understands its own layout and can accommodate accordingly; for example, a grid would follow essentially the same rules as our "list" example above, with additional considerations for navigating rows. "Ragged" 2D layouts might have complex rules requiring explicit neighbors, and therefore are typically easier to implement as nested lanes.

-----

#### OnLayout()

Runs whenever layout occurs as a result of the UI elements changing.

```cs
protected override void OnLayout();
```

-----

### Events

#### ValueChange

Event raised when the [Value](slider.md#value) changes.

```cs
public event EventHandler<System.EventArgs>? ValueChange;
```

##### Event Type

[EventHandler](https://learn.microsoft.com/en-us/dotnet/api/system.eventhandler-1)<[EventArgs](https://learn.microsoft.com/en-us/dotnet/api/system.eventargs)>

-----

