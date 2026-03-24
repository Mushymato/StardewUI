using StardewUI.Framework.Converters;
using StardewValley.Menus;

namespace StardewUI.ModIntegration;

/// <summary>stardew access API</summary>
public interface IStardewAccessApi
{
    /// <summary>
    /// Speaks the content of the given element while using the menu query to prevent speaking multiple times in the menu.
    /// </summary>
    /// <param name="element">The element to be spoken.</param>
    /// <param name="interrupt">Whether to skip the currently speaking text or not.</param>
    /// <returns>true if the element was spoken otherwise false.</returns>
    public bool SayMenuElement(IScreenReadable element, bool interrupt = true);

    /// <summary>Translate some text using Stardew Access translations</summary>
    /// <param name="translationKey"></param>
    /// <param name="tokens"></param>
    /// <param name="translationCategory"></param>
    /// <param name="disableWarning"></param>
    /// <returns></returns>
    public string Translate(string translationKey, object? tokens = null, string translationCategory = "Default",
        bool disableWarning = false);
}

/// <summary>Manages Stardew Access (shoaib.stardewaccess) integration</summary>
public static class StardewAccessIntegration
{
    /// <summary>Stardew Access API instance</summary>
    internal static IStardewAccessApi? Api { get; set; } = null;

    /// <summary>Initialize lookup anything integration</summary>
    public static void Initialize(IModHelper helper)
    {
        if ((Api = helper.ModRegistry.GetApi<IStardewAccessApi>("shoaib.stardewaccess")) != null)
            Logger.Log("shoaib.stardewaccess is loaded, integration enabled");
    }

    /// <summary>Say the currently hovered menu element using <see cref="IStardewAccessApi.SayMenuElement(IScreenReadable, bool)"/></summary>
    /// <param name="path">Sequence of all elements, and their relative positions, that the mouse coordinates are
    /// currently within.</param>
    public static void SayHoveredMenuElement(ViewChild[] path)
    {
        if (Api == null)
            return;
        if (path.LastOrDefault(x => x.View.ScreenRead is not null)?.View.ScreenRead is ScreenReadableData screenRead)
        {
            Api.SayMenuElement(screenRead);
        }
        else if (path.Length > 0)
        {
            IView lastView = path[^1].View;
            foreach (ViewChild child in lastView.GetChildren())
            {
                if (child.View.ScreenRead != null)
                {
                    Api.SayMenuElement(child.View.ScreenRead);
                    return;
                }
            }
        }
    }

    /// <summary>Construct a <see cref="ScreenReadableData"/>, mark as automatic</summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static ScreenReadableData? MakeScreenRead(string text)
    {
        if (Api == null)
            return null;
        return new ScreenReadableData(text, isAutomatic: true);
    }

    /// <summary>Construct a <see cref="ScreenReadableData"/> using translated text from Stardew Access</summary>
    /// <param name="translationKey"></param>
    /// <param name="tokens"></param>
    /// <returns></returns>
    public static ScreenReadableData? MakeScreenReadTranslated(string translationKey, object? tokens = null)
    {
        if (Api == null)
            return null;
        return new ScreenReadableData(
            Api.Translate(translationKey, tokens, translationCategory: "Menu"),
            isAutomatic: true
        );
    }
}

/// <summary>
/// A screen readable bit of text.
/// Although <see cref="IScreenReadable"/> is a vanilla interface, it does nothing
/// by itself and requires a screen reader mod to do things.
/// </summary>
/// <param name="screenReaderText"><inheritdoc cref="IScreenReadable.ScreenReaderText"/></param>
/// <param name="screenReaderDescription"><inheritdoc cref="IScreenReadable.ScreenReaderDescription"/></param>
/// <param name="isAutomatic"><inheritdoc cref="IsAutomatic"/></param>
public sealed class ScreenReadableData(string? screenReaderText, string? screenReaderDescription = null, bool isAutomatic = false) : IScreenReadable
{
    /// <summary>
    /// Marks this as an automatically set screen readable data,
    /// as opposed to one created and set by attribute bindings.
    /// Automatic screen readable data cannot override non-automatic.
    /// </summary>
    public readonly bool IsAutomatic = isAutomatic;

    /// <inheritdoc />
    public string? ScreenReaderText => screenReaderText;

    /// <inheritdoc />
    public string? ScreenReaderDescription => screenReaderDescription;

    /// <inheritdoc />
    public bool ScreenReaderIgnore => false;
}

/// <summary>Converts a string to <see cref="ScreenReadableData"/></summary>
public sealed class ScreenReadableStringConverter : IValueConverter<string, ScreenReadableData>
{
    /// <inheritdoc />
    public ScreenReadableData Convert(string value)
    {
        return new ScreenReadableData(screenReaderText: value);
    }
}
