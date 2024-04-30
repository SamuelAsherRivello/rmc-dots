using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Lessons.SpinningCube.Lesson02_ExecuteOnce
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