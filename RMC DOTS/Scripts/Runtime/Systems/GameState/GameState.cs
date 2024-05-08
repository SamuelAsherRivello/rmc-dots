using Unity.Entities;
namespace RMC.DOTS.Systems.GameState
{
    // Games can use some or all of these
    // Avoid adding/removing states once this ships
    public enum GameState
    {
        None,
        Initializing,
        Initialized,
        GameStarting,
        GameStarted,
        RoundStarting,
        RoundStarted,
        GameEnding,
        GameEnded
    }
}