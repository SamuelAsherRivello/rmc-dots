using RMC.DOTS.Systems.PhysicsTrigger;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Samples.RollABall3D.RollABall3D_Version02_DOTS
{
    /// <summary>
    /// This authoring component gets added to the player GameObject in the Roll A Ball sub scene.
    /// Authoring components define what ECS components get added to an entity during GameObject to Entity conversion
    /// Further we can define data values that will be set in the ECS components.
    /// </summary>
    public class PlayerMoveAuthoring : MonoBehaviour
    {
        // We can set this field through the inspector in the Unity editor
        public float Speed = 7.5f;
    }

    /// <summary>
    /// The baker class is where we define which components and values are added to the converted entity
    /// </summary>
    public class PlayerMoveBaker : Baker<PlayerMoveAuthoring>
    {
        public override void Bake(PlayerMoveAuthoring authoring)
        {
            // First we get a reference to the outputted entity. The Dynamic TransformUsageFlags mean that this entity
            // will have all the necessary transform components to move it in the game world.
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            
            //Physics : #1 arbitrarily means the player
            AddComponent<PhysicsTriggerInput1Tag>(entity);
            
            // Next we add components and tags to the entity as needed.
            AddComponent<PlayerTag>(entity);
            
            // Here we reference the MoveForce value in the authoring class and set that data in the new component
            AddComponent(entity, new PlayerMoveComponent { Value = authoring.Speed });
        }
    }
}