using Unity.Entities;
using Unity.Mathematics;
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
        
        public static PhysicsVelocityImpulseComponent FromForce(Vector3 force)
        {
            return new PhysicsVelocityImpulseComponent
            {
                CanBeNegative = false,
                MinForce = force,
                MaxForce = force
            };
        }
        
        public static PhysicsVelocityImpulseComponent FromRandomForce(Vector3 minForce, Vector3 maxForce, bool canBeNegative)
        {
            return new PhysicsVelocityImpulseComponent
            {
                CanBeNegative = canBeNegative,
                MinForce = minForce,
                MaxForce = maxForce
            };
        }
    }
}