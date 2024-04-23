using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.Spawn
{
    public struct SpawnEmitterComponent : IComponentData
    {
        // SpawnPosition
        public Vector3 SpawnPosition;
        
        // SpawnsPerOperation
        public int SpawnsPerOperation;
        
        // SpawnsPerTotal
        public int SpawnsPerTotal;
        
        // TimeTillSpawnInSeconds
        public float TimeTillSpawnInSeconds;
        
        // For resetting TimeTillSpawnInSeconds after each spawn
        public float TimeTillSpawnInSecondsRO; //Readonly
    }
}