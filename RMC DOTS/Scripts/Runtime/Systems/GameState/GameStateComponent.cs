using Unity.Entities;
namespace RMC.DOTS.Systems.GameState
{
    public struct GameStateComponent : IComponentData
    {
        public bool IsGamePaused;
        public bool IsGameOver;
        public GameState GameState;
        public RoundData RoundData;
    }
}