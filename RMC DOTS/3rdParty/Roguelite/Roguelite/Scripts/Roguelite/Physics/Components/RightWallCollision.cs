using Unity.Entities;
using Unity.Physics;

namespace Roguelite.Physics
{
    public struct RightWallCollision : IComponentData
    {
        public ColliderCastHit Collision;

        public RightWallCollision(ColliderCastHit collision)
        {
            Collision = collision;
        }
    }
}