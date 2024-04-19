using System.Collections.Generic;
using RMC.Core.Audio;
using RMC.DOTS.Samples.RollABall3D.Shared;
using RMCDotsInputActionNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RMC.DOTS.Samples.RollABall3D.RollABall3D_Version01_GO
{
    //  Namespace Properties ------------------------------

    //  Class Attributes ----------------------------------

    /// <summary>
    /// The Example is the main entry point to the demo
    /// </summary>
    public class RollABall3D_Version01_GO : MonoBehaviour
    {
        //  Events ----------------------------------------


        //  Properties ------------------------------------
        private bool IsGamePaused
        {
            get
            {
                return __isGamePaused;
            }
            set
            {
                __isGamePaused = value;
                _player.GetComponent<Rigidbody>().isKinematic = __isGamePaused || __isGameOver;
            }
        }
        
        private bool IsGameOver
        {
            get
            {
                return __isGameOver;
            }
            set
            {
                __isGameOver = value;
                _player.GetComponent<Rigidbody>().isKinematic = __isGameOver || __isGamePaused;
            }
        }
        
        //  Fields ----------------------------------------
        [SerializeField] 
        private Player _player;

        [SerializeField] 
        private Common _common;

        [SerializeField] 
        private List<Pickup> _pickups = new List<Pickup>();

        /// <summary>
        /// NOTE: This reference is packaged for DOTS use only
        /// but for consistency across demos, we use it here in the non-DOTS version too.
        /// </summary>
        private RMCDotsInputAction _rmcDotsInputAction;
        
        private int _score = 0;
        private int _scoreMax = 0;
        
        // The double "__" is to discourage direct use
        private bool __isGameOver = false;
        
        // The double "__" is to discourage direct use
        private bool __isGamePaused = true;
        
        //  Unity Methods  --------------------------------
        protected void Awake()
        {
            _rmcDotsInputAction = new RMCDotsInputAction();
        }

        protected void Start()
        {

            _player.OnPickup.AddListener(Player_OnPickup);
            
            _common.MainUI.OnRestartRequest.AddListener(MainUI_OnRestartRequest);
            _common.MainUI.OnRestartConfirm.AddListener(MainUI_OnRestartConfirm);
            _common.MainUI.OnRestartCancel.AddListener(MainUI_OnRestartCancel);
            _common.MainUI.StatusLabel.text = $"Use Arrow Keys";
            _common.MainUI.RestartButton.text = "Restart";
            
            IsGamePaused = false;
            _scoreMax = 3;
            _score = 0;
            UpdateScore();
        }

        protected void OnEnable()
        {
            _rmcDotsInputAction.Enable();
        }
        
        protected void OnDisable()
        {
            _rmcDotsInputAction.Disable();
        }

        protected void Update()
        {
            if (IsGameOver || IsGamePaused)
            {
                return;
            }
            
            foreach (Pickup pickup in _pickups)
            {
                pickup.Rotate();
            }
            
            // Read the current input via our custom input asset
            Vector2 playerMoveInput = _rmcDotsInputAction.Standard.Move.ReadValue<Vector2>();
            Vector3 movement = new Vector3(playerMoveInput.x, 0.0f, playerMoveInput.y) * Time.deltaTime;
            _player.Move(movement);
        }


        //  Methods ---------------------------------------
        private void UpdateScore()
        {
            _common.MainUI.ScoreLabel.text = $"Score: {_score}/{_scoreMax}";

        }

        //  Event Handlers --------------------------------
        private void MainUI_OnRestartRequest()
        {
            IsGamePaused = true;
            AudioManager.Instance.PlayAudioClip("Click01");
        }
        
        private void MainUI_OnRestartConfirm()
        {
            IsGamePaused = false;
            AudioManager.Instance.PlayAudioClip("Click01");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        
        private void MainUI_OnRestartCancel()
        {
            IsGamePaused = false;
            AudioManager.Instance.PlayAudioClip("Click02");
        }

        
        private void Player_OnPickup(Pickup pickup)
        {
            if (IsGameOver)
            {
                return;
            }
            
            pickup.gameObject.SetActive (false);

            AudioManager.Instance.PlayAudioClip("Pickup01");
            
            if (++_score >= _scoreMax)
            {
                IsGameOver = true;
                _common.MainUI.StatusLabel.text = "You Win!";
            }
            
            UpdateScore();
        }
    }
}