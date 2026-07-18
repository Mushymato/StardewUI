using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewUI.Animation;
using StardewUI.Events;
using StardewUI.Graphics;
using StardewUI.Input;
using StardewUI.Layout;
using StardewUI.ModIntegration;
using StardewValley.Menus;

namespace StardewUI.Widgets;

/// <summary>
/// A text input field that allows typing from a physical or virtual keyboard.
/// </summary>
public partial class TextInput : View
{
    /// <summary>
    /// Event raised when the <see cref="Text"/> changes.
    /// </summary>
    public event EventHandler<EventArgs>? TextChanged;

    /// <summary>
    /// For compatibility reasons Background is alias for Border.
    /// <inheritdoc cref="Frame.Border"/>
    /// <c>(unofficial-mushymato)</c>
    /// </summary>
    public Sprite? Background
    {
        get => frame.Border;
        set => frame.Border = value;
    }

    /// <summary>
    /// <inheritdoc cref="Frame.Border"/>
    /// <c>(unofficial-mushymato)</c>
    /// </summary>
    public Sprite? Border
    {
        get => frame.Border;
        set
        {
            if (value != frame.Border)
            {
                frame.Border = value;
                OnPropertyChanged(nameof(Border));
            }
        }
    }

    /// <inheritdoc cref="Frame.BorderThickness"/>
    public Edges BorderThickness
    {
        get => frame.BorderThickness;
        set
        {
            if (value != frame.BorderThickness)
            {
                frame.BorderThickness = value;
                OnPropertyChanged(nameof(BorderThickness));
            }
        }
    }

    /// <summary>
    /// Sprite to draw for the cursor showing the current text position.
    /// </summary>
    public Sprite? Caret
    {
        get => caret.Sprite;
        set
        {
            if (value != caret.Sprite)
            {
                caret.Sprite = value;
                OnPropertyChanged(nameof(Caret));
            }
        }
    }

    /// <summary>
    /// The zero-based position of the caret within the text.
    /// </summary>
    /// <remarks>
    /// This value is the string position; e.g. if the <see cref="Text"/> has a length of 5, and the current caret
    /// position is 2, then the caret is between the 2nd and 3rd characters. The value cannot be greater than the length
    /// of the current text.
    /// </remarks>
    public int CaretPosition
    {
        get => TextBeforeCursor.Length;
        set
        {
            if (SetCaretPosition(value))
            {
                OnPropertyChanged(nameof(CaretPosition));
            }
        }
    }

    /// <summary>
    /// Number of characters selected, stored as number of characters before or after the caret.
    /// <c>(unofficial-mushymato)</c>
    /// </summary>
    /// <remarks>
    /// This value does participate in <see cref="TextBeforeCursor"/> or <see cref="TextAfterCursor"/> adding up to equal total text length.
    /// When positive, it is always smaller than <see cref="TextAfterCursor"/> length.
    /// When negative, it's absolute value is always smaller <see cref="TextBeforeCursor"/> length.
    /// </remarks>
    public int CaretSelectionSize
    {
        get => field;
        set
        {
            int val;
            string selectedText;

            if (value > 0)
                val = Math.Min(value, TextAfterCursor.Length);
            else if (value < 0)
                val = Math.Max(value, -TextBeforeCursor.Length);
            else
                val = value;

            if (field == val)
                return;

            if (val > 0)
                selectedText = TextAfterCursor[..val];
            else if (val < 0)
                selectedText = TextBeforeCursor[^(-val)..];
            else
                selectedText = string.Empty;

            field = val;
            OnPropertyChanged(nameof(CaretSelectionSize));
            SelectedText = selectedText;
            OnPropertyChanged(nameof(SelectedText));
            UpdateRealSelectionPosition();
        }
    }

    /// <summary>
    /// Read-only property for selected text, set via changes to <see cref="CaretSelectionSize"/>.
    /// <c>(unofficial-mushymato)</c>
    /// </summary>
    public string SelectedText { get; private set; } = string.Empty;

    /// <summary>
    /// The width to draw the <see cref="Caret"/>, if different from the sprite's source width.
    /// </summary>
    public float? CaretWidth
    {
        get => caret.Layout.Width.Type == LengthType.Px ? caret.Layout.Width.Value : null;
        set
        {
            if (value != CaretWidth)
            {
                caret.Layout = new()
                {
                    Width = value.HasValue ? Length.Px(value.Value) : Length.Content(),
                    Height = Length.Stretch(),
                };
                OnPropertyChanged(nameof(CaretWidth));
            }
        }
    }



    /// <summary>
    /// Whether the input is enabled.
    /// </summary>
    /// <remarks>
    /// Disabled text inputs have a darkened appearance and do not accept captures or text entry.
    /// </remarks>
    public bool Enabled
    {
        get => enabled;
        set
        {
            if (value == enabled)
            {
                return;
            }
            enabled = value;
            frame.BackgroundTint = value ? Color.White : Color.LightGray;
            if (!value)
            {
                Release();
            }
            OnPropertyChanged(nameof(Enabled));
        }
    }

    /// <summary>
    /// The font with which to render text. Defaults to <see cref="Game1.smallFont"/>.
    /// </summary>
    public SpriteFont Font
    {
        get => label.Font;
        set
        {
            if (value != label.Font)
            {
                label.Font = value;
                OnPropertyChanged(nameof(Font));
            }
        }
    }

    /// <summary>
    /// The maximum number of characters allowed in this field.
    /// </summary>
    /// <remarks>
    /// The default value is <c>0</c> which does not impose any limit.
    /// </remarks>
    public int MaxLength
    {
        get => maxLength;
        set
        {
            if (value != maxLength)
            {
                maxLength = value;
                if (value > 0 && Text.Length > value)
                {
                    Text = Text[..value];
                }
                OnPropertyChanged(nameof(MaxLength));
            }
        }
    }

    /// <summary>
    /// Placeholder text to display when the <see cref="Text"/> is empty and input is not captured.
    /// </summary>
    public string Placeholder
    {
        get => placeholder.Text;
        set
        {
            if (value != placeholder.Text)
            {
                placeholder.Text = value;
                OnPropertyChanged(nameof(Placeholder));
            }
        }
    }

    /// <summary>
    /// Color of the <see cref="Placeholder"/> text when displayed.
    /// </summary>
    public Color PlaceholderColor
    {
        get => placeholder.Color;
        set
        {
            if (value != placeholder.Color)
            {
                placeholder.Color = value;
                OnPropertyChanged(nameof(PlaceholderColor));
            }
        }
    }

    /// <inheritdoc cref="Frame.ShadowAlpha"/>
    public float ShadowAlpha
    {
        get => frame.ShadowAlpha;
        set
        {
            if (value != frame.ShadowAlpha)
            {
                frame.ShadowAlpha = value;
                OnPropertyChanged(nameof(ShadowAlpha));
            }
        }
    }

    /// <inheritdoc cref="Frame.ShadowOffset"/>
    public Vector2 ShadowOffset
    {
        get => frame.ShadowOffset;
        set
        {
            if (value != frame.ShadowOffset)
            {
                frame.ShadowOffset = value;
                OnPropertyChanged(nameof(ShadowOffset));
            }
        }
    }

    /// <summary>
    /// Color of displayed text, as well as the <see cref="Caret"/> tint color.
    /// </summary>
    public Color TextColor
    {
        get => label.Color;
        set
        {
            if (value != label.Color)
            {
                label.Color = value;
                caret.Tint = value;
                selection.Tint = value * 0.2f;
                OnPropertyChanged(nameof(TextColor));
            }
        }
    }

    /// <summary>
    /// The text currently entered.
    /// </summary>
    /// <remarks>
    /// Setting this to a new value will reset the caret position to the end of the text.
    /// </remarks>
    public string Text
    {
        get => TextBeforeCursor + TextAfterCursor;
        set => SetText(value);
    }

    private string TextAfterCursor
    {
        get => textAfterCursor;
        set
        {
            textAfterCursor = value;
            label.Text = Text;
        }
    }

    private string TextBeforeCursor
    {
        get => textBeforeCursor;
        set
        {
            textBeforeCursor = value;
            label.Text = Text;
        }
    }

    // Extra space to leave between the nominal caret position (exactly at the beginning of a character) and the actual
    // drawn caret position, which should ideally be shown within the whitespace.
    private const int CARET_POSITION_OFFSET = 2;

    // A very small positive offset we add to the search position when trying to move the caret to the mouse cursor.
    // In general, the caret should always move BEFORE the character that was clicked on; however, this has a tendency
    // to "overshoot" if the user tries to click between two characters (as many are accustomed to doing). To
    // compensate, we shift the position slightly to the right.
    //
    // Note: We have to be careful not to overdo this in case of a very thin character, like "i" or "l". If the offset
    // is bigger or almost as big as the actual character width, we'll just miss it entirely.
    private const float CARET_SEARCH_OFFSET = 4.0f;

    // Tries to always keep a few characters to the left/right of the caret position in view.
    // This is similar to the ScrollContainer's own peeking, but we can't use that because the text is a single view.
    private const int SCROLL_PEEKING_PX = 30;

    private readonly Image caret;
    private readonly Animator<Image, Visibility> caretBlinkAnimator;
    private readonly Image selection;
    private readonly Frame frame;
    private readonly Label label;
    private readonly Label placeholder;
    private readonly ScrollContainer scrollContainer;
    private readonly TextBoxInterceptor textBoxInterceptor;
    private readonly TextInputSubscriber textInputSubscriber;
    private readonly Panel textPanel;

    private bool enabled = true;
    private bool isTextEntryMenuShown;
    private int maxLength;
    private string textAfterCursor = "";
    private string textBeforeCursor = "";

    /// <summary>
    /// Initializes a new <see cref="TextInput"/>.
    /// </summary>
    public TextInput()
    {
        Focusable = true;

        caret = new Image()
        {
            Name = "TextInputCursor",
            Layout = new() { Width = Length.Px(2), Height = Length.Stretch() },
            Fit = ImageFit.Stretch,
            Sprite = new(Game1.staminaRect),
            Tint = Game1.textColor,
            Visibility = Visibility.Hidden,
        };
        caretBlinkAnimator = Animator.On(
            caret,
            i => i.Visibility,
            (_, _, progress) => progress < 0.5f ? Visibility.Visible : Visibility.Hidden,
            (i, v) => i.Visibility = v
        );
        caretBlinkAnimator.Loop = true;
        selection = new Image()
        {
            Name = "TextInputSelection",
            Layout = LayoutParameters.Fill(),
            Fit = ImageFit.Stretch,
            Sprite = new(Game1.staminaRect),
            Tint = Game1.textColor * 0.2f,
            Visibility = Visibility.Hidden,
        };
        label = new()
        {
            Name = "TextInputLabel",
            Layout = LayoutParameters.FitContent(),
            // Right margin allows scrolling past end; this is similar to peeking, but intended for typing strings that
            // are longer than text box width can fit.
            Margin = new(Right: SCROLL_PEEKING_PX),
            MaxLines = 1,
        };
        placeholder = new()
        {
            Name = "TextInputPlaceholder",
            Layout = LayoutParameters.AutoRow(),
            MaxLines = 1,
        };
        textPanel = new Panel()
        {
            Layout = LayoutParameters.AutoRow(),
            VerticalContentAlignment = Alignment.Middle,
            Children = [placeholder, label, selection, caret],
        };
        scrollContainer = new()
        {
            Name = "TextInputScrollContainer",
            Layout = LayoutParameters.AutoRow(),
            Margin = new(Left: -CARET_POSITION_OFFSET, Top: -4, Bottom: -4),
            Padding = new(Left: CARET_POSITION_OFFSET, Top: 4, Bottom: 4),
            Orientation = Orientation.Horizontal,
            Content = textPanel,
        };
        var textBoxSprite = UiSprites.TextBox;
        frame = new()
        {
            Layout = LayoutParameters.AutoRow(),
            Border = textBoxSprite,
            BorderThickness = textBoxSprite.FixedEdges ?? new(4),
            VerticalContentAlignment = Alignment.Middle,
            Content = scrollContainer,
        };
        textBoxInterceptor = new(this);
        textInputSubscriber = new(this, Game1.keyboardFocusInstance.Window);

        Font = Game1.smallFont;
        TextColor = Game1.textColor;
        PlaceholderColor = Color.Gray;

        ScreenRead = StardewAccessIntegration.MakeScreenReadTranslated(
            "options_element-text_box_info",
            GetScreenReadTokens
        );
    }

    /// <inheritdoc />
    protected override IEnumerable<ViewChild> GetLocalChildren()
    {
        return [new(frame, Vector2.Zero)];
    }

    /// <inheritdoc />
    protected override bool IsContentDirty()
    {
        return frame.IsDirty();
    }

    /// <inheritdoc />
    public override void OnClick(ClickEventArgs e)
    {
        if (Enabled && e.IsPrimaryButton())
        {
            Capture(e.Position);
            e.Handled = true;
        }
    }

    /// <inheritdoc />
    public override void OnDrag(PointerEventArgs e)
    {
        if (Enabled && Game1.keyboardDispatcher.Subscriber == textInputSubscriber)
        {
            Select(e.Position);
            e.Handled = true;
        }
    }

    /// <inheritdoc />
    protected override void OnDrawContent(ISpriteBatch b)
    {
        frame.Draw(b);
    }

    /// <inheritdoc />
    protected override void OnMeasure(Vector2 availableSize)
    {
        var limits = Layout.GetLimits(availableSize);
        frame.Measure(limits);
        ContentSize = Layout.Resolve(availableSize, () => frame.OuterSize);
        UpdateRealCaretPosition();
    }

    private void Capture(Vector2 cursorPosition)
    {
        Release(); // In case of switch between mouse and controller
        if (Game1.options.gamepadControls)
        {
            // Vanilla text entry doesn't support moving the caret, so make sure we're at the end.
            CaretPosition = Text.Length;
            textBoxInterceptor.Width = (int)OuterSize.X;
            textBoxInterceptor.Height = (int)OuterSize.Y;
            textBoxInterceptor.Selected = true;
            Game1.showTextEntry(textBoxInterceptor);
            isTextEntryMenuShown = true;
        }
        else
        {
            MoveCaretToCursor(cursorPosition);
            caretBlinkAnimator.Start(Visibility.Visible, Visibility.Hidden, TimeSpan.FromSeconds(1));
            Game1.keyboardDispatcher.Subscriber = textInputSubscriber;
        }
        UpdatePlaceholderVisibility();
    }

    private void Select(Vector2 cursorPosition)
    {
        MoveCaretToCursor(cursorPosition, selecting: true);
    }

    private void HandleSpecialKey(Keys key)
    {
        // TODO: add shift select range
        switch (key)
        {
            case Keys.Left:
                CaretPosition--;
                break;
            case Keys.Right:
                CaretPosition++;
                break;
            case Keys.Home:
                CaretPosition = 0;
                break;
            case Keys.End:
                CaretPosition = Text.Length;
                break;
            case Keys.Delete:
                if (EraseSelectedText())
                {
                    OnTextChanged();
                }
                else
                {
                    if (TextAfterCursor.Length > 0)
                    {
                        TextAfterCursor = TextAfterCursor[1..];
                        label.Text = Text;
                        OnTextChanged();
                    }
                }
                break;
        }
    }

    private void Insert(char c)
    {
        switch (c)
        {
            case '\b':
                if (EraseSelectedText())
                {
                    OnTextChanged();
                }
                else
                {
                    if (TextBeforeCursor.Length > 0)
                    {
                        TextBeforeCursor = TextBeforeCursor[..^1];
                        OnTextChanged();
                    }
                }
                break;
            case '\t':
            case '\r':
                Release();
                break;
            default:
                if (!char.IsControl(c))
                {
                    EraseSelectedText();
                    if (MaxLength == 0 || Text.Length < MaxLength)
                    {
                        TextBeforeCursor += c;
                        OnTextChanged();
                    }
                }
                break;
        }
    }

    private void Insert(string text)
    {
        if (MaxLength > 0)
        {
            var remainingLength = Math.Max(MaxLength - Text.Length, 0);
            if (text.Length > remainingLength)
            {
                text = text[..remainingLength];
            }
        }
        if (text.Length > 0)
        {
            TextBeforeCursor += text;
            OnTextChanged();
        }
    }

    /// <summary>
    /// Erases the selected text. Note that it does not immediately call <see cref="OnTextChanged"/>.
    /// </summary>
    /// <returns>True if there was text to erase.</returns>
    private bool EraseSelectedText()
    {
        int selectionSize = CaretSelectionSize;
        if (selectionSize == 0)
        {
            return false;
        }
        else if (selectionSize > 0)
        {
            TextAfterCursor = TextAfterCursor[selectionSize..];
        }
        else
        {
            TextBeforeCursor = TextBeforeCursor[..^Math.Abs(selectionSize)];
        }
        return true;
    }

    private string? ClipboardCopy()
    {
        if (CaretSelectionSize == 0)
            return null;
        return SelectedText;
    }

    private string? ClipboardCut()
    {
        if (CaretSelectionSize == 0)
            return null;
        string selected = SelectedText;
        if (EraseSelectedText())
            OnTextChanged();
        return selected;
    }

    private void SelectAll()
    {
        CaretPosition = 0;
        CaretSelectionSize = Text.Length;
    }

    private void MoveCaretToCursor(Vector2 cursorPosition, bool selecting = false)
    {
        if (Text.Length == 0)
            return;
        var searchOrigin = new Vector2(
            BorderThickness.Left - CARET_SEARCH_OFFSET - scrollContainer.ScrollOffset,
            BorderThickness.Top
        );
        int caretPosition = CaretPositionSearch(cursorPosition - searchOrigin);
        if (selecting)
        {
            CaretSelectionSize = caretPosition - CaretPosition;
        }
        else
        {
            CaretPosition = caretPosition;
        }
    }

    /// <summary>
    /// Search for caret position, i.e. which character should the caret be positioned after.
    /// This is used for <see cref="CaretPosition"/> as well <see cref="CaretSelectionSize"/>.
    /// </summary>
    /// <param name="position">Cursor position</param>
    /// <returns>Caret position</returns>
    private int CaretPositionSearch(Vector2 position)
    {
        if (position.X < 0)
        {
            return 0;
        }
        if (position.X > ContentSize.X + scrollContainer.ScrollOffset)
        {
            return textBeforeCursor.Length + textAfterCursor.Length;
        }
        // Taking into account proportional widths, bearings, kernings, etc., we know very little about the relationship
        // of pixel positions to character positions and don't want to reimplement the entire font system.
        // A reasonably (?) fast solution should be to actually measure partial strings, using a binary search on the
        // length of the before/after string.
        float textBeforeCursorWidth = Font.MeasureString(TextBeforeCursor).X;
        var (previousCharacterCount, labelText, labelOffset) =
            position.X < textBeforeCursorWidth
                ? (0, TextBeforeCursor, position.X)
                : (TextBeforeCursor.Length, TextAfterCursor, position.X - textBeforeCursorWidth);
        var searchStart = 0;
        var searchEnd = labelText.Length;
        while (searchStart < searchEnd)
        {
            int searchMid = (int)MathF.Ceiling((searchStart + searchEnd) / 2.0f);
            var searchText = labelText[0..searchMid];
            var textWidth = Font.MeasureString(searchText).X - Font.MeasureString(searchText[^1..]).X / 2;
            if (labelOffset < textWidth)
            {
                searchEnd = Math.Min(searchEnd - 1, searchMid);
            }
            else
            {
                searchStart = Math.Max(searchStart + 1, searchMid);
            }
        }
        return previousCharacterCount + searchStart;
    }

    private void OnTextChanged()
    {
        CaretSelectionSize = 0;
        UpdateRealCaretPosition();
        UpdatePlaceholderVisibility();
        TextChanged?.Invoke(this, EventArgs.Empty);
        OnPropertyChanged(nameof(Text));
    }

    private object? GetScreenReadTokens(string text)
    {
        return new { label = string.IsNullOrEmpty(text) ? Placeholder : text, value = Text };
    }

    private void Release()
    {
        textBoxInterceptor.Selected = false;
        textInputSubscriber.Selected = false;
        Game1.closeTextEntry();
        isTextEntryMenuShown = false;
        caretBlinkAnimator.Stop();
        caret.Visibility = Visibility.Hidden;
        UpdatePlaceholderVisibility();
    }

    private bool SetCaretPosition(int position)
    {
        var fullText = Text;
        position = Math.Clamp(position, 0, fullText.Length);
        if (position == CaretPosition)
        {
            return false;
        }
        TextBeforeCursor = position > 0 ? fullText[0..position] : "";
        TextAfterCursor = position < fullText.Length ? fullText[position..] : "";
        CaretSelectionSize = 0;
        UpdateRealCaretPosition();
        return true;
    }

    private void SetText(string text)
    {
        if (text == Text)
        {
            return;
        }
        if (maxLength > 0 && text.Length > maxLength)
        {
            text = text[..maxLength];
        }
        TextBeforeCursor = text;
        TextAfterCursor = "";
        OnTextChanged();
    }

    private void UpdatePlaceholderVisibility()
    {
        placeholder.Visibility =
            textBeforeCursor.Length == 0
            && textAfterCursor.Length == 0
            && !isTextEntryMenuShown
            && Game1.keyboardDispatcher.Subscriber != textInputSubscriber
                ? Visibility.Visible
                : Visibility.Hidden;
    }

    private void UpdateRealCaretPosition()
    {
        float textBeforeCursorWidth = Font.MeasureString(TextBeforeCursor).X;
        int x = (int)MathF.Round(textBeforeCursorWidth) - CARET_POSITION_OFFSET;
        caret.Margin = new(Left: x);
        UpdateRealSelectionPosition();
    }

    private void UpdateRealSelectionPosition()
    {
        int selectionSize = CaretSelectionSize;
        if (selectionSize == 0)
        {
            selection.Visibility = Visibility.Hidden;
            UpdateScrollToCaretPosition(caret.Margin.Left);
        }
        else
        {
            selection.Visibility = Visibility.Visible;
            Vector2 availableSize = new(Font.MeasureString(SelectedText).X, caret.ContentSize.Y);
            if (selectionSize > 0)
            {
                // L to R selection, take caret.Margin as is
                selection.Margin = caret.Margin;
                UpdateScrollToCaretPosition(selection.Margin.Left + availableSize.X);
            }
            else if (selectionSize < 0)
            {
                // R to L selection, take caret.Margin.Left minus width of selected text.
                selection.Margin = new(Left: caret.Margin.Left - (int)MathF.Round(availableSize.X));
                UpdateScrollToCaretPosition(selection.Margin.Left);
            }
            // Add in the margin to account for how measurement works (kind of a hack)
            availableSize.X += selection.Margin.Left;
            selection.Measure(availableSize);
        }
    }

    private void UpdateScrollToCaretPosition(float x)
    {
        if (x < scrollContainer.ScrollOffset + SCROLL_PEEKING_PX)
        {
            scrollContainer.ScrollOffset = x - SCROLL_PEEKING_PX;
        }
        else if (x > scrollContainer.ScrollOffset + scrollContainer.OuterSize.X - SCROLL_PEEKING_PX)
        {
            scrollContainer.ScrollOffset = x - scrollContainer.OuterSize.X + SCROLL_PEEKING_PX;
        }
    }

    private class TextBoxInterceptor(TextInput owner)
        : TextBox(Game1.staminaRect, Game1.staminaRect, Game1.smallFont, Color.Black),
            ICaptureTarget
    {
        private readonly TextInput owner = owner;

        public IView CapturingView => owner;

        public override void Draw(SpriteBatch spriteBatch, bool drawShadow = true)
        {
            var b = new PropagatedSpriteBatch(spriteBatch, Transform.FromTranslation(new(X, Y)));
            owner.Draw(b);
        }

#if SDV17
        public override void RecieveCommandInput(char command, KeyboardModifier modifiers)
#else
        public override void RecieveCommandInput(char command)
#endif
        {
            if (Selected)
            {
                owner.Insert(command);
            }
        }

        public override void RecieveTextInput(char inputChar)
        {
            if (Selected)
            {
                owner.Insert(inputChar);
            }
        }

        public override void RecieveTextInput(string text)
        {
            if (Selected)
            {
                owner.Insert(text);
            }
        }

        public void ReleaseCapture()
        {
            owner.Release();
        }

        void ICaptureTarget.Update(TimeSpan elapsed)
        {
            if (owner.isTextEntryMenuShown && Game1.textEntry is null)
            {
                ReleaseCapture();
                return;
            }
        }
    }

    // Used for when we *don't* have controller input, thus don't use the virtual keyboard, don't want to accidentally
    // incur any side effects of the TextBoxInterceptor.
    private class TextInputSubscriber(TextInput owner, GameWindow window) : ICaptureTarget, IKeyboardSubscriber
    {
        private readonly TextInput owner = owner;
        private readonly GameWindow window = window;

        public bool Selected
        {
            get => selected;
            set
            {
                if (value == selected)
                {
                    return;
                }
                selected = value;
                if (selected)
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

        public IView CapturingView => owner;

        private bool selected;

        public void RecieveTextInput(char inputChar)
        {
            if (Selected)
            {
                owner.Insert(inputChar);
            }
        }

        public void RecieveTextInput(string text)
        {
            if (Selected)
            {
                owner.Insert(text);
            }
        }

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
        public void RecieveCommandInput(char command, KeyboardModifier modifiers)
        {
            // TODO: support Alt/Control + Backspace erasing a whole word
            if (Selected)
                owner.Insert(command);
        }

        public void RecieveSpecialInput(Keys key, KeyboardModifier modifiers)
        {
            // KeyboardDispatcher is not consistent about which "special" keys it dispatches, depending on the platform.
            // It's better not to implement this, and instead set up a separate (direct) subscription.
        }

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
        public void RecieveCommandInput(char command)
        {
            if (Selected)
            owner.Insert(command);
        }

        public void RecieveSpecialInput(Keys key)
        {
            // KeyboardDispatcher is not consistent about which "special" keys it dispatches, depending on the platform.
            // It's better not to implement this, and instead set up a separate (direct) subscription.
        }
#endif
    }
}
