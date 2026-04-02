---
title: StardewAccessIntegration
description: Manages Stardew Access (shoaib.stardewaccess) integration
search:
    boost: 0.002
---

<link rel="stylesheet" href="/StardewUI/stylesheets/reference.css" />

/// html | div.api-reference

# Class StardewAccessIntegration

## Definition

<div class="api-definition" markdown>

Namespace: [StardewUI.ModIntegration](index.md)  
Assembly: StardewUI.dll  

</div>

Manages Stardew Access (shoaib.stardewaccess) integration

```cs
public static class StardewAccessIntegration
```

**Inheritance**  
[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) ⇦ StardewAccessIntegration

## Members

### Methods

 | Name | Description |
| --- | --- |
| [Initialize(IModHelper)](#initializeimodhelper) | Initialize lookup anything integration | 
| [MakeScreenReadDelegated(Func&lt;string, string&gt;, Int32)](#makescreenreaddelegatedfuncstring-string-int) | Make a [ScreenReadableData](screenreadabledata.md) with a particular text delegate. | 
| [MakeScreenReadTranslated(string, Func&lt;string, Object&gt;, Int32)](#makescreenreadtranslatedstring-funcstring-object-int) | Make a [ScreenReadableData](screenreadabledata.md) using translated text from Stardew Access | 
| [SayHoveredMenuElement(ViewChild)](#sayhoveredmenuelementviewchild) | Say the currently hovered menu element using [SayMenuElement(IScreenReadable, Boolean)](istardewaccessapi.md#saymenuelementiscreenreadable-bool) | 

## Details

### Methods

#### Initialize(IModHelper)

Initialize lookup anything integration

```cs
public static void Initialize(StardewModdingAPI.IModHelper helper);
```

##### Parameters

**`helper`** &nbsp; IModHelper

-----

#### MakeScreenReadDelegated(Func&lt;string, string&gt;, int)

Make a [ScreenReadableData](screenreadabledata.md) with a particular text delegate.

```cs
public static StardewUI.ModIntegration.ScreenReadableData MakeScreenReadDelegated(Func<string, string> textDelegate, int precedence);
```

##### Parameters

**`textDelegate`** &nbsp; [Func](https://learn.microsoft.com/en-us/dotnet/api/system.func-2)<[string](https://learn.microsoft.com/en-us/dotnet/api/system.string), [string](https://learn.microsoft.com/en-us/dotnet/api/system.string)>  
Text delegate used to modify the inner text

**`precedence`** &nbsp; [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)

##### Returns

[ScreenReadableData](screenreadabledata.md)

-----

#### MakeScreenReadTranslated(string, Func&lt;string, Object&gt;, int)

Make a [ScreenReadableData](screenreadabledata.md) using translated text from Stardew Access

```cs
public static StardewUI.ModIntegration.ScreenReadableData MakeScreenReadTranslated(string translationKey, Func<string, System.Object> getTokens, int precedence);
```

##### Parameters

**`translationKey`** &nbsp; [string](https://learn.microsoft.com/en-us/dotnet/api/system.string)  
Stardew Access translation key

**`getTokens`** &nbsp; [Func](https://learn.microsoft.com/en-us/dotnet/api/system.func-2)<[string](https://learn.microsoft.com/en-us/dotnet/api/system.string), [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object)>  
Delegate that takes a string and returns translation tokens

**`precedence`** &nbsp; [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)

##### Returns

[ScreenReadableData](screenreadabledata.md)

-----

#### SayHoveredMenuElement(ViewChild)

Say the currently hovered menu element using [SayMenuElement(IScreenReadable, Boolean)](istardewaccessapi.md#saymenuelementiscreenreadable-bool)

```cs
public static void SayHoveredMenuElement(StardewUI.ViewChild path);
```

##### Parameters

**`path`** &nbsp; [ViewChild](../viewchild.md)  
Sequence of all elements, and their relative positions, that the mouse coordinates are currently within.

-----

