using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.FollowTarget
{
    public class TargetComponentAuthoring : MonoBehaviour
    {
        public LayerMask MemberOfLayerMask;

        public class TargetComponentAuthoringBaker : Baker<TargetComponentAuthoring>
        {
            public override void Bake(TargetComponentAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent<TargetComponent>(entity,
                    new TargetComponent
                    {
                        MemberOfLayerMask = authoring.MemberOfLayerMask,
                    });

            }
        }
    }
}
