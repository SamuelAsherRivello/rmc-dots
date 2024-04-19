using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.GameState
{
    /// <summary>
    /// Place this MonoBehaviour on a GameObject in the Scene
    /// To enable the <see cref="PhysicsTriggerSystem"/>
    /// </summary>
    public class GameStateSystemAuthoring : MonoBehaviour
    {
        [SerializeField] 
        public bool IsSystemEnabled = true;
        
        public struct GameStateSystemIsEnabledTag : IComponentData {}
        
        public class GameStateBaker : Baker<GameStateSystemAuthoring>
        {
            public override void Bake(GameStateSystemAuthoring systemAuthoring)
            {
                if (systemAuthoring.IsSystemEnabled)
                {
                    Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                    
                    //Turn system on
                    AddComponent<GameStateSystemIsEnabledTag>(entity);
                    
                    //Create first component with default values
                    AddComponent<GameStateComponent>(entity,
                        new GameStateComponent { IsGamePaused = false, IsGameOver = false });
                }
            }
        }
    }
}
