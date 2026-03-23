---
title: HoveredCharacterConverter
description: Convert Object to LookupAnythingHoveredSubject
search:
    boost: 0.002
---

<link rel="stylesheet" href="/StardewUI/stylesheets/reference.css" />

/// html | div.api-reference

# Class HoveredCharacterConverter

## Definition

<div class="api-definition" markdown>

Namespace: [StardewUI.ModIntegration.LookupAnything](index.md)  
Assembly: StardewUI.dll  

</div>

Convert Object to [LookupAnythingHoveredSubject](lookupanythinghoveredsubject.md)

```cs
public class HoveredCharacterConverter : 
    StardewUI.Framework.Converters.IValueConverter<StardewValley.NPC, StardewUI.ModIntegration.LookupAnything.LookupAnythingHoveredSubject>, 
    StardewUI.Framework.Converters.IValueConverter
```

**Inheritance**  
[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) ⇦ HoveredCharacterConverter

**Implements**  
[IValueConverter](../../framework/converters/ivalueconverter-2.md)<NPC, [LookupAnythingHoveredSubject](lookupanythinghoveredsubject.md)>, [IValueConverter](../../framework/converters/ivalueconverter.md)

## Members

### Constructors

 | Name | Description |
| --- | --- |
| [HoveredCharacterConverter()](#hoveredcharacterconverter) |  | 

### Methods

 | Name | Description |
| --- | --- |
| [Convert(NPC)](#convertnpc) | Converts a value from the source type to the destination type. | 

## Details

### Constructors

#### HoveredCharacterConverter()



```cs
public HoveredCharacterConverter();
```

-----

### Methods

#### Convert(NPC)

Converts a value from the source type to the destination type.

```cs
public StardewUI.ModIntegration.LookupAnything.LookupAnythingHoveredSubject Convert(StardewValley.NPC value);
```

##### Parameters

**`value`** &nbsp; NPC  
The value to convert.

##### Returns

[LookupAnythingHoveredSubject](lookupanythinghoveredsubject.md)

-----

