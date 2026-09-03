namespace StardewUI.ModIntegration;

/// <summary>Manages lookup anything integration</summary>
public static class LookupAnythingIntegration
{
    /// <summary>Whether Lookup Anything is loaded</summary>
    public static bool IsLoaded { get; set; } = false;

    /// <summary>Initialize lookup anything integration</summary>
    public static void Initialize(IModHelper helper)
    {
        if (IsLoaded = helper.ModRegistry.IsLoaded("Pathoschild.LookupAnything"))
            Logger.Log("Pathoschild.LookupAnything is loaded, integration enabled");
    }

    /// <summary>
    /// Find the final hovered subject in a view hover path, and set that to the top level view menu.
    /// </summary>
    /// <param name="path">Sequence of all elements, and their relative positions, that the mouse coordinates are
    /// currently within.</param>
    public static void SetSubject(ViewChild[] path)
    {
        if (!IsLoaded)
            return;

        // Lookup Anything only checks Game1.activeClickableMenu for these conventional fields.
        // If the top level menu is not a ViewMenu, do nothing.
        // TODO: send pathos a PR to change this into properties/API
        if (Game1.activeClickableMenu is not ViewMenu activeViewMenu)
            return;

        if (path.LastOrDefault(x => x.View.HoveredSubject is not null)?.View.HoveredSubject is LookupAnythingHoveredSubject laSubject)
        {
            activeViewMenu.hoveredItem = laSubject.HoveredItem;
            activeViewMenu.hoveredNpc = laSubject.HoveredNpc;
        }
        else
        {
            activeViewMenu.hoveredItem = null;
            activeViewMenu.hoveredNpc = null;
        }
    }
}

/// <summary>The lookup anything hovered subject to supply to <see cref="ViewMenu"/></summary>
public record LookupAnythingHoveredSubject(Item? HoveredItem = null, NPC? HoveredNpc = null);
