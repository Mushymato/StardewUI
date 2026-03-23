---
title: HoveredSObjectConverter
description: Convert Object to LookupAnythingHoveredSubject
search:
    boost: 0.002
---

<link rel="stylesheet" href="/StardewUI/stylesheets/reference.css" />

/// html | div.api-reference

# Class HoveredSObjectConverter

## Definition

<div class="api-definition" markdown>

Namespace: [StardewUI.ModIntegration.LookupAnything](index.md)  
Assembly: StardewUI.dll  

</div>

Convert Object to [LookupAnythingHoveredSubject](lookupanythinghoveredsubject.md)

```cs
public class HoveredSObjectConverter : 
    StardewUI.Framework.Converters.IValueConverter<StardewValley.Object, StardewUI.ModIntegration.LookupAnything.LookupAnythingHoveredSubject>, 
    StardewUI.Framework.Converters.IValueConverter
```

**Inheritance**  
[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) ⇦ HoveredSObjectConverter

**Implements**  
[IValueConverter](../../framework/converters/ivalueconverter-2.md)<Object, [LookupAnythingHoveredSubject](lookupanythinghoveredsubject.md)>, [IValueConverter](../../framework/converters/ivalueconverter.md)

## Members

### Constructors

 | Name | Description |
| --- | --- |
| [HoveredSObjectConverter()](#hoveredsobjectconverter) |  | 

### Methods

 | Name | Description |
| --- | --- |
| [Convert(Object)](#convertobject) | Converts a value from the source type to the destination type. | 

## Details

### Constructors

#### HoveredSObjectConverter()



```cs
public HoveredSObjectConverter();
```

-----

### Methods

#### Convert(Object)

Converts a value from the source type to the destination type.

```cs
public StardewUI.ModIntegration.LookupAnything.LookupAnythingHoveredSubject Convert(StardewValley.Object value);
```

##### Parameters

**`value`** &nbsp; Object  
The value to convert.

##### Returns

[LookupAnythingHoveredSubject](lookupanythinghoveredsubject.md)

-----

