// using RMC.DOTS.SystemGroups;
// using Unity.Burst;
// using Unity.Collections;
// using Unity.Entities;
// using UnityEngine;
//
// namespace RMC.DOTS.Systems.Timer
// {
//     [UpdateInGroup(typeof(UnpauseableSystemGroup))]
//     [RequireMatchingQueriesForUpdate]
//     [BurstCompile]
//     public partial class BaseTimerSystem<TTimerComponent> : SystemBase
//         where TTimerComponent : struct, IComponentData
//     {
//         /// <summary>
//         /// We set the TimeScale as virtual so that subclasses can have their own timescales if needed.
//         /// A use case for this is for faster speeds or faster enemies in strategy games.
//         /// </summary>
//         protected virtual float TimeScale
//         {
//             get
//             {
//                 return 1;
//             }
//         }
//
//         /// <summary>
//         /// This is the scaled time - applying the Timescale to the delta time.
//         /// </summary>
//         private float ScaledDeltaTime
//         {
//             get { return SystemAPI.Time.DeltaTime * TimeScale; }
//         }
//
//         private EndSimulationEntityCommandBufferSystem _ecbSystem;
//         private EntityQuery _timerQuery;
//         
//         [BurstCompile]
//         protected override void OnCreate()
//         {
//             RequireForUpdate<BaseTimerSystemAuthoring.BaseTimerSystemIsEnabledTag>();
//             RequireForUpdate<EndInitializationEntityCommandBufferSystem.Singleton>();
//         }
//
//         [BurstCompile]
//         protected override void OnUpdate()
//         {
//             var ecb = _ecbSystem.CreateCommandBuffer();
//
//             // The main idea here is that everything that has the Timer tag of type T will only be processed
//             _timerQuery = this.EntityManager.CreateEntityQuery(
//                 ComponentType.ReadWrite<BaseTimerComponent<TTimerComponent>>());
//             
//             UpdateTimerJob job = new UpdateTimerJob
//             {
//                 CurrentTimeInterval = ScaledDeltaTime,
//                 _ecb = ecb.AsParallelWriter()
//             };
//             job.ScheduleParallel(_timerQuery);
//             _ecbSystem.AddJobHandleForProducer(Dependency);
//   
//         }
//
//
//         [BurstCompile]
//         private partial struct UpdateTimerJob : IJobEntity
//         {
//             [ReadOnly] public float CurrentTimeInterval;
//             public EntityCommandBuffer.ParallelWriter _ecb;
//
//             public void Execute(Entity entity, int index, ref BaseTimerComponent<TTimerComponent> timerComponent)
//             {
//                 timerComponent.ElapsedTime += CurrentTimeInterval;
//                 Debug.Log("Tick");
//                 if (timerComponent.IsDone)
//                 {
//                     Debug.Log("Remove");
//                     _ecb.RemoveComponent(index, entity, typeof(BaseTimerComponent<TTimerComponent>));
//                 }
//             }
//         }
//     }
// }
