using System.Threading.Tasks;
using RMC.Audio;
using RMC.Core.Utilities;
using RMC.DOTS.SystemGroups;
using RMC.DOTS.Systems.GameState;
using RMC.DOTS.Systems.Scoring;
using RMC.DOTS.Utilities;
using Unity.Entities;
using Unity.Scenes;
using UnityEngine;

namespace RMC.DOTS.Samples.Templates.DOTSGameTemplate
{
    //  Namespace Properties ------------------------------

    //  Class Attributes ----------------------------------

    /// <summary>
    /// The Example is the main entry point to the demo.
    ///
    /// Responsibilities include to wire together the ECS areas, and the GameObject areas like UI
    /// </summary>
    public class DOTSTemplate : MonoBehaviour
    {
        //  Events ----------------------------------------


        //  Properties ------------------------------------
        
        /// <summary>
        /// This is used to freeze the rendering of the game.
        /// An alternative would be to have each of my custom systems
        /// check the GameStateSystem.IsGamePaused property.
        /// </summary>
        public bool IsEnabledAllPauseableSystems
        {
            set
            {
                World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<SimulationSystemGroup>().Enabled = value;
                World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<PauseablePresentationSystemGroup>().Enabled = value;
            }
            get
            {
                var isEnabled =
                    World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<SimulationSystemGroup>().Enabled &&
                    World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<PauseablePresentationSystemGroup>()
                        .Enabled;
                return isEnabled;
            }
        }
        
        
        //  Fields ----------------------------------------
        [SerializeField] 
        private Common _common;

        [SerializeField] 
        private SubScene _subScene;

        [SerializeField] 
        private bool IsDebug = false;

        private GameStateSystem _gameStateSystem;

        private World _ecsWorld;

        //  Unity Methods  --------------------------------
        protected async void Start()
        {
            
            // ECS
            _ecsWorld = await DOTSUtility.GetWorldAsync(_subScene);

            // Game State
            _gameStateSystem = _ecsWorld.GetExistingSystemManaged<GameStateSystem>();
            _gameStateSystem.OnIsGameOverChanged += GameStateSystem_OnIsGameOverChanged;
            _gameStateSystem.OnIsGamePausedChanged += GameStateSystem_OnIsGamePausedChanged;
            _gameStateSystem.OnGameStateChanged += GameStateSystem_OnGameStateChanged;

            // Scoring
            ScoringSystem scoringSystem = _ecsWorld.GetExistingSystemManaged<ScoringSystem>();
            scoringSystem.OnScoringComponentChanged += ScoresEventSystem_OnScoresChanged;

            // UI
            _common.MainUI.OnRestartRequest.AddListener(MainUI_OnRestartRequest);
            _common.MainUI.OnRestartConfirm.AddListener(MainUI_OnRestartConfirm);
            _common.MainUI.OnRestartCancel.AddListener(MainUI_OnRestartCancel);

            _common.MainUI.StatusLabel.text = $"Use Arrow Keys";
            _common.MainUI.RestartButton.text = "Restart";

            await InitializeAsync();
        }

        protected void OnDestroy()
        {
            DOTSUtility.DisposeAllWorlds();
        }


        //  Methods ---------------------------------------
        private async Task InitializeAsync()
        {
            await DOTSUtility.IsWorldReadyAsync(_subScene);
            _gameStateSystem.GameState = GameState.Initializing;
        }


        //  Event Handlers --------------------------------
        private void GameStateSystem_OnGameStateChanged(GameState gameState)
        {
            if (IsDebug)
            {
                Debug.Log($"OnGameStateChanged() gameState = {gameState}");
            }
            
            
            switch (gameState)
            {
                case GameState.Initializing:
                    _gameStateSystem.GameState = GameState.Initialized;
                    break;
                case GameState.Initialized:
                    _gameStateSystem.GameState = GameState.GameStarted;
                    break;
                case GameState.GameStarted: 
                    _gameStateSystem.IsGamePaused = false;
                    _gameStateSystem.IsGameOver = false;
                    break;
                case GameState.GameEnded: 
                     _gameStateSystem.IsGamePaused = true; 
                     _gameStateSystem.IsGameOver = true;
                    break;
            }
        }
        
        private void GameStateSystem_OnIsGameOverChanged(bool isGameOver)
        {
            if (isGameOver)
            {
                // Show victory
                _common.MainUI.StatusLabel.text = "You Win!";

                // Freeze game
                IsEnabledAllPauseableSystems = false;
            }
        }


        private void GameStateSystem_OnIsGamePausedChanged(bool isGamePaused)
        {
            IsEnabledAllPauseableSystems = !isGamePaused;;
        }
        

        
        private void ScoresEventSystem_OnScoresChanged(ScoringComponent scoringComponent)
        {
            _common.MainUI.ScoreLabel.text = 
                $"Score: {scoringComponent.ScoreComponent01.ScoreCurrent}/{scoringComponent.ScoreComponent01.ScoreMax}";
            
            if (scoringComponent.ScoreComponent01.ScoreCurrent >= scoringComponent.ScoreComponent01.ScoreMax)
            {
                _gameStateSystem.GameState = GameState.GameEnded;
            }
        }
        
        private void MainUI_OnRestartRequest()
        {
            AudioManager.Instance.PlayAudioClip("Click01");

            _gameStateSystem.IsGamePaused = true;
        }


        private void MainUI_OnRestartCancel()
        {
            AudioManager.Instance.PlayAudioClip("Click02");

            //only unpause if the game is not over
            if (!_gameStateSystem.IsGameOver)
            {
                _gameStateSystem.IsGamePaused = false;
            }
        }

        
        private async void MainUI_OnRestartConfirm()
        {
            AudioManager.Instance.PlayAudioClip("Click01");

            _gameStateSystem.IsGamePaused = false;

            await DOTSUtility.ReloadWorldAsync(_subScene);
        }

    }
}