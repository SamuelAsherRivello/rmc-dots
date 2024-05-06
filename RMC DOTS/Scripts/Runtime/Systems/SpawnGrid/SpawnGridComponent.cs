using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.SpawnGrid
{
    public struct SpawnGridComponent : IComponentData
    {
        public Entity Prefab;
        public Vector3 FromPosition;
        public Vector3 ToPosition;
        public int Rows;
        public int Columns;
        public int Floors;
    }
}
