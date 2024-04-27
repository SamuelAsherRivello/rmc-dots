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
        //WASD or LEFT STICK
        public float2 MoveFloat2;
        
        //ARROWS or RIGHT STICK
        public float2 LookFloat2;
        
        //SPACE or SOUTH BUTTON
        public bool IsPressedAction1;
        public bool WasPressedThisFrameAction1;
        
        //RETURN or EAST BUTTON
        public bool IsPressedAction2;
        public bool WasPressedThisFrameAction2;
    }
}
#endif //ENABLE_INPUT_SYSTEM