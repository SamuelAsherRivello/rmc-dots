using System;
using Unity.Entities;

namespace Roguelite.Movement
{
    [Serializable]
    public struct MovementStatsData : IComponentData
    {
        public float MoveSpeed;
        public float Gravity;
    }
}