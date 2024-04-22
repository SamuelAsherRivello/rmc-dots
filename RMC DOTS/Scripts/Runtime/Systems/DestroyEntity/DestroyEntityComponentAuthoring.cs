using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.DestroyEntity
{
    public class DestroyEntityComponentAuthoring : MonoBehaviour
    {
        [SerializeField] 
        public bool WillDestroy = true;
        
        [Tooltip("The default of 0 will destroy the entity immediately")]
        [SerializeField] 
        public float TimeTillDestroyInSeconds = 0; //
        
        public class DestroyEntityComponentAuthoringBaker : Baker<DestroyEntityComponentAuthoring>
        {
            public override void Bake(DestroyEntityComponentAuthoring authoring)
            {
                if (authoring.WillDestroy)
                {
                    Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                    
                    //Delete entity
                    AddComponent<DestroyEntityComponent>(entity, new DestroyEntityComponent()
                    {
                        TimeTillDestroyInSeconds = authoring.TimeTillDestroyInSeconds
                    });
                }
            }
        }
    }
}
