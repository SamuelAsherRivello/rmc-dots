using System;
using RMC.DOTS.SystemGroups;
using Unity.Entities;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace RMC.DOTS.Systems.Scoring
{
    [UpdateInGroup(typeof(PauseablePresentationSystemGroup))]
    [RequireMatchingQueriesForUpdate]
    public partial class ScoringSystem : SystemBase
    {
        
        public Action<ScoringComponent> OnScoringComponentChanged;
        private ScoringComponent _lastScoringComponent;

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
            // WithChangeFilter is NOT accurate. It means any RefRW was used, regardless of actual value change
            foreach (var scoringComponent 
                     in SystemAPI.Query<RefRO<ScoringComponent>>().WithChangeFilter<ScoringComponent>())
            {

                //TODO: Remove the need for this check
                //For now, this quits if the incoming value is NOT actually changed
                if (_lastScoringComponent.Equals(scoringComponent.ValueRO))
                {
                    return;
                }
                _lastScoringComponent = scoringComponent.ValueRO;
                ScoringComponent = _lastScoringComponent;
            }
        }


    }
}