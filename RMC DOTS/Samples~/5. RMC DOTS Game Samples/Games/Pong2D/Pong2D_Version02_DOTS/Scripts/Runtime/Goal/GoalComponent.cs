﻿using Unity.Entities;

namespace RMC.DOTS.Samples.Pong2D.Pong2D_Version02_DOTS
{
    public struct GoalComponent : IComponentData
    {
        public PlayerType PlayerType;
    }
}