using Unity.Entities;

namespace RMC.Playground3D.Pong2D_Version02_DOTS
{
    // This tag component allows us to easily get a reference to our player
    // as we can query for the entity with this tag
    public struct PaddleCPUTag : IComponentData {}
}