using System.Collections;
using PropertyChanged.SourceGenerator;
using StardewValley;
using StardewValley.ItemTypeDefinitions;

namespace StardewUITest.Examples;

internal record EdiblesViewItemModel(ParsedItemData Data)
{
    public Item ReprItem => ItemRegistry.Create(Data.QualifiedItemId);
}

internal partial record EdiblesViewModel(string HeaderText, EdiblesViewItemModel?[] Items)
{
    [Notify]
    private IList? activeItems;

    [Notify]
    private int count = 22;

    public void AddRow()
    {
        Count += 11;
    }

    public void RemoveRow()
    {
        if (count > 11)
            Count -= 11;
    }

    public static EdiblesViewModel LoadFromGameData()
    {
        int[] edibleCategories =
        [
            StardewValley.Object.CookingCategory,
            StardewValley.Object.EggCategory,
            StardewValley.Object.FishCategory,
            StardewValley.Object.FruitsCategory,
            StardewValley.Object.meatCategory,
            StardewValley.Object.MilkCategory,
            StardewValley.Object.VegetableCategory,
        ];
        var items = ItemRegistry
            .ItemTypes.Single(type => type.Identifier == ItemRegistry.type_object)
            .GetAllIds()
            .Select(id => ItemRegistry.GetDataOrErrorItem(id))
            .Where(data => edibleCategories.Contains(data.Category));
        return new("All Edibles", items.Select(data => new EdiblesViewItemModel(data)).ToArray());
    }
}
