using System.Threading.Tasks;
using HarmonyLib;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using ActsFromThePastMultiplayerBalance.Code.Powers;
using ActsFromThePast;
using System.Reflection;
using System;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Combat;
namespace ActsFromThePastMultiplayerBalance.Patches;

[HarmonyPatch(typeof(GremlinMad))]
public static class GremlinMadPatch
{
	[HarmonyPrefix]
	[HarmonyPatch("AfterAddedToRoom")]
	static bool AfterAddedToRoomPatch(GremlinMad __instance, ref Task __result)
	{
		__result = AfterAddedToRoomAsync(__instance);
		return false;
	}

	static Task BaseAfterAddedToRoom()
    {
        return Task.CompletedTask;
    }

	static async Task AfterAddedToRoomAsync(GremlinMad instance)
	{
		await BaseAfterAddedToRoom();
        int AngryAmount = (int?)typeof(GremlinMad)?.GetProperty("AngryAmount", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(instance) ?? 1;
        await PowerCmd.Apply<MultiplayerAngryPower>(new ThrowingPlayerChoiceContext(), instance.Creature, AngryAmount, instance.Creature, null);
		MethodInfo? methodInfo = typeof(GremlinMad).GetMethod("OnDeath", BindingFlags.NonPublic | BindingFlags.Instance);
        if (methodInfo != null)
        {
            var OnDeath = (Action<Creature>)Delegate.CreateDelegate(typeof(Action<Creature>), instance, methodInfo);
            instance.Creature.Died += OnDeath;
        }
		GremlinLeaderHelper.SubscribeToLeaderDeath(instance.Creature, (CombatState)instance.CombatState);
	}
}
