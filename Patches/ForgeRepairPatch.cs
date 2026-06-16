using System;
using HarmonyLib;
using LevelRecipeGate.Services;
using ProjectM;
using ProjectM.Network;
using ProjectM.Shared;
using Stunlock.Core;
using Unity.Collections;
using Unity.Entities;

namespace LevelRecipeGate.Patches
{
	[HarmonyPatch(typeof(ForgeSystem_Events), nameof(ForgeSystem_Events.OnUpdate))]
	public static class ForgeRepairPatch
	{
		[HarmonyPrefix]
		public static void Prefix(ForgeSystem_Events __instance)
		{
			EcsContext.SetEntityManager(__instance.EntityManager);
			NativeArray<Entity> entities = __instance._RepairItemEventQuery.ToEntityArray(Allocator.Temp);

			try
			{
				foreach (Entity eventEntity in entities)
				{
					try
					{
						if (!eventEntity.Has<FromCharacter>())
							continue;

						FromCharacter fromCharacter = eventEntity.Read<FromCharacter>();

						if (!__instance.TryGetInteractedForge(fromCharacter.Character, out Entity forgeEntity, out Forge_Shared forge))
							continue;

						Entity itemEntity = forge.ItemEntity._Entity;
						if (!TryGetRepairRecipe(itemEntity, out PrefabGUID repairRecipe))
							continue;

						RecipeGateEventBlocker.TryBlockForgeRepairEvent(
							eventEntity,
							repairRecipe,
							forgeEntity,
							itemEntity.GetPrefabGUID().GuidHash);
					}
					catch (Exception ex)
					{
						Plugin.Logger.LogWarning($"[{Plugin.Name}] Forge repair event check failed: {ex}");
					}
				}
			}
			finally
			{
				entities.Dispose();
			}
		}

		private static bool TryGetRepairRecipe(Entity itemEntity, out PrefabGUID repairRecipe)
		{
			repairRecipe = default(PrefabGUID);

			if (!itemEntity.Exists() || !itemEntity.Has<Durability>())
				return false;

			repairRecipe = itemEntity.Read<Durability>().RepairRecipe;
			return repairRecipe.GuidHash != 0;
		}
	}
}
