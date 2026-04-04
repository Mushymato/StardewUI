using Microsoft.Xna.Framework.Graphics;
using StardewUI.Graphics;
using StardewUI.ModIntegration;

namespace StardewUI.Widgets;

/// <summary>
/// A view that renders item using <see cref="Item.drawInMenu"/>.
/// <c>(unofficial-mushymato)</c>
/// </summary>
public partial class ItemIcon : View
{
    /// <summary>
    /// The item to show.
    /// </summary>
    public Item? Item
    {
        get => field;
        set
        {
            if (field != value)
            {
                field = value;
                hoveredSubject = new(HoveredItem: value);
                OnPropertyChanged(nameof(Item));
            }
        }
    } = null;

    /// <summary>
    /// Draw scale, this is 1f based instead of 4f based.
    /// </summary>
    public float Scale
    {
        get => field;
        set
        {
            if (field != value)
            {
                field = value;
                OnPropertyChanged(nameof(Scale));
            }
        }
    } = 1f;

    /// <summary>
    /// How to render the stack number and quality.
    /// </summary>
    public StackDrawType DrawStack
    {
        get => field;
        set
        {
            if (value != field)
            {
                field = value;
                OnPropertyChanged(nameof(DrawStack));
            }
        }
    } = StackDrawType.Draw;

    /// <summary>
    /// Tint color (multiplier) to apply when drawing.
    /// </summary>
    public Color Tint
    {
        get => field;
        set
        {
            if (value != field)
            {
                field = value;
                OnPropertyChanged(nameof(Tint));
            }
        }
    } = Color.White;

    /// <summary>
    /// Whether to draw a shadow underneath the item.
    /// </summary>
    public bool DrawShadow
    {
        get => field;
        set
        {
            if (field != value)
            {
                field = value;
                OnPropertyChanged(nameof(DrawShadow));
            }
        }
    } = false;

    /// <inheritdoc />
    public override LookupAnythingHoveredSubject? HoveredSubject
    {
        get => hoveredSubject;
        set { }
    }

    private LookupAnythingHoveredSubject? hoveredSubject = null;

    /// <inheritdoc />
    protected override void OnDrawContent(ISpriteBatch b)
    {
        b.DelegateDraw(DelegatedDrawInMenu);
    }

    private void DelegatedDrawInMenu(SpriteBatch batch, Vector2 vector)
    {
        Item?.drawInMenu(batch, vector, Scale, 1f, 1f, DrawStack, Tint, DrawShadow);
    }

    /// <inheritdoc />
    protected override void OnMeasure(Vector2 availableSize)
    {
        ContentSize = Layout.Resolve(availableSize, static () => new(64, 64));
    }
}
