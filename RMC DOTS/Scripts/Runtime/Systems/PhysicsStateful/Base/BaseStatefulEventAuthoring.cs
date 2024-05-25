using Unity.Assertions;
using Unity.Entities;
using Unity.Physics.Authoring;
using UnityEditor;
using UnityEngine;

namespace Unity.Physics.PhysicsStateful
{
    public enum StatefulEventAuthoringValidationMode
    {
        Full_AllScenes,
        Partial_CurrentSceneOnly,
        Off
    }
    
    
    /// <summary>
    /// This is a base authoring to handle some helpful validation
    /// </summary>
    [RequireComponent(typeof(PhysicsBodyAuthoring), typeof(PhysicsShapeAuthoring))]
    public class BaseStatefulEventAuthoring : MonoBehaviour
    {
        [Tooltip("Validation helps to confirm GameObject is properly setup for Physics")]
        [Header("Validation")]
        [SerializeField]
        private PhysicsBodyAuthoring _physicsBodyAuthoring;

        [SerializeField]
        private PhysicsShapeAuthoring _physicsShapeAuthoring;
        
        
        //Leave this setting on full
        private static readonly StatefulEventAuthoringValidationMode ValidationModeMode = 
            StatefulEventAuthoringValidationMode.Full_AllScenes;


        public void OnBakeValidation(BaseStatefulEventAuthoring authoring)
        {
            //NO validation
            if (ValidationModeMode == StatefulEventAuthoringValidationMode.Off)
            {
                return;
            }

            //Current Scene Validation
            bool isCurrentScene = gameObject.activeInHierarchy;
            bool isPrefab = PrefabUtility.IsPartOfAnyPrefab(gameObject);
            if (ValidationModeMode == StatefulEventAuthoringValidationMode.Partial_CurrentSceneOnly &&
                !isCurrentScene)
            {
                return;
            }
            string locationDetails = "";
            if (!isCurrentScene)
            {
                if (isPrefab)
                {
                    locationDetails = $" in a Prefab";
                }
                else
                {
                    locationDetails = $" in Scene '{gameObject.scene.name}'";
                }
              
            }
            
            string errorPrefix = $"DOTS Bake Error on GameObject '{gameObject.name}'{locationDetails}. More...\n\n";
            string errorSuffix = $"before baking.\n\n";
            string belongsToError =       $"{errorPrefix} Set 'BelongsTo' value to something (Not Everything, Not Nothing) on {nameof(PhysicsShapeAuthoring)} Component {errorSuffix}";
            string collidesWithError =    $"{errorPrefix} Set 'CollidesWith' value to something (Not Everything, Not Nothing) on {nameof(PhysicsShapeAuthoring)} Component {errorSuffix}";
            string collisionResponseErrorForCollision = $"{errorPrefix} Set 'CollisionResponse' to '{CollisionResponsePolicy.CollideRaiseCollisionEvents}' on {nameof(PhysicsShapeAuthoring)} Component {errorSuffix}";
            string collisionResponseErrorForTrigger = $"{errorPrefix} Set 'CollisionResponse' to '{CollisionResponsePolicy.RaiseTriggerEvents}' on {nameof(PhysicsShapeAuthoring)} Component {errorSuffix}";


            // Validation: Enough Components?
            if (_physicsShapeAuthoring == null)
            {
                Debug.LogError($"{errorPrefix} Add the {nameof(PhysicsShapeAuthoring)} Component {errorSuffix}");
            }

            if (_physicsBodyAuthoring == null)
            {
                Debug.LogError($"{errorPrefix} Add the {nameof(PhysicsBodyAuthoring)} Component {errorSuffix}");
            }
            
            if (_physicsShapeAuthoring == null || _physicsBodyAuthoring == null)
            {
                return;
            }

            // Validation: Enough Tags?
            if (_physicsShapeAuthoring.BelongsTo.Equals(PhysicsCategoryTags.Nothing) || 
                _physicsShapeAuthoring.BelongsTo.Equals(PhysicsCategoryTags.Everything))
            {
                Debug.LogError(belongsToError);
            }

            if (_physicsShapeAuthoring.CollidesWith.Equals(PhysicsCategoryTags.Nothing) || 
                _physicsShapeAuthoring.CollidesWith.Equals(PhysicsCategoryTags.Everything))
            {
                Debug.LogError(collidesWithError);
            }
            
            if (authoring is StatefulCollisionEventAuthoring)
            {
                if (_physicsShapeAuthoring.CollisionResponse != CollisionResponsePolicy.CollideRaiseCollisionEvents)
                {
                    Debug.LogError(collisionResponseErrorForCollision);
                }
            }
            else if (authoring is StatefulTriggerEventAuthoring)
            {
                if (_physicsShapeAuthoring.CollisionResponse != CollisionResponsePolicy.RaiseTriggerEvents)
                {
                    Debug.LogError(collisionResponseErrorForTrigger);
                }
            }

        }
    }
}
