using System;
using LevelRecipeGate.Config;
using ProjectM;
using ProjectM.Network;
using Stunlock.Core;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace LevelRecipeGate.Services
{
	internal static class RecipeGateEventBlocker
	{
		private static readonly AssetGuid BlockedCraftSctAssetGuid = AssetGuid.FromString("4f3a2b28-77e7-4f7a-9fc2-b2e19cddf9e1");
		private static readonly PrefabGUID BlockedCraftSctType = new PrefabGUID(311215354);

		internal static void TryBlockCraftEvent(Entity eventEntity, PrefabGUID recipeGuid)
		{
			TryBlockEvent(eventEntity, recipeGuid, "craft", "start craft");
		}

		internal static void TryBlockForgeRepairEvent(Entity eventEntity, PrefabGUID repairRecipe, Entity forgeEntity, int itemGuidHash)
		{
			TryBlockEvent(
				eventEntity,
				repairRecipe,
				"forge repair",
				$"forge={forgeEntity.Index}:{forgeEntity.Version}, item={itemGuidHash}",
				forgeEntity);
		}

		private static void TryBlockEvent(Entity eventEntity, PrefabGUID recipeGuid, string action, string context, Entity forgeEntity = default(Entity))
		{
			if (!ConfigStore.TryGetRequiredLevel(recipeGuid.GuidHash, out int requiredLevel))
				return;
            
			int playerHighestGearLevel = TryGetPlayerHighestGearLevel(eventEntity, out User user, out Entity userEntity, out Entity characterEntity, out FromCharacter fromCharacter);

			Plugin.Logger.LogInfo($"[{Plugin.Name}] {action} check gate={recipeGuid.GuidHash}, playerHighestGearLevel={playerHighestGearLevel}, requiredLevel={requiredLevel}, {context}");

			if (playerHighestGearLevel < 0)
			{
				eventEntity.Destroy();
				if (forgeEntity.Exists())
					TryQueueForgeRemoveItem(fromCharacter, forgeEntity);

				TrySendBlockedCraftSct(userEntity, characterEntity);
				TrySendSystemMessage(user, $"Unable to determine your gear level. {action} blocked.");
				Plugin.Logger.LogWarning($"[{Plugin.Name}] BLOCKED {action} gate={recipeGuid.GuidHash}: could not read player gear level.");
				return;
			}

			if (playerHighestGearLevel >= requiredLevel)
			{
				Plugin.Logger.LogInfo($"[{Plugin.Name}] ALLOWED {action} gate={recipeGuid.GuidHash}, playerHighestGearLevel={playerHighestGearLevel}, requiredLevel={requiredLevel}");
				return;
			}

			eventEntity.Destroy();
			if (forgeEntity.Exists())
				TryQueueForgeRemoveItem(fromCharacter, forgeEntity);

			TrySendBlockedCraftSct(userEntity, characterEntity);

			string message = ConfigStore.LevelRecipeBlockedMessage
				.Replace("{level}", requiredLevel.ToString())
				.Replace("{current}", playerHighestGearLevel.ToString())
				.Replace("{recipe}", recipeGuid.GuidHash.ToString());

			TrySendSystemMessage(user, message);

			Plugin.Logger.LogInfo($"[{Plugin.Name}] BLOCKED {action} gate={recipeGuid.GuidHash}, playerHighestGearLevel={playerHighestGearLevel}, requiredLevel={requiredLevel}");
		}

		private static int TryGetPlayerHighestGearLevel(Entity eventEntity, out User user, out Entity userEntity, out Entity characterEntity, out FromCharacter fromCharacter)
		{
			user = default(User);
			userEntity = Entity.Null;
			characterEntity = Entity.Null;
			fromCharacter = default(FromCharacter);

			try
			{
				if (!eventEntity.Has<FromCharacter>())
					return -1;

				fromCharacter = eventEntity.Read<FromCharacter>();

				userEntity = fromCharacter.User;
				characterEntity = fromCharacter.Character;
				if (!userEntity.Exists() || !userEntity.Has<User>())
					return -1;

				user = userEntity.Read<User>();
				int highestLevel = PlayerLevelService.UpdateAndGetHighestGearLevel(EcsContext.EntityManager, user, characterEntity);

				Plugin.Logger.LogInfo($"[{Plugin.Name}] Highest recorded gear level for {user.CharacterName} = {highestLevel}");

				return highestLevel;
			}
			catch (Exception ex)
			{
				Plugin.Logger.LogWarning($"[{Plugin.Name}] Failed reading highest player gear level: {ex.Message}");
				return -1;
			}
		}

		private static void TryQueueForgeRemoveItem(FromCharacter fromCharacter, Entity forgeEntity)
		{
			try
			{
				if (!forgeEntity.Exists() || !forgeEntity.Has<NetworkId>())
					return;

				Entity removeEventEntity = EcsContext.EntityManager.CreateEntity(
					ComponentType.ReadWrite<FromCharacter>(),
					ComponentType.ReadWrite<ForgeEvents.RemoveItem>());

				removeEventEntity.Write(fromCharacter);
				removeEventEntity.Write(new ForgeEvents.RemoveItem
				{
					ToInventory = forgeEntity.Read<NetworkId>(),
					InventoryIndex = 0
				});
			}
			catch (Exception ex)
			{
				Plugin.Logger.LogWarning($"[{Plugin.Name}] Could not queue forge item removal: {ex}");
			}
		}

		private static void TrySendBlockedCraftSct(Entity userEntity, Entity characterEntity)
		{
			try
			{
				if (!userEntity.Exists() || !characterEntity.Exists())
					return;

				ScrollingCombatTextMessage.Create(
					EcsContext.EntityManager,
					EcsContext.EndSimulationEntityCommandBufferSystem.CreateCommandBuffer(),
					BlockedCraftSctAssetGuid,
					characterEntity.Read<Translation>().Value,
					new float3(),
					characterEntity,
					0f,
					BlockedCraftSctType,
					userEntity);
			}
			catch (Exception ex)
			{
				Plugin.Logger.LogWarning($"[{Plugin.Name}] Could not send blocked craft SCT: {ex}");
			}
		}

		private static void TrySendSystemMessage(User user, string message)
		{
			try
			{
				FixedString512Bytes fixedMessage = message;
				ServerChatUtils.SendSystemMessageToClient(EcsContext.EntityManager, user, ref fixedMessage);
			}
			catch (Exception ex)
			{
				Plugin.Logger.LogWarning($"[{Plugin.Name}] Could not send system message: {ex}");
			}
		}
	}
}
