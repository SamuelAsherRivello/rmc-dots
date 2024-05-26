using System;
using Unity.Entities;

namespace RMC.DOTS.Lessons.UI.UIToolkit
{
    [RequireMatchingQueriesForUpdate]
    public partial class SimpleScoreSystem : SystemBase
    {
        
        //  Properties (GO) -------------------------------
        public SimpleScoreComponent SimpleScoreComponent
        {
            get
            {
                return SystemAPI.GetSingleton<SimpleScoreComponent>();
            }
            set
            {
                SystemAPI.SetSingleton<SimpleScoreComponent>(value);
                OnScoreChanged?.Invoke(value);
            }
        }
        
        
        //  Fields (GO) -----------------------------------
        public Action<SimpleScoreComponent> OnScoreChanged;
        private SimpleScoreComponent _lastSimpleScoreComponent;

        
        //  Methods (GO) ----------------------------------
        public void IncrementScoreBy(int scoreDelta)
        {
            SimpleScoreComponent = new SimpleScoreComponent()
            {
                Score = SimpleScoreComponent.Score + scoreDelta
            };
        }
        
        
        //  Methods (DOTS) --------------------------------
        protected override void OnCreate()
        {
            RequireForUpdate<SimpleScoreComponent>();
        }
        
        protected override void OnUpdate()
        {
            // WithChangeFilter is NOT accurate. It means any RefRW was used, regardless of actual value change
            foreach (var scoringComponent 
                     in SystemAPI.Query<RefRO<SimpleScoreComponent>>().WithChangeFilter<SimpleScoreComponent>())
            {

                if (_lastSimpleScoreComponent.Equals(scoringComponent.ValueRO))
                {
                    return;
                }
                _lastSimpleScoreComponent = scoringComponent.ValueRO;
                SimpleScoreComponent = _lastSimpleScoreComponent;
            }
        }
    }
}