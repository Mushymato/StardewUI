using StardewUI.ModIntegration;

namespace StardewUI.Framework.Converters;

/// <summary>Convert <see cref="Item"/> to <see cref="LookupAnythingHoveredSubject"/></summary>
public sealed class LookupAnythingHoveredItemConverter : IValueConverter<Item, LookupAnythingHoveredSubject?>
{
    /// <inheritdoc />
    public LookupAnythingHoveredSubject? Convert(Item value) => new(HoveredItem: value);
}

/// <summary>Convert <see cref="NPC"/> to <see cref="LookupAnythingHoveredSubject"/></summary>
public sealed class LookupAnythingHoveredNpcConverter : IValueConverter<NPC, LookupAnythingHoveredSubject?>
{
    /// <inheritdoc />
    public LookupAnythingHoveredSubject? Convert(NPC value) => new(HoveredNpc: value);
}
