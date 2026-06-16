using System;
using Stunlock.Core;
using Unity.Collections;
using Unity.Entities;

namespace LevelRecipeGate.Services
{
	internal static class EcsContext
	{
		private static EntityManager _entityManager;
		private static World _world;
		private static EndSimulationEntityCommandBufferSystem _endSimulationEntityCommandBufferSystem;

		internal static EntityManager EntityManager
		{
			get
			{
				if (!HasEntityManager)
					throw new InvalidOperationException("EntityManager has not been initialized for ECS extensions.");

				return _entityManager;
			}
		}

		internal static bool HasEntityManager { get; private set; }

		internal static EndSimulationEntityCommandBufferSystem EndSimulationEntityCommandBufferSystem
		{
			get
			{
				if (_endSimulationEntityCommandBufferSystem == null)
					_endSimulationEntityCommandBufferSystem = EntityManager.World.GetExistingSystemManaged<EndSimulationEntityCommandBufferSystem>();

				return _endSimulationEntityCommandBufferSystem;
			}
		}

		internal static void SetEntityManager(EntityManager entityManager)
		{
			World world = entityManager.World;
			if (!HasEntityManager || !ReferenceEquals(_world, world))
			{
				_world = world;
				_endSimulationEntityCommandBufferSystem = world.GetExistingSystemManaged<EndSimulationEntityCommandBufferSystem>();
			}

			_entityManager = entityManager;
			HasEntityManager = true;
		}

		internal static void Clear()
		{
			_entityManager = default(EntityManager);
			_world = null;
			_endSimulationEntityCommandBufferSystem = null;
			HasEntityManager = false;
		}
	}

	internal static class ECSExtensions
	{
		public static T Read<T>(this Entity entity) where T : struct
		{
			return EcsContext.EntityManager.GetComponentData<T>(entity);
		}

		public static void Write<T>(this Entity entity, T componentData) where T : struct
		{
			EcsContext.EntityManager.SetComponentData(entity, componentData);
		}

		public static DynamicBuffer<T> ReadBuffer<T>(this Entity entity) where T : struct
		{
			return EcsContext.EntityManager.GetBuffer<T>(entity);
		}

		public static DynamicBuffer<T> AddBuffer<T>(this Entity entity) where T : struct
		{
			return EcsContext.EntityManager.AddBuffer<T>(entity);
		}

		public static void Add<T>(this Entity entity) where T : struct
		{
			EcsContext.EntityManager.AddComponent<T>(entity);
		}

		public static void Remove<T>(this Entity entity) where T : struct
		{
			EcsContext.EntityManager.RemoveComponent<T>(entity);
		}

		public static bool Has<T>(this Entity entity) where T : struct
		{
			return EcsContext.EntityManager.HasComponent<T>(entity);
		}

		public static NativeArray<ComponentType> GetComponentTypes(this Entity entity, Allocator allocator = Allocator.Temp)
		{
			return EcsContext.EntityManager.GetComponentTypes(entity, allocator);
		}

		public static bool Exists(this Entity entity)
		{
			return entity != Entity.Null && EcsContext.EntityManager.Exists(entity);
		}

		public static void Destroy(this Entity entity)
		{
			EcsContext.EntityManager.DestroyEntity(entity);
		}

		public static PrefabGUID GetPrefabGUID(this Entity entity)
		{
			return entity.Exists() && entity.Has<PrefabGUID>()
				? entity.Read<PrefabGUID>()
				: default(PrefabGUID);
		}
	}
}
