using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.Spawner
{
    public struct SpawnerRequestComponent : IComponentData
    {
        public Vector3 Position;
    }
}
