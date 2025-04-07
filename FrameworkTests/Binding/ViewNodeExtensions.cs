using StardewUI.Framework.Binding;

namespace StardewUI.Framework.Tests.Binding;

internal static class ViewNodeExtensions
{
    // Small helper to avoid having to specify the 'elapsed' argument in tests, where it is usually meaningless.
    public static bool Update(this IViewNode node)
    {
        return node.Update(TimeSpan.Zero);
    }
}
