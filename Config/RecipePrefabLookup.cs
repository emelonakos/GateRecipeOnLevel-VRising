using System;
using System.Collections.Generic;

namespace LevelRecipeGate.Config
{
	internal static class RecipePrefabLookup
	{
		private static readonly Dictionary<string, int> NameToGuid = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
		{
			{ "Recipe_Weapon_Axe_T01_Bone", -837028877 },
			{ "Recipe_Weapon_Axe_T02_Bone_Reinforced", 1031414138 },
			{ "Recipe_Weapon_Axe_T03_Copper", -1864396632 },
			{ "Recipe_Weapon_Axe_T04_Copper_Reinforced", -411123427 },
			{ "Recipe_Weapon_Axe_T05_Iron", 305819079 },
			{ "Recipe_Weapon_Axe_T06_Iron_Reinforced", 690858507 },
			{ "Recipe_Weapon_Axe_T07_DarkSilver", -1896566066 },
			{ "Recipe_Weapon_Axe_T08_Sanguine", -67490827 },
			{ "Recipe_Weapon_Axe_T09_ShadowMatter", -998610023 },
			{ "Recipe_Weapon_Claws_T05_Iron", -1520452495 },
			{ "Recipe_Weapon_Claws_T06_Iron_Reinforced", -1690827442 },
			{ "Recipe_Weapon_Claws_T07_DarkSilver", 1020521578 },
			{ "Recipe_Weapon_Claws_T08_Sanguine", -749910443 },
			{ "Recipe_Weapon_Crossbow_T01_Bone", -1384817143 },
			{ "Recipe_Weapon_Crossbow_T02_Bone_Reinforced", -1421664082 },
			{ "Recipe_Weapon_Crossbow_T03_Copper", 841082368 },
			{ "Recipe_Weapon_Crossbow_T04_Copper_Reinforced", -283375796 },
			{ "Recipe_Weapon_Crossbow_T05_Iron", 1268051742 },
			{ "Recipe_Weapon_Crossbow_T06_Iron_Reinforced", 1341382268 },
			{ "Recipe_Weapon_Crossbow_T07_DarkSilver", -971743976 },
			{ "Recipe_Weapon_Crossbow_T08_Sanguine", -1064000514 },
			{ "Recipe_Weapon_Crossbow_T09_ShadowMatter", -178724798 },
			{ "Recipe_Weapon_Daggers_T05_Iron", 908837210 },
			{ "Recipe_Weapon_Daggers_T06_Iron_Reinforced", -328931595 },
			{ "Recipe_Weapon_Daggers_T07_DarkSilver", 847424089 },
			{ "Recipe_Weapon_Daggers_T08_Sanguine", 268825874 },
			{ "Recipe_Weapon_FishingPole_T01", 319663209 },
			{ "Recipe_Weapon_GreatSword_T05_Iron", 1731901666 },
			{ "Recipe_Weapon_GreatSword_T06_Iron_Reinforced", 648459378 },
			{ "Recipe_Weapon_GreatSword_T07_DarkSilver", -2116357114 },
			{ "Recipe_Weapon_GreatSword_T08_Sanguine", 1944286219 },
			{ "Recipe_Weapon_GreatSword_T09_ShadowMatter", -1525227854 },
			{ "Recipe_Weapon_Longbow_T03_Copper", -514405267 },
			{ "Recipe_Weapon_Longbow_T04_Copper_Reinforced", 777859879 },
			{ "Recipe_Weapon_Longbow_T05_Iron", -80393444 },
			{ "Recipe_Weapon_Longbow_T06_Iron_Reinforced", -149592989 },
			{ "Recipe_Weapon_Longbow_T07_DarkSilver", -1063439615 },
			{ "Recipe_Weapon_Longbow_T08_Sanguine", -603557479 },
			{ "Recipe_Weapon_Longbow_T09_ShadowMatter", 1378881717 },
			{ "Recipe_Weapon_LumberjackAxe_T01_Trader", 2136704250 },
			{ "Recipe_Weapon_Mace_T01_Bone", -1064109772 },
			{ "Recipe_Weapon_Mace_T02_Bone_Reinforced", 1377610318 },
			{ "Recipe_Weapon_Mace_T03_Copper", -356991727 },
			{ "Recipe_Weapon_Mace_T04_Copper_Reinforced", 897446828 },
			{ "Recipe_Weapon_Mace_T05_Iron", -612459251 },
			{ "Recipe_Weapon_Mace_T06_Iron_Reinforced", -1538728965 },
			{ "Recipe_Weapon_Mace_T07_DarkSilver", 532951453 },
			{ "Recipe_Weapon_Mace_T08_Sanguine", -1492594940 },
			{ "Recipe_Weapon_Mace_T09_ShadowMatter", -240353582 },
			{ "Recipe_Weapon_MinersMace_T01_Trader", -1476908192 },
			{ "Recipe_Weapon_Pistols_T05_Iron", 1314793960 },
			{ "Recipe_Weapon_Pistols_T06_Iron_Reinforced", -1015239074 },
			{ "Recipe_Weapon_Pistols_T07_DarkSilver", -296690999 },
			{ "Recipe_Weapon_Pistols_T08_Sanguine", 1058461467 },
			{ "Recipe_Weapon_Pistols_T09_ShadowMatter", -299780538 },
			{ "Recipe_Weapon_Reaper_T01_Bone", 1678839668 },
			{ "Recipe_Weapon_Reaper_T02_Bone_Reinforced", -238493462 },
			{ "Recipe_Weapon_Reaper_T03_Copper", 787254471 },
			{ "Recipe_Weapon_Reaper_T04_Copper_Reinforced", -681071811 },
			{ "Recipe_Weapon_Reaper_T05_Iron", 1109951557 },
			{ "Recipe_Weapon_Reaper_T06_Iron_Reinforced", 537685806 },
			{ "Recipe_Weapon_Reaper_T07_DarkSilver", -1112081437 },
			{ "Recipe_Weapon_Reaper_T08_Sanguine", -1816552963 },
			{ "Recipe_Weapon_Reaper_T09_ShadowMatter", -884753903 },
			{ "Recipe_Weapon_Slashers_T01_Bone", -1536889801 },
			{ "Recipe_Weapon_Slashers_T02_Bone_Reinforced", 1679813913 },
			{ "Recipe_Weapon_Slashers_T03_Copper", -1560601100 },
			{ "Recipe_Weapon_Slashers_T04_Copper_Reinforced", 396156173 },
			{ "Recipe_Weapon_Slashers_T05_Iron", -808348493 },
			{ "Recipe_Weapon_Slashers_T06_Iron_Reinforced", 1469893872 },
			{ "Recipe_Weapon_Slashers_T07_DarkSilver", -1919160227 },
			{ "Recipe_Weapon_Slashers_T08_Sanguine", 373339628 },
			{ "Recipe_Weapon_Slashers_T09_ShadowMatter", 501702204 },
			{ "Recipe_Weapon_Spear_T01_Bone", 1394854694 },
			{ "Recipe_Weapon_Spear_T02_Bone_Reinforced", -1328539101 },
			{ "Recipe_Weapon_Spear_T03_Copper", -791471134 },
			{ "Recipe_Weapon_Spear_T04_Copper_Reinforced", -118222260 },
			{ "Recipe_Weapon_Spear_T05_Iron", 239811022 },
			{ "Recipe_Weapon_Spear_T06_Iron_Reinforced", -499925914 },
			{ "Recipe_Weapon_Spear_T07_DarkSilver", -194303255 },
			{ "Recipe_Weapon_Spear_T08_Sanguine", -314047482 },
			{ "Recipe_Weapon_Spear_T09_ShadowMatter", -190896313 },
			{ "Recipe_Weapon_Sword_T01_Bone", -2125590443 },
			{ "Recipe_Weapon_Sword_T02_Bone_Reinforced", 1742703328 },
			{ "Recipe_Weapon_Sword_T03_Copper", -267802321 },
			{ "Recipe_Weapon_Sword_T04_Copper_Reinforced", 774557022 },
			{ "Recipe_Weapon_Sword_T05_Iron", -2098625697 },
			{ "Recipe_Weapon_Sword_T06_Iron_Reinforced", -1052674868 },
			{ "Recipe_Weapon_Sword_T07_DarkSilver", 374085302 },
			{ "Recipe_Weapon_Sword_T08_Sanguine", 895742048 },
			{ "Recipe_Weapon_Sword_T09_ShadowMatter", 1363919271 },
			{ "Recipe_Weapon_TwinBlades_T05_Iron", -496801516 },
			{ "Recipe_Weapon_TwinBlades_T06_Iron_Reinforced", 1687058710 },
			{ "Recipe_Weapon_TwinBlades_T07_DarkSilver", 895579931 },
			{ "Recipe_Weapon_TwinBlades_T08_Sanguine", 1259720344 },
			{ "Recipe_Weapon_Whip_T05_Iron", 688528978 },
			{ "Recipe_Weapon_Whip_T06_Iron_Reinforced", 465080212 },
			{ "Recipe_Weapon_Whip_T07_DarkSilver", 1507781061 },
			{ "Recipe_Weapon_Whip_T08_Sanguine", -1968497565 },
			{ "Recipe_Weapon_Whip_T09_ShadowMatter", -941901707 }
		};

		private static readonly Dictionary<int, string> GuidToName = BuildGuidToName();

		internal static bool TryResolve(string value, out int guid)
		{
			guid = 0;

			if (string.IsNullOrWhiteSpace(value))
				return false;

			string trimmed = value.Trim();
			if (NameToGuid.TryGetValue(trimmed, out guid))
				return true;

			return int.TryParse(trimmed, out guid);
		}

		internal static string Format(int guid)
		{
			return GuidToName.TryGetValue(guid, out string name)
				? name
				: guid.ToString();
		}

		internal static object ToConfigValue(int guid)
		{
			return GuidToName.TryGetValue(guid, out string name)
				? (object)name
				: guid;
		}

		internal static bool HasName(int guid)
		{
			return GuidToName.ContainsKey(guid);
		}

		private static Dictionary<int, string> BuildGuidToName()
		{
			var result = new Dictionary<int, string>();
			foreach (var kvp in NameToGuid)
			{
				if (!result.ContainsKey(kvp.Value))
					result[kvp.Value] = kvp.Key;
			}

			return result;
		}
	}
}
