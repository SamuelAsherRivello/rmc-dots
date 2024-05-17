using Unity.Entities;

namespace RMC.DOTS.Samples.Games.TwinStickShooter3D.TwinStickShooter3D_Version02_DOTS
{
    public struct EnemySpawnComponent : IComponentData
    {
        // ROM
        public readonly Entity Prefab;
        public readonly float SpawnDistanceToPlayer;
        public readonly float InitialMoveSpeed;
        public readonly float InitialTurnSpeed;
        public readonly float InitialHealth;
        public readonly float SpawnIntervalInSecondsMin;
        public readonly float SpawnIntervalInSecondsMax;
        public float SpawnIntervalInSecondsCurrent;
        
        // RAM - Per difficulty
        public float SpawnNextAtElapsedTime;
        
        public EnemySpawnComponent(Entity newPrefab,
            float newSpawnDistanceToPlayer,
            float newInitialMoveSpeed,
            float newInitialTurnSpeed,
            float newInitialHealth,
            float spawnIntervalInSecondsMin,
            float spawnIntervalInSecondsMax
            )
        {
            Prefab = newPrefab;
            SpawnDistanceToPlayer = newSpawnDistanceToPlayer;
            InitialMoveSpeed = newInitialMoveSpeed;
            InitialTurnSpeed = newInitialTurnSpeed;
            InitialHealth = newInitialHealth;
            SpawnIntervalInSecondsMin = spawnIntervalInSecondsMin;
            SpawnIntervalInSecondsMax = spawnIntervalInSecondsMax;
            
            //Will be overwritten each wave
            SpawnIntervalInSecondsCurrent = -1;
            
            //Will be overwritten each frame
            SpawnNextAtElapsedTime = -1;
            
        }

    }
}