using System.Collections.Generic;
using System.Threading.Tasks;
using RMC.Audio;
using RMC.DOTS.Samples.Pong2D.Shared;
using RMCDotsInputActionNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RMC.DOTS.Samples.Pong2D.Pong2D_Version01_GO
{
    //  Namespace Properties ------------------------------

    //  Class Attributes ----------------------------------
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
    
    public enum PlayerType
    {
        Human,
        CPU
    }
    
    /// <summary>
    /// The Example is the main entry point to the demo.
    ///
    /// Responsibilities include to wire together the ECS areas, and the GameObject areas like UI
    /// </summary>
    public class Pong2D_Version01_GO : MonoBehaviour
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
                RefreshIsKinematic();
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
                RefreshIsKinematic();
            }
        }
        
        private int Player01ScoreCurrent
        {
            get
            {
                return __player01ScoreCurrent;
            }
            set
            {
                __player01ScoreCurrent = value;
                RefreshScores();
            }
        }

        private int Player02ScoreCurrent
        {
            get
            {
                return __player02ScoreCurrent;
            }
            set
            {
                __player02ScoreCurrent = value;
                RefreshScores();
            }
        }

        [SerializeField] 
        private bool IsDebug = false;

        //  Fields ----------------------------------------
        [SerializeField] 
        private Common _common;

        /// <summary>
        /// NOTE: This reference is packaged for DOTS use only
        /// but for consistency across demos, we use it here in the non-DOTS version too.
        /// </summary>
        private RMCDotsInputAction _rmcDotsInputAction;

        [SerializeField]
        private PaddleHuman _paddleHuman;
        
        [SerializeField]
        private PaddleCPU _paddleCPU;
        
        [SerializeField]
        private Projectile _projectilePrefab;

        private List<Projectile> _projectiles = new List<Projectile>();

        private Vector3 _paddleHumanMovement = new Vector3();
        private Vector3 _paddleCPUMovement = new Vector3();
        private GameState _gameState = GameState.None;
        private int __player01ScoreCurrent = 0;
        private const int Player01ScoreMax = 3;
        private int __player02ScoreCurrent = 0;
        private const int Player02ScoreMax = 3;
        
        // The double "__" is to discourage direct use
        private bool __isGameOver = false;
        
        // The double "__" is to discourage direct use
        private bool __isGamePaused = true;
        
        //  Unity Methods  --------------------------------
        protected void Awake()
        {
            _rmcDotsInputAction = new RMCDotsInputAction();
        }
        
        protected void OnEnable()
        {
            _rmcDotsInputAction.Enable();
        }
        
        protected void OnDisable()
        {
            _rmcDotsInputAction.Disable();
        }
        
        protected void Start()
        {
            // UI
            _common.MainUI.OnRestartRequest.AddListener(MainUI_OnRestartRequest);
            _common.MainUI.OnRestartConfirm.AddListener(MainUI_OnRestartConfirm);
            _common.MainUI.OnRestartCancel.AddListener(MainUI_OnRestartCancel);
            _common.MainUI.StatusLabel.text = $"Use Arrow Keys";
            _common.MainUI.RestartButton.text = "Restart";

            // GameState
            SetGameState(GameState.Initialized);
         
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
                    if (_gameState == GameState.RoundStarted ||
                        _gameState == GameState.GameEnded)
                    {
                        Debug.Log("TODO: Restart from UI not from mouse. And then show 'Are you sure?' dialog.");
                        MainUI_OnRestartConfirm();
                    }
                }
            }
            
            
            
            if (IsGameOver || IsGamePaused)
            {
                return;
            }


            // HUMAN
            // Read the current input via our custom input asset
            // Use WASD, or if no input, use Arrow Keys
            Vector2 _paddleHumanInput = _rmcDotsInputAction.Standard.Move.ReadValue<Vector2>();

            if (_paddleHumanInput.magnitude == 0)
            {
                _paddleHumanInput = _rmcDotsInputAction.Standard.Look.ReadValue<Vector2>();
            }

            _paddleHumanMovement = new Vector3(0, _paddleHumanInput.y, 0);


            // CPU
            // loop through all projectiles and find the one closest projectile to _paddleCPU
            Projectile closestProjectile = null;
            float closestDistance = float.MaxValue;
            if (_projectiles.Count > 0)
            {
                foreach (var projectile in _projectiles)
                {
                    float distance = Vector3.Distance(_paddleCPU.transform.position, projectile.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestProjectile = projectile;
                    }
                }

                // Move towards closest projectile
                var deltaY = closestProjectile.transform.position.y - _paddleCPU.transform.position.y;
                _paddleCPUMovement = new Vector3(0, deltaY, 0);
            }


        }

        protected void FixedUpdate()
        {
            _paddleHuman.Move(_paddleHumanMovement);
            _paddleCPU.Move(_paddleCPUMovement);
        }

        
        //  Methods ---------------------------------------
        private void RefreshIsKinematic()
        {
            _paddleHuman.Rigidbody.isKinematic = __isGamePaused || __isGameOver;
            _paddleCPU.Rigidbody.isKinematic = __isGamePaused || __isGameOver;

            foreach (var projectiles in _projectiles)
            {
                projectiles.Rigidbody.isKinematic = __isGamePaused || __isGameOver;
            }
        }

        
        private void RefreshScores()
        {
            _common.MainUI.Score01Label.text = $"{Player01ScoreCurrent:00}";
            
            _common.MainUI.Score02Label.text = $"{Player02ScoreCurrent:00}";
            
            if (Player01ScoreCurrent >= Player01ScoreMax ||
                Player02ScoreCurrent >= Player02ScoreMax)
            {
                SetGameState(GameState.GameEnded);
            }
            else
            {
                if (_gameState == GameState.RoundStarted)
                {
                    SetGameState(GameState.RoundStarted);
                }
            }
        }
        
     
        private void SetGameState(GameState gameState)
        {
            if (IsDebug)
            {
                Debug.Log($"OnGameStateChanged() gameState = {gameState}");
            }

            _gameState = gameState;
            switch (_gameState)
            {
                case GameState.Initialized:
                    SetGameState(GameState.GameStarted);
                    break;
                case GameState.GameStarted:
                    Player01ScoreCurrent = 0;
                    Player02ScoreCurrent = 0;
                    IsGameOver = false;
                    IsGamePaused = false;
                    SetGameState(GameState.RoundStarted);
                    break;
                case GameState.RoundStarted:

                    SpawnProjectile();
                
                    
                    break;
                case GameState.GameEnded:
                    IsGameOver = true;
                    IsGamePaused = true;
                    
                    if (Player01ScoreCurrent >= Player01ScoreMax)
                    {
                        _common.MainUI.StatusLabel.text = "You Win!";
                    } 
                    else if (Player02ScoreCurrent >= Player02ScoreMax)
                    {
                        _common.MainUI.StatusLabel.text = "You Lose!";
                    }
                    break;
            
            }
        }

        private void SpawnProjectile()
        {
            Projectile projectile = Instantiate(    
                _projectilePrefab, 
                new Vector3(0, 0, 0),    
                Quaternion.identity);

            _projectiles.Add(projectile);
            projectile.OnGoalHit.AddListener(Projectile_OnGoalHit);
            
            //Always move randomly but only upper-right to start
            var y = Random.Range(0.5f, 1f);
            Vector3 initialForce = new Vector3(
                y * 1.1f, 
                y, 
                0);
            projectile.AddForce(initialForce);
        }


        private void DestroyProjectile(Projectile projectile)
        {
            _projectiles.Remove(projectile);
            Destroy(projectile.gameObject);
        }

        
        //  Event Handlers --------------------------------
        private void Projectile_OnGoalHit(Projectile projectile, Goal goal)
        {
            if (goal.PlayerType == PlayerType.Human)
            {
                Player01ScoreCurrent++;
            }
            else
            {
                Player02ScoreCurrent++;
            }   
            
            AudioManager.Instance.PlayAudioClip("Goal01");
            DestroyProjectile(projectile);
        }
        
        
        private void MainUI_OnRestartRequest()
        {
            AudioManager.Instance.PlayAudioClip("Click01");
            IsGamePaused = true;
        }

        

        private void MainUI_OnRestartCancel()
        {
            AudioManager.Instance.PlayAudioClip("Click02");

            // Only unpause if the game is not over
            if (!IsGameOver)
            {
                IsGamePaused = false;
            }
        }
        
        
        
        private async void MainUI_OnRestartConfirm()
        {
            IsGamePaused = false;
            IsGameOver = false;
            
            
            AudioManager.Instance.PlayAudioClip("Click01");

            // Wait for sound to play
            await Task.Delay(200);
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
            
    
        }
    }
}