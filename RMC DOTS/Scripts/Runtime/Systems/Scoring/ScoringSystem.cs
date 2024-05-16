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
                Debug.Log("Getting");
                return SystemAPI.GetSingleton<ScoringComponent>();
            }
            set
            {
                SystemAPI.SetSingleton<ScoringComponent>(value);
                Debug.Log("Score seto to : "  + value);
                OnScoringComponentChanged?.Invoke(value);
            }
        }
        
        protected override void OnCreate()
        {
            RequireForUpdate<ScoringComponent>();
        }
        
        protected override void OnUpdate()
        {
            // WithChangeFilter means it was changed **OR** simply any RefRW was used, regardless of actual change
            foreach (var scoringComponent 
                     in SystemAPI.Query<RefRO<ScoringComponent>>().WithChangeFilter<ScoringComponent>())
            {
                //Debug.Log("eq;" + ScoringComponent.Equals(scoringComponent.ValueRO) + "because a: " + scoringComponent.ValueRO.ScoreComponent01.ScoreCurrent +  " and b: " + ScoringComponent.ScoreComponent01.ScoreCurrent);
                
                //if (!ScoringComponent.Equals(scoringComponent.ValueRO))
                {
                    ScoringComponent = scoringComponent.ValueRO;
                }
            }
        }


    }
}