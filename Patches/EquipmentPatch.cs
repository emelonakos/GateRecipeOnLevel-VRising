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
	[HarmonyPatch(typeof(EquipItemSystem), nameof(EquipItemSystem.OnUpdate))]
	public static class EquipItemPatch
	{
		[HarmonyPrefix]
		public static void Prefix(EquipItemSystem __instance)
		{
			EcsContext.SetEntityManager(__instance.EntityManager);
			NativeArray<Entity> entities = __instance._EventQuery.ToEntityArray(Allocator.Temp);

			try
			{
				foreach (Entity eventEntity in entities)
				{
					try
					{
						if (!eventEntity.Has<FromCharacter>() || !eventEntity.Has<EquipItemEvent>())
							continue;

						FromCharacter fromCharacter = eventEntity.Read<FromCharacter>();
						EquipItemEvent equipItemEvent = eventEntity.Read<EquipItemEvent>();

						if (!InventoryUtilities.TryGetItemAtSlot(__instance.EntityManager, fromCharacter.Character, equipItemEvent.SlotIndex, out InventoryBuffer item))
							continue;

						RecipeGateEventBlocker.TryBlockWeaponEquipEvent(eventEntity, item.ItemEntity._Entity, item.ItemType);
					}
					catch (Exception ex)
					{
						Plugin.Logger.LogWarning($"[{Plugin.Name}] Equip item event check failed: {ex}");
					}
				}
			}
			finally
			{
				entities.Dispose();
			}
		}
	}

	[HarmonyPatch(typeof(EquipItemFromInventorySystem), nameof(EquipItemFromInventorySystem.OnUpdate))]
	public static class EquipItemFromInventoryPatch
	{
		[HarmonyPrefix]
		public static void Prefix(EquipItemFromInventorySystem __instance)
		{
			EcsContext.SetEntityManager(__instance.EntityManager);
			NativeArray<Entity> entities = __instance._Query.ToEntityArray(Allocator.Temp);

			try
			{
				foreach (Entity eventEntity in entities)
				{
					try
					{
						if (!eventEntity.Has<EquipItemFromInventoryEvent>())
							continue;

						EquipItemFromInventoryEvent equipItemFromInventoryEvent = eventEntity.Read<EquipItemFromInventoryEvent>();
						if (!EcsContext.TryGetEntityFromNetworkId(equipItemFromInventoryEvent.FromInventory, out Entity fromInventory))
							continue;

						if (!InventoryUtilities.TryGetItemAtSlot(__instance.EntityManager, fromInventory, equipItemFromInventoryEvent.SlotIndex, out InventoryBuffer item))
							continue;

						RecipeGateEventBlocker.TryBlockWeaponEquipEvent(eventEntity, item.ItemEntity._Entity, item.ItemType);
					}
					catch (Exception ex)
					{
						Plugin.Logger.LogWarning($"[{Plugin.Name}] Equip item from inventory event check failed: {ex}");
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
