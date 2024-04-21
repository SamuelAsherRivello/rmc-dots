using Unity.Entities;

namespace RMC.DOTS.Systems.Player
{
    // This tag component allows us to easily get a reference to our player
    // as we can query for the entity with this tag
    public struct PlayerTag : IComponentData {}
}