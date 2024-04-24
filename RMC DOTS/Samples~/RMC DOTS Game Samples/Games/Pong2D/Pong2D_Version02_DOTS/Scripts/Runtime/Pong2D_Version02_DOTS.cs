using System.Threading.Tasks;
using RMC.Audio;
using RMC.DOTS.Systems.GameState;
using RMC.DOTS.Systems.Scoring;
using RMC.DOTS.Utilities;
using RMC.DOTS.Samples.Pong2D.Shared;
using RMC.DOTS.Systems.Spawn;
using Unity.Entities;
using Unity.Scenes;
using UnityEngine;

namespace RMC.DOTS.Samples.Pong2D.Pong2D_Version02_DOTS
{
    //  Namespace Properties ------------------------------

    //  Class Attributes ----------------------------------

    /// <summary>
    /// The Example is the main entry point to the demo.
    ///
    /// Responsibilities include to wire together the ECS areas, and the GameObject areas like UI
    /// </summary>
    public class Pong2D_Version02_DOTS : MonoBehaviour
    {
        //  Events ----------------------------------------


        //  Properties ------------------------------------
        
        /// <summary>
        /// This is used to freeze the rendering of the game.
        /// An alternative would be to have each of my custom systems
        /// check the GameStateSystem.IsGamePaused property.
        /// </summary>
        public bool IsEnabledSimulationSystemGroup
        {
            set
            {
                var simulationSystemGroup = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<SimulationSystemGroup>();
                simulationSystemGroup.Enabled = value;
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
        private SpawnSystem spawnSystem;
        private ScoringSystem _scoringSystem;

        private World _ecsWorld;

        //  Unity Methods  --------------------------------
        protected async void Start()
        {
            _ecsWorld = await DOTSUtility.GetWorldAsync(_subScene);

            // Game State
            _gameStateSystem = _ecsWorld.GetExistingSystemManaged<GameStateSystem>();
            _gameStateSystem.OnIsGameOverChanged += GameStateSystem_OnIsGameOverChanged;
            _gameStateSystem.OnIsGamePausedChanged += GameStateSystem_OnIsGamePausedChanged;
            _gameStateSystem.OnGameStateChanged += GameStateSystem_OnGameStateChanged;
            
            // Scoring
            _scoringSystem = _ecsWorld.GetExistingSystemManaged<ScoringSystem>();
            _scoringSystem.OnScoresChanged += ScoresEventSystem_OnScoresChanged;

            // Spawner
            spawnSystem = _ecsWorld.GetExistingSystemManaged<SpawnSystem>();
     
            // UI
            _common.MainUI.OnRestartRequest.AddListener(MainUI_OnRestartRequest);
            _common.MainUI.OnRestartConfirm.AddListener(MainUI_OnRestartConfirm);
            _common.MainUI.OnRestartCancel.AddListener(MainUI_OnRestartCancel);
            _common.MainUI.StatusLabel.text = $"Use Arrow Keys";
            _common.MainUI.RestartButton.text = "Restart";

            await InitializeAsync();
         
        }

        protected void Update()
        {
            //TODO: Remove this hack after fixing UI. UI Does not take input currently
            if (Input.GetMouseButtonDown(0))
            {
                var screenPercentageX = Input.mousePosition.x / Screen.width;
                var screenPercentageY = Input.mousePosition.y / Screen.height;
                if (screenPercentageX > 0.65f && screenPercentageY > 0.80f)
                {
                    if (_gameStateSystem.GameState == GameState.RoundStarted ||
                        _gameStateSystem.GameState == GameState.GameEnded)
                    {
                        Debug.Log("TODO: Restart from UI not from mouse.");
                        MainUI_OnRestartConfirm();
                    }
                }
          
            }
        }

        protected void OnDestroy()
        {
            DOTSUtility.DisposeAllWorlds();
        }


        //  Methods ---------------------------------------
        private async Task InitializeAsync()
        {
            //TODO: Is this still needed? (Yes, multiple balls happen without it)
            await Task.Delay(300);
            
            _gameStateSystem.GameState = GameState.Initialized;
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
                case GameState.Initialized:
                    _gameStateSystem.GameState = GameState.GameStarted;
                    
                    break;
                case GameState.GameStarted: 
                    _gameStateSystem.IsGameOver = false;
                    _gameStateSystem.IsGamePaused = false;
                    _gameStateSystem.GameState = GameState.RoundStarted;
                    break;
                case GameState.RoundStarted:
                    if (spawnSystem.CanSpawn)
                    {
                        spawnSystem.Spawn(new SpawnRequestComponent
                        {
                            Position = new Vector3 (0,0,0)
                        });
                    }
                    break;
                case GameState.GameEnded:
                    _gameStateSystem.IsGameOver = true;
                    break;
            
            }
        }

        private void GameStateSystem_OnIsGameOverChanged(bool isGameOver)
        {
            // Update UI
            if (isGameOver)
            {
                if (_scoringSystem.ScoringComponent.ScoreComponent01.ScoreCurrent >= 
                    _scoringSystem.ScoringComponent.ScoreComponent01.ScoreMax)
                {
                    _common.MainUI.StatusLabel.text = "You Win!";
                } else if (_scoringSystem.ScoringComponent.ScoreComponent02.ScoreCurrent >= 
                           _scoringSystem.ScoringComponent.ScoreComponent02.ScoreMax)
                {
                    _common.MainUI.StatusLabel.text = "You Lose!";
                }
            }
            
            // Freeze game
            IsEnabledSimulationSystemGroup = !isGameOver;
        }


        private void GameStateSystem_OnIsGamePausedChanged(bool isGamePaused)
        {
            IsEnabledSimulationSystemGroup = !isGamePaused;
        }
        

        private void ScoresEventSystem_OnScoresChanged(ScoringComponent scoringComponent)
        {
            if (IsDebug)
            {
                //Debug.Log($"OnScoresChanged() scoringComponent = {scoringComponent.ScoreComponent01.ScoreCurrent} vs {scoringComponent.ScoreComponent02.ScoreCurrent}");
            }
                        
            _common.MainUI.Score01Label.text = 
                $"{scoringComponent.ScoreComponent01.ScoreCurrent:00}";
            
            _common.MainUI.Score02Label.text = 
                $"{scoringComponent.ScoreComponent02.ScoreCurrent:00}";
            
            if (scoringComponent.ScoreComponent01.ScoreCurrent >= scoringComponent.ScoreComponent01.ScoreMax ||
                scoringComponent.ScoreComponent02.ScoreCurrent >= scoringComponent.ScoreComponent02.ScoreMax)
            {
                //TODO: Why doesn't the audio play for the final goal's collision?
                _gameStateSystem.GameState = GameState.GameEnded;
            }
            else
            {
                if (_gameStateSystem.GameState == GameState.RoundStarted)
                {
                    _gameStateSystem.GameState = GameState.RoundStarted;
                }
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