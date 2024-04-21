using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Samples.Templates.DOTSGameTemplate
{
    public class MoveComponentAuthoring : MonoBehaviour
    {
        public float Speed = 7.5f;
        
        public class MoveComponentAuthoringBaker : Baker<MoveComponentAuthoring>
        {
            public override void Bake(MoveComponentAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                                
                AddComponent<MoveComponent>(entity, 
                    new MoveComponent { Value = authoring.Speed });
            }
        }
    }
}