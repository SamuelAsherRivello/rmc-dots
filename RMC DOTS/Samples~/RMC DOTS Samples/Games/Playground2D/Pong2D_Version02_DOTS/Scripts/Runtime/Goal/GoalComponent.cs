using RMC.DOTS.Systems.PhysicsTrigger;
using Unity.Entities;
using UnityEngine;

namespace RMC.Playground3D.Pong2D_Version02_DOTS
{
    public struct GoalComponent : IComponentData
    {
        public PlayerType PlayerType;
    }
}