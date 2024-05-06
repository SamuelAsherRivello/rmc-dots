using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace RMC.DOTS.Systems.SpawnGrid
{
    public struct SpawnedGridElementComponent : IComponentData
    {
        public float3 PositionInitial;
        public int Row;
        public int Column;
        public int Floor;
    }
}
