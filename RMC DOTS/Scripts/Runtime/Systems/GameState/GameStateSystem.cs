using System;
using RMC.DOTS.SystemGroups;
using Unity.Entities;

namespace RMC.DOTS.Systems.GameState
{
    /// <summary>
    /// This system offers a GameObject-friendy way to
    /// subscribe to, get, and set the GameState.
    /// </summary>
    [UpdateInGroup(typeof(UnpauseableSystemGroup))]
    public partial class GameStateSystem : SystemBase
    {
        public Action<bool> OnIsGameOverChanged;
        public Action<bool> OnIsGamePausedChanged;
        public Action<GameState> OnGameStateChanged;
        public Action<RoundData> OnRoundDataChanged;
        
        public bool IsGameOver
        {
            get
            {
                return SystemAPI.GetSingleton<GameStateComponent>().IsGameOver;
            }
            set
            {
                var gameStateComponent = SystemAPI.GetSingleton<GameStateComponent>();

                SystemAPI.SetSingleton<GameStateComponent>(new GameStateComponent
                {
                    IsGameOver = value,
                    IsGamePaused = gameStateComponent.IsGamePaused,
                    RoundData = gameStateComponent.RoundData,
                    GameState = gameStateComponent.GameState
                });
                OnIsGameOverChanged?.Invoke(value);
            }
        }
        
        public bool IsGamePaused
        {
            get
            {
                return SystemAPI.GetSingleton<GameStateComponent>().IsGamePaused;
            }
            set
            {
                var gameStateComponent = SystemAPI.GetSingleton<GameStateComponent>();

                SystemAPI.SetSingleton<GameStateComponent>(new GameStateComponent
                {
                    IsGameOver = gameStateComponent.IsGameOver,
                    IsGamePaused = value,
                    RoundData = gameStateComponent.RoundData,
                    GameState = gameStateComponent.GameState
                });

                OnIsGamePausedChanged?.Invoke(value);
            }
        }

        public GameState GameState
        {
            get
            {
                
                // Allow for "first-frame" access or similar, perhaps before systems are ready
                if (!SystemAPI.HasSingleton<GameStateComponent>())
                {
                    SystemAPI.SetSingleton<GameStateComponent>(new GameStateComponent());
                }
                
                return SystemAPI.GetSingleton<GameStateComponent>().GameState;
            }
            set
            {
                var gameStateComponent = SystemAPI.GetSingleton<GameStateComponent>();

                SystemAPI.SetSingleton<GameStateComponent>(new GameStateComponent
                {
                    IsGameOver = gameStateComponent.IsGameOver,
                    IsGamePaused = gameStateComponent.IsGamePaused,
                    RoundData = gameStateComponent.RoundData,
                    GameState = value
                });

                OnGameStateChanged?.Invoke(value);
            }
        }
        
        public RoundData RoundData
        {
            get
            {
                return SystemAPI.GetSingleton<GameStateComponent>().RoundData;
            }
            set
            {
                var gameStateComponent = SystemAPI.GetSingleton<GameStateComponent>();

                SystemAPI.SetSingleton<GameStateComponent>(new GameStateComponent
                {
                    IsGameOver = gameStateComponent.IsGameOver,
                    IsGamePaused = gameStateComponent.IsGamePaused,
                    RoundData = value,
                    GameState = gameStateComponent.GameState
                });

                OnRoundDataChanged?.Invoke(value);
            }
        }

   
        protected override void OnCreate()
        {
            RequireForUpdate<GameStateSystemAuthoring.GameStateSystemIsEnabledTag>();

        }

        /// <summary>
        ///  Nothing needed here, unless I want changes NOT set through the API
        ///  above, but instead directly on components to be reflected here
        ///  If so, copy from <see cref="ScoringSystem"/>
        /// </summary>
        protected override void OnUpdate()
        {
            //Nothing
        }


    }
}