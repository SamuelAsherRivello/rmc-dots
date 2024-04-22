using Unity.Entities;

namespace RMC.DOTS.Systems.DestroyEntity
{
    public struct DestroyEntityComponent : IComponentData
    {
        public float TimeTillDestroyInSeconds; //The default of 0 will destroy the entity immediately
    }
}