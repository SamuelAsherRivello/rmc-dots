using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.FollowTarget
{
    public class FollowTargetSystemAuthoring : MonoBehaviour
    {
        [SerializeField] 
        public bool IsSystemEnabled = true;

        public struct FollowTargetSystemAuthoringIsEnabledTag : IComponentData { }

        public class FollowTargetSystemAuthoringBaker : Baker<FollowTargetSystemAuthoring>
        {
            public override void Bake(FollowTargetSystemAuthoring authoring)
            {
                if (authoring.IsSystemEnabled)
                {
                    Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                    AddComponent<FollowTargetSystemAuthoringIsEnabledTag>(entity);
                }
            }
        }
    }
}