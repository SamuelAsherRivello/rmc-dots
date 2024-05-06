using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Lessons.SharedComponent
{
    public class ClickCountSharedComponentAuthoring : MonoBehaviour
    {
        public class ClickCountSharedComponentAuthoringBaker : Baker<ClickCountSharedComponentAuthoring>
        {
            public override void Bake(ClickCountSharedComponentAuthoring authoring)
            {
                
                //  Entity  ------------------------------------
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddSharedComponent(entity, new ClickCountSharedComponent
                {
                    Value = 0
                });
            }
        }
    }
}