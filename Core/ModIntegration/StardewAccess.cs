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
        }
        if (screenRead != null)
            Api.SayMenuElement(screenRead);
    }

    /// <summary>
    /// Make a <see cref="ScreenReadableData"/> with a particular text delegate.
    /// </summary>
    /// <param name="textDelegate">Text delegate used to modify the inner text</param>
    /// <param name="precedence"></param>
    /// <returns></returns>
    public static ScreenReadableData? MakeScreenReadDelegated(Func<string, string> textDelegate, int precedence = 1)
    {
        if (Api == null)
            return null;
        return new ScreenReadableData() { ScreenReaderTextDelegate = textDelegate, Precedence = precedence };
    }

    /// <summary>Make a <see cref="ScreenReadableData"/> using translated text from Stardew Access</summary>
    /// <param name="translationKey">Stardew Access translation key</param>
    /// <param name="getTokens">Delegate that takes a string and returns translation tokens</param>
    /// <param name="precedence"></param>
    /// <returns></returns>
    public static ScreenReadableData? MakeScreenReadTranslated(string translationKey, Func<string, object?> getTokens, int precedence = 1)
    {
        if (Api == null)
            return null;
        return new ScreenReadableData()
        {
            ScreenReaderTextDelegate = (text) =>
            {
                string result = Api.Translate(translationKey, getTokens(text), translationCategory: "Menu");
                return result;
            },
            Precedence = precedence
        };
    }
}

/// <summary>
/// A screen readable bit of text.
/// Although <see cref="IScreenReadable"/> is a vanilla interface, it does nothing
/// by itself and will be used with screen reader mods.
/// </summary>
[DuckType]
public sealed class ScreenReadableData() : IScreenReadable
{
    /// <summary>Backing field of <see cref="ScreenReaderText"/></summary>
    private string screenReaderTextInner = string.Empty;
    /// <inheritdoc />
    public string? ScreenReaderText
    {
        get => ScreenReaderTextDelegate?.Invoke(screenReaderTextInner) ?? screenReaderTextInner;
        set => screenReaderTextInner = value ?? string.Empty;
    }

    /// <inheritdoc />
    public string? ScreenReaderDescription { get; set; }

    /// <inheritdoc />
    public bool ScreenReaderIgnore { get; set; } = false;

    /// <summary>
    /// How prioritized this screen reader element is.
    /// The lowest precedence element will be read,
    /// even if the hover path has more specific elements.
    /// Custom screen read fields should use negative values.
    /// while screen read fields set by the View should have value 0 or greater.
    /// </summary>
    public int Precedence { get; set; } = 0;

    /// <summary>A delegate used to modify </summary>
    public Func<string, string>? ScreenReaderTextDelegate { get; set; } = null;
}
