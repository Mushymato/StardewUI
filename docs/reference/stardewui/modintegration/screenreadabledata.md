---
title: ScreenReadableData
description: A screen readable bit of text. Although IScreenReadable is a vanilla interface, it does nothing by itself and will be used with screen reader mods.
search:
    boost: 0.002
---

<link rel="stylesheet" href="/StardewUI/stylesheets/reference.css" />

/// html | div.api-reference

# Class ScreenReadableData

## Definition

<div class="api-definition" markdown>

Namespace: [StardewUI.ModIntegration](index.md)  
Assembly: StardewUI.dll  

</div>

A screen readable bit of text. Although IScreenReadable is a vanilla interface, it does nothing by itself and will be used with screen reader mods.

```cs
[StardewUI.DuckType]
public class ScreenReadableData : StardewValley.Menus.IScreenReadable
```

**Inheritance**  
[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) ⇦ ScreenReadableData

**Implements**  
IScreenReadable

## Members

### Constructors

 | Name | Description |
| --- | --- |
| [ScreenReadableData()](#screenreadabledata) | A screen readable bit of text. Although IScreenReadable is a vanilla interface, it does nothing by itself and will be used with screen reader mods. | 

### Properties

 | Name | Description |
| --- | --- |
| [Precedence](#precedence) | How prioritized this screen reader element is. The lowest precedence element will be read, even if the hover path has more specific elements. Custom screen read fields should use negative values. while screen read fields set by the View should have value 0 or greater. | 
| [ScreenReaderDescription](#screenreaderdescription) | If set, a translated tooltip-like description for this component which can be displayed by screen readers, in addition to the ScreenReaderText. | 
| [ScreenReaderIgnore](#screenreaderignore) | Whether this is a purely visual component which should be ignored by screen readers. | 
| [ScreenReaderText](#screenreadertext) | If set, the translated text which represents this component for a screen reader. This may be the displayed text (for a text component), or an equivalent representation (e.g. "exit" for an 'X' button). | 
| [ScreenReaderTextDelegate](#screenreadertextdelegate) | A delegate used to modify | 

## Details

### Constructors

#### ScreenReadableData()

A screen readable bit of text. Although IScreenReadable is a vanilla interface, it does nothing by itself and will be used with screen reader mods.

```cs
public ScreenReadableData();
```

-----

### Properties

#### Precedence

How prioritized this screen reader element is. The lowest precedence element will be read, even if the hover path has more specific elements. Custom screen read fields should use negative values. while screen read fields set by the View should have value 0 or greater.

```cs
public int Precedence { get; set; }
```

##### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)

-----

#### ScreenReaderDescription

If set, a translated tooltip-like description for this component which can be displayed by screen readers, in addition to the ScreenReaderText.

```cs
public string ScreenReaderDescription { get; set; }
```

##### Property Value

[string](https://learn.microsoft.com/en-us/dotnet/api/system.string)

-----

#### ScreenReaderIgnore

Whether this is a purely visual component which should be ignored by screen readers.

```cs
public bool ScreenReaderIgnore { get; set; }
```

##### Property Value

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)

-----

#### ScreenReaderText

If set, the translated text which represents this component for a screen reader. This may be the displayed text (for a text component), or an equivalent representation (e.g. "exit" for an 'X' button).

```cs
public string ScreenReaderText { get; set; }
```

##### Property Value

[string](https://learn.microsoft.com/en-us/dotnet/api/system.string)

-----

#### ScreenReaderTextDelegate

A delegate used to modify

```cs
public Func<string, string> ScreenReaderTextDelegate { get; set; }
```

##### Property Value

[Func](https://learn.microsoft.com/en-us/dotnet/api/system.func-2)<[string](https://learn.microsoft.com/en-us/dotnet/api/system.string), [string](https://learn.microsoft.com/en-us/dotnet/api/system.string)>

-----

