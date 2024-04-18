using System;
using RMC.DOTS.Systems.GameState;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.Scoring
{
    [UpdateBefore(typeof(GameStateSystem))]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial class ScoringSystem : SystemBase
    {
        public Action<ScoringComponent> OnScoresChanged;
        public ScoringComponent ScoringComponent { get; private set; }
        
        protected override void OnUpdate()
        {
            foreach (var scoringComponent in SystemAPI.Query<ScoringComponent>().WithChangeFilter<ScoringComponent>())
            {
                ScoringComponent = scoringComponent;
                OnScoresChanged?.Invoke(ScoringComponent);
            }
        }
    }
}