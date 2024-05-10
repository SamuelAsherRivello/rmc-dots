using NUnit.Framework;
using System.Reflection;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs.LowLevel.Unsafe;
#if !UNITY_DOTSRUNTIME
using UnityEngine.LowLevel;
#endif

namespace WayneGames.ECSTestsFixture
{
	#if NET_DOTS
	    public class EmptySystem : ComponentSystem
	    {
	        protected override void OnUpdate()
	        {
	        }

	        public new EntityQuery GetEntityQuery(params EntityQueryDesc[] queriesDesc)
	        {
	            return base.GetEntityQuery(queriesDesc);
	        }

	        public new EntityQuery GetEntityQuery(params ComponentType[] componentTypes)
	        {
	            return base.GetEntityQuery(componentTypes);
	        }

	        public new EntityQuery GetEntityQuery(NativeArray<ComponentType> componentTypes)
	        {
	            return base.GetEntityQuery(componentTypes);
	        }

	        public unsafe new BufferFromEntity<T> GetBufferFromEntity<T>(bool isReadOnly = false) where T : struct, IBufferElementData
	        {
	            CheckedState()->AddReaderWriter(isReadOnly ? ComponentType.ReadOnly<T>() : ComponentType.ReadWrite<T>());
	            return EntityManager.GetBufferFromEntity<T>(isReadOnly);
	        }
	    }
	#else
	public partial class EmptySystem : SystemBase
	{
		protected override void OnUpdate() {}

		public new EntityQuery GetEntityQuery(params EntityQueryDesc[] queriesDesc) => base.GetEntityQuery(queriesDesc);

		public new EntityQuery GetEntityQuery(params ComponentType[] componentTypes) => base.GetEntityQuery(componentTypes);

		public new EntityQuery GetEntityQuery(NativeArray<ComponentType> componentTypes) => base.GetEntityQuery(componentTypes);
	}

	#endif

	public class InternalECSTestsFixture
	{
		[SetUp]
		public virtual void Setup()
		{
	#if UNITY_DOTSRUNTIME
	            Unity.Runtime.TempMemoryScope.EnterScope();
	#endif
		}

		[TearDown]
		public virtual void TearDown()
		{
	#if UNITY_DOTSRUNTIME
	            Unity.Runtime.TempMemoryScope.ExitScope();
	#endif
		}
	}

	/// <summary>
	/// Copied from the Entities package and slightly modified to enable default world creation and fixing a call to an internal method via reflection.
	/// </summary>
	public abstract class ECSTestsFixture : InternalECSTestsFixture
	{
		protected World PreviousWorld;
		protected World World;
#if !UNITY_DOTSRUNTIME
		protected PlayerLoopSystem PreviousPlayerLoop;
#endif
		protected EntityManager EntityManager;
		protected EntityManager.EntityManagerDebug ManagerDebug;

		// False = Empty World
		// True = Full Default World like in production
		protected bool CreateDefaultWorld = false;
		
		private bool JobsDebuggerWasEnabled;

		[SetUp]
		public override void Setup()
		{
			base.Setup();

	#if !UNITY_DOTSRUNTIME
			// unit tests preserve the current player loop to restore later, and start from a blank slate.
			PreviousPlayerLoop = PlayerLoop.GetCurrentPlayerLoop();
			PlayerLoop.SetPlayerLoop(PlayerLoop.GetDefaultPlayerLoop());
	#endif

			PreviousWorld = World;
			World = World.DefaultGameObjectInjectionWorld =
				CreateDefaultWorld ? DefaultWorldInitialization.Initialize("Default Test World") : new World("Empty Test World");
			EntityManager = World.EntityManager;
			ManagerDebug = new EntityManager.EntityManagerDebug(EntityManager);

			// Many ECS tests will only pass if the Jobs Debugger enabled;
			// force it enabled for all tests, and restore the original value at teardown.
			JobsDebuggerWasEnabled = JobsUtility.JobDebuggerEnabled;
			JobsUtility.JobDebuggerEnabled = true;
	#if !UNITY_DOTSRUNTIME
			//JobsUtility.ClearSystemIds();
			JobUtility_ClearSystemIds();
	#endif
		}

		[TearDown]
		public override void TearDown()
		{
			if (World != null && World.IsCreated)
			{
				// Clean up systems before calling CheckInternalConsistency because we might have filters etc
				// holding on SharedComponentData making checks fail
				while (World.Systems.Count > 0)
					World.DestroySystemManaged(World.Systems[0]);

				ManagerDebug.CheckInternalConsistency();

				World.Dispose();
				World = null;

				World = PreviousWorld;
				PreviousWorld = null;
				EntityManager = default;
			}

			JobsUtility.JobDebuggerEnabled = JobsDebuggerWasEnabled;
	#if !UNITY_DOTSRUNTIME
			//JobsUtility.ClearSystemIds();
			JobUtility_ClearSystemIds();
	#endif

	#if !UNITY_DOTSRUNTIME
			PlayerLoop.SetPlayerLoop(PreviousPlayerLoop);
	#endif

			base.TearDown();
		}

		// calls JobUtility.ClearSystemIds() (internal method)
		private void JobUtility_ClearSystemIds() =>
			typeof(JobsUtility).GetMethod("ClearSystemIds", BindingFlags.Static | BindingFlags.NonPublic).Invoke(null, null);
	}
}
