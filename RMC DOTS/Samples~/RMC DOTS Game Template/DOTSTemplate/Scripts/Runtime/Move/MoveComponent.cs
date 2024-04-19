using Unity.Entities;

namespace RMC.DOTS.Samples.DOTSTemplate
{
    /// <summary>
    /// This component defines the strength of force that is applied to the player when moving through the world
    /// </summary>
    public struct MoveComponent : IComponentData
    {
        public float Value;
    }
}