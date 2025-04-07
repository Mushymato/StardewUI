using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewUI.Framework.Behaviors;
using StardewUI.Framework.Binding;
using StardewUI.Framework.Content;
using StardewUI.Framework.Converters;
using StardewUI.Framework.Dom;
using StardewUI.Framework.Sources;
using Xunit.Abstractions;

namespace StardewUI.Framework.Tests.Binding;

public partial class BindingTests
{
    protected class FakeAssetCache : IAssetCache
    {
        private readonly Dictionary<string, object> assets = [];

        public IAssetCacheEntry<T> Get<T>(string name)
            where T : notnull
        {
            return assets.TryGetValue(name, out var asset)
                ? (FakeAssetCacheEntry<T>)(asset)
                : throw new KeyNotFoundException($"Asset '{name}' not registered.");
        }

        public void Put<T>(string name, T asset)
            where T : notnull
        {
            if (assets.TryGetValue(name, out var oldValue) && oldValue is FakeAssetCacheEntry<T> oldEntry)
            {
                oldEntry.IsValid = false;
            }
            assets[name] = new FakeAssetCacheEntry<T>(asset);
        }
    }

    class FakeAssetCacheEntry<T>(T asset) : IAssetCacheEntry<T>
    {
        public T Asset { get; } = asset;

        public bool IsValid { get; set; } = true;
    }

    protected class FakeResolutionScope : IResolutionScope
    {
        private readonly Dictionary<string, string> translations = [];

        public void AddTranslation(string key, string translation)
        {
            translations.Add(key, translation);
        }

        public Translation? GetTranslation(string key)
        {
            // Not used in tests; we implement GetTranslationValue instead.
            return null;
        }

        public string GetTranslationValue(string key)
        {
            return translations.GetValueOrDefault(key) ?? "";
        }
    }

    protected class FakeResolutionScopeFactory : IResolutionScopeFactory
    {
        public FakeResolutionScope DefaultScope { get; } = new FakeResolutionScope();

        private readonly Dictionary<Document, FakeResolutionScope> perDocumentScopes = [];

        public void AddForDocument(Document document, FakeResolutionScope scope)
        {
            perDocumentScopes.Add(document, scope);
        }

        public IResolutionScope CreateForDocument(Document document)
        {
            return perDocumentScopes.TryGetValue(document, out var scope) ? scope : DefaultScope;
        }
    }

    protected FakeAssetCache AssetCache { get; }
    protected BehaviorFactory BehaviorFactory { get; }
    protected FakeResolutionScopeFactory ResolutionScopeFactory { get; }
    protected FakeResolutionScope ResolutionScope { get; }

    private readonly IValueConverterFactory valueConverterFactory;
    private readonly IValueSourceFactory valueSourceFactory;
    private readonly IViewFactory viewFactory;
    private readonly IViewBinder viewBinder;

    public BindingTests(ITestOutputHelper output)
    {
        Logger.Monitor = new TestMonitor(output);
        viewFactory = new RootViewFactory([]);
        AssetCache = new FakeAssetCache();
        valueSourceFactory = new ValueSourceFactory(AssetCache);
        valueConverterFactory = new RootValueConverterFactory([]);
        var attributeBindingFactory = new AttributeBindingFactory(valueSourceFactory, valueConverterFactory);
        var eventBindingFactory = new EventBindingFactory(valueSourceFactory, valueConverterFactory);
        ResolutionScopeFactory = new FakeResolutionScopeFactory();
        ResolutionScope = ResolutionScopeFactory.DefaultScope;
        BehaviorFactory = new();
        viewBinder = new ReflectionViewBinder(attributeBindingFactory, eventBindingFactory);
    }

    protected IViewNode BuildTreeFromMarkup(string markup, object model)
    {
        var viewNodeFactory = new ViewNodeFactory(
            viewFactory,
            valueSourceFactory,
            valueConverterFactory,
            viewBinder,
            AssetCache,
            ResolutionScopeFactory,
            BehaviorFactory
        );
        var document = Document.Parse(markup);
        var tree = viewNodeFactory.CreateNode(document);
        tree.Context = BindingContext.Create(model);
        tree.Update();
        return tree;
    }
}
