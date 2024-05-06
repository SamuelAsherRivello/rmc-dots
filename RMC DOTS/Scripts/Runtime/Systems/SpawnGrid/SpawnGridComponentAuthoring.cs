using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.SpawnGrid
{
    public class SpawnGridComponentAuthoring : MonoBehaviour
    {
        public GameObject Prefab;
        public Transform FromPosition;
        public Transform ToPosition;
        
        [Range(1, 1000)]
        public int Rows = 1;
        
        [Range(1, 1000)]
        public int Columns = 1;
        
        [Range(1, 1000)]
        public int Floors = 1;

        public class SpawnGridComponentAuthoringBaker : Baker<SpawnGridComponentAuthoring>
        {
            public override void Bake(SpawnGridComponentAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);

                if (authoring.Prefab == null ||
                    authoring.FromPosition == null || 
                    authoring.ToPosition == null)
                {
                    return;
                }
                
                AddComponent<SpawnGridComponent>(entity,
                    new SpawnGridComponent
                    {
                        Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                        FromPosition = authoring.FromPosition.position,
                        ToPosition = authoring.ToPosition.position,
                        Rows = authoring.Rows,
                        Columns = authoring.Columns,
                        Floors = authoring.Floors
                    });
                
                AddComponent<SpawnGridSystemExecuteOnceTag>(entity);

            }
        }
    }
}
