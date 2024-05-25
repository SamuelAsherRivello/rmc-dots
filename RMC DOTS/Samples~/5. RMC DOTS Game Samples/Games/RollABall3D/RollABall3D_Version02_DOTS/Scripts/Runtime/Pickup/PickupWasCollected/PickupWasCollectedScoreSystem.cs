using RMC.DOTS.SystemGroups;
using RMC.DOTS.Systems.Scoring;
using Unity.Burst;
using Unity.Entities;

namespace RMC.DOTS.Samples.RollABall3D.RollABall3D_Version02_DOTS
{
    [UpdateInGroup(typeof(UnpauseablePresentationSystemGroup))]
    [UpdateBefore(typeof(PickupWasCollectedDestroySystem))] 
    public partial struct PickupWasCollectedScoreSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PickupTag>();
            state.RequireForUpdate<ScoringComponent>();
            state.RequireForUpdate<PickupWasCollectedTag>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (pickupTag, pickupWasCollectedTag)
                     in SystemAPI.Query<PickupTag, PickupWasCollectedTag>())
            {
                var pickupCounter = SystemAPI.GetSingleton<ScoringComponent>();
                pickupCounter.ScoreComponent01.ScoreCurrent += 1;
                SystemAPI.SetSingleton(pickupCounter);
            }
        }
    }
}