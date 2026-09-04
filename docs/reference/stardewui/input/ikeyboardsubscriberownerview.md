---
title: IKeyboardSubscriberOwnerView
description: Denotes a view that can be the owner of a KeyboardSubscriberWithOwner.
search:
    boost: 0.002
---

<link rel="stylesheet" href="/StardewUI/stylesheets/reference.css" />

/// html | div.api-reference

# Interface IKeyboardSubscriberOwnerView

## Definition

<div class="api-definition" markdown>

Namespace: [StardewUI.Input](index.md)  
Assembly: StardewUI.dll  

</div>

Denotes a view that can be the owner of a [KeyboardSubscriberWithOwner](keyboardsubscriberwithowner.md).

```cs
public interface IKeyboardSubscriberOwnerView : StardewUI.IView, 
    System.IDisposable, System.ComponentModel.INotifyPropertyChanged
```

**Implements**  
[IView](../iview.md), [IDisposable](https://learn.microsoft.com/en-us/dotnet/api/system.idisposable), [INotifyPropertyChanged](https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.inotifypropertychanged)

## Members

### Methods

 | Name | Description |
| --- | --- |
| [HandleSpecialKey(Keys)](#handlespecialkeykeys) | Handle non-text entry key. | 
| [InsertChar(Char)](#insertcharchar) | Accept new entered char | 
| [InsertString(string)](#insertstringstring) | Accept new entered string | 
| [Release()](#release) | Function when the subscriber is released | 

## Details

### Methods

#### HandleSpecialKey(Keys)

Handle non-text entry key.

```cs
void HandleSpecialKey(Microsoft.Xna.Framework.Input.Keys keyCode);
```

##### Parameters

**`keyCode`** &nbsp; [Keys](https://docs.monogame.net/api/Microsoft.Xna.Framework.Input.Keys.html)

-----

#### InsertChar(char)

Accept new entered char

```cs
void InsertChar(char inputChar);
```

##### Parameters

**`inputChar`** &nbsp; [Char](https://learn.microsoft.com/en-us/dotnet/api/system.char)

-----

#### InsertString(string)

Accept new entered string

```cs
void InsertString(string text);
```

##### Parameters

**`text`** &nbsp; [string](https://learn.microsoft.com/en-us/dotnet/api/system.string)

-----

#### Release()

Function when the subscriber is released

```cs
void Release();
```

-----

