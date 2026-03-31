using StardewUI.ModIntegration;
using StardewValley.Menus;

namespace StardewUI.Framework.Converters;

/// <summary>Converts a string to <see cref="ScreenReadableData"/></summary>
public sealed class ScreenReadableStringConverter : IValueConverter<string, ScreenReadableData>
{
    /// <inheritdoc />
    public ScreenReadableData Convert(string value)
    {
        return new ScreenReadableData() { ScreenReaderText = value };
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
        return new ScreenReadableData()
        {
            ScreenReaderText = value.ScreenReaderText,
            ScreenReaderDescription = value.ScreenReaderDescription,
        };
    }
}
