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
| [MakeScreenReadDelegated(Func&lt;string, string&gt;)](#makescreenreaddelegatedfuncstring-string) | Make a [ScreenReadableData](screenreadabledata.md) with a particular text delegate. | 
| [MakeScreenReadTranslated(string, Func&lt;string, Object&gt;)](#makescreenreadtranslatedstring-funcstring-object) | Make a [ScreenReadableData](screenreadabledata.md) using translated text from Stardew Access | 
| [SayHoveredMenuElement(ViewChild)](#sayhoveredmenuelementviewchild) | Say the currently hovered menu element using [SayMenuElement(IScreenReadable, Boolean)](istardewaccessapi.md#saymenuelementiscreenreadable-bool) | 
| [TrySetScreenReadText(ScreenReadableData, string, Int32)](#trysetscreenreadtextscreenreadabledata-string-int) | Set the [ScreenReaderText](screenreadabledata.md#screenreadertext) on a [ScreenReadableData](screenreadabledata.md) to the new text. Creating the [ScreenReadableData](screenreadabledata.md) if needed. | 

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

#### MakeScreenReadDelegated(Func&lt;string, string&gt;)

Make a [ScreenReadableData](screenreadabledata.md) with a particular text delegate.

```cs
public static StardewUI.ModIntegration.ScreenReadableData MakeScreenReadDelegated(Func<string, string> textDelegate);
```

##### Parameters

**`textDelegate`** &nbsp; [Func](https://learn.microsoft.com/en-us/dotnet/api/system.func-2)<[string](https://learn.microsoft.com/en-us/dotnet/api/system.string), [string](https://learn.microsoft.com/en-us/dotnet/api/system.string)>  
Text delegate used to modify the inner text

##### Returns

[ScreenReadableData](screenreadabledata.md)

-----

#### MakeScreenReadTranslated(string, Func&lt;string, Object&gt;)

Make a [ScreenReadableData](screenreadabledata.md) using translated text from Stardew Access

```cs
public static StardewUI.ModIntegration.ScreenReadableData MakeScreenReadTranslated(string translationKey, Func<string, System.Object> getTokens);
```

##### Parameters

**`translationKey`** &nbsp; [string](https://learn.microsoft.com/en-us/dotnet/api/system.string)  
Stardew Access translation key

**`getTokens`** &nbsp; [Func](https://learn.microsoft.com/en-us/dotnet/api/system.func-2)<[string](https://learn.microsoft.com/en-us/dotnet/api/system.string), [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object)>  
Delegate that takes a string and returns translation tokens

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

#### TrySetScreenReadText(ScreenReadableData, string, int)

Set the [ScreenReaderText](screenreadabledata.md#screenreadertext) on a [ScreenReadableData](screenreadabledata.md) to the new text. Creating the [ScreenReadableData](screenreadabledata.md) if needed.

```cs
public static StardewUI.ModIntegration.ScreenReadableData TrySetScreenReadText(StardewUI.ModIntegration.ScreenReadableData existing, string text, int precedence);
```

##### Parameters

**`existing`** &nbsp; [ScreenReadableData](screenreadabledata.md)  
Pre-existing instance

**`text`** &nbsp; [string](https://learn.microsoft.com/en-us/dotnet/api/system.string)  
Screen read text

**`precedence`** &nbsp; [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)  
Precedence value

##### Returns

[ScreenReadableData](screenreadabledata.md)

-----

