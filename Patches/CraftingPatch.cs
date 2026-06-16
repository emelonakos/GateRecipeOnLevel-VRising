using System;
using HarmonyLib;
using LevelRecipeGate.Services;
using ProjectM;
using ProjectM.Network;
using Stunlock.Core;
using Unity.Collections;
using Unity.Entities;

namespace LevelRecipeGate.Patches
{
	[HarmonyPatch(typeof(StartCraftingSystem), nameof(StartCraftingSystem.OnUpdate))]
	public static class CraftingPatch
	{
		[HarmonyPrefix]
		public static void Prefix(StartCraftingSystem __instance)
		{
			EcsContext.SetEntityManager(__instance.EntityManager);
			NativeArray<Entity> entities = __instance._StartCraftItemEventQuery.ToEntityArray(Allocator.Temp);

			try
			{
				foreach (Entity eventEntity in entities)
				{
					try
					{
						if (!eventEntity.Has<StartCraftItemEvent>())
							continue;

						PrefabGUID recipeGuid = eventEntity.Read<StartCraftItemEvent>().RecipeId;
						if (recipeGuid.GuidHash == 0)
							continue;

						RecipeGateEventBlocker.TryBlockCraftEvent(eventEntity, recipeGuid);
					}
					catch (Exception ex)
					{
						Plugin.Logger.LogWarning($"[{Plugin.Name}] Craft event check failed: {ex}");
					}
				}
			}
			finally
			{
				entities.Dispose();
			}
		}
	}
}
