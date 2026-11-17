using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace StardewUI.Input;

#if SDV17
using StardewValley;
#else
/// <summary>KeyboardModifier backport</summary>
[Flags]
public enum KeyboardModifier
{
    /// <summary>No modifier</summary>
    None = 0,
    /// <summary>LeftShift or RightShift</summary>
    Shift = 1,
    /// <summary>LeftControl or RightControl</summary>
    Control = 2,
    /// <summary>LeftAlt or RightAlt</summary>
    Alt = 4,
    /// <remark>Mainly used for Mac system parity</remark>
    System = 8
}
#endif

/// <summary>
/// An implementation of <see cref="IKeyboardSubscriber"/> that forwards keys to the owning <see cref="IKeyboardSubscriberOwnerView"/>.
/// </summary>
/// <param name="owner">The view that owns this subscriber.</param>
/// <param name="window"></param>
public class KeyboardSubscriberWithOwner(IKeyboardSubscriberOwnerView owner, GameWindow window) : ICaptureTarget, IKeyboardSubscriber
{
    private readonly IKeyboardSubscriberOwnerView owner = owner;
    private readonly GameWindow window = window;

    /// <summary>
    /// Whether this subscriber is active.
    /// When this is changed to true, this subscriber is registered to <see cref="Game1.keyboardDispatcher"/> and key capturing begins.
    /// When this is changed to false, it is removed from <see cref="Game1.keyboardDispatcher"/> and key capturing ends.
    /// </summary>
    public bool Selected
    {
        get => field;
        set
        {
            if (value == field)
            {
                return;
            }
            field = value;
            if (field)
            {
                Game1.keyboardDispatcher.Subscriber = this;
                if (PlatformUsesWindowEvents())
                {
                    window.KeyDown += Window_KeyDown;
                }
                else
                {
                    KeyboardInput.KeyDown += KeyboardInput_KeyDown;
                }
            }
            else
            {
                if (PlatformUsesWindowEvents())
                {
                    window.KeyDown -= Window_KeyDown;
                }
                else
                {
                    KeyboardInput.KeyDown -= KeyboardInput_KeyDown;
                }
                if (Game1.keyboardDispatcher.Subscriber == this)
                {
                    Game1.keyboardDispatcher.Subscriber = null;
                }
            }
        }
    }

    /// <inheritdoc/>
    public IView CapturingView => owner;

    /// <inheritdoc/>
    public void RecieveTextInput(char inputChar)
    {
        if (Selected)
        {
            owner.InsertChar(inputChar);
        }
    }

    /// <inheritdoc/>
    public void RecieveTextInput(string text)
    {
        if (Selected)
        {
            owner.InsertString(text);
        }
    }

    /// <inheritdoc/>
    public void ReleaseCapture()
    {
        owner.Release();
    }

    private void KeyboardInput_KeyDown(object sender, KeyEventArgs e)
    {
        owner.HandleSpecialKey(e.KeyCode, GetKeyboardModifier());
    }

    private void Window_KeyDown(object? sender, InputKeyEventArgs e)
    {
        owner.HandleSpecialKey(e.Key, GetKeyboardModifier());
    }

    // Same logic used in KeyboardDispatcher.
    private static bool PlatformUsesWindowEvents()
    {
        return Environment.OSVersion.Platform == PlatformID.Unix
            || Environment.OSVersion.Platform == PlatformID.Win32NT;
    }

    private static KeyboardModifier GetKeyboardModifier()
    {
        // this mimics the base game KeyboardModifier logic in KeyboardDispatcher.Poll added for 1.6.16
        // chose to do it ourselves here so that it also works for current version
        KeyboardState keyboardState = Game1.input.GetKeyboardState();
        KeyboardModifier keyboardModifier = KeyboardModifier.None;
        if (keyboardState.IsKeyDown(Keys.LeftShift) || keyboardState.IsKeyDown(Keys.RightShift))
        {
            keyboardModifier |= KeyboardModifier.Shift;
        }
        if (keyboardState.IsKeyDown(Keys.LeftControl) || keyboardState.IsKeyDown(Keys.RightControl))
        {
            keyboardModifier |= KeyboardModifier.Control;
        }
        if (keyboardState.IsKeyDown(Keys.LeftAlt) || keyboardState.IsKeyDown(Keys.RightAlt))
        {
            keyboardModifier |= KeyboardModifier.Alt;
        }
        if (keyboardState.IsKeyDown(Keys.LeftWindows) || keyboardState.IsKeyDown(Keys.RightWindows))
        {
            keyboardModifier |= KeyboardModifier.System;
        }
        return keyboardModifier;
    }

#if SDV17
    /// <inheritdoc/>
    public void RecieveCommandInput(char command, KeyboardModifier modifiers)
    {
        // TODO: support Alt/Control + Backspace erasing a whole word
        if (Selected)
            owner.InsertChar(command);
    }

    /// <inheritdoc/>
    public void RecieveSpecialInput(Keys key, KeyboardModifier modifiers)
    {
        // KeyboardDispatcher is not consistent about which "special" keys it dispatches, depending on the platform.
        // It's better not to implement this, and instead set up a separate (direct) subscription.
    }

    /// <inheritdoc/>
    public string? ClipboardCopy()
    {
        return Selected ? owner.ClipboardCopy() : null;
    }

    /// <inheritdoc/>
    public string? ClipboardCut()
    {
        return Selected ? owner.ClipboardCut() : null;
    }

    /// <inheritdoc/>
    public void SelectAll()
    {
        if (Selected)
            owner.SelectAll();
    }
#else

    /// <inheritdoc/>
    public void RecieveCommandInput(char command)
    {
        if (Selected)
            owner.InsertChar(command);
    }

    /// <inheritdoc/>
    public void RecieveSpecialInput(Keys key)
    {
        // KeyboardDispatcher is not consistent about which "special" keys it dispatches, depending on the platform.
        // It's better not to implement this, and instead set up a separate (direct) subscription.
    }
#endif
}
