using Unity.Entities;
using Unity.Physics;

namespace Roguelite.Physics
{
    public struct LeftWallCollision : IComponentData
    {
        public ColliderCastHit Collision;

        public LeftWallCollision(ColliderCastHit collision)
        {
            Collision = collision;
        }
    }
}