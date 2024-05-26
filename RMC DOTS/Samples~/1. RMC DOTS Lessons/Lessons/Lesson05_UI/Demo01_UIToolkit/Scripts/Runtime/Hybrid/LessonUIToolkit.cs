using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Lessons.UI.UIToolkit
{
    public class LessonUIToolkit : MonoBehaviour
    {
        
        //  Fields (GO) -----------------------------------
        [SerializeField] 
        private Common _common;

        
        //  Fields (DOTS) ---------------------------------
        private World _ecsWorld;
        private SimpleScoreSystem _simpleScoreSystem;
        
        
        //  Unity Methods  --------------------------------
        protected void Start()
        {
            Debug.Log("Lesson UI Toolkit. Start()");
            
            // DOTS World
            _ecsWorld = World.DefaultGameObjectInjectionWorld;
            
            // DOTS System
            _simpleScoreSystem = _ecsWorld.GetExistingSystemManaged<SimpleScoreSystem>();
            _simpleScoreSystem.OnScoreChanged += ScoresEventSystem_OnScoresChanged;

            // GO User Interface
            _common.MainUI.OnIncrementScore.AddListener(MainUI_OnIncrementScore);
            _common.MainUI.StatusLabel.text = $"Use Arrow Keys";
            _common.MainUI.IncrementScoreButton.text = "Increment Score";
            
        }
        
        
        //  Methods  -------------------------------------
        private void UpdateScoreUI(SimpleScoreComponent simpleScoreComponent = default)
        {
            // Show blank values upon startup
            if (simpleScoreComponent.Equals(default(SimpleScoreComponent)))
            {
                simpleScoreComponent = new SimpleScoreComponent();
            }
            
            _common.MainUI.ScoreLabel.text = $"Score: {simpleScoreComponent.Score}";
        }

        
        //  Event Handlers (GO) ------------------------------
        private void MainUI_OnIncrementScore()
        {
            _simpleScoreSystem.IncrementScoreBy(1);
        }
        
        
        //  Event Handlers (DOTS) -----------------------------
        private void ScoresEventSystem_OnScoresChanged(SimpleScoreComponent simpleScoreComponent)
        {
            UpdateScoreUI(simpleScoreComponent);
        }
    }
}