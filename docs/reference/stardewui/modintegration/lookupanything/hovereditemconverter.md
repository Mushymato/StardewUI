---
title: HoveredItemConverter
description: Convert Item to LookupAnythingHoveredSubject
search:
    boost: 0.002
---

<link rel="stylesheet" href="/StardewUI/stylesheets/reference.css" />

/// html | div.api-reference

# Class HoveredItemConverter

## Definition

<div class="api-definition" markdown>

Namespace: [StardewUI.ModIntegration.LookupAnything](index.md)  
Assembly: StardewUI.dll  

</div>

Convert Item to [LookupAnythingHoveredSubject](lookupanythinghoveredsubject.md)

```cs
public class HoveredItemConverter : 
    StardewUI.Framework.Converters.IValueConverter<StardewValley.Item, StardewUI.ModIntegration.LookupAnything.LookupAnythingHoveredSubject>, 
    StardewUI.Framework.Converters.IValueConverter
```

**Inheritance**  
[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) ⇦ HoveredItemConverter

**Implements**  
[IValueConverter](../../framework/converters/ivalueconverter-2.md)<Item, [LookupAnythingHoveredSubject](lookupanythinghoveredsubject.md)>, [IValueConverter](../../framework/converters/ivalueconverter.md)

## Members

### Constructors

 | Name | Description |
| --- | --- |
| [HoveredItemConverter()](#hovereditemconverter) |  | 

### Methods

 | Name | Description |
| --- | --- |
| [Convert(Item)](#convertitem) | Converts a value from the source type to the destination type. | 

## Details

### Constructors

#### HoveredItemConverter()



```cs
public HoveredItemConverter();
```

-----

### Methods

#### Convert(Item)

Converts a value from the source type to the destination type.

```cs
public StardewUI.ModIntegration.LookupAnything.LookupAnythingHoveredSubject Convert(StardewValley.Item value);
```

##### Parameters

**`value`** &nbsp; Item  
The value to convert.

##### Returns

[LookupAnythingHoveredSubject](lookupanythinghoveredsubject.md)

-----

