using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.Spawn
{
    public struct SpawnRequestComponent : IComponentData
    {
        public Vector3 Position;
    }
}
