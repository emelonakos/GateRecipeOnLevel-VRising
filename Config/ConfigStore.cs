using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;

namespace LevelRecipeGate.Config
{
	internal static class ConfigStore
	{
		private const int ReloadDebounceMilliseconds = 500;
		private static string ConfigDir => Path.Combine("BepInEx", "config", "LevelRecipeGate");
		private static readonly object SyncRoot = new object();
		private static FileSystemWatcher _fileWatcher;
		private static Timer _debounceTimer;
		private static bool _disposed;

		public static string LevelRecipeCfgFile => Path.Combine(ConfigDir, "level_recipe_blocks.json");

		internal static readonly Dictionary<int, int> RecipeMinLevelByGuid = new Dictionary<int, int>();
		internal static bool LevelRecipeBlocksEnabled = true;
		internal static string LevelRecipeBlockedMessage = "You cannot craft this yet. Required gear level: {level}.";

		private sealed class LevelRecipeConfigFile
		{
			public bool enabled { get; set; } = true;
			public string message { get; set; } = "You cannot craft this yet. Required gear level: {level}.";
			public List<LevelRecipeBlockEntry> recipe_level_blocks { get; set; } = new List<LevelRecipeBlockEntry>();
		}

		private sealed class LevelRecipeBlockEntry
		{
			public int min_level { get; set; }
			public List<object> recipes { get; set; } = new List<object>();
		}

		internal static void EnsureConfigsExist()
		{
			Directory.CreateDirectory(ConfigDir);

			if (!File.Exists(LevelRecipeCfgFile))
			{
				var example = new LevelRecipeConfigFile
				{
					enabled = true,
					message = "You cannot craft this yet. Required gear level: {level}.",
					recipe_level_blocks = CreateDefaultLevelRecipeBlocks()
				};

				File.WriteAllText(LevelRecipeCfgFile, JsonSerializer.Serialize(example, new JsonSerializerOptions { WriteIndented = true }));
			}
		}

		private static List<LevelRecipeBlockEntry> CreateDefaultLevelRecipeBlocks()
		{
			return new List<LevelRecipeBlockEntry>
			{
				new LevelRecipeBlockEntry
				{
					min_level = 33,
					recipes = new List<object>
					{
						"Recipe_Weapon_Axe_T05_Iron",
						"Recipe_Weapon_Claws_T05_Iron",
						"Recipe_Weapon_Crossbow_T05_Iron",
						"Recipe_Weapon_Daggers_T05_Iron",
						"Recipe_Weapon_GreatSword_T05_Iron",
						"Recipe_Weapon_Longbow_T05_Iron",
						"Recipe_Weapon_Mace_T05_Iron",
						"Recipe_Weapon_Pistols_T05_Iron",
						"Recipe_Weapon_Reaper_T05_Iron",
						"Recipe_Weapon_Slashers_T05_Iron",
						"Recipe_Weapon_Spear_T05_Iron",
						"Recipe_Weapon_Sword_T05_Iron",
						"Recipe_Weapon_TwinBlades_T05_Iron",
						"Recipe_Weapon_Whip_T05_Iron",
						"Item_MagicSource_General_T05_Relic"
					}
				},
				new LevelRecipeBlockEntry
				{
					min_level = 50,
					recipes = new List<object>
					{
						"Recipe_Armor_Boots_T06_Iron_Brute",
						"Recipe_Armor_Boots_T06_Iron_Rogue",
						"Recipe_Armor_Boots_T06_Iron_Scholar",
						"Recipe_Armor_Boots_T06_Iron_Warrior",
						"Recipe_Armor_Chest_T06_Iron_Brute",
						"Recipe_Armor_Chest_T06_Iron_Rogue",
						"Recipe_Armor_Chest_T06_Iron_Scholar",
						"Recipe_Armor_Chest_T06_Iron_Warrior",
						"Recipe_Armor_Gloves_T06_Iron_Brute",
						"Recipe_Armor_Gloves_T06_Iron_Rogue",
						"Recipe_Armor_Gloves_T06_Iron_Scholar",
						"Recipe_Armor_Gloves_T06_Iron_Warrior",
						"Recipe_Armor_Legs_T06_Iron_Brute",
						"Recipe_Armor_Legs_T06_Iron_Rogue",
						"Recipe_Armor_Legs_T06_Iron_Scholar",
						"Recipe_Armor_Legs_T06_Iron_Warrior",
						"Recipe_MagicSource_General_T06_AmethystPendant",
						"Recipe_MagicSource_General_T06_EmeraldNecklace",
						"Recipe_MagicSource_General_T06_MistStoneNecklace",
						"Recipe_MagicSource_General_T06_RubyPendant",
						"Recipe_MagicSource_General_T06_SapphirePendant",
						"Recipe_MagicSource_General_T06_TopazAmulet",
						"Recipe_Weapon_Axe_T06_Iron_Reinforced",
						"Recipe_Weapon_Claws_T06_Iron_Reinforced",
						"Recipe_Weapon_Crossbow_T06_Iron_Reinforced",
						"Recipe_Weapon_Daggers_T06_Iron_Reinforced",
						"Recipe_Weapon_GreatSword_T06_Iron_Reinforced",
						"Recipe_Weapon_Longbow_T06_Iron_Reinforced",
						"Recipe_Weapon_Mace_T06_Iron_Reinforced",
						"Recipe_Weapon_Pistols_T06_Iron_Reinforced",
						"Recipe_Weapon_Reaper_T06_Iron_Reinforced",
						"Recipe_Weapon_Slashers_T06_Iron_Reinforced",
						"Recipe_Weapon_Spear_T06_Iron_Reinforced",
						"Recipe_Weapon_Sword_T06_Iron_Reinforced",
						"Recipe_Weapon_TwinBlades_T06_Iron_Reinforced",
						"Recipe_Weapon_Whip_T06_Iron_Reinforced"
					}
				},
				new LevelRecipeBlockEntry
				{
					min_level = 65,
					recipes = new List<object>
					{
						"Recipe_Weapon_Axe_T07_DarkSilver",
						"Recipe_Weapon_Claws_T07_DarkSilver",
						"Recipe_Weapon_Crossbow_T07_DarkSilver",
						"Recipe_Weapon_Daggers_T07_DarkSilver",
						"Recipe_Weapon_GreatSword_T07_DarkSilver",
						"Recipe_Weapon_Longbow_T07_DarkSilver",
						"Recipe_Weapon_Mace_T07_DarkSilver",
						"Recipe_Weapon_Pistols_T07_DarkSilver",
						"Recipe_Weapon_Reaper_T07_DarkSilver",
						"Recipe_Weapon_Slashers_T07_DarkSilver",
						"Recipe_Weapon_Spear_T07_DarkSilver",
						"Recipe_Weapon_Sword_T07_DarkSilver",
						"Recipe_Weapon_TwinBlades_T07_DarkSilver",
						"Recipe_Weapon_Whip_T07_DarkSilver",
						"Recipe_MagicSource_General_T07_BloodwineAmulet"
					}
				},
				new LevelRecipeBlockEntry
				{
					min_level = 74,
					recipes = new List<object>
					{
						"Recipe_Armor_Boots_T08_DarkSilver_Brute",
						"Recipe_Armor_Boots_T08_DarkSilver_Rogue",
						"Recipe_Armor_Boots_T08_DarkSilver_Scholar",
						"Recipe_Armor_Boots_T08_DarkSilver_Warrior",
						"Recipe_Armor_Chest_T08_DarkSilver_Brute",
						"Recipe_Armor_Chest_T08_DarkSilver_Rogue",
						"Recipe_Armor_Chest_T08_DarkSilver_Scholar",
						"Recipe_Armor_Chest_T08_DarkSilver_Warrior",
						"Recipe_Armor_Gloves_T08_DarkSilver_Brute",
						"Recipe_Armor_Gloves_T08_DarkSilver_Rogue",
						"Recipe_Armor_Gloves_T08_DarkSilver_Scholar",
						"Recipe_Armor_Gloves_T08_DarkSilver_Warrior",
						"Recipe_Armor_Legs_T08_DarkSilver_Brute",
						"Recipe_Armor_Legs_T08_DarkSilver_Rogue",
						"Recipe_Armor_Legs_T08_DarkSilver_Scholar",
						"Recipe_Armor_Legs_T08_DarkSilver_Warrior",
						"Recipe_Weapon_Axe_T08_Sanguine",
						"Recipe_Weapon_Claws_T08_Sanguine",
						"Recipe_Weapon_Crossbow_T08_Sanguine",
						"Recipe_Weapon_Daggers_T08_Sanguine",
						"Recipe_Weapon_GreatSword_T08_Sanguine",
						"Recipe_Weapon_Longbow_T08_Sanguine",
						"Recipe_Weapon_Mace_T08_Sanguine",
						"Recipe_Weapon_Pistols_T08_Sanguine",
						"Recipe_Weapon_Reaper_T08_Sanguine",
						"Recipe_Weapon_Slashers_T08_Sanguine",
						"Recipe_Weapon_Spear_T08_Sanguine",
						"Recipe_Weapon_Sword_T08_Sanguine",
						"Recipe_Weapon_TwinBlades_T08_Sanguine",
						"Recipe_Weapon_Whip_T08_Sanguine",
						"Recipe_MagicSource_General_T08_Beast",
						"Recipe_MagicSource_General_T08_CrimsonSky",
						"Recipe_MagicSource_General_T08_Delusion",
						"Recipe_MagicSource_General_T08_FrozenCrypt",
						"Recipe_MagicSource_General_T08_Madness",
						"Recipe_MagicSource_General_T08_WickedProphet",
						"Item_Vampire_Coating_Frost",
						"Item_Vampire_Coating_Blood",
						"Item_Vampire_Coating_Chaos",
						"Item_Vampire_Coating_Illusion",
						"Item_Vampire_Coating_Storm",
						"Item_Vampire_Coating_Unholy"
					}
				},
				new LevelRecipeBlockEntry
				{
					min_level = 80,
					recipes = new List<object>()
				}
			};
		}

		internal static void InitializeFileWatcher()
		{
			Directory.CreateDirectory(ConfigDir);

			if (_fileWatcher != null)
			{
				_fileWatcher.Changed -= OnConfigFileChanged;
				_fileWatcher.Created -= OnConfigFileChanged;
				_fileWatcher.Renamed -= OnConfigFileChanged;
				_fileWatcher.Dispose();
			}

			_disposed = false;
			_fileWatcher = new FileSystemWatcher
			{
				Path = ConfigDir,
				Filter = Path.GetFileName(LevelRecipeCfgFile),
				NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size | NotifyFilters.FileName,
				EnableRaisingEvents = true
			};

			_fileWatcher.Changed += OnConfigFileChanged;
			_fileWatcher.Created += OnConfigFileChanged;
			_fileWatcher.Renamed += OnConfigFileChanged;
		}

		internal static void DisposeFileWatcher()
		{
			lock (SyncRoot)
			{
				_disposed = true;
				_debounceTimer?.Dispose();
				_debounceTimer = null;

				if (_fileWatcher != null)
				{
					_fileWatcher.Changed -= OnConfigFileChanged;
					_fileWatcher.Created -= OnConfigFileChanged;
					_fileWatcher.Renamed -= OnConfigFileChanged;
					_fileWatcher.Dispose();
					_fileWatcher = null;
				}
			}
		}

		internal static void LoadLevelRecipeBlocksFromDisk()
		{
			try
			{
				EnsureConfigsExist();

				string json = File.ReadAllText(LevelRecipeCfgFile);
				var cfg = JsonSerializer.Deserialize<LevelRecipeConfigFile>(
					json,
					new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

				if (cfg == null)
				{
					Plugin.Logger.LogWarning($"[{Plugin.Name}] Level recipe config was empty.");
					return;
				}

				var loadedRecipes = new Dictionary<int, int>();
				bool enabled = cfg.enabled;
				string message = string.IsNullOrWhiteSpace(cfg.message)
					? "You cannot craft this yet. Required gear level: {level}."
					: cfg.message;
				bool needsRewrite = false;

				if (cfg.recipe_level_blocks != null)
				{
					foreach (var block in cfg.recipe_level_blocks)
					{
						if (block == null || block.recipes == null)
							continue;

						foreach (object recipe in block.recipes)
						{
							if (!TryReadRecipeGuid(recipe, out int guid, out bool shouldRewriteRecipe))
							{
								Plugin.Logger.LogWarning($"[{Plugin.Name}] Ignoring unknown recipe prefab '{recipe}' in {LevelRecipeCfgFile}.");
								continue;
							}

							needsRewrite |= shouldRewriteRecipe;

							// If the same recipe appears twice, keep the highest level requirement.
							if (!loadedRecipes.TryGetValue(guid, out int existing) || block.min_level > existing)
								loadedRecipes[guid] = block.min_level;
						}
					}
				}

				lock (SyncRoot)
				{
					RecipeMinLevelByGuid.Clear();
					foreach (var kvp in loadedRecipes)
						RecipeMinLevelByGuid[kvp.Key] = kvp.Value;

					LevelRecipeBlocksEnabled = enabled;
					LevelRecipeBlockedMessage = message;
				}

				Plugin.Logger.LogInfo($"[{Plugin.Name}] Loaded {loadedRecipes.Count} level-gated recipe GUID(s). Enabled={enabled}.");

				if (needsRewrite)
					SaveLevelRecipeBlocksToDisk();
			}
			catch (Exception ex)
			{
				Plugin.Logger.LogError($"[{Plugin.Name}] Failed loading level recipe config: {ex}");
			}
		}

		internal static void SaveLevelRecipeBlocksToDisk()
		{
			try
			{
				Directory.CreateDirectory(ConfigDir);

				Dictionary<int, int> snapshot = GetRecipeLevelGatesSnapshot();
				bool enabled;
				string message;

				lock (SyncRoot)
				{
					enabled = LevelRecipeBlocksEnabled;
					message = LevelRecipeBlockedMessage;
				}

				var grouped = snapshot
					.GroupBy(kvp => kvp.Value)
					.OrderBy(g => g.Key)
					.Select(g => new LevelRecipeBlockEntry
					{
						min_level = g.Key,
						recipes = g
							.Select(kvp => RecipePrefabLookup.ToConfigValue(kvp.Key))
							.OrderBy(value => value.ToString())
							.ToList()
					})
					.ToList();

				var cfg = new LevelRecipeConfigFile
				{
					enabled = enabled,
					message = message,
					recipe_level_blocks = grouped
				};

				File.WriteAllText(LevelRecipeCfgFile, JsonSerializer.Serialize(cfg, new JsonSerializerOptions { WriteIndented = true }));
			}
			catch (Exception ex)
			{
				Plugin.Logger.LogError($"[{Plugin.Name}] Failed saving level recipe config: {ex}");
			}
		}

		internal static bool TryGetRequiredLevel(int recipeGuid, out int requiredLevel)
		{
			lock (SyncRoot)
			{
				requiredLevel = 0;
				return LevelRecipeBlocksEnabled && RecipeMinLevelByGuid.TryGetValue(recipeGuid, out requiredLevel);
			}
		}

		internal static Dictionary<int, int> GetRecipeLevelGatesSnapshot()
		{
			lock (SyncRoot)
			{
				return new Dictionary<int, int>(RecipeMinLevelByGuid);
			}
		}

		internal static bool TryResolveRecipePrefab(string value, out int recipeGuid)
		{
			return RecipePrefabLookup.TryResolve(value, out recipeGuid);
		}

		internal static string FormatRecipePrefab(int recipeGuid)
		{
			return RecipePrefabLookup.Format(recipeGuid);
		}

		internal static void SetRecipeLevelGate(int recipeGuid, int minLevel)
		{
			lock (SyncRoot)
			{
				RecipeMinLevelByGuid[recipeGuid] = minLevel;
			}

			SaveLevelRecipeBlocksToDisk();
		}

		internal static bool RemoveRecipeLevelGate(int recipeGuid)
		{
			bool removed;
			lock (SyncRoot)
			{
				removed = RecipeMinLevelByGuid.Remove(recipeGuid);
			}

			if (removed)
				SaveLevelRecipeBlocksToDisk();
			return removed;
		}

		private static void OnConfigFileChanged(object sender, FileSystemEventArgs e)
		{
			lock (SyncRoot)
			{
				if (_disposed)
					return;

				_debounceTimer?.Dispose();
				_debounceTimer = new Timer(_ => ReloadFromWatcher(), null, TimeSpan.FromMilliseconds(ReloadDebounceMilliseconds), Timeout.InfiniteTimeSpan);
			}
		}

		private static void ReloadFromWatcher()
		{
			if (_disposed || !File.Exists(LevelRecipeCfgFile))
				return;

			Plugin.Logger.LogInfo($"[{Plugin.Name}] Detected config change, reloading {Path.GetFileName(LevelRecipeCfgFile)}.");
			LoadLevelRecipeBlocksFromDisk();
		}

		private static bool TryReadRecipeGuid(object value, out int recipeGuid, out bool shouldRewrite)
		{
			recipeGuid = 0;
			shouldRewrite = false;

			if (value is JsonElement element)
			{
				if (element.ValueKind == JsonValueKind.Number && element.TryGetInt32(out recipeGuid))
				{
					shouldRewrite = RecipePrefabLookup.HasName(recipeGuid);
					return true;
				}

				if (element.ValueKind == JsonValueKind.String)
					return TryReadRecipeGuidString(element.GetString(), out recipeGuid, out shouldRewrite);

				return false;
			}

			if (value is int intValue)
			{
				recipeGuid = intValue;
				shouldRewrite = RecipePrefabLookup.HasName(recipeGuid);
				return true;
			}

			if (value is long longValue && longValue >= int.MinValue && longValue <= int.MaxValue)
			{
				recipeGuid = (int)longValue;
				shouldRewrite = RecipePrefabLookup.HasName(recipeGuid);
				return true;
			}

			if (value is string stringValue)
				return TryReadRecipeGuidString(stringValue, out recipeGuid, out shouldRewrite);

			return value != null && TryReadRecipeGuidString(value.ToString(), out recipeGuid, out shouldRewrite);
		}

		private static bool TryReadRecipeGuidString(string value, out int recipeGuid, out bool shouldRewrite)
		{
			recipeGuid = 0;
			shouldRewrite = false;

			if (!RecipePrefabLookup.TryResolve(value, out recipeGuid))
				return false;

			shouldRewrite = int.TryParse(value, out _) && RecipePrefabLookup.HasName(recipeGuid);
			return true;
		}
	}
}
