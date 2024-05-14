using Unity.Entities;

namespace RMC.DOTS.Systems.Timer
{
    public struct TimerComponent : IComponentData 
    {
        public bool IsTimerDone 
        {
            get 
            {
                return ElapsedTimeInSeconds >= TargetDurationInSeconds;
            }
        }
        
        public void UpdateTimer (float deltaTime)
        {
            ElapsedTimeInSeconds += deltaTime;
        } 
        
        private float ElapsedTimeInSeconds;
        public readonly float TargetDurationInSeconds;
 
        public TimerComponent(float targetDurationInSeconds) : this() 
        {
            ElapsedTimeInSeconds = 0;
            TargetDurationInSeconds = targetDurationInSeconds;
        }
    }
}