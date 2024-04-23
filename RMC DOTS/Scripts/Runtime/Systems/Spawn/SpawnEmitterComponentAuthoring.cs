using System;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.Spawn
{
    public class SpawnEmitterComponentAuthoring : MonoBehaviour
    {
        public float TimeTillSpawnInSeconds; //The default of 0 will spawn the entity immediately
        public Transform SpawnPosition;
        
        [Range(1, 1000)]
        public int SpawnsPerOperation = 1;
        
        [Range(1, 100000)]
        public int SpawnsPerTotal = 1;

        protected void OnValidate()
        {
            // SpawnsPerOperation must be less than SpawnsPerTotal
            SpawnsPerOperation = Math.Clamp(SpawnsPerOperation, 1, SpawnsPerTotal);
        }

        public class SpawnEmitterComponentAuthoringBaker : Baker<SpawnEmitterComponentAuthoring>
        {
            public override void Bake(SpawnEmitterComponentAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent<SpawnEmitterComponent>(entity,
                    new SpawnEmitterComponent
                    {
                            // SpawnPosition
                            SpawnPosition = authoring.SpawnPosition.position,
                        
                            // SpawnsPerOperation
                            SpawnsPerOperation = authoring.SpawnsPerOperation,
                        
                            // SpawnsPerTotal
                            SpawnsPerTotal = authoring.SpawnsPerTotal,
                        
                            // TimeTillSpawnInSeconds
                            TimeTillSpawnInSeconds = authoring.TimeTillSpawnInSeconds,
                            TimeTillSpawnInSecondsRO = authoring.TimeTillSpawnInSeconds
                        
                    });

            }
        }
    }
}
