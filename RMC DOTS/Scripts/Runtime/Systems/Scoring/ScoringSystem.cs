using System;
using RMC.DOTS.SystemGroups;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.Scoring
{
    [UpdateInGroup(typeof(PauseableSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial class ScoringSystem : SystemBase
    {
        
        public Action<ScoringComponent> OnScoringComponentChanged;

        public ScoringComponent ScoringComponent
        {
            get
            {
                // In rare cases, (first frame of scene) there is no singleton yet
                return SystemAPI.GetSingleton<ScoringComponent>();
            }
            set
            {
                SystemAPI.SetSingleton<ScoringComponent>(value);
                OnScoringComponentChanged?.Invoke(value);
            }
        }
        
        protected override void OnCreate()
        {
            RequireForUpdate<ScoringComponent>();
        }
        
        protected override void OnUpdate()
        {
            // Listen for changes and broadcast it
            foreach (var scoringComponent 
                     in SystemAPI.Query<RefRO<ScoringComponent>>().WithChangeFilter<ScoringComponent>())
            {
                ScoringComponent = scoringComponent.ValueRO;
            }
        }


    }
}