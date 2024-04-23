using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.PhysicsVelocityImpulse
{
    /// <summary>
    /// Apply a linear impulse to an entity EXACTLY ONE TIME
    /// </summary>
    public struct PhysicsVelocityImpulseComponent : IComponentData
    {
        public bool CanBeNegative;
        public Vector3 MinForce;
        public Vector3 MaxForce;
    }
}