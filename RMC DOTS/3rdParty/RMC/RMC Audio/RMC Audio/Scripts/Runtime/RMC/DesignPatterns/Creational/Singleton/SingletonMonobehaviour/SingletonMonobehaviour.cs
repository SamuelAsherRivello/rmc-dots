using UnityEditor;
using UnityEngine;

namespace RMC.Audio.DesignPatterns.Creational.Singleton.CustomSingletonMonobehaviour
{

    public abstract class SingletonMonobehaviour : MonoBehaviour
    {
        private static bool _IsShuttingDown = false;

        public bool HasDontDestroyOnLoad
        {
            get
            {
                return (gameObject.hideFlags & HideFlags.DontSave) != HideFlags.DontSave;
            }
        }
        
        public static bool IsShuttingDown
        {
            get
            {
                return _IsShuttingDown;
            }
            internal set
            {
                _IsShuttingDown = value;
            }
        }
        
#if UNITY_EDITOR

        [InitializeOnLoadMethod]
        static void InitializeOnLoadMethod()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        /// <summary>
        /// This InitializeOnLoadMethod scheme is designed to
        /// Properly reset the IsShuttingDown each time play mode ends
        ///
        /// The IsShuttingDown helps prevent non-singletons in their OnDestroy()
        /// from accidentally calling singleton.Instantiate which causes issues
        /// 
        /// </summary>
        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredEditMode)
            {
                //Debug.Log(1);
                IsShuttingDown = false;
            }
            else if (state == PlayModeStateChange.EnteredPlayMode)
            {
               // Debug.Log(2);
            }
            //Debug.Log(state);
        }
#endif
    }

    public abstract class SingletonMonobehaviour<T> : SingletonMonobehaviour  where T : MonoBehaviour
    {
        
        //  Properties ------------------------------------------
        
        public static bool IsInstantiated
        {
            get
            {
                return _Instance != null;
            }
        }

        public static T Instance
        {
            get
            {
                if (!IsInstantiated)
                {
                    Instantiate();
                }
                return _Instance;
            }
            set
            {
                _Instance = value;
            }

        }

        //  Fields -------------------------------------------------
        private static T _Instance;
       
        public delegate void OnInstantiateCompletedDelegate(T instance);
        public static OnInstantiateCompletedDelegate OnInstantiateCompleted;

        //  Instantiation ------------------------------------------

        public static T Instantiate()
        {
            
            var instances = GameObject.FindObjectsByType<T>(FindObjectsSortMode.InstanceID);
            if (instances.Length > 1)
            {
                Debug.Log("instances.Length: " + instances.Length);
                Debug.Log("destroy one");
                Destroy(instances[2].gameObject);
            }
            
            if (IsShuttingDown || !Application.isPlaying)
            {
                Debug.LogError("Must check IsShuttingDown before calling Instantiate/Instance.");
            }
            
            if (!IsInstantiated)
            {
                _Instance = GameObject.FindObjectOfType<T>();

                if (_Instance == null)
                {
                    GameObject go = new GameObject();
                    _Instance = go.AddComponent<T>();
                    go.name = _Instance.GetType().FullName;
                    
                }

                // Notify child scope
                (_Instance as SingletonMonobehaviour<T>).InstantiateCompleted();

                // Notify observing scope(s)
                if (OnInstantiateCompleted != null)
                {
                    OnInstantiateCompleted(_Instance);
                }
            }
            
            //SafeDontDestroyOnLoad()
            _Instance.gameObject.transform.parent = null;
            DontDestroyOnLoad(_Instance.gameObject);
            
            return _Instance;
        }


        //  Unity Methods ------------------------------------------
        
        /// <summary>
        /// Detect and solve corner case
        /// </summary>
        protected void OnApplicationQuit()
        {
            Destroy();
        }
        
        /// <summary>
        /// Detect and solve corner case
        /// </summary>
        protected virtual void OnDestroy()
        {
            if (HasDontDestroyOnLoad)
            {
                return;
            }
            Destroy();
        }
        
        /// <summary>
        /// Detect and solve corner case
        /// </summary>
        public static void Destroy()
        {
            
            IsShuttingDown = true;

            if (IsInstantiated)
            {
                if (Application.isPlaying)
                {
                    Destroy(_Instance.gameObject);
                }
                else
                {
                    DestroyImmediate(_Instance.gameObject);
                }

                _Instance = null;
            }
        }

        //  Other Methods ------------------------------------------
        public virtual void InstantiateCompleted()
        {
            // Override, if desired
        }

    }
}