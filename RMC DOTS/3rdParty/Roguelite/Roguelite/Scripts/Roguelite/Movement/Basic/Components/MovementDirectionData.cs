using Unity.Entities;
using Unity.Mathematics;

namespace Roguelite.Movement
{
    public struct MovementDirectionData : IComponentData
    {
        public float3 Movement;
        public float HorizontalInput;
        public float LookDirection;

        public MovementDirectionData(float lookDirection)
        {
            Movement = default;
            HorizontalInput = default;
            LookDirection = lookDirection;
        }
    }
}