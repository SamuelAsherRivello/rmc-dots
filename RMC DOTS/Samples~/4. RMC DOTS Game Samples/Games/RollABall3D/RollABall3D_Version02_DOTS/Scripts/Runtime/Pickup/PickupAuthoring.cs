using RMC.DOTS.Systems.PhysicsTrigger;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Samples.RollABall3D.RollABall3D_Version02_DOTS
{
    /// <summary>
    /// This authoring component allows us to define the speed and direction a pickup will rotate. This script is added
    /// to a GameObject in the RollABallSubScene and the Baker will convert it to an entity at runtime.
    /// </summary>
    public class PickupAuthoring : MonoBehaviour
    {
        public class PickupAuthoringBaker : Baker<PickupAuthoring>
        {
            public override void Bake(PickupAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                          
                AddComponent<PickupTag>(entity);
            }
        }
    }
}