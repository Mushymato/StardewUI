---
title: KeyboardSubscriberWithOwner
description: An implementation of IKeyboardSubscriber that forwards keys to the owning IKeyboardSubscriberOwnerView.
search:
    boost: 0.002
---

<link rel="stylesheet" href="/StardewUI/stylesheets/reference.css" />

/// html | div.api-reference

# Class KeyboardSubscriberWithOwner

## Definition

<div class="api-definition" markdown>

Namespace: [StardewUI.Input](index.md)  
Assembly: StardewUI.dll  

</div>

An implementation of IKeyboardSubscriber that forwards keys to the owning [IKeyboardSubscriberOwnerView](ikeyboardsubscriberownerview.md).

```cs
public class KeyboardSubscriberWithOwner : StardewUI.Input.ICaptureTarget, 
    StardewValley.IKeyboardSubscriber
```

**Inheritance**  
[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) ⇦ KeyboardSubscriberWithOwner

**Implements**  
[ICaptureTarget](icapturetarget.md), IKeyboardSubscriber

## Members

### Constructors

 | Name | Description |
| --- | --- |
| [KeyboardSubscriberWithOwner(IKeyboardSubscriberOwnerView, GameWindow)](#keyboardsubscriberwithownerikeyboardsubscriberownerview-gamewindow) | An implementation of IKeyboardSubscriber that forwards keys to the owning [IKeyboardSubscriberOwnerView](ikeyboardsubscriberownerview.md). | 

### Properties

 | Name | Description |
| --- | --- |
| [CapturingView](#capturingview) | The view that initiated the capturing. May be the same object as the [ICaptureTarget](icapturetarget.md), or may be the "owner" of a hidden TextBox or other IKeyboardSubscriber. | 
| [Selected](#selected) | Whether this subscriber is active. When this is changed to true, this subscriber is registered to keyboardDispatcher and key capturing begins. When this is changed to false, it is removed from keyboardDispatcher and key capturing ends. | 

### Methods

 | Name | Description |
| --- | --- |
| [RecieveCommandInput(Char)](#recievecommandinputchar) |  | 
| [RecieveSpecialInput(Keys)](#recievespecialinputkeys) |  | 
| [RecieveTextInput(Char)](#recievetextinputchar) |  | 
| [RecieveTextInput(string)](#recievetextinputstring) |  | 
| [ReleaseCapture()](#releasecapture) | Stops input capturing from this target. | 

## Details

### Constructors

#### KeyboardSubscriberWithOwner(IKeyboardSubscriberOwnerView, GameWindow)

An implementation of IKeyboardSubscriber that forwards keys to the owning [IKeyboardSubscriberOwnerView](ikeyboardsubscriberownerview.md).

```cs
public KeyboardSubscriberWithOwner(StardewUI.Input.IKeyboardSubscriberOwnerView owner, Microsoft.Xna.Framework.GameWindow window);
```

##### Parameters

**`owner`** &nbsp; [IKeyboardSubscriberOwnerView](ikeyboardsubscriberownerview.md)  
The view that owns this subscriber.

**`window`** &nbsp; [GameWindow](https://docs.monogame.net/api/Microsoft.Xna.Framework.GameWindow.html)

-----

### Properties

#### CapturingView

The view that initiated the capturing. May be the same object as the [ICaptureTarget](icapturetarget.md), or may be the "owner" of a hidden TextBox or other IKeyboardSubscriber.

```cs
public StardewUI.IView CapturingView { get; }
```

##### Property Value

[IView](../iview.md)

-----

#### Selected

Whether this subscriber is active. When this is changed to true, this subscriber is registered to keyboardDispatcher and key capturing begins. When this is changed to false, it is removed from keyboardDispatcher and key capturing ends.

```cs
public bool Selected { get; set; }
```

##### Property Value

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)

-----

### Methods

#### RecieveCommandInput(char)



```cs
public void RecieveCommandInput(char command);
```

##### Parameters

**`command`** &nbsp; [Char](https://learn.microsoft.com/en-us/dotnet/api/system.char)

-----

#### RecieveSpecialInput(Keys)



```cs
public void RecieveSpecialInput(Microsoft.Xna.Framework.Input.Keys key);
```

##### Parameters

**`key`** &nbsp; [Keys](https://docs.monogame.net/api/Microsoft.Xna.Framework.Input.Keys.html)

-----

#### RecieveTextInput(char)



```cs
public void RecieveTextInput(char inputChar);
```

##### Parameters

**`inputChar`** &nbsp; [Char](https://learn.microsoft.com/en-us/dotnet/api/system.char)

-----

#### RecieveTextInput(string)



```cs
public void RecieveTextInput(string text);
```

##### Parameters

**`text`** &nbsp; [string](https://learn.microsoft.com/en-us/dotnet/api/system.string)

-----

#### ReleaseCapture()

Stops input capturing from this target.

```cs
public void ReleaseCapture();
```

-----

