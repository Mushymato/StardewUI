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
        ScreenReadableData? screenRead = null;
        for (int i = path.Length - 1; i >= 0; i--)
        {
            ScreenReadableData? thisScreenRead = path[i].View.ScreenRead;
            if (thisScreenRead == null)
                continue;
            if (screenRead != null)
            {
                if (screenRead.Precedence > thisScreenRead.Precedence)
                    screenRead = thisScreenRead;
                else
                    continue;
            }
            else
            {
                screenRead ??= thisScreenRead;
            }

            if (screenRead.Precedence == 0)
                break;
        }
        if (screenRead != null)
            Api.SayMenuElement(screenRead);
    }

    /// <summary>Construct a <see cref="ScreenReadableData"/>, mark as automatic</summary>
    /// <param name="text">Screen read text</param>
    /// <param name="precedence">Precedence value</param>
    /// <returns></returns>
    public static ScreenReadableData? MakeScreenRead(string text, int precedence = 0)
    {
        if (Api == null)
            return null;
        return new ScreenReadableData(precedence) { ScreenReaderText = text };
    }

    /// <summary>Construct a <see cref="ScreenReadableData"/> using translated text from Stardew Access</summary>
    /// <param name="translationKey">Stardew Access translation key</param>
    /// <param name="tokens">Stardew Access translation tokens</param>
    /// <param name="precedence">Precedence value</param>
    /// <returns></returns>
    public static ScreenReadableData? MakeScreenReadTranslated(string translationKey, object? tokens = null, int precedence = 0)
    {
        if (Api == null)
            return null;
        return new ScreenReadableData(precedence)
        {
            ScreenReaderText = Api.Translate(translationKey, tokens, translationCategory: "Menu")
        };
    }
}

/// <summary>
/// A screen readable bit of text.
/// Although <see cref="IScreenReadable"/> is a vanilla interface, it does nothing
/// by itself and will be used with screen reader mods.
/// </summary>
/// <param name="precedence">
/// How prioritized this screen reader element is.
/// Custom screen read fields should use negative values
/// while screen read fields set by the View should have value 0 or greater
/// </param>
public sealed class ScreenReadableData(int precedence = -1) : IScreenReadable
{
    /// <summary>
    /// Marks this as an automatically set screen readable data,
    /// as opposed to one created and set by attribute bindings.
    /// Automatic screen readable data cannot override non-automatic.
    /// </summary>
    public readonly int Precedence = precedence;

    /// <inheritdoc />
    public string? ScreenReaderText { get; set; }

    /// <inheritdoc />
    public string? ScreenReaderDescription { get; set; }

    /// <inheritdoc />
    public bool ScreenReaderIgnore => false;
}

/// <summary>Converts a string to <see cref="ScreenReadableData"/></summary>
public sealed class ScreenReadableStringConverter : IValueConverter<string, ScreenReadableData>
{
    /// <inheritdoc />
    public ScreenReadableData Convert(string value)
    {
        return new ScreenReadableData(-1) { ScreenReaderText = value };
    }
}

/// <summary>Converts a general <see cref="IScreenReadable"/> to <see cref="ScreenReadableData"/></summary>
public sealed class ScreenReadableInterfaceConverter : IValueConverter<IScreenReadable, ScreenReadableData>
{
    /// <inheritdoc />
    public ScreenReadableData Convert(IScreenReadable value)
    {
        if (value is ScreenReadableData screenRead)
            return screenRead;
        return new ScreenReadableData(-1)
        {
            ScreenReaderText = value.ScreenReaderText,
            ScreenReaderDescription = value.ScreenReaderDescription
        };
    }
}
