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
| [MakeScreenRead(string, Int32)](#makescreenreadstring-int) | Construct a [ScreenReadableData](screenreadabledata.md), mark as automatic | 
| [MakeScreenReadTranslated(string, Object, Int32)](#makescreenreadtranslatedstring-object-int) | Construct a [ScreenReadableData](screenreadabledata.md) using translated text from Stardew Access | 
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

#### MakeScreenRead(string, int)

Construct a [ScreenReadableData](screenreadabledata.md), mark as automatic

```cs
public static StardewUI.ModIntegration.ScreenReadableData MakeScreenRead(string text, int precedence);
```

##### Parameters

**`text`** &nbsp; [string](https://learn.microsoft.com/en-us/dotnet/api/system.string)  
Screen read text

**`precedence`** &nbsp; [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)  
Precedence value

##### Returns

[ScreenReadableData](screenreadabledata.md)

-----

#### MakeScreenReadTranslated(string, Object, int)

Construct a [ScreenReadableData](screenreadabledata.md) using translated text from Stardew Access

```cs
public static StardewUI.ModIntegration.ScreenReadableData MakeScreenReadTranslated(string translationKey, System.Object tokens, int precedence);
```

##### Parameters

**`translationKey`** &nbsp; [string](https://learn.microsoft.com/en-us/dotnet/api/system.string)  
Stardew Access translation key

**`tokens`** &nbsp; [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object)  
Stardew Access translation tokens

**`precedence`** &nbsp; [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)  
Precedence value

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

