using Unity.Entities;

namespace RMC.DOTS.Systems.Spawn
{
    public struct SpawnConfigurationComponent : IComponentData
    {
        public Entity SpawnPrefab;
    }
}
