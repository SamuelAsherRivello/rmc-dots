using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Samples.Pong2D.Pong2D_Version02_DOTS
{
    /// <summary>
    /// Apply a linear impulse to an entity EXACTLY ONE TIME
    /// </summary>
    public struct ApplyLinearImpulseComponent : IComponentData
    {
        public Vector3 Value;
        public bool WasApplied;
    }
}