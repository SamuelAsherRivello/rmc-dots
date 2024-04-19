using System;
using RMC.DOTS.SystemGroups;
using Unity.Entities;

namespace RMC.DOTS.Systems.Scoring
{
    [UpdateInGroup(typeof(PauseableSystemGroup))]
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