using HarmonyLib;
using ActsFromThePast.Acts.TheBeyond.Enemies;
using ActsFromThePastMultiplayerBalance.Code.Powers;
using ActsFromThePast.Powers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Localization;
using System.Collections.Generic;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Powers;
namespace ActsFromThePastMultiplayerBalance.Patches;

[HarmonyPatch(typeof(ShiftingStrengthDownPower))]
public static class ShiftingStrengthDownPowerPatch
{
	[HarmonyPrefix]
	[HarmonyPatch(typeof(ShiftingStrengthDownPower), "OriginModel", MethodType.Getter)]
	static bool OriginModelPatch(Transient __instance, ref AbstractModel __result)
	{
		__result = ModelDb.Power<MultiplayerShiftingPower>();
		return false;
	}

    [HarmonyPrefix]
	[HarmonyPatch(typeof(ShiftingStrengthDownPower), "Title", MethodType.Getter)]
	static bool TitlePatch(Transient __instance, ref LocString __result)
	{
		__result = ModelDb.Power<MultiplayerShiftingPower>().Title;
		return false;
	}

    [HarmonyPrefix]
	[HarmonyPatch(typeof(ShiftingStrengthDownPower), "ExtraHoverTips", MethodType.Getter)]
	static bool ExtraHoverTipsPatch(Transient __instance, ref IEnumerable<IHoverTip> __result)
	{
		__result = 
        [
            HoverTipFactory.FromPower<MultiplayerShiftingPower>(),
            HoverTipFactory.FromPower<StrengthPower>()
        ];
		return false;
	}
}
