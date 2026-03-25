---
title: LookupAnythingHoveredItemConverter
description: Convert Item to LookupAnythingHoveredSubject
search:
    boost: 0.002
---

<link rel="stylesheet" href="/StardewUI/stylesheets/reference.css" />

/// html | div.api-reference

# Class LookupAnythingHoveredItemConverter

## Definition

<div class="api-definition" markdown>

Namespace: [StardewUI.Framework.Converters](index.md)  
Assembly: StardewUI.dll  

</div>

Convert Item to [LookupAnythingHoveredSubject](../../modintegration/lookupanythinghoveredsubject.md)

```cs
public class LookupAnythingHoveredItemConverter : 
    StardewUI.Framework.Converters.IValueConverter<StardewValley.Item, StardewUI.ModIntegration.LookupAnythingHoveredSubject>, 
    StardewUI.Framework.Converters.IValueConverter
```

**Inheritance**  
[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) ⇦ LookupAnythingHoveredItemConverter

**Implements**  
[IValueConverter](ivalueconverter-2.md)<Item, [LookupAnythingHoveredSubject](../../modintegration/lookupanythinghoveredsubject.md)>, [IValueConverter](ivalueconverter.md)

## Members

### Constructors

 | Name | Description |
| --- | --- |
| [LookupAnythingHoveredItemConverter()](#lookupanythinghovereditemconverter) |  | 

### Methods

 | Name | Description |
| --- | --- |
| [Convert(Item)](#convertitem) | Converts a value from the source type to the destination type. | 

## Details

### Constructors

#### LookupAnythingHoveredItemConverter()



```cs
public LookupAnythingHoveredItemConverter();
```

-----

### Methods

#### Convert(Item)

Converts a value from the source type to the destination type.

```cs
public StardewUI.ModIntegration.LookupAnythingHoveredSubject Convert(StardewValley.Item value);
```

##### Parameters

**`value`** &nbsp; Item  
The value to convert.

##### Returns

[LookupAnythingHoveredSubject](../../modintegration/lookupanythinghoveredsubject.md)

-----

