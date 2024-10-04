﻿namespace StardewUI.Framework.Dom;

/// <summary>
/// Element in a StarML document, including the tag and all enclosed attributes.
/// </summary>
public interface IElement
{
    /// <summary>
    /// The parsed tag name.
    /// </summary>
    string Tag { get; }

    /// <summary>
    /// The parsed list of attributes applied to this instance of the tag.
    /// </summary>
    IReadOnlyList<IAttribute> Attributes { get; }

    /// <summary>
    /// The parsed list of events applied to this instance of the tag.
    /// </summary>
    IReadOnlyList<IEvent> Events { get; }
}

/// <summary>
/// Record implementation of a StarML <see cref="IElement"/>.
/// </summary>
/// <param name="Tag">The tag name.</param>
/// <param name="Attributes">The attributes applied to this tag.</param>
/// <param name="Events">The events applied to this tag.</param>
public record SElement(string Tag, IReadOnlyList<SAttribute> Attributes, IReadOnlyList<SEvent> Events) : IElement
{
    IReadOnlyList<IAttribute> IElement.Attributes => Attributes;

    IReadOnlyList<IEvent> IElement.Events => Events;
}
