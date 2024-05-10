using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.FollowTarget
{
    public class FollowerComponentAuthoring : MonoBehaviour
    {
        public LayerMask TargetsLayerMask;
        public float AngularSpeed = 10;
        public float LinearSpeed = 10;
        public float Radius = 3;
            
        public class FollowTargetComponentAuthoringBaker : Baker<FollowerComponentAuthoring>
        {
            public override void Bake(FollowerComponentAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent<FollowerComponent>(entity,
                    new FollowerComponent
                    {
                        TargetsLayerMask = authoring.TargetsLayerMask,
                        AngularSpeed = authoring.AngularSpeed,
                        LinearSpeed = authoring.LinearSpeed,
                        Radius = authoring.Radius,
                    });

            }
        }
    }
}
