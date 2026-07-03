using StardewModdingAPI.Events;
using StardewUI.Animation;
using StardewUI.Input;
using StardewUI.ModIntegration;

namespace StardewUI;

/// <summary>
/// Entry point for Stardew UI. Must be called from <see cref="Mod.Entry(IModHelper)"/>.
/// </summary>
public static partial class UI
{
    /// <summary>
    /// Helper for game input.
    /// </summary>
    internal static IInputHelper InputHelper => EnsureInitialized(() => modHelper.Input);

    /// <summary>
    /// The main thread ID, i.e. whatever thread calls UI.Initialize
    /// </summary>
    private static int mainThreadId = 0;
    internal static bool IsMainThread => mainThreadId == Environment.CurrentManagedThreadId;

    private static IModHelper modHelper = null!;

    /// <summary>
    /// Initialize the framework.
    /// </summary>
    /// <param name="helper">Helper for the calling mod.</param>
    /// <param name="monitor">SMAPI logging helper.</param>
    public static void Initialize(IModHelper helper, IMonitor monitor)
    {
        if (modHelper is not null)
        {
            throw new InvalidOperationException("UI is already initialized.");
        }
        modHelper = helper;
        Logger.Monitor = monitor;
        helper.Events.GameLoop.UpdateTicked += GameLoop_UpdateTicked;
        helper.Events.GameLoop.GameLaunched += GameLoop_GameLaunched;
        mainThreadId = Environment.CurrentManagedThreadId;
    }

    private static void GameLoop_GameLaunched(object? sender, GameLaunchedEventArgs e)
    {
        // mod integrations
        LookupAnythingIntegration.Initialize(modHelper);
        StardewAccessIntegration.Initialize(modHelper);
    }

    private static T EnsureInitialized<T>(Func<T> selector)
    {
        if (modHelper is null)
        {
            throw new InvalidOperationException(
                "StardewUI has not been initialized. Ensure you've called UI.Initialize(helper) from your mod's "
                    + "Entry method."
            );
        }
        return selector();
    }

    private static void GameLoop_UpdateTicked(object? sender, UpdateTickedEventArgs e)
    {
        var elapsed = Game1.currentGameTime.ElapsedGameTime;
        AnimationRunner.Tick(elapsed);
        if (Game1.keyboardDispatcher?.Subscriber is ICaptureTarget captureTarget)
        {
            captureTarget.Update(elapsed);
        }
    }
}
