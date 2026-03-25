---
title: ScreenReadableInterfaceConverter
description: Converts a general IScreenReadable to ScreenReadableData
search:
    boost: 0.002
---

<link rel="stylesheet" href="/StardewUI/stylesheets/reference.css" />

/// html | div.api-reference

# Class ScreenReadableInterfaceConverter

## Definition

<div class="api-definition" markdown>

Namespace: [StardewUI.Framework.Converters](index.md)  
Assembly: StardewUI.dll  

</div>

Converts a general IScreenReadable to [ScreenReadableData](../../modintegration/screenreadabledata.md)

```cs
public class ScreenReadableInterfaceConverter : 
    StardewUI.Framework.Converters.IValueConverter<StardewValley.Menus.IScreenReadable, StardewUI.ModIntegration.ScreenReadableData>, 
    StardewUI.Framework.Converters.IValueConverter
```

**Inheritance**  
[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) ⇦ ScreenReadableInterfaceConverter

**Implements**  
[IValueConverter](ivalueconverter-2.md)<IScreenReadable, [ScreenReadableData](../../modintegration/screenreadabledata.md)>, [IValueConverter](ivalueconverter.md)

## Members

### Constructors

 | Name | Description |
| --- | --- |
| [ScreenReadableInterfaceConverter()](#screenreadableinterfaceconverter) |  | 

### Methods

 | Name | Description |
| --- | --- |
| [Convert(IScreenReadable)](#convertiscreenreadable) | Converts a value from the source type to the destination type. | 

## Details

### Constructors

#### ScreenReadableInterfaceConverter()



```cs
public ScreenReadableInterfaceConverter();
```

-----

### Methods

#### Convert(IScreenReadable)

Converts a value from the source type to the destination type.

```cs
public StardewUI.ModIntegration.ScreenReadableData Convert(StardewValley.Menus.IScreenReadable value);
```

##### Parameters

**`value`** &nbsp; IScreenReadable  
The value to convert.

##### Returns

[ScreenReadableData](../../modintegration/screenreadabledata.md)

-----

