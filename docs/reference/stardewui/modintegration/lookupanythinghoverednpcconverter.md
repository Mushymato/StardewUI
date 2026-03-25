---
title: LookupAnythingHoveredNpcConverter
description: Convert NPC to LookupAnythingHoveredSubject
search:
    boost: 0.002
---

<link rel="stylesheet" href="/StardewUI/stylesheets/reference.css" />

/// html | div.api-reference

# Class LookupAnythingHoveredNpcConverter

## Definition

<div class="api-definition" markdown>

Namespace: [StardewUI.ModIntegration](index.md)  
Assembly: StardewUI.dll  

</div>

Convert NPC to [LookupAnythingHoveredSubject](lookupanythinghoveredsubject.md)

```cs
public class LookupAnythingHoveredNpcConverter : 
    StardewUI.Framework.Converters.IValueConverter<StardewValley.NPC, StardewUI.ModIntegration.LookupAnythingHoveredSubject>, 
    StardewUI.Framework.Converters.IValueConverter
```

**Inheritance**  
[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) ⇦ LookupAnythingHoveredNpcConverter

**Implements**  
[IValueConverter](../framework/converters/ivalueconverter-2.md)<NPC, [LookupAnythingHoveredSubject](lookupanythinghoveredsubject.md)>, [IValueConverter](../framework/converters/ivalueconverter.md)

## Members

### Constructors

 | Name | Description |
| --- | --- |
| [LookupAnythingHoveredNpcConverter()](#lookupanythinghoverednpcconverter) |  | 

### Methods

 | Name | Description |
| --- | --- |
| [Convert(NPC)](#convertnpc) | Converts a value from the source type to the destination type. | 

## Details

### Constructors

#### LookupAnythingHoveredNpcConverter()



```cs
public LookupAnythingHoveredNpcConverter();
```

-----

### Methods

#### Convert(NPC)

Converts a value from the source type to the destination type.

```cs
public StardewUI.ModIntegration.LookupAnythingHoveredSubject Convert(StardewValley.NPC value);
```

##### Parameters

**`value`** &nbsp; NPC  
The value to convert.

##### Returns

[LookupAnythingHoveredSubject](lookupanythinghoveredsubject.md)

-----

