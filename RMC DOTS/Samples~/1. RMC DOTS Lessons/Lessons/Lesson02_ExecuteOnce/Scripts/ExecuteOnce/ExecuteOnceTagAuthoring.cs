using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Lessons.ExecuteOnce
{
    public class ExecuteOnceTagAuthoring : MonoBehaviour
    {
        public class ExecuteOnceTagAuthoringBaker : Baker<ExecuteOnceTagAuthoring>
        {
            public override void Bake(ExecuteOnceTagAuthoring authoring)
            {
                
                //  Entity  ------------------------------------
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new ExecuteOnceTag
                {
                    
                });
            }
        }
    }
}