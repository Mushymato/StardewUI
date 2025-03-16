using System.Text;

namespace StardewUI.Framework.Binding;

internal static class DebugExtensions
{
    public static string ToDebugString(this IViewNode node)
    {
        var sb = new StringBuilder();
        node.Print(sb, true);
        return sb.ToString();
    }
}
