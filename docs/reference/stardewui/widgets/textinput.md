---
title: TextInput
description: A text input field that allows typing from a physical or virtual keyboard.
search:
    boost: 0.002
---

<link rel="stylesheet" href="/StardewUI/stylesheets/reference.css" />

/// html | div.api-reference

# Class TextInput

!!! tip "Framework View"

    The tag name of this view is `<textinput>`.
## Definition

<div class="api-definition" markdown>

Namespace: [StardewUI.Widgets](index.md)  
Assembly: StardewUI.dll  

</div>

A text input field that allows typing from a physical or virtual keyboard.

```cs
[StardewUI.GenerateDescriptor]
public class TextInput : StardewUI.View, 
    StardewUI.Input.IKeyboardSubscriberOwnerView
```

**Inheritance**  
[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) ⇦ [View](../view.md) ⇦ TextInput

**Implements**  
[IKeyboardSubscriberOwnerView](../input/ikeyboardsubscriberownerview.md)

## Members

### Constructors

 | Name | Description |
| --- | --- |
| [TextInput()](#textinput) | Initializes a new [TextInput](textinput.md). | 

### Fields

 | Name | Description |
| --- | --- |
| [ParentScrollingBounds](../view.md#parentscrollingbounds) | The parent scrolling bounds propagated down from an ancestor [ScrollContainer](scrollcontainer.md).<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 

### Properties

 | Name | Description |
| --- | --- |
| [ActualBounds](../view.md#actualbounds)<br/>`actual-bounds` | The bounds of this view relative to the origin (0, 0).<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [Background](#background)<br/>`background` | For compatibility reasons Background is alias for Border. `(unofficial-mushymato)` | 
| [Border](#border)<br/>`border` | `(unofficial-mushymato)` | 
| [BorderSize](../view.md#bordersize)<br/>`border-size` | The layout size (not edge thickness) of the entire drawn area including the border, i.e. the [InnerSize](../view.md#innersize) plus any borders defined in [GetBorderThickness()](../view.md#getborderthickness). Does not include the [Margin](../view.md#margin).<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [BorderThickness](#borderthickness)<br/>`border-thickness` | The thickness of the border edges. | 
| [Caret](#caret)<br/>`caret` | Sprite to draw for the cursor showing the current text position. | 
| [CaretPosition](#caretposition)<br/>`caret-position` | The zero-based position of the caret within the text. | 
| [CaretSelectionSize](#caretselectionsize)<br/>`caret-selection-size` | Number of characters selected, stored as number of characters before or after the caret. `(unofficial-mushymato)` | 
| [CaretWidth](#caretwidth)<br/>`caret-width` | The width to draw the [Caret](textinput.md#caret), if different from the sprite's source width. | 
| [ClipOrigin](../view.md#cliporigin)<br/>`clip-origin` | Origin position for the [ClipSize](../iview.md#clipsize).<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [ClipSize](../view.md#clipsize)<br/>`clip-size` | Size of the clipping rectangle, outside which content will not be displayed.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [ContentBounds](../view.md#contentbounds)<br/>`content-bounds` | The true bounds of this view's content; i.e. [ActualBounds](../iview.md#actualbounds) excluding margins.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [ContentSize](../view.md#contentsize)<br/>`content-size` | The size of the view's content, which is drawn inside the padding. Subclasses set this in their [OnMeasure(Vector2)](../view.md#onmeasurevector2) method and padding, margins, etc. are handled automatically.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [Draggable](../view.md#draggable)<br/>`draggable` | Whether or not this view should fire drag events such as [DragStart](../view.md#dragstart) and [Drag](../view.md#drag).<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [Enabled](#enabled)<br/>`enabled` | Whether the input is enabled. | 
| [FloatingBounds](../view.md#floatingbounds)<br/>`floating-bounds` | Contains the bounds of all floating elements in this view tree, including the current view and all descendants.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [FloatingElements](../view.md#floatingelements)<br/>`floating-elements` | The floating elements to display relative to this view.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [Focusable](../view.md#focusable)<br/>`focusable` | Whether or not the view should be able to receive focus. Applies only to this specific view, not its children.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [FocusableTag](../view.md#focusabletag)<br/>`focusable-tag` | A string tag that identifies this view for use in [FocusOnTaggedView(string)](../framework/imenucontroller.md#focusontaggedviewstring).<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [Font](#font)<br/>`font` | The font with which to render text. Defaults to smallFont. | 
| [HandlesOpacity](../view.md#handlesopacity)<br/>`handles-opacity` | Whether the specific view type handles its own opacity.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [HoveredSubject](../view.md#hoveredsubject)<br/>`hovered-subject` | When Lookup Anything (Pathoschild.LookupAnything) is loaded, this Object or NPC subject is given to lookup anything for it's menu. `(unofficial-mushymato)`<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [InnerSize](../view.md#innersize)<br/>`inner-size` | The size allocated to the entire area inside the border, i.e. [ContentSize](../view.md#contentsize) plus any [Padding](../view.md#padding). Does not include border or [Margin](../view.md#margin).<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [IsFocusable](../view.md#isfocusable)<br/>`is-focusable` | Whether or not the view can receive controller focus, i.e. the stick/d-pad controlled cursor can move to this view. Not generally applicable for mouse controls.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [IsWithinScrollBounds](../view.md#iswithinscrollbounds)<br/>`is-within-scroll-bounds` | Whether this view is currently within the scrolling bounds, updated during [Measure(Vector2)](../iview.md#measurevector2).<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [ItemSpan](../view.md#itemspan)<br/>`item-span` | Item span that defines how many cells this item takes up when it's the child of a grid. Span only considers the primary orientation and cannot exceed one row/col When this is -1, take the remainder of the row/col. `(unofficial-mushymato)`<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [LastAvailableSize](../view.md#lastavailablesize)<br/>`last-available-size` | The most recent size used in a [Measure(Vector2)](../view.md#measurevector2) pass. Used for additional dirty checks.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [Layout](../view.md#layout)<br/>`layout` | Layout settings for this view; determines how its dimensions will be computed.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [LayoutOffset](../view.md#layoutoffset)<br/>`layout-offset` | Pixel offset of the view's content, which is applied to all pointer events and child queries.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [Margin](../view.md#margin)<br/>`margin` | Margins (whitespace outside border) for this view.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [MaxLength](#maxlength)<br/>`max-length` | The maximum number of characters allowed in this field. | 
| [Name](../view.md#name)<br/>`name` | Simple name for this view, used in log/debug output; does not affect behavior.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [Opacity](../view.md#opacity)<br/>`opacity` | Opacity (alpha level) of the view.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [OuterSize](../view.md#outersize)<br/>`outer-size` | The size of the entire area occupied by this view including margins, border and padding.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [Padding](../view.md#padding)<br/>`padding` | Padding (whitespace inside border) for this view.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [Placeholder](#placeholder)<br/>`placeholder` | Placeholder text to display when the [Text](textinput.md#text) is empty and input is not captured. | 
| [PlaceholderColor](#placeholdercolor)<br/>`placeholder-color` | Color of the [Placeholder](textinput.md#placeholder) text when displayed. | 
| [PointerEventsEnabled](../view.md#pointereventsenabled)<br/>`pointer-events-enabled` | Whether this view should receive pointer events like [Click](../view.md#click) or [Drag](../view.md#drag).<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [PointerStyle](../view.md#pointerstyle)<br/>`pointer-style` | Pointer style to use when this view is hovered.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [ScreenRead](../view.md#screenread)<br/>`screen-read` | When a screen reader mod (shoaib.stardewaccess) is loaded, this element will be announced by the screen reader using this value. `(unofficial-mushymato)`<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [ScrollWithChildren](../view.md#scrollwithchildren)<br/>`scroll-with-children` | If set to an axis, specifies that when any child of the view is scrolled into view (using [ScrollIntoView(IEnumerable&lt;ViewChild&gt;, Vector2)](../view.md#scrollintoviewienumerableviewchild-vector2)), then this entire view should be scrolled along with it.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [SelectedText](#selectedtext)<br/>`selected-text` | Read-only property for selected text, set via changes to [CaretSelectionSize](textinput.md#caretselectionsize). `(unofficial-mushymato)` | 
| [ShadowAlpha](#shadowalpha)<br/>`shadow-alpha` | Alpha value for the shadow. If set to the default of zero, no shadow will be drawn. | 
| [ShadowOffset](#shadowoffset)<br/>`shadow-offset` | Offset to draw the sprite shadow, which is a second copy of the [Background](frame.md#background) drawn entirely black. Shadows will not be visible unless [ShadowAlpha](frame.md#shadowalpha) is non-zero. | 
| [Tags](../view.md#tags)<br/>`tags` | The user-defined tags for this view.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [Text](#text)<br/>`text` | The text currently entered. | 
| [TextColor](#textcolor)<br/>`text-color` | Color of displayed text, as well as the [Caret](textinput.md#caret) tint color. | 
| [Tooltip](../view.md#tooltip)<br/>`tooltip` | Localized tooltip to display on hover, if any.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [Transform](../view.md#transform)<br/>`transform` | Local transformation to apply to this view, including any children and floating elements.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [TransformOrigin](../view.md#transformorigin)<br/>`transform-origin` | Relative origin position for any [Transform](../iview.md#transform) on this view.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [Visibility](../view.md#visibility)<br/>`visibility` | Visibility for this view.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [ZIndex](../view.md#zindex)<br/>`z-index` | Z order for this view within its direct parent. Higher indices draw later (on top).<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 

### Methods

 | Name | Description |
| --- | --- |
| [ContainsPoint(Vector2)](../view.md#containspointvector2) | Checks if a given point, relative to the view's origin, is within its bounds.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [Dispose()](../view.md#dispose) | <span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [Draw(ISpriteBatch)](../view.md#drawispritebatch) | Draws the content for this view.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [FindFocusableDescendant(Vector2, Direction)](../view.md#findfocusabledescendantvector2-direction) | Searches for a focusable child within this view that is reachable in the specified `direction`, and returns a result containing the view and search path if found.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [FocusSearch(Vector2, Direction)](../view.md#focussearchvector2-direction) | Finds the next focusable component in a given direction that does _not_ overlap with a current position.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [GetBorderThickness()](../view.md#getborderthickness) | Measures the thickness of each edge of the border, if the view has a border.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [GetChildAt(Vector2, Boolean, Boolean)](../view.md#getchildatvector2-bool-bool) | Finds the child at a given position.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [GetChildPosition(IView)](../view.md#getchildpositioniview) | Computes or retrieves the position of a given direct child.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [GetChildren(Boolean)](../view.md#getchildrenbool) | Gets the current children of this view.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [GetChildrenAt(Vector2)](../view.md#getchildrenatvector2) | Finds all children at a given position.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [GetDefaultFocusChild()](../view.md#getdefaultfocuschild) | Gets the direct child that should contain cursor focus when a menu or overlay containing this view is first opened.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [GetLocalChildren()](#getlocalchildren) | Gets the view's children with positions relative to the content area.<br><span class="muted" markdown>(Overrides [View](../view.md).[GetLocalChildren()](../view.md#getlocalchildren))</span> | 
| [GetLocalChildrenAt(Vector2)](../view.md#getlocalchildrenatvector2) | Searches for all views at a given position relative to the content area.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [HandleSpecialKey(Keys)](#handlespecialkeykeys) | Handle non-text entry key. | 
| [HasOutOfBoundsContent()](../view.md#hasoutofboundscontent) | Checks if the view has content or elements that are all or partially outside the [ActualBounds](../iview.md#actualbounds).<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [HasOwnContent()](../view.md#hasowncontent) | Checks if this view displays its own content, independent of any floating elements or children.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [InsertChar(Char)](#insertcharchar) | Accept new entered char | 
| [InsertString(string)](#insertstringstring) | Accept new entered string | 
| [IsContentDirty()](#iscontentdirty) | Checks whether or not the internal content/layout has changed.<br><span class="muted" markdown>(Overrides [View](../view.md).[IsContentDirty()](../view.md#iscontentdirty))</span> | 
| [IsDirty()](../view.md#isdirty) | Checks whether or not the view is dirty - i.e. requires a new layout with a full [Measure(Vector2)](../iview.md#measurevector2).<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [IsVisible(Vector2?)](../view.md#isvisiblevector2) | Checks if the view is effectively visible, i.e. if it has anything to draw.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [LogFocusSearch(string)](../view.md#logfocussearchstring) | Outputs a debug log entry with the current view type, name and specified message.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [Measure(Vector2)](../view.md#measurevector2) | Performs layout on this view, updating its [OuterSize](../iview.md#outersize), [ActualBounds](../iview.md#actualbounds) and [ContentBounds](../iview.md#contentbounds), and arranging any children in their respective positions.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [OnButtonPress(ButtonEventArgs)](../view.md#onbuttonpressbuttoneventargs) | Called when a button press is received while this view is in the focus path.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [OnButtonRepeat(ButtonEventArgs)](../view.md#onbuttonrepeatbuttoneventargs) | Called when a button press is first received, and at recurring intervals thereafter, for as long as the button is held and this view remains in the focus path.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [OnClick(ClickEventArgs)](#onclickclickeventargs) | Called when a click is received within this view's bounds.<br><span class="muted" markdown>(Overrides [View](../view.md).[OnClick(ClickEventArgs)](../view.md#onclickclickeventargs))</span> | 
| [OnDispose()](../view.md#ondispose) | Performs additional cleanup when [Dispose()](../view.md#dispose) is called.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [OnDrag(PointerEventArgs)](#ondragpointereventargs) | Called when the view is being dragged (mouse moved while left button held).<br><span class="muted" markdown>(Overrides [View](../view.md).[OnDrag(PointerEventArgs)](../view.md#ondragpointereventargs))</span> | 
| [OnDrawBorder(ISpriteBatch)](../view.md#ondrawborderispritebatch) | Draws the view's border, if it has one.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [OnDrawContent(ISpriteBatch)](#ondrawcontentispritebatch) | Draws the inner content of this view.<br><span class="muted" markdown>(Overrides [View](../view.md).[OnDrawContent(ISpriteBatch)](../view.md#ondrawcontentispritebatch))</span> | 
| [OnDrop(PointerEventArgs)](../view.md#ondroppointereventargs) | Called when the mouse button is released after at least one [OnDrag(PointerEventArgs)](../iview.md#ondragpointereventargs).<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [OnMeasure(Vector2)](#onmeasurevector2) | Performs the internal layout.<br><span class="muted" markdown>(Overrides [View](../view.md).[OnMeasure(Vector2)](../view.md#onmeasurevector2))</span> | 
| [OnPointerMove(PointerMoveEventArgs)](../view.md#onpointermovepointermoveeventargs) | Called when a pointer movement related to this view occurs.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [OnPropertyChanged(PropertyChangedEventArgs)](../view.md#onpropertychangedpropertychangedeventargs) | Raises the [PropertyChanged](../view.md#propertychanged) event.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [OnPropertyChanged(string)](../view.md#onpropertychangedstring) | Raises the [PropertyChanged](../view.md#propertychanged) event.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [OnUpdate(TimeSpan)](../view.md#onupdatetimespan) | Runs on every update tick.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [OnWheel(WheelEventArgs)](../view.md#onwheelwheeleventargs) | Called when a wheel event is received within this view's bounds.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [Release()](#release) | Function when the subscriber is released | 
| [ResetDirty()](../view.md#resetdirty) | Resets any dirty state associated with this view.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [ScrollIntoView(IEnumerable&lt;ViewChild&gt;, Vector2)](../view.md#scrollintoviewienumerableviewchild-vector2) | Attempts to scroll the specified target into view, including all of its ancestors, if not fully in view.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [ToString()](../view.md#tostring) | <span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [UpdateParentScrollingBounds(Bounds)](../view.md#updateparentscrollingboundsbounds) | Propagate new scrolling bounds to this view and it's children<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 

### Events

 | Name | Description |
| --- | --- |
| [ButtonPress](../view.md#buttonpress) | Event raised when any button on any input device is pressed.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [ButtonRepeat](../view.md#buttonrepeat) | Event raised when a button is being held while the view is in focus, and has been held long enough since the initial [ButtonPress](../view.md#buttonpress) or the previous `ButtonRepeat` to trigger a repeated press.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [Click](../view.md#click) | Event raised when the view receives a click.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [Drag](../view.md#drag) | Event raised when the view is being dragged using the mouse.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [DragEnd](../view.md#dragend) | Event raised when mouse dragging is stopped, i.e. when the button is released. Always raised after the last [Drag](../view.md#drag), and only once per drag operation.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [DragStart](../view.md#dragstart) | Event raised when mouse dragging is first activated. Always raised before the first [Drag](../view.md#drag), and only once per drag operation.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [LeftClick](../view.md#leftclick) | Event raised when the view receives a click initiated from the left mouse button, or the controller's action button (A).<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [PointerEnter](../view.md#pointerenter) | Event raised when the pointer enters the view.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [PointerLeave](../view.md#pointerleave) | Event raised when the pointer exits the view.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [PointerMove](../view.md#pointermove) | Event raised when the pointer moves within the view.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [PropertyChanged](../view.md#propertychanged) | <span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [RightClick](../view.md#rightclick) | Event raised when the view receives a click initiated from the right mouse button, or the controller's tool-use button (X).<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 
| [TextChanged](#textchanged) | Event raised when the [Text](textinput.md#text) changes. | 
| [Wheel](../view.md#wheel) | Event raised when the scroll wheel moves.<br><span class="muted" markdown>(Inherited from [View](../view.md))</span> | 

## Details

### Constructors

#### TextInput()

Initializes a new [TextInput](textinput.md).

```cs
public TextInput();
```

-----

### Properties

#### Background

For compatibility reasons Background is alias for Border. `(unofficial-mushymato)`

```cs
public StardewUI.Graphics.Sprite Background { get; set; }
```

##### Property Value

[Sprite](../graphics/sprite.md)

-----

#### Border

`(unofficial-mushymato)`

```cs
public StardewUI.Graphics.Sprite Border { get; set; }
```

##### Property Value

[Sprite](../graphics/sprite.md)

-----

#### BorderThickness

The thickness of the border edges.

```cs
public StardewUI.Layout.Edges BorderThickness { get; set; }
```

##### Property Value

[Edges](../layout/edges.md)

##### Remarks

This property has no effect on the appearance of the [Border](frame.md#border), but affects how content is positioned inside the border. It is often correct to set it to the same value as the [FixedEdges](../graphics/sprite.md#fixededges) of the [Border](frame.md#border) sprite, but the values are considered independent.

-----

#### Caret

Sprite to draw for the cursor showing the current text position.

```cs
public StardewUI.Graphics.Sprite Caret { get; set; }
```

##### Property Value

[Sprite](../graphics/sprite.md)

-----

#### CaretPosition

The zero-based position of the caret within the text.

```cs
public int CaretPosition { get; set; }
```

##### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)

##### Remarks

This value is the string position; e.g. if the [Text](textinput.md#text) has a length of 5, and the current caret position is 2, then the caret is between the 2nd and 3rd characters. The value cannot be greater than the length of the current text.

-----

#### CaretSelectionSize

Number of characters selected, stored as number of characters before or after the caret. `(unofficial-mushymato)`

```cs
public int CaretSelectionSize { get; set; }
```

##### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)

##### Remarks

This value does participate in `TextBeforeCursor` or `TextAfterCursor` adding up to equal total text length. When positive, it is always smaller than `TextAfterCursor` length. When negative, it's absolute value is always smaller `TextBeforeCursor` length.

-----

#### CaretWidth

The width to draw the [Caret](textinput.md#caret), if different from the sprite's source width.

```cs
public float? CaretWidth { get; set; }
```

##### Property Value

[Nullable](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<[Single](https://learn.microsoft.com/en-us/dotnet/api/system.single)>

-----

#### Enabled

Whether the input is enabled.

```cs
public bool Enabled { get; set; }
```

##### Property Value

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)

##### Remarks

Disabled text inputs have a darkened appearance and do not accept captures or text entry.

-----

#### Font

The font with which to render text. Defaults to smallFont.

```cs
public Microsoft.Xna.Framework.Graphics.SpriteFont Font { get; set; }
```

##### Property Value

[SpriteFont](https://docs.monogame.net/api/Microsoft.Xna.Framework.Graphics.SpriteFont.html)

-----

#### MaxLength

The maximum number of characters allowed in this field.

```cs
public int MaxLength { get; set; }
```

##### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)

##### Remarks

The default value is `0` which does not impose any limit.

-----

#### Placeholder

Placeholder text to display when the [Text](textinput.md#text) is empty and input is not captured.

```cs
public string Placeholder { get; set; }
```

##### Property Value

[string](https://learn.microsoft.com/en-us/dotnet/api/system.string)

-----

#### PlaceholderColor

Color of the [Placeholder](textinput.md#placeholder) text when displayed.

```cs
public Microsoft.Xna.Framework.Color PlaceholderColor { get; set; }
```

##### Property Value

[Color](https://docs.monogame.net/api/Microsoft.Xna.Framework.Color.html)

-----

#### SelectedText

Read-only property for selected text, set via changes to [CaretSelectionSize](textinput.md#caretselectionsize). `(unofficial-mushymato)`

```cs
public string SelectedText { get; private set; }
```

##### Property Value

[string](https://learn.microsoft.com/en-us/dotnet/api/system.string)

-----

#### ShadowAlpha

Alpha value for the shadow. If set to the default of zero, no shadow will be drawn.

```cs
public float ShadowAlpha { get; set; }
```

##### Property Value

[Single](https://learn.microsoft.com/en-us/dotnet/api/system.single)

-----

#### ShadowOffset

Offset to draw the sprite shadow, which is a second copy of the [Background](frame.md#background) drawn entirely black. Shadows will not be visible unless [ShadowAlpha](frame.md#shadowalpha) is non-zero.

```cs
public Microsoft.Xna.Framework.Vector2 ShadowOffset { get; set; }
```

##### Property Value

[Vector2](https://docs.monogame.net/api/Microsoft.Xna.Framework.Vector2.html)

-----

#### Text

The text currently entered.

```cs
public string Text { get; set; }
```

##### Property Value

[string](https://learn.microsoft.com/en-us/dotnet/api/system.string)

##### Remarks

Setting this to a new value will reset the caret position to the end of the text.

-----

#### TextColor

Color of displayed text, as well as the [Caret](textinput.md#caret) tint color.

```cs
public Microsoft.Xna.Framework.Color TextColor { get; set; }
```

##### Property Value

[Color](https://docs.monogame.net/api/Microsoft.Xna.Framework.Color.html)

-----

### Methods

#### GetLocalChildren()

Gets the view's children with positions relative to the content area.

```cs
protected override System.Collections.Generic.IEnumerable<StardewUI.ViewChild> GetLocalChildren();
```

##### Returns

[IEnumerable](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<[ViewChild](../viewchild.md)>

##### Remarks

This has the same signature as [GetChildren(Boolean)](../view.md#getchildrenbool) but assumes that coordinates are in the same space as those used in [OnDrawContent(ISpriteBatch)](../view.md#ondrawcontentispritebatch), i.e. not accounting for margin/border/padding. These coordinates are automatically adjusted in the [GetChildren(Boolean)](../view.md#getchildrenbool) to be relative to the entire view. 

 The default implementation returns an empty sequence. Composite views must override this method in order for user interactions to behave correctly.

-----

#### HandleSpecialKey(Keys)

Handle non-text entry key.

```cs
public void HandleSpecialKey(Microsoft.Xna.Framework.Input.Keys key);
```

##### Parameters

**`key`** &nbsp; [Keys](https://docs.monogame.net/api/Microsoft.Xna.Framework.Input.Keys.html)

-----

#### InsertChar(char)

Accept new entered char

```cs
public void InsertChar(char c);
```

##### Parameters

**`c`** &nbsp; [Char](https://learn.microsoft.com/en-us/dotnet/api/system.char)

-----

#### InsertString(string)

Accept new entered string

```cs
public void InsertString(string text);
```

##### Parameters

**`text`** &nbsp; [string](https://learn.microsoft.com/en-us/dotnet/api/system.string)

-----

#### IsContentDirty()

Checks whether or not the internal content/layout has changed.

```cs
protected override bool IsContentDirty();
```

##### Returns

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)

  `true` if content has changed; otherwise `false`.

##### Remarks

The base implementation of [IsDirty()](../view.md#isdirty) only checks if the base layout attributes have changed, i.e. [Layout](../view.md#layout), [Margin](../view.md#margin), [Padding](../view.md#padding), etc. It does not know about content/data in any subclasses; those that accept content parameters (like text) will typically use [DirtyTracker&lt;T&gt;](../layout/dirtytracker-1.md) to hold that content and should implement this method to check their [IsDirty](../layout/dirtytracker-1.md#isdirty) states.

-----

#### OnClick(ClickEventArgs)

Called when a click is received within this view's bounds.

```cs
public override void OnClick(StardewUI.Events.ClickEventArgs e);
```

##### Parameters

**`e`** &nbsp; [ClickEventArgs](../events/clickeventargs.md)  
The event data.

-----

#### OnDrag(PointerEventArgs)

Called when the view is being dragged (mouse moved while left button held).

```cs
public override void OnDrag(StardewUI.Events.PointerEventArgs e);
```

##### Parameters

**`e`** &nbsp; [PointerEventArgs](../events/pointereventargs.md)  
The event data.

-----

#### OnDrawContent(ISpriteBatch)

Draws the inner content of this view.

```cs
protected override void OnDrawContent(StardewUI.Graphics.ISpriteBatch b);
```

##### Parameters

**`b`** &nbsp; [ISpriteBatch](../graphics/ispritebatch.md)  
Sprite batch to hold the drawing output.

##### Remarks

This is called from [Draw(ISpriteBatch)](../view.md#drawispritebatch) after applying both [Margin](../view.md#margin) and [Padding](../view.md#padding).

-----

#### OnMeasure(Vector2)

Performs the internal layout.

```cs
protected override void OnMeasure(Microsoft.Xna.Framework.Vector2 availableSize);
```

##### Parameters

**`availableSize`** &nbsp; [Vector2](https://docs.monogame.net/api/Microsoft.Xna.Framework.Vector2.html)  
Size available in the container, after applying padding, margin and borders.

##### Remarks

This is called from [Measure(Vector2)](../view.md#measurevector2) only when the layout is dirty (layout parameters or content changed) and a new layout is actually required. Subclasses must implement this and set [ContentSize](../view.md#contentsize) once layout is complete. Typically, [Resolve(Vector2, Func&lt;Vector2&gt;)](../layout/layoutparameters.md#resolvevector2-funcvector2) should be used in order to ensure that the original [LayoutParameters](../layout/layoutparameters.md) are respected (e.g. if the actual content size is smaller than the configured size). 

 The `availableSize` provided to the method is pre-adjusted for [Margin](../view.md#margin), [Padding](../view.md#padding), and any border determined by [GetBorderThickness()](../view.md#getborderthickness).

-----

#### Release()

Function when the subscriber is released

```cs
public void Release();
```

-----

### Events

#### TextChanged

Event raised when the [Text](textinput.md#text) changes.

```cs
public event EventHandler<System.EventArgs>? TextChanged;
```

##### Event Type

[EventHandler](https://learn.microsoft.com/en-us/dotnet/api/system.eventhandler-1)<[EventArgs](https://learn.microsoft.com/en-us/dotnet/api/system.eventargs)>

-----

