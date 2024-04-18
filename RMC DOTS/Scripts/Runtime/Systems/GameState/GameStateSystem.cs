using System;
using Unity.Entities;

namespace RMC.DOTS.Systems.GameState
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public partial class GameStateSystem : SystemBase
    {
        public Action<bool> OnIsGameOverChanged;
        public Action<bool> OnIsGamePausedChanged;
        public Action<GameState> OnGameStateChanged;
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
                    GameState = gameStateComponent.GameState
                });

                OnIsGamePausedChanged?.Invoke(value);
            }
        }

        public GameState GameState
        {
            get
            {
                return SystemAPI.GetSingleton<GameStateComponent>().GameState;
            }
            set
            {
                var gameStateComponent = SystemAPI.GetSingleton<GameStateComponent>();

                SystemAPI.SetSingleton<GameStateComponent>(new GameStateComponent
                {
                    IsGameOver = gameStateComponent.IsGameOver,
                    IsGamePaused = gameStateComponent.IsGamePaused,
                    GameState = value
                });

                OnGameStateChanged?.Invoke(value);
            }
        }
        
        protected override void OnCreate()
        {
            RequireForUpdate<GameStateSystemAuthoring.GameStateSystemIsEnabledTag>();
        }
        
        protected override void OnUpdate() { }


    }
}