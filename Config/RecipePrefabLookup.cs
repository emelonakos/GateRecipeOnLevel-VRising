using System;
using System.Collections.Generic;

namespace LevelRecipeGate.Config
{
	internal static class RecipePrefabLookup
	{
		private static readonly Dictionary<string, int> NameToGuid = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
		{
			{ "Item_MagicSource_General_T05_Relic", -650855520 },
			{ "Item_Vampire_Coating_Blood", -1617973064 },
			{ "Item_Vampire_Coating_Chaos", -1051190225 },
			{ "Item_Vampire_Coating_Frost", -1087318964 },
			{ "Item_Vampire_Coating_Illusion", 1148284648 },
			{ "Item_Vampire_Coating_Storm", 2128629897 },
			{ "Item_Vampire_Coating_Unholy", 1002732935 },
			{ "Recipe_Armor_Boots_T06_Iron_Brute", 564937663 },
			{ "Recipe_Armor_Boots_T06_Iron_Rogue", -501436877 },
			{ "Recipe_Armor_Boots_T06_Iron_Scholar", -1790839980 },
			{ "Recipe_Armor_Boots_T06_Iron_Warrior", 1598255582 },
			{ "Recipe_Armor_Boots_T08_DarkSilver_Brute", 481223129 },
			{ "Recipe_Armor_Boots_T08_DarkSilver_Rogue", 1020324654 },
			{ "Recipe_Armor_Boots_T08_DarkSilver_Scholar", 26969974 },
			{ "Recipe_Armor_Boots_T08_DarkSilver_Warrior", -1671420432 },
			{ "Recipe_Armor_Chest_T06_Iron_Brute", 1640689004 },
			{ "Recipe_Armor_Chest_T06_Iron_Rogue", -921085381 },
			{ "Recipe_Armor_Chest_T06_Iron_Scholar", 969479018 },
			{ "Recipe_Armor_Chest_T06_Iron_Warrior", 917114760 },
			{ "Recipe_Armor_Chest_T08_DarkSilver_Brute", 909405972 },
			{ "Recipe_Armor_Chest_T08_DarkSilver_Rogue", 2080647005 },
			{ "Recipe_Armor_Chest_T08_DarkSilver_Scholar", -246992105 },
			{ "Recipe_Armor_Chest_T08_DarkSilver_Warrior", 636393327 },
			{ "Recipe_Armor_Gloves_T06_Iron_Brute", 55560401 },
			{ "Recipe_Armor_Gloves_T06_Iron_Rogue", 550971753 },
			{ "Recipe_Armor_Gloves_T06_Iron_Scholar", 2029351741 },
			{ "Recipe_Armor_Gloves_T06_Iron_Warrior", 1321683558 },
			{ "Recipe_Armor_Gloves_T08_DarkSilver_Brute", -693799437 },
			{ "Recipe_Armor_Gloves_T08_DarkSilver_Rogue", 894482163 },
			{ "Recipe_Armor_Gloves_T08_DarkSilver_Scholar", -494730465 },
			{ "Recipe_Armor_Gloves_T08_DarkSilver_Warrior", 1193907705 },
			{ "Recipe_Armor_Legs_T06_Iron_Brute", 1446070886 },
			{ "Recipe_Armor_Legs_T06_Iron_Rogue", 1989724461 },
			{ "Recipe_Armor_Legs_T06_Iron_Scholar", 1934342576 },
			{ "Recipe_Armor_Legs_T06_Iron_Warrior", 1489561003 },
			{ "Recipe_Armor_Legs_T08_DarkSilver_Brute", 392270656 },
			{ "Recipe_Armor_Legs_T08_DarkSilver_Rogue", 24363319 },
			{ "Recipe_Armor_Legs_T08_DarkSilver_Scholar", 1352971933 },
			{ "Recipe_Armor_Legs_T08_DarkSilver_Warrior", 1912958943 },
			{ "Recipe_MagicSource_General_T06_AmethystPendant", 575942293 },
			{ "Recipe_MagicSource_General_T06_EmeraldNecklace", -1789687685 },
			{ "Recipe_MagicSource_General_T06_MistStoneNecklace", 2113597811 },
			{ "Recipe_MagicSource_General_T06_RubyPendant", 1192551289 },
			{ "Recipe_MagicSource_General_T06_SapphirePendant", 932186802 },
			{ "Recipe_MagicSource_General_T06_TopazAmulet", 1272778289 },
			{ "Recipe_MagicSource_General_T07_BloodwineAmulet", 307631810 },
			{ "Recipe_MagicSource_General_T08_Beast", -590297568 },
			{ "Recipe_MagicSource_General_T08_CrimsonSky", -1485680334 },
			{ "Recipe_MagicSource_General_T08_Delusion", -321571889 },
			{ "Recipe_MagicSource_General_T08_FrozenCrypt", -831940419 },
			{ "Recipe_MagicSource_General_T08_Madness", 1926933208 },
			{ "Recipe_MagicSource_General_T08_WickedProphet", -715761764 },
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
