using Unity.Burst;
using Unity.Entities;
namespace RMC.DOTS.Systems.FrameCount
{
    /// <summary>
    /// Debugging system that makes the "FrameCountSystem.FrameCount" static value easily
    /// available from any scope.
    ///
    /// TODO: I thought framecount was not easily accessible in some systems, but perhaps it is with Unity's Time.frameCount?
    /// If that's ALWAYS available, then consider to remove this custom system entirely
    ///
    /// 
    /// </summary>
    [UpdateInGroup(typeof(PresentationSystemGroup), OrderLast = true), BurstCompile]
    public partial struct FrameCountSystem : ISystem
    {
        sealed class SharedFrameCount
        {
            internal static readonly SharedStatic<int> Ref = SharedStatic<int>.GetOrCreate<SharedFrameCount>();
        }

        
        public static int FrameCount
        {
            get => SharedFrameCount.Ref.Data;
            private set => SharedFrameCount.Ref.Data = value;
        }

        public void OnCreate(ref SystemState state)
        {
            //-1 means system is not running
            FrameCount = -1;
            
            state.RequireForUpdate<FrameCountSystemAuthoring.FrameCountSystemIsEnabledTag>();
        }

        public void OnDestroy(ref SystemState state) => FrameCount = 0;
        [BurstCompile] public void OnUpdate(ref SystemState state)
        {
            if (FrameCount == -1)
            {
                FrameCount = 0;
            }
            
            FrameCount++;
        }
    }
}
