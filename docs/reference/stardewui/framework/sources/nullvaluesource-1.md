---
title: NullValueSource&lt;T&gt;
description: Value source that always provides a null/default value, and does not allow writing.
search:
    boost: 0.002
---

<link rel="stylesheet" href="/StardewUI/stylesheets/reference.css" />

/// html | div.api-reference

# Class NullValueSource&lt;T&gt;

## Definition

<div class="api-definition" markdown>

Namespace: [StardewUI.Framework.Sources](index.md)  
Assembly: StardewUI.dll  

</div>

Value source that always provides a null/default value, and does not allow writing.

```cs
public class NullValueSource<T> : StardewUI.Framework.Sources.IValueSource<T>, 
    StardewUI.Framework.Sources.IValueSource
```

### Type Parameters

**`T`**  
The return type of the context property.


**Inheritance**  
[Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) ⇦ NullValueSource&lt;T&gt;

**Implements**  
[IValueSource&lt;T&gt;](ivaluesource-1.md), [IValueSource](ivaluesource.md)

## Remarks

Can be used in place of a real [IValueSource&lt;T&gt;](ivaluesource-1.md) when no data is available, e.g. when a complex binding is attempted when a `null` value is at the root, and therefore the destination type cannot be determined.

## Members

### Fields

 | Name | Description |
| --- | --- |
| [Instance](#instance) | Immutable default instance of a [NullValueSource&lt;T&gt;](nullvaluesource-1.md). | 

### Properties

 | Name | Description |
| --- | --- |
| [CanRead](#canread) | Whether or not the source can be read from, i.e. if an attempt to **get** the [Value](ivaluesource.md#value) should succeed. | 
| [CanWrite](#canwrite) | Whether or not the source can be written back to, i.e. if an attempt to **set** the [Value](ivaluesource.md#value) should succeed. | 
| [DisplayName](#displayname) | Descriptive name for the property, used primarily for debug views and log/exception messages. | 
| [Value](#value) |  | 
| [ValueType](#valuetype) | The compile-time type of the value tracked by this source; the type parameter for [IValueSource&lt;T&gt;](ivaluesource-1.md). | 

### Methods

 | Name | Description |
| --- | --- |
| [Update(Boolean)](#updatebool) | Checks if the value needs updating, and if so, updates [Value](ivaluesource.md#value) to the latest. | 

## Details

### Fields

#### Instance

Immutable default instance of a [NullValueSource&lt;T&gt;](nullvaluesource-1.md).

```cs
public static readonly StardewUI.Framework.Sources.NullValueSource<T> Instance;
```

##### Field Value

[NullValueSource&lt;T&gt;](nullvaluesource-1.md)

-----

### Properties

#### CanRead

Whether or not the source can be read from, i.e. if an attempt to **get** the [Value](ivaluesource.md#value) should succeed.

```cs
public bool CanRead { get; }
```

##### Property Value

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)

-----

#### CanWrite

Whether or not the source can be written back to, i.e. if an attempt to **set** the [Value](ivaluesource.md#value) should succeed.

```cs
public bool CanWrite { get; }
```

##### Property Value

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)

-----

#### DisplayName

Descriptive name for the property, used primarily for debug views and log/exception messages.

```cs
public string DisplayName { get; }
```

##### Property Value

[string](https://learn.microsoft.com/en-us/dotnet/api/system.string)

-----

#### Value



```cs
public T Value { get; set; }
```

##### Property Value

`T`

-----

#### ValueType

The compile-time type of the value tracked by this source; the type parameter for [IValueSource&lt;T&gt;](ivaluesource-1.md).

```cs
public System.Type ValueType { get; }
```

##### Property Value

[Type](https://learn.microsoft.com/en-us/dotnet/api/system.type)

-----

### Methods

#### Update(bool)

Checks if the value needs updating, and if so, updates [Value](ivaluesource.md#value) to the latest.

```cs
public bool Update(bool force);
```

##### Parameters

**`force`** &nbsp; [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)  
If `true`, forces the source to update its value even if it isn't considered dirty. This should never be used in a regular binding, but can be useful in sources that are intended for occasional or one-shot use such as event handler arguments.

##### Returns

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)

  `true` if the [Value](ivaluesource.md#value) was updated; `false` if it already held the most recent value.

##### Remarks

This method is called every frame, for every binding, and providing a correct return value is essential in order to avoid slowdowns due to unnecessary rebinds.

-----

