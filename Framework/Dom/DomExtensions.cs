namespace StardewUI.Framework.Dom;

internal static class DomExtensions
{
    public static IAttribute? Find(this IEnumerable<IAttribute> attributes, string name)
    {
        return attributes.FirstOrDefault(attr => attr.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public static SNode? FindTemplate(this Document document, string name)
    {
        return document.Templates.FirstOrDefault(tpl =>
            tpl.Attributes.Find("name")?.Value.Equals(name, StringComparison.OrdinalIgnoreCase) == true
        );
    }
}
