using Unity.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.StateMachine
{
    public abstract partial class StateMachineSystemBase : SystemBase
    {
        EntityQuery _stateQuery;
        List<State> stateProcessors = new List<State>();
        EntityCommandBufferSystem commandBufferSystem;

        protected EntityCommandBuffer Commands { get; private set; }
        int stateIdCounter = 1;

        protected override void OnCreate()
        {
            base.OnCreate();
            commandBufferSystem = InitializeEntityCommandBufferSystem();
            _stateQuery = GetStateEntityQuery();
        }

        protected abstract EntityQuery GetStateEntityQuery();

        protected virtual EntityCommandBufferSystem InitializeEntityCommandBufferSystem()
        {
            return World.GetExistingSystemManaged<EntityCommandBufferSystem>();
        }

        bool commandsInitializedEarly = false;

        protected void InitilizeCommandsEarly()
        {
            if(commandsInitializedEarly)
            {
                return;
            }
            Commands = commandBufferSystem.CreateCommandBuffer();
            commandsInitializedEarly = true;
        }

        protected override void OnUpdate()
        {
       
            if(commandsInitializedEarly == false)
            {
                Commands = commandBufferSystem.CreateCommandBuffer();
            }
            else
            {
                commandsInitializedEarly = false;
            }    
            
            updateStates();
        }

        void updateStates()
        {
       
            for (int i = 0; i < stateProcessors.Count; i++)
            {
                var state = stateProcessors[i];
                state.OnBeforeUpdate();
            }

            using(var chunks = _stateQuery.ToArchetypeChunkArray(Allocator.Temp))
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

                        for (int j = 0; j < stateProcessors.Count; j++)
                        {
                            var stateProcessor = stateProcessors[j];

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

                        for (int i = 0; i < stateProcessors.Count; i++)
                        {
                            var state = stateProcessors[i];

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

                        for (int i = 0; i < stateProcessors.Count; i++)
                        {
                            var state = stateProcessors[i];

                            if (stateId.currentStateID != state.ID || stateId.stateIdToSwitch != state.ID)
                            {
                                continue;
                            }

                            state.OnUpdate(entity);
                        }
                    }
                }
            }

            for (int i = 0; i < stateProcessors.Count; i++)
            {
                var state = stateProcessors[i];
                state.OnAfterUpdate();
            }
        }

        private int GetStateIndex<T>() where T : State
        {
            foreach (var state in stateProcessors)
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

            for (int i = 0; i < stateProcessors.Count; i++)
            {
                var otherState = stateProcessors[i];
                var otherType = otherState.GetType();

                if (stateType.IsSubclassOf(otherType))
                {
                    stateProcessors[i] = state;
                    replaced = true;
                }
            }
            if (replaced == false)
            {
                stateProcessors.Add(state);
            }

            state.ID = stateIdCounter;
            stateIdCounter++;
            state.Initialize(this);
        }

		public bool IsInState<T>(Entity e) where T : State
        {
            if (HasComponent<StateID>(e) == false)
                return false;
            var stateId = GetComponent<StateID>(e);
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
            var stateId = GetComponent<StateID>(entity);
            stateId.RequestSwitchToState(id);
            SetComponent(entity, stateId);
        }

        public class State
        {
            internal int ID;

            public StateMachineSystemBase System { get; protected set; }
            protected EntityCommandBuffer Commands;
            protected EntityManager EntityManager;
            protected World World;

            
            public virtual void Initialize(StateMachineSystemBase system)
            {
                System = system;
                EntityManager = System.EntityManager;
                World = System.World;
            }

            public bool IsPendingStateChange(Entity entity)
            {
                var stateId = GetComponent<StateID>(entity);
                return stateId.currentStateID != stateId.stateIdToSwitch;
            }

            public bool Exists(Entity entity)
            {
                return System.EntityManager.Exists(entity);
            }

            public bool HasComponent<T>(Entity entity) where T : unmanaged, IComponentData
            {
                return System.HasComponent<T>(entity);
            }

            public bool HasBuffer<T>(Entity entity) where T : unmanaged, IBufferElementData
            {
                return System.HasBuffer<T>(entity);
            }

            public T GetComponent<T>(Entity entity) where T : unmanaged, IComponentData
            {
                return System.GetComponent<T>(entity);
            }

            public DynamicBuffer<T> GetBuffer<T>(Entity entity) where T : unmanaged, IBufferElementData
            {
                return System.GetBuffer<T>(entity);
            }

            public T GetComponentObject<T>(Entity entity)
            {
                return System.EntityManager.GetComponentObject<T>(entity);
            }

            public void SetComponent<T>(Entity entity, T data) where T : unmanaged, IComponentData
            {
                System.SetComponent<T>(entity, data);
            }

            protected EntityQuery GetEntityQuery(params ComponentType[] types)
            {
                return System.GetEntityQuery(types);
            }

            public bool IsInState<K>(Entity e) where K : State
            {
                return System.IsInState<K>(e);
            }

            public void RequestStateChange<K>(Entity entity) where K : State
            {
                System.RequestStateChange<K>(entity);
            }

            public virtual void OnBeforeUpdate()
            {
                Commands = System.Commands;
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

    public partial class StateMachineSystem<S> : StateMachineSystemBase where S : struct, IComponentData
    {
        protected override EntityQuery GetStateEntityQuery()
        {
            return GetEntityQuery(typeof(StateID), ComponentType.ReadOnly<S>());
        }
    }
}