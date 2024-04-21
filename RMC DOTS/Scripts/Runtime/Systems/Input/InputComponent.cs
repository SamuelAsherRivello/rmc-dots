#if ENABLE_INPUT_SYSTEM
using Unity.Entities;
using Unity.Mathematics;

namespace RMC.DOTS.Systems.Input
{
    public struct InputComponent : IComponentData
    {
        public float2 Move;
    }
}
#endif //ENABLE_INPUT_SYSTEM