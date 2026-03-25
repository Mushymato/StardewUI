---
title: LookupAnythingHoveredSubject
description: The lookup anything hovered subject to supply to ViewMenu
search:
    boost: 0.002
---

<link rel="stylesheet" href="/StardewUI/stylesheets/reference.css" />

/// html | div.api-reference

# Class LookupAnythingHoveredSubject

## Definition

<div class="api-definition" markdown>

Namespace: [StardewUI.ModIntegration](index.md)  
Assembly: StardewUI.dll  

</div>

The lookup anything hovered subject to supply to [ViewMenu](../viewmenu.md)

```cs
public record LookupAnythingHoveredSubject : 
    IEquatable<StardewUI.ModIntegration.LookupAnythingHoveredSubject>
```

**Inheritance**  
[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) ⇦ LookupAnythingHoveredSubject

**Implements**  
[IEquatable](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1)<[LookupAnythingHoveredSubject](lookupanythinghoveredsubject.md)>

## Members

### Constructors

 | Name | Description |
| --- | --- |
| [LookupAnythingHoveredSubject(Item, NPC)](#lookupanythinghoveredsubjectitem-npc) | The lookup anything hovered subject to supply to [ViewMenu](../viewmenu.md) | 

### Properties

 | Name | Description |
| --- | --- |
| [EqualityContract](#equalitycontract) |  | 
| [HoveredItem](#hovereditem) |  | 
| [HoveredNpc](#hoverednpc) |  | 

## Details

### Constructors

#### LookupAnythingHoveredSubject(Item, NPC)

The lookup anything hovered subject to supply to [ViewMenu](../viewmenu.md)

```cs
public LookupAnythingHoveredSubject(StardewValley.Item HoveredItem, StardewValley.NPC HoveredNpc);
```

##### Parameters

**`HoveredItem`** &nbsp; Item

**`HoveredNpc`** &nbsp; NPC

-----

### Properties

#### EqualityContract



```cs
protected System.Type EqualityContract { get; }
```

##### Property Value

[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type)

-----

#### HoveredItem



```cs
public StardewValley.Item HoveredItem { get; set; }
```

##### Property Value

Item

-----

#### HoveredNpc



```cs
public StardewValley.NPC HoveredNpc { get; set; }
```

##### Property Value

NPC

-----

