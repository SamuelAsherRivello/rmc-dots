using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.Culling
{
    public class CullingComponentAuthoring : MonoBehaviour
    {
        [SerializeField] 
        public bool WillDestroyWhenCulled = false;
        
        public class CullingComponentAuthoringBaker : Baker<CullingComponentAuthoring>
        {
            public override void Bake(CullingComponentAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent<CullingComponent>(entity,
                    new CullingComponent
                    (
                        authoring.WillDestroyWhenCulled
                    ));

            }
        }
    }
}
