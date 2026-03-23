using StardewUI.Framework.Converters;

namespace StardewUI.ModIntegration.LookupAnything;

/// <summary>The lookup anything hovered subject to supply to <see cref="ViewMenu"/></summary>
public record LookupAnythingHoveredSubject(Item? HoveredItem, NPC? HoveredNpc)
{
    /// <summary>Whether Lookup Anything is loaded</summary>
    public static bool IsLookupAnythingLoaded { get; set; } = false;
}

/// <summary>Convert <see cref="Item"/> to <see cref="LookupAnythingHoveredSubject"/></summary>
public class HoveredItemConverter : IValueConverter<Item, LookupAnythingHoveredSubject>
{
    /// <inheritdoc />
    public LookupAnythingHoveredSubject Convert(Item value) => LookupAnythingHoveredSubject.IsLookupAnythingLoaded ? new(value, null) : null!;
}

/// <summary>Convert <see cref="NPC"/> to <see cref="LookupAnythingHoveredSubject"/></summary>
public class HoveredNpcConverter : IValueConverter<NPC, LookupAnythingHoveredSubject>
{
    /// <inheritdoc />
    public LookupAnythingHoveredSubject Convert(NPC value) => LookupAnythingHoveredSubject.IsLookupAnythingLoaded ? new(null, value) : null!;
}
