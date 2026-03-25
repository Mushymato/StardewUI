---
title: LookupAnythingIntegration
description: Manages lookup anything integration
search:
    boost: 0.002
---

<link rel="stylesheet" href="/StardewUI/stylesheets/reference.css" />

/// html | div.api-reference

# Class LookupAnythingIntegration

## Definition

<div class="api-definition" markdown>

Namespace: [StardewUI.ModIntegration](index.md)  
Assembly: StardewUI.dll  

</div>

Manages lookup anything integration

```cs
public static class LookupAnythingIntegration
```

**Inheritance**  
[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) ⇦ LookupAnythingIntegration

## Members

### Properties

 | Name | Description |
| --- | --- |
| [IsLoaded](#isloaded) | Whether Lookup Anything is loaded | 

### Methods

 | Name | Description |
| --- | --- |
| [Initialize(IModHelper)](#initializeimodhelper) | Initialize lookup anything integration | 
| [SetSubject(ViewChild)](#setsubjectviewchild) | Find the final hovered subject in a view hover path, and set that to the top level view menu. | 

## Details

### Properties

#### IsLoaded

Whether Lookup Anything is loaded

```cs
public static bool IsLoaded { get; set; }
```

##### Property Value

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)

-----

### Methods

#### Initialize(IModHelper)

Initialize lookup anything integration

```cs
public static void Initialize(StardewModdingAPI.IModHelper helper);
```

##### Parameters

**`helper`** &nbsp; IModHelper

-----

#### SetSubject(ViewChild)

Find the final hovered subject in a view hover path, and set that to the top level view menu.

```cs
public static void SetSubject(StardewUI.ViewChild path);
```

##### Parameters

**`path`** &nbsp; [ViewChild](../viewchild.md)  
Sequence of all elements, and their relative positions, that the mouse coordinates are currently within.

-----

