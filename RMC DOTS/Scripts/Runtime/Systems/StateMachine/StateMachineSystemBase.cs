using Unity.Collections;
using System.Collections.Generic;
using Unity.Entities;

namespace RMC.DOTS.Systems.StateMachine
{
    /// <summary>
    /// Heavily updated and modified from source.
    /// 
    /// Inspiration: See <see cref="https://github.com/PhilSA/PolymorphicStructs"/>
    /// 
    /// </summary>
    public abstract partial class StateMachineSystemBase : SystemBase
    {
        private EntityQuery _stateEntityQuery;
        private readonly List<State> _stateProcessors = new List<State>();
        private EntityCommandBufferSystem _entityCommandBufferSystem;
        private int _stateIdCounter = 1;
        private bool _commandsInitializedEarly = false;
        
        protected EntityCommandBuffer EntityCommandBuffer { get; private set; }
        

        protected override void OnCreate()
        {
            base.OnCreate();
            _entityCommandBufferSystem = InitializeEntityCommandBufferSystem();
            _stateEntityQuery = GetStateEntityQuery();
        }

        protected abstract EntityQuery GetStateEntityQuery();

        protected virtual EntityCommandBufferSystem InitializeEntityCommandBufferSystem()
        {
            return World.GetExistingSystemManaged<EntityCommandBufferSystem>();
        }

        /// <summary>
        /// TODO: Why does this exist as an option? 
        /// </summary>
        protected void InitializeCommandsEarly()
        {
            if(_commandsInitializedEarly)
            {
                return;
            }
            EntityCommandBuffer = _entityCommandBufferSystem.CreateCommandBuffer();
            _commandsInitializedEarly = true;
        }

        protected override void OnUpdate()
        {
            if(_commandsInitializedEarly == false)
            {
                EntityCommandBuffer = _entityCommandBufferSystem.CreateCommandBuffer();
            }
            else
            {
                _commandsInitializedEarly = false;
            }    
            
            updateStates();
        }

        void updateStates()
        {
       
            for (int i = 0; i < _stateProcessors.Count; i++)
            {
                var state = _stateProcessors[i];
                state.OnBeforeUpdate();
            }

            using(var chunks = _stateEntityQuery.ToArchetypeChunkArray(Allocator.Temp))
            {
                // EXIT
                foreach (var chunk in chunks)
                {
                    var entities = chunk.GetNativeArray(GetEntityTypeHandle());
                    var stateIds = chunk.GetNativeArray(GetComponentTypeHandle<StateID>());

                    for (int i = 0; i < chunk.Count; i++)
                    {
                        var entity = entities[i];
                        var stateId = stateIds[i];

                        for (int j = 0; j < _stateProcessors.Count; j++)
                        {
                            var stateProcessor = _stateProcessors[j];

                            if (stateId.StateIdToSwitch == stateId.currentStateID)
                            {
                                continue;
                            }

                            if (stateId.StateIdToSwitch == stateProcessor.ID)
                            {
                                continue;
                            }

                            if(stateId.currentStateID != stateProcessor.ID)
                            {
                                continue;
                            }

                            stateProcessor.OnExit(entity);
                        }
                    }
                }

                // ENTER
                foreach (var chunk in chunks)
                {
                    var entities = chunk.GetNativeArray(GetEntityTypeHandle());
                    var stateIds = chunk.GetNativeArray(GetComponentTypeHandle<StateID>());

                    for (int c = 0; c < chunk.Count; c++)
                    {
                        var entity = entities[c];
                        var stateId = stateIds[c];

                        for (int i = 0; i < _stateProcessors.Count; i++)
                        {
                            var state = _stateProcessors[i];

                            if (stateId.StateIdToSwitch == stateId.currentStateID)
                            {
                                continue;
                            }

                            if (stateId.StateIdToSwitch != state.ID)
                            {
                                continue;
                            }

                            stateId.currentStateID = state.ID;
                            stateId.stateIdToSwitch = state.ID;
                            stateIds[c] = stateId;

                            state.OnEnter(entity);
                        }
                    }
                }

                // UPDATE
                foreach (var chunk in chunks)
                {
                    var entities = chunk.GetNativeArray(GetEntityTypeHandle());
                    var stateIds = chunk.GetNativeArray(GetComponentTypeHandle<StateID>());

                    for (int c = 0; c < chunk.Count; c++)
                    {
                        var entity = entities[c];
                        var stateId = stateIds[c];

                        for (int i = 0; i < _stateProcessors.Count; i++)
                        {
                            var state = _stateProcessors[i];

                            if (stateId.currentStateID != state.ID || stateId.stateIdToSwitch != state.ID)
                            {
                                continue;
                            }

                            state.OnUpdate(entity);
                        }
                    }
                }
            }

            for (int i = 0; i < _stateProcessors.Count; i++)
            {
                var state = _stateProcessors[i];
                state.OnAfterUpdate();
            }
        }

        private int GetStateIndex<T>() where T : State
        {
            foreach (var state in _stateProcessors)
            {
                if (state is T)
                {
                    return state.ID;
                }
            }
            return -1;
        }

        protected T RegisterState<T>() where T : State, new()
        {
            var state = new T();
            RegisterState(state);
            return state;
        }

        protected void RegisterState(State state)
        {
            var stateType = state.GetType();

            bool replaced = false;

            for (int i = 0; i < _stateProcessors.Count; i++)
            {
                var otherState = _stateProcessors[i];
                var otherType = otherState.GetType();

                if (stateType.IsSubclassOf(otherType))
                {
                    _stateProcessors[i] = state;
                    replaced = true;
                }
            }
            if (replaced == false)
            {
                _stateProcessors.Add(state);
            }

            state.ID = _stateIdCounter;
            _stateIdCounter++;
            state.Initialize(this);
        }

		public bool IsInState<T>(Entity e) where T : State
        {
            if (SystemAPI.HasComponent<StateID>(e) == false)
                return false;
            var stateId = SystemAPI.GetComponent<StateID>(e);
            var stateIndex = GetStateIndex<T>();
            return stateIndex == stateId.currentStateID;
        }

        public void RequestStateChange<T>(Entity entity) where T : State
        {
            var id = GetStateIndex<T>();

            if(id == -1)
            {
                throw new System.InvalidOperationException($"No registered state found with type {typeof(T).Name}");
            }
            var stateId = SystemAPI.GetComponent<StateID>(entity);
            stateId.RequestSwitchToState(id);
            SystemAPI.SetComponent(entity, stateId);
        }

        public class State
        {
            internal int ID;

            public StateMachineSystemBase StateMachineSystemBase { get; protected set; }
            protected EntityCommandBuffer EntityCommandBuffer;
            protected EntityManager EntityManager;
            protected World World;

            
            public virtual void Initialize(StateMachineSystemBase smSystemBase)
            {
                StateMachineSystemBase = smSystemBase;
                EntityManager = StateMachineSystemBase.EntityManager;
                World = StateMachineSystemBase.World;
            }

            public bool IsPendingStateChange(Entity entity)
            {
                var stateId = GetComponent<StateID>(entity);
                return stateId.currentStateID != stateId.stateIdToSwitch;
            }

            public bool Exists(Entity entity)
            {
                return StateMachineSystemBase.EntityManager.Exists(entity);
            }

            public bool HasComponent<T>(Entity entity) where T : unmanaged, IComponentData
            {
                return StateMachineSystemBase.HasComponent<T>(entity);
            }

            public bool HasBuffer<T>(Entity entity) where T : unmanaged, IBufferElementData
            {
                return StateMachineSystemBase.HasBuffer<T>(entity);
            }

            public T GetComponent<T>(Entity entity) where T : unmanaged, IComponentData
            {
                return StateMachineSystemBase.GetComponent<T>(entity);
            }

            public DynamicBuffer<T> GetBuffer<T>(Entity entity) where T : unmanaged, IBufferElementData
            {
                return StateMachineSystemBase.GetBuffer<T>(entity);
            }

            public T GetComponentObject<T>(Entity entity)
            {
                return StateMachineSystemBase.EntityManager.GetComponentObject<T>(entity);
            }

            public void SetComponent<T>(Entity entity, T data) where T : unmanaged, IComponentData
            {
                StateMachineSystemBase.SetComponent<T>(entity, data);
            }

			public T GetSingleton<T>() where T : unmanaged, IComponentData
			{
				return StateMachineSystemBase.GetSingleton<T>();
			}

			public void GetSingleton<T>(T value) where T : unmanaged, IComponentData
			{
				StateMachineSystemBase.SetSingleton<T>(value);
			}

			protected EntityQuery GetEntityQuery(params ComponentType[] types)
            {
                return StateMachineSystemBase.GetEntityQuery(types);
            }

            public bool IsInState<K>(Entity e) where K : State
            {
                return StateMachineSystemBase.IsInState<K>(e);
            }

            public void RequestStateChange<K>(Entity entity) where K : State
            {
                StateMachineSystemBase.RequestStateChange<K>(entity);
            }

            public virtual void OnBeforeUpdate()
            {
                EntityCommandBuffer = StateMachineSystemBase.EntityCommandBuffer;
            }

            public virtual void OnAfterUpdate()
            {
            }

            public virtual void OnEnter(Entity entity)
            {
            }

            public virtual void OnExit(Entity entity)
            {
            }

            public virtual void OnUpdate(Entity entity)
            {
            }
        }
    }

    /// <summary>
    /// Create a new State Machine that is keyed on one particular component type of "S".
    /// </summary>
    /// <typeparam name="S"></typeparam>
    public partial class StateMachineSystem<S> : StateMachineSystemBase where S : struct, IComponentData
    {
        protected override EntityQuery GetStateEntityQuery()
        {
            return GetEntityQuery(typeof(StateID), ComponentType.ReadOnly<S>());
        }

        /// <summary>
        /// Srivello added. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
		public void RequestStateChangeForAllEntities<T>() where T : State
		{
			var query = GetStateEntityQuery();
			var entities = query.ToEntityArray(Allocator.Temp);
			foreach (Entity entity in entities)
			{
				RequestStateChange<T>(entity);
			}
		}
	}
}