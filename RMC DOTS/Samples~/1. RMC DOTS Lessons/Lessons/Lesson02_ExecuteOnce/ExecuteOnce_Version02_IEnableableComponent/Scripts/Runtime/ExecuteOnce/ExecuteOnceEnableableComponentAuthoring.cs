using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Lessons.ExecuteOnce.ExecuteOnce_Version02_IEnableableComponent
{
    public class ExecuteOnceEnableableComponentAuthoring : MonoBehaviour
    {
        public class ExecuteOnceEnableableComponentAuthoringBaker : Baker<ExecuteOnceEnableableComponentAuthoring>
        {
            public override void Bake(ExecuteOnceEnableableComponentAuthoring authoring)
            {
                
                //  Entity  ------------------------------------
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new ExecuteOnceEnableableTag
                {
                    
                });
            }
        }
    }
}