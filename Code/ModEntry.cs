using HarmonyLib;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Modding;

namespace ActsFromThePastMultiplayerBalance;

[ModInitializer("Init")]
public static class ModEntry
{
    private static Harmony? _harmony;
    public const string ModId = "ActsFromThePastMultiplayerBalance";
    public const string ResPath = $"res://{ModId}";

    public static MegaCrit.Sts2.Core.Logging.Logger Logger { get; } = new(ModId, MegaCrit.Sts2.Core.Logging.LogType.Generic);

    public static void Init()
    {
        Log.Info($"[{ModId}] Initializing...");

        _harmony = new Harmony($"com.kziz3988.{ModId}");
        _harmony.PatchAll();

        Log.Info($"[{ModId}] Loaded successfully.");
    }
}
