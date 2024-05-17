using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Samples.Pong2D.Pong2D_Version02_DOTS
{
    public class ProjectileHasHitGoalSystemAuthoring : MonoBehaviour
    {
        [SerializeField] 
        public bool IsSystemEnabled = true;

        public struct ProjectileHasHitGoalSystemIsEnabled: IComponentData { }

        public class ProjectileHasHitGoalSystemAuthoringBaker : Baker<ProjectileHasHitGoalSystemAuthoring>
        {
            public override void Bake(ProjectileHasHitGoalSystemAuthoring authoring)
            {
                if (authoring.IsSystemEnabled)
                {
                    Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                    AddComponent<ProjectileHasHitGoalSystemIsEnabled>(entity);
                }
                
            }
        }
    }
}
