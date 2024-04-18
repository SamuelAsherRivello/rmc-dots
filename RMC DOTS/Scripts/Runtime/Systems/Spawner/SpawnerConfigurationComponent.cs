using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.Spawner
{
    public struct SpawnerConfigurationComponent : IComponentData
    {
        public Entity Prefab;
    }
}
