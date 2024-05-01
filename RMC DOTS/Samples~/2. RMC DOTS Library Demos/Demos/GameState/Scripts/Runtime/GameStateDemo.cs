using System.Threading.Tasks;
using RMC.DOTS.Systems.GameState;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Demos.GameStateDemo
{
    /// <summary>
    /// See <see cref="GameStateSystem"/>
    /// </summary>
    public class GameStateDemo : MonoBehaviour
    {
        //  Fields ----------------------------------------
        private GameStateSystem _gameStateSystem;
        
        //  Initialization --------------------------------
        protected void Start()
        {
            Debug.Log("GameState Demo. Watch the console.");

            // Get the world
            World world = World.DefaultGameObjectInjectionWorld;

            // Get the system
            _gameStateSystem = world.GetExistingSystemManaged<GameStateSystem>();
            
            // Listen for changes
            _gameStateSystem.OnGameStateChanged += OnGameStateChanged;
            _gameStateSystem.OnIsGameOverChanged += OnIsGameOverChanged;
            _gameStateSystem.OnIsGamePausedChanged += OnIsGamePausedChanged;
            
            // Make changes
            SetDefaultsAsync();
        }

        private async void SetDefaultsAsync()
        {
            //TODO: Remove this first-frame limitation of ScoringComponent? Lowpriority
            
            // Wait one frame
            await Task.Delay(100);
        
            // Make changes
            _gameStateSystem.IsGameOver = false;
            _gameStateSystem.IsGamePaused = false;
            _gameStateSystem.GameState = GameState.Initializing;
        }
        

        protected void OnDestroy()
        {
            _gameStateSystem.OnGameStateChanged -= OnGameStateChanged;
            _gameStateSystem.OnIsGameOverChanged -= OnIsGameOverChanged;
            _gameStateSystem.OnIsGamePausedChanged -= OnIsGamePausedChanged;
        }

        //  Methods ---------------------------------------

        //  Event Handlers --------------------------------
        
        private void OnIsGamePausedChanged(bool isGamePaused)
        {
            Debug.Log($"OnIsGamePausedChanged() isGamePaused = {isGamePaused}");
        }
        
        private void OnIsGameOverChanged(bool isGameOver)
        {
            Debug.Log($"OnIsGameOverChanged() isGameOver = {isGameOver}");
        }

        private void OnGameStateChanged(GameState gameState)
        {
            Debug.Log($"OnGameStateChanged() gameState = {gameState}");
            
            //In production, you can use some or all of the available state names
            //Do not edit the original enum
            switch (gameState)
            {
                case GameState.Initializing:
                    _gameStateSystem.GameState = GameState.Initialized;
                    break;
                case GameState.Initialized:
                    _gameStateSystem.GameState = GameState.GameStarting;
                    break;
                case GameState.GameStarting:
                    _gameStateSystem.GameState = GameState.GameStarted;
                    break;
                case GameState.GameStarted:
                    _gameStateSystem.GameState = GameState.RoundStarting;
                    break;
                case GameState.RoundStarting:
                    _gameStateSystem.GameState = GameState.RoundStarted;
                    break;
                case GameState.RoundStarted:
                    _gameStateSystem.GameState = GameState.GameEnding;
                    break;
                case GameState.GameEnding:
                    _gameStateSystem.GameState = GameState.GameEnded;
                    break;
            }

        }
    }
}