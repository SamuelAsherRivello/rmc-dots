using Unity.Entities;

namespace RMC.DOTS.Samples.RollABall3D.RollABall3D_Version02_DOTS
{
    /// <summary>
    /// This component gets added to a pickup if it has been picked up this frame. An entity with this component will
    /// then run on systems to increment the score counter, play a sound, and destroy the pickup.
    /// </summary>
    public struct PickupWasCollectedTag : IComponentData {}
}