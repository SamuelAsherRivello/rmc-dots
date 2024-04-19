using Unity.Entities;
using Unity.Physics;

namespace Roguelite.Physics
{
    public struct GroundCollision : IComponentData
    {
        public ColliderCastHit Collision;
        public bool SkipThisCollision;

        public GroundCollision(ColliderCastHit collision, bool skipThisCollision = false)
        {
            Collision = collision;
            SkipThisCollision = skipThisCollision;
        }
    }
}