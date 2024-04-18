#if ENABLE_INPUT_SYSTEM
using Unity.Entities;
using Unity.Mathematics;

namespace RMC.DOTS.Systems.Input
{
    /// <summary>
    /// This component will live on a dedicated input entity. Only one of these entities will exist in the entity world
    /// making this component a singleton component.
    /// The GetPlayerInputSystem will write to this component in the initialization system group, and subsequent systems
    /// in the simulation system group will be able to read this input.
    /// </summary>
    public struct InputComponent : IComponentData
    {
        // The float2 type is similar to the Vector2 type as it is composed of two X,Y float values. This new type is
        // part of the Unity.Mathematics library, which has many types and math operations that are fully compatible
        // with the high-performance Burst compiler.
        public float2 Move;
    }
}
#endif //ENABLE_INPUT_SYSTEM