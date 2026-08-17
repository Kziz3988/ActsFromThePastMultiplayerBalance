using System.Threading.Tasks;
using HarmonyLib;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using ActsFromThePast.Acts.TheBeyond.Enemies;
using ActsFromThePastMultiplayerBalance.Code.Powers;
using ActsFromThePast;
using System.Collections.Generic;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Vfx.Utilities;
using MegaCrit.Sts2.Core.Models;
using System.Reflection;
namespace ActsFromThePastMultiplayerBalance.Patches;

[HarmonyPatch(typeof(GremlinNob))]
public static class GremlinNobPatch
{
	[HarmonyPrefix]
	[HarmonyPatch("Bellow")]
	static bool BellowPatch(GremlinNob __instance, ref Task __result, IReadOnlyList<Creature> targets)
	{
		__result = BellowAsync(__instance);
        return false;
	}

	static async Task BellowAsync(GremlinNob instance)
	{
        typeof(GremlinNob).GetMethod("PlayerBellowSfx", BindingFlags.NonPublic | BindingFlags.Instance)?.Invoke(instance, null);
        TalkCmd.Play(MonsterModel.L10NMonsterLookup("ACTSFROMTHEPAST-GREMLIN_NOB.moves.BELLOW.banter"), instance.Creature, VfxColor.Red, VfxDuration.VeryLong);
        VfxCmd.PlayOnCreatureCenter(instance.Creature, "vfx/vfx_scream");
        NGame.Instance?.ScreenShake(ShakeStrength.Strong, ShakeDuration.Long);
        await Cmd.Wait(0.8f);
        int EnrageAmount = (int?)typeof(GremlinNob)?.GetProperty("EnrageAmount", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(instance) ?? 2;
		await PowerCmd.Apply<MultiplayerEnragePower>(new ThrowingPlayerChoiceContext(), instance.Creature, EnrageAmount, instance.Creature, null);
	}
}
