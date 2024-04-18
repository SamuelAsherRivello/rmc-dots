using Unity.Entities;

namespace RMC.Playground3D.RollABall3D_Version02_DOTS
{
    /// <summary>
    /// This component defines the strength of force that is applied to the player when moving through the world
    /// </summary>
    public struct PlayerMoveComponent : IComponentData
    {
        public float Value;
    }
}