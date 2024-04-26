#if ENABLE_INPUT_SYSTEM
using Unity.Entities;
using Unity.Mathematics;

namespace RMC.DOTS.Systems.Input
{
    /// <summary>
    /// Basic reusable input.
    /// If you need to add more input, it's probably best
    /// to COMPLETELY COPY/PASTE and create a NEW input pipeline and system
    /// </summary>
    public struct InputComponent : IComponentData
    {
        public float2 Move;
        public float2 Look;
        public bool Action1;
        public bool Action2;
    }
}
#endif //ENABLE_INPUT_SYSTEM