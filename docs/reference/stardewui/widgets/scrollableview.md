---
title: ScrollableView
description: Provides a content container and accompanying scrollbar.
search:
    boost: 0.002
---

<link rel="stylesheet" href="/StardewUI/stylesheets/reference.css" />

/// html | div.api-reference

# Class ScrollableView

!!! tip "Framework View"

    The tag name of this view is `<scrollable>`.
## Definition

<div class="api-definition" markdown>

Namespace: [StardewUI.Widgets](index.md)  
Assembly: StardewUI.dll  

</div>

Provides a content container and accompanying scrollbar.

```cs
[StardewUI.GenerateDescriptor]
public class ScrollableView : StardewUI.Widgets.ComponentView<T>, 
    StardewUI.Layout.IFloatContainer
```

**Inheritance**  
[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) ⇦ [DecoratorView&lt;T&gt;](decoratorview-1.md) ⇦ [ComponentView&lt;T&gt;](componentview-1.md) ⇦ ScrollableView

**Implements**  
[IFloatContainer](../layout/ifloatcontainer.md)

## Remarks

This does not add any extra UI elements aside from the scrollbar, like [ScrollableFrameView](scrollableframeview.md) does, and is more suitable for highly customized menus. 

 Currently supports only vertically-scrolling content.

## Members

### Constructors

 | Name | Description |
| --- | --- |
| [ScrollableView()](#scrollableview) |  | 

### Properties

 | Name | Description |
| --- | --- |
| [ActualBounds](decoratorview-1.md#actualbounds)<br/>`actual-bounds` | The bounds of this view relative to the origin (0, 0).<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [ClipOrigin](decoratorview-1.md#cliporigin)<br/>`clip-origin` | Origin position for the [ClipSize](../iview.md#clipsize).<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [ClipSize](decoratorview-1.md#clipsize)<br/>`clip-size` | Size of the clipping rectangle, outside which content will not be displayed.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [Content](#content)<br/>`content` | The content to make scrollable. | 
| [ContentBounds](decoratorview-1.md#contentbounds)<br/>`content-bounds` | The true bounds of this view's content; i.e. [ActualBounds](../iview.md#actualbounds) excluding margins.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [DragToScroll](#dragtoscroll)<br/>`drag-to-scroll` | The [Draggable](../view.md#draggable) of the scrollable view. If enabled, this view can be dragged (e.g. touch screen controls) `(unofficial-mushymato)` | 
| [FloatingBounds](decoratorview-1.md#floatingbounds)<br/>`floating-bounds` | Contains the bounds of all floating elements in this view tree, including the current view and all descendants.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [FloatingElements](#floatingelements)<br/>`floating-elements` | The floating elements to display relative to this view. | 
| [FocusableTag](decoratorview-1.md#focusabletag)<br/>`focusable-tag` | A string tag that identifies this view for use in [FocusOnTaggedView(string)](../framework/imenucontroller.md#focusontaggedviewstring).<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [HoveredSubject](decoratorview-1.md#hoveredsubject)<br/>`hovered-subject` | When Lookup Anything (Pathoschild.LookupAnything) is loaded, this Object or NPC subject is given to lookup anything for it's menu. `(unofficial-mushymato)`<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [IsFocusable](decoratorview-1.md#isfocusable)<br/>`is-focusable` | Whether or not the view can receive controller focus, i.e. the stick/d-pad controlled cursor can move to this view. Not generally applicable for mouse controls.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [IsWithinScrollBounds](decoratorview-1.md#iswithinscrollbounds)<br/>`is-within-scroll-bounds` | Whether this view is currently within the scrolling bounds, updated during [Measure(Vector2)](../iview.md#measurevector2).<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [ItemSpan](decoratorview-1.md#itemspan)<br/>`item-span` | Item span that defines how many cells this item takes up when it's the child of a grid. Span only considers the primary orientation and cannot exceed one row/col When this is -1, take the remainder of the row/col. `(unofficial-mushymato)`<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [Layout](decoratorview-1.md#layout)<br/>`layout` | The current layout parameters, which determine how [Measure(Vector2)](../iview.md#measurevector2) will behave.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [Name](decoratorview-1.md#name)<br/>`name` | Simple name for this view, used in log/debug output; does not affect behavior.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [Opacity](decoratorview-1.md#opacity)<br/>`opacity` | Opacity (alpha level) of the view.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [OuterSize](decoratorview-1.md#outersize)<br/>`outer-size` | The true computed layout size resulting from a single [Measure(Vector2)](../iview.md#measurevector2) pass.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [Peeking](#peeking)<br/>`peeking` | Amount of extra distance above/below scrolled content; see [Peeking](scrollcontainer.md#peeking). | 
| [PointerEventsEnabled](decoratorview-1.md#pointereventsenabled)<br/>`pointer-events-enabled` | Whether this view should receive pointer events like [Click](../iview.md#click) or [Drag](../iview.md#drag).<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [PointerStyle](decoratorview-1.md#pointerstyle)<br/>`pointer-style` | Pointer style to use when this view is hovered.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [Progress](#progress)<br/>`progress` | The [Progress](scrollbar.md#progress) of the scrollable view. `(unofficial-mushymato)` | 
| [ScreenRead](decoratorview-1.md#screenread)<br/>`screen-read` | When a screen reader mod (shoaib.stardewaccess) is loaded, this element will be announced by the screen reader using this value. `(unofficial-mushymato)`<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [ScrollbarDownSprite](#scrollbardownsprite)<br/>`scrollbar-down-sprite` | The [DownSprite](scrollbar.md#downsprite) used for the scrollbar. | 
| [ScrollbarMargin](#scrollbarmargin)<br/>`scrollbar-margin` | The [Margin](scrollbar.md#margin) of the scrollbar. | 
| [ScrollbarThumbSprite](#scrollbarthumbsprite)<br/>`scrollbar-thumb-sprite` | The [ThumbSprite](scrollbar.md#thumbsprite) used for the scrollbar. | 
| [ScrollbarTrackSprite](#scrollbartracksprite)<br/>`scrollbar-track-sprite` | The [TrackSprite](scrollbar.md#tracksprite) used for the scrollbar. | 
| [ScrollbarUpSprite](#scrollbarupsprite)<br/>`scrollbar-up-sprite` | The [UpSprite](scrollbar.md#upsprite) used for the scrollbar. | 
| [ScrollbarVisibility](#scrollbarvisibility)<br/>`scrollbar-visibility` | The [ForcedVisibility](scrollbar.md#forcedvisibility) of the scrollbar. | 
| [ScrollStep](#scrollstep)<br/>`scroll-step` | The [ScrollStep](scrollcontainer.md#scrollstep) of the scrollable view. `(unofficial-mushymato)` | 
| [ScrollWithChildren](decoratorview-1.md#scrollwithchildren)<br/>`scroll-with-children` | If set to an axis, specifies that when any child of the view is scrolled into view (using [ScrollIntoView(IEnumerable&lt;ViewChild&gt;, Vector2)](../iview.md#scrollintoviewienumerableviewchild-vector2)), then this entire view should be scrolled along with it.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [Tags](decoratorview-1.md#tags)<br/>`tags` | The user-defined tags for this view.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [Tooltip](decoratorview-1.md#tooltip)<br/>`tooltip` | Tooltip data to display on hover, if any.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [Transform](decoratorview-1.md#transform)<br/>`transform` | Local transformation to apply to this view, including any children and floating elements.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [TransformOrigin](decoratorview-1.md#transformorigin)<br/>`transform-origin` | Relative origin position for any [Transform](../iview.md#transform) on this view.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [View](componentview-1.md#view)<br/>`view` | <span class="muted" markdown>(Inherited from [ComponentView&lt;T&gt;](componentview-1.md))</span> | 
| [Visibility](decoratorview-1.md#visibility)<br/>`visibility` | Drawing visibility for this view.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [ZIndex](decoratorview-1.md#zindex)<br/>`z-index` | Z order for this view within its direct parent. Higher indices draw later (on top).<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 

### Methods

 | Name | Description |
| --- | --- |
| [ContainerScrollIntoView(ViewChild, Vector2)](#containerscrollintoviewviewchild-vector2) | Convienence function that resets scrolling then calls [ScrollIntoView(IEnumerable&lt;ViewChild&gt;, Vector2)](../iview.md#scrollintoviewienumerableviewchild-vector2) on the [ScrollContainer](scrollcontainer.md) of this view, using a known [ViewChild](../viewchild.md) of [Content](scrollcontainer.md#content). This achieves effect of scrolling to a particular child outside of [FocusSearch(Vector2, Direction)](../iview.md#focussearchvector2-direction). | 
| [ContainsPoint(Vector2)](decoratorview-1.md#containspointvector2) | Checks if a given point, relative to the view's origin, is within its bounds.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [CreateView()](#createview) | Creates and returns the root view.<br><span class="muted" markdown>(Overrides [ComponentView&lt;T&gt;](componentview-1.md).[CreateView()](componentview-1.md#createview))</span> | 
| [Dispose()](decoratorview-1.md#dispose) | <span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [Draw(ISpriteBatch)](decoratorview-1.md#drawispritebatch) | Draws the content for this view.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [FocusSearch(Vector2, Direction)](decoratorview-1.md#focussearchvector2-direction) | Finds the next focusable component in a given direction that does _not_ overlap with a current position.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
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
| [OnLayout()](decoratorview-1.md#onlayout) | Runs whenever layout occurs as a result of the UI elements changing.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [OnPointerMove(PointerMoveEventArgs)](decoratorview-1.md#onpointermovepointermoveeventargs) | Called when a pointer movement related to this view occurs.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [OnPropertyChanged(PropertyChangedEventArgs)](decoratorview-1.md#onpropertychangedpropertychangedeventargs) | Raises the [PropertyChanged](decoratorview-1.md#propertychanged) event.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [OnPropertyChanged(string)](decoratorview-1.md#onpropertychangedstring) | Raises the [PropertyChanged](decoratorview-1.md#propertychanged) event.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [OnUpdate(TimeSpan)](decoratorview-1.md#onupdatetimespan) | Runs on every update tick.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 
| [OnWheel(WheelEventArgs)](#onwheelwheeleventargs) | Called when a wheel event is received within this view's bounds.<br><span class="muted" markdown>(Overrides [DecoratorView&lt;T&gt;](decoratorview-1.md).[OnWheel(WheelEventArgs)](decoratorview-1.md#onwheelwheeleventargs))</span> | 
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
| [Wheel](decoratorview-1.md#wheel) | Event raised when the scroll wheel moves.<br><span class="muted" markdown>(Inherited from [DecoratorView&lt;T&gt;](decoratorview-1.md))</span> | 

## Details

### Constructors

#### ScrollableView()



```cs
public ScrollableView();
```

-----

### Properties

#### Content

The content to make scrollable.

```cs
public StardewUI.IView Content { get; set; }
```

##### Property Value

[IView](../iview.md)

-----

#### DragToScroll

The [Draggable](../view.md#draggable) of the scrollable view. If enabled, this view can be dragged (e.g. touch screen controls) `(unofficial-mushymato)`

```cs
public bool DragToScroll { get; set; }
```

##### Property Value

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)

-----

#### FloatingElements

The floating elements to display relative to this view.

```cs
public System.Collections.Generic.IList<StardewUI.Layout.FloatingElement> FloatingElements { get; set; }
```

##### Property Value

[IList](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ilist-1)<[FloatingElement](../layout/floatingelement.md)>

-----

#### Peeking

Amount of extra distance above/below scrolled content; see [Peeking](scrollcontainer.md#peeking).

```cs
public float Peeking { get; set; }
```

##### Property Value

[Single](https://learn.microsoft.com/en-us/dotnet/api/system.single)

-----

#### Progress

The [Progress](scrollbar.md#progress) of the scrollable view. `(unofficial-mushymato)`

```cs
public float Progress { get; set; }
```

##### Property Value

[Single](https://learn.microsoft.com/en-us/dotnet/api/system.single)

-----

#### ScrollbarDownSprite

The [DownSprite](scrollbar.md#downsprite) used for the scrollbar.

```cs
public StardewUI.Graphics.Sprite ScrollbarDownSprite { get; set; }
```

##### Property Value

[Sprite](../graphics/sprite.md)

-----

#### ScrollbarMargin

The [Margin](scrollbar.md#margin) of the scrollbar.

```cs
public StardewUI.Layout.Edges ScrollbarMargin { get; set; }
```

##### Property Value

[Edges](../layout/edges.md)

-----

#### ScrollbarThumbSprite

The [ThumbSprite](scrollbar.md#thumbsprite) used for the scrollbar.

```cs
public StardewUI.Graphics.Sprite ScrollbarThumbSprite { get; set; }
```

##### Property Value

[Sprite](../graphics/sprite.md)

-----

#### ScrollbarTrackSprite

The [TrackSprite](scrollbar.md#tracksprite) used for the scrollbar.

```cs
public StardewUI.Graphics.Sprite ScrollbarTrackSprite { get; set; }
```

##### Property Value

[Sprite](../graphics/sprite.md)

-----

#### ScrollbarUpSprite

The [UpSprite](scrollbar.md#upsprite) used for the scrollbar.

```cs
public StardewUI.Graphics.Sprite ScrollbarUpSprite { get; set; }
```

##### Property Value

[Sprite](../graphics/sprite.md)

-----

#### ScrollbarVisibility

The [ForcedVisibility](scrollbar.md#forcedvisibility) of the scrollbar.

```cs
public StardewUI.Layout.Visibility? ScrollbarVisibility { get; set; }
```

##### Property Value

[Nullable](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<[Visibility](../layout/visibility.md)>

-----

#### ScrollStep

The [ScrollStep](scrollcontainer.md#scrollstep) of the scrollable view. `(unofficial-mushymato)`

```cs
public float ScrollStep { get; set; }
```

##### Property Value

[Single](https://learn.microsoft.com/en-us/dotnet/api/system.single)

-----

### Methods

#### ContainerScrollIntoView(ViewChild, Vector2)

Convienence function that resets scrolling then calls [ScrollIntoView(IEnumerable&lt;ViewChild&gt;, Vector2)](../iview.md#scrollintoviewienumerableviewchild-vector2) on the [ScrollContainer](scrollcontainer.md) of this view, using a known [ViewChild](../viewchild.md) of [Content](scrollcontainer.md#content). This achieves effect of scrolling to a particular child outside of [FocusSearch(Vector2, Direction)](../iview.md#focussearchvector2-direction).

```cs
public bool ContainerScrollIntoView(StardewUI.ViewChild child, out Microsoft.Xna.Framework.Vector2 distance);
```

##### Parameters

**`child`** &nbsp; [ViewChild](../viewchild.md)  
Target child to scroll to.

**`distance`** &nbsp; [Vector2](https://docs.monogame.net/api/Microsoft.Xna.Framework.Vector2.html)  
Final scrolled distance

##### Returns

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)

-----

#### CreateView()

Creates and returns the root view.

```cs
protected override StardewUI.Widgets.ScrollContainer CreateView();
```

##### Returns

[ScrollContainer](scrollcontainer.md)

-----

#### OnWheel(WheelEventArgs)

Called when a wheel event is received within this view's bounds.

```cs
public override void OnWheel(StardewUI.Events.WheelEventArgs e);
```

##### Parameters

**`e`** &nbsp; [WheelEventArgs](../events/wheeleventargs.md)  
The event data.

-----

