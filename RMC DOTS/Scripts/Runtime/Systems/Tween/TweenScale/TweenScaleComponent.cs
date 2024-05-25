using Unity.Entities;

namespace RMC.DOTS.Systems.Tween
{
    public struct TweenScaleComponent : IComponentData
    {
        public float From;
        public float To;
        public float DurationInSeconds;
        
        // Set from system
        public float _ElapsedTimeInSeconds;

        public TweenScaleComponent(float from, float to, float durationInSeconds)
        {
            From = from;
            To = to;
            DurationInSeconds = durationInSeconds;
            _ElapsedTimeInSeconds = 0;
        }
    }
}