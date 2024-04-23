using Unity.Entities;
using Unity.Mathematics;

namespace RMC.DOTS.Systems.Waypoints
{
    /// <summary>
    /// Unity ECS does not support lists or arrays ON ENTITIES
    /// So you use a Buffer of type IBufferElementData instead.
    /// </summary>
    public struct WaypointBufferElementData : IBufferElementData
    {
        public float3 Position;
    }
}
