using System.Collections.ObjectModel;
using System.ComponentModel;
using PropertyChanged.SourceGenerator;
using StardewValley;
using StardewValley.ItemTypeDefinitions;

namespace StardewUITest.Examples;

internal partial record EdiblesViewModel(string HeaderText, ParsedItemData?[] Items) : INotifyPropertyChanged
{
    [Notify]
    private ObservableCollection<object?>? activeItems;
    public event PropertyChangedEventHandler? PropertyChanged;

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
            .Where(data => edibleCategories.Contains(data.Category))
            .ToArray();
        return new("All Edibles", items);
    }
}
