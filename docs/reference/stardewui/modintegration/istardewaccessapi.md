---
title: IStardewAccessApi
description: stardew access API
search:
    boost: 0.002
---

<link rel="stylesheet" href="/StardewUI/stylesheets/reference.css" />

/// html | div.api-reference

# Interface IStardewAccessApi

## Definition

<div class="api-definition" markdown>

Namespace: [StardewUI.ModIntegration](index.md)  
Assembly: StardewUI.dll  

</div>

stardew access API

```cs
public interface IStardewAccessApi
```

## Members

### Methods

 | Name | Description |
| --- | --- |
| [SayMenuElement(IScreenReadable, Boolean)](#saymenuelementiscreenreadable-bool) | Speaks the content of the given element while using the menu query to prevent speaking multiple times in the menu. | 
| [Translate(string, Object, string, Boolean)](#translatestring-object-string-bool) | Translate some text using Stardew Access translations | 

## Details

### Methods

#### SayMenuElement(IScreenReadable, bool)

Speaks the content of the given element while using the menu query to prevent speaking multiple times in the menu.

```cs
bool SayMenuElement(StardewValley.Menus.IScreenReadable element, bool interrupt);
```

##### Parameters

**`element`** &nbsp; IScreenReadable  
The element to be spoken.

**`interrupt`** &nbsp; [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)  
Whether to skip the currently speaking text or not.

##### Returns

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)

  true if the element was spoken otherwise false.

-----

#### Translate(string, Object, string, bool)

Translate some text using Stardew Access translations

```cs
string Translate(string translationKey, System.Object tokens, string translationCategory, bool disableWarning);
```

##### Parameters

**`translationKey`** &nbsp; [string](https://learn.microsoft.com/en-us/dotnet/api/system.string)

**`tokens`** &nbsp; [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object)

**`translationCategory`** &nbsp; [string](https://learn.microsoft.com/en-us/dotnet/api/system.string)

**`disableWarning`** &nbsp; [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)

##### Returns

[string](https://learn.microsoft.com/en-us/dotnet/api/system.string)

-----

