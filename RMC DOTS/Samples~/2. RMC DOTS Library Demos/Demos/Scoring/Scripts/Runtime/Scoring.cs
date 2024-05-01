using System.Threading.Tasks;
using RMC.DOTS.Systems.Scoring;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Demos.Scoring
{
    /// <summary>
    /// See <see cref="ScoringSystem"/>
    /// </summary>
    public class Scoring : MonoBehaviour
    {
        //  Fields ----------------------------------------
        private ScoringSystem _scoringSystem;
        
        //  Initialization --------------------------------
        protected void Start()
        {
            Debug.Log("Scoring Demo. Watch the console.");

            // Get the world
            World world = World.DefaultGameObjectInjectionWorld;

            // Get the system
            _scoringSystem = world.GetExistingSystemManaged<ScoringSystem>();
            
            // Listen for changes
            _scoringSystem.OnScoringComponentChanged += OnScoresChanged;

            // Make changes
            SetDefaultsAsync();
        }

        private async void SetDefaultsAsync()
        {
            //TODO: Remove this first-frame limitation of ScoringComponent? Lowpriority
            
            // Wait one frame
            await Task.Delay(100);
        
            // Must get and set the ENTIRE object for any changes
            ScoringComponent newScoringComponent = _scoringSystem.ScoringComponent;
            newScoringComponent.ScoreComponent01.ScoreCurrent = 1;
            _scoringSystem.ScoringComponent = newScoringComponent;
        }

        protected void OnDestroy()
        {
            _scoringSystem.OnScoringComponentChanged -= OnScoresChanged;

        }
        
        
        //  Event Handlers --------------------------------
        private void OnScoresChanged(ScoringComponent scoringComponent)
        {
            Debug.Log($"OnScoresChanged() ScoreCurrent01 = {scoringComponent.ScoreComponent01.ScoreCurrent}");

            if (scoringComponent.ScoreComponent01.ScoreCurrent == scoringComponent.ScoreComponent01.ScoreMax)
            {
                Debug.Log("You have enough points to win!");
            }
            else
            {
                // Must get and set the ENTIRE object for any changes
                ScoringComponent newScoringComponent = scoringComponent;
                newScoringComponent.ScoreComponent01.ScoreCurrent++;
                _scoringSystem.ScoringComponent = newScoringComponent;
            }
        }
        
    }
}