using Microsoft.Xna.Framework.Input;

namespace StardewUI.Input;

/// <summary>
/// Denotes a view that can be the owner of a <see cref="KeyboardSubscriberWithOwner"/>.
/// </summary>
public interface IKeyboardSubscriberOwnerView : IView
{
    /// <summary>Accept new entered char</summary>
    /// <param name="inputChar"></param>
    void InsertChar(char inputChar);

    /// <summary>Accept new entered string</summary>
    /// <param name="text"></param>
    void InsertString(string text);

    /// <summary>Handle non-text entry key.</summary>
    /// <param name="keyCode"></param>
    /// <param name="modifiers"></param>
    void HandleSpecialKey(Keys keyCode, KeyboardModifier modifiers);

    /// <summary>Function when the subscriber is released</summary>
    void Release();

    /// <summary>Get string to copy</summary>
    /// <returns>Selected string</returns>
    string? ClipboardCopy();

    /// <summary>Get string to copy and immediately delete it from text input</summary>
    /// <returns>Selected string</returns>
    string? ClipboardCut();

    /// <summary>Select all text</summary>
    void SelectAll();
}
