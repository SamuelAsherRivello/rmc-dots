using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.Entities;
using Unity.Entities.Serialization;
using Unity.Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RMC.DOTS.Utilities
{
    //  Namespace Properties ------------------------------

    //  Class Attributes ----------------------------------

    /// <summary>
    /// Replace with comments...
    /// </summary>
    public static class DOTSUtility
    {
        //  Events ----------------------------------------

        //  Properties ------------------------------------

        //  Fields ----------------------------------------

        //  Methods ---------------------------------------


        /// <summary>
        /// Gets the world reference, INCLUDING after a game start.
        /// </summary>
        public static async Task<World> GetWorldAsync(SubScene subScene)
        {
            // 1 Press play
            World world = World.DefaultGameObjectInjectionWorld;
            
            // 2 "UI" Restart
            if (world == null)
            {
                DefaultWorldInitialization.Initialize("Default World", false);
                world = World.DefaultGameObjectInjectionWorld;
                await DOTSUtility.LoadSceneAsync(world.Unmanaged, subScene.SceneGUID);
            }

            return world;
        }
        
        /// <summary>
        /// Restart the game DURING playmode
        /// </summary>
        public static async Task ReloadWorldAsync(SubScene subScene)
        {
            World world = World.DefaultGameObjectInjectionWorld;
            
            SceneSystem.UnloadScene(world.Unmanaged, subScene.SceneGUID,
                SceneSystem.UnloadParameters.DestroyMetaEntities);

            DOTSUtility.DisposeAllWorlds();

            await DOTSUtility.LoadSceneAsync(SceneManager.GetActiveScene());
        }
        
        /// <summary>
        /// Restart the game AFTER playmode, including with "Play Mode Options" on
        /// </summary>
        public static void DisposeAllWorlds()
        {
            World.DisposeAllWorlds();
        }
        
        
        /// <summary>
        /// LoadSceneAsync with await
        /// </summary>
        public static async Task LoadSceneAsync(UnityEngine.SceneManagement.Scene scene )
        {
            var handle = SceneManager.LoadSceneAsync(scene.name);
            while (handle != null && !handle.isDone)
            {
                await Task.Yield();
            }
        }

        /// <summary>
        /// LoadSceneAsync with await
        /// </summary>
        public static async Task LoadSceneAsync(WorldUnmanaged world, Unity.Entities.Hash128 sceneGUID, 
            SceneSystem.LoadParameters parameters = default)
        {
            Entity handle = SceneSystem.LoadSceneAsync(world, sceneGUID, parameters);
            while (!SceneSystem.IsSceneLoaded(world, handle))
            {
                await Task.Yield();
            }
        }


    }
}