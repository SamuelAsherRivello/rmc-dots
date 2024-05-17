using Unity.Entities;

namespace RMC.DOTS.Demos.SpawnGrid
{
    public struct GridElementMoveComponent : IComponentData
    {
        //Set from unity inspector
        public float Amplitude;
        public float Speed;
    }
}
