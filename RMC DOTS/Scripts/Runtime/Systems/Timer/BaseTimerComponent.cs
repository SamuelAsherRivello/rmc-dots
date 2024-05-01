using Unity.Entities;

namespace RMC.DOTS.Systems.Timer
{
    public struct BaseTimerComponent<T> : IComponentData where T : struct 
    {
        public bool IsDone 
        {
            get 
            {
                return ElapsedTime >= TargetDuration;
            }
        }
        
        public float ElapsedTime;
        public float TargetDuration;
 
        public BaseTimerComponent(float targetDuration) : this() 
        {
            ElapsedTime = 0;
            TargetDuration = targetDuration;
        }
    }
}