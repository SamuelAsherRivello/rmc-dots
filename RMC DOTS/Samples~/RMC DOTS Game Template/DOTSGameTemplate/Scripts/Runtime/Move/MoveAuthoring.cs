using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Samples.Templates.DOTSGameTemplate
{
    public class MoveAuthoring : MonoBehaviour
    {
        public float Speed = 7.5f;
        
        public class PlayerMoveAuthoringBaker : Baker<MoveAuthoring>
        {
            public override void Bake(MoveAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                                
                AddComponent<MoveComponent>(entity, 
                    new MoveComponent { Value = authoring.Speed });
            }
        }
    }
}