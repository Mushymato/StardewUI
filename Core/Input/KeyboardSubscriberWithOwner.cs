using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace StardewUI.Input;

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
        owner.HandleSpecialKey(e.KeyCode);
    }

    private void Window_KeyDown(object? sender, InputKeyEventArgs e)
    {
        owner.HandleSpecialKey(e.Key);
    }

    // Same logic used in KeyboardDispatcher.
    private static bool PlatformUsesWindowEvents()
    {
        return Environment.OSVersion.Platform == PlatformID.Unix
            || Environment.OSVersion.Platform == PlatformID.Win32NT;
    }

#if SDV17
    /// <inheritdoc/>
    public void RecieveCommandInput(char command, KeyboardModifier modifiers)
    {
        // TODO: support Alt/Control + Backspace erasing a whole word
        if (Selected)
            owner.Insert(command);
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

    public string? ClipboardCut()
    {
        return Selected ? owner.ClipboardCut() : null;
    }

    public void SelectAll()
    {
        if (Selected)
        {
            owner.SelectAll();
        }
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
