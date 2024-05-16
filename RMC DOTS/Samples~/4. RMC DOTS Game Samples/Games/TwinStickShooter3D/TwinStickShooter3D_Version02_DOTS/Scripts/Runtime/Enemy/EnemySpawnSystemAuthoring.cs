using RMC.DOTS.Systems.Spawn;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Samples.Games.TwinStickShooter3D.TwinStickShooter3D_Version02_DOTS
{
    /// <summary>
    /// Place this MonoBehaviour on a GameObject in the Scene
    /// To enable the <see cref="SpawnSystem"/>
    /// </summary>

    public class EnemySpawnSystemAuthoring : MonoBehaviour
    {
        public GameObject Prefab;
        public float SpawnDistanceToPlayer = 10.0f;
        public float InitialMoveSpeed = 4.0f;
        public float InitialTurnSpeed = 1.5f;
        public float InitialHealth = 1.0f;
        public float SpawnIntervalInSecondsMin = 0.5f;
        public float SpawnIntervalInSecondsMax = 1.0f;
        
        [SerializeField] 
        public bool IsSystemEnabled = true;

        public struct EnemySpawnSystemIsEnabledTag : IComponentData { }

        public class EnemySpawnerSystemAuthoringBaker : Baker<EnemySpawnSystemAuthoring>
        {
            public override void Bake(EnemySpawnSystemAuthoring authoring)
            {
                if (authoring.IsSystemEnabled)
                {
                    var entity = GetEntity(TransformUsageFlags.None);

                    // NOTE: This fails after clicking 'Restart' Button. Known issue. Use something else.
                    //var elapsedTime = World.DefaultGameObjectInjectionWorld.Time.ElapsedTime
                    var elapsedTime = Time.time; //workaround

                    AddComponent<EnemySpawnSystemIsEnabledTag>(entity);
                    AddComponent(entity, new EnemySpawnComponent(
                        GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                        authoring.SpawnDistanceToPlayer,
                        authoring.InitialMoveSpeed,
                        authoring.InitialTurnSpeed,
                        authoring.InitialHealth,
                        authoring.SpawnIntervalInSecondsMin,
                        authoring.SpawnIntervalInSecondsMax));
                }
            }
        }
    }
}