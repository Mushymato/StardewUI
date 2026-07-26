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
    void HandleSpecialKey(Keys keyCode);

    /// <summary>Function when the subscriber is released</summary>
    void Release();
}
