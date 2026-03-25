---
title: ScreenReadableStringConverter
description: Converts a string to ScreenReadableData
search:
    boost: 0.002
---

<link rel="stylesheet" href="/StardewUI/stylesheets/reference.css" />

/// html | div.api-reference

# Class ScreenReadableStringConverter

## Definition

<div class="api-definition" markdown>

Namespace: [StardewUI.ModIntegration](index.md)  
Assembly: StardewUI.dll  

</div>

Converts a string to [ScreenReadableData](screenreadabledata.md)

```cs
public class ScreenReadableStringConverter : 
    StardewUI.Framework.Converters.IValueConverter<string, StardewUI.ModIntegration.ScreenReadableData>, 
    StardewUI.Framework.Converters.IValueConverter
```

**Inheritance**  
[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) ⇦ ScreenReadableStringConverter

**Implements**  
[IValueConverter](../framework/converters/ivalueconverter-2.md)<[string](https://learn.microsoft.com/en-us/dotnet/api/system.string), [ScreenReadableData](screenreadabledata.md)>, [IValueConverter](../framework/converters/ivalueconverter.md)

## Members

### Constructors

 | Name | Description |
| --- | --- |
| [ScreenReadableStringConverter()](#screenreadablestringconverter) |  | 

### Methods

 | Name | Description |
| --- | --- |
| [Convert(string)](#convertstring) | Converts a value from the source type to the destination type. | 

## Details

### Constructors

#### ScreenReadableStringConverter()



```cs
public ScreenReadableStringConverter();
```

-----

### Methods

#### Convert(string)

Converts a value from the source type to the destination type.

```cs
public StardewUI.ModIntegration.ScreenReadableData Convert(string value);
```

##### Parameters

**`value`** &nbsp; [string](https://learn.microsoft.com/en-us/dotnet/api/system.string)  
The value to convert.

##### Returns

[ScreenReadableData](screenreadabledata.md)

-----

