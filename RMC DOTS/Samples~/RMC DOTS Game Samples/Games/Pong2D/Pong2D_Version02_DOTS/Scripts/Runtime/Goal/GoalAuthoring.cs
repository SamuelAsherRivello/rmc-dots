using RMC.DOTS.Systems.PhysicsTrigger;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Samples.Pong2D.Pong2D_Version02_DOTS
{
    public class GoalAuthoring : MonoBehaviour
    {
        public PlayerType PlayerType;
        
        [Header("PhysicsTrigger System")]
        public LayerMask MemberOfLayerMask;
        public LayerMask CollidesWithLayerMask;
    }

    public class GoalAuthoringBaker : Baker<GoalAuthoring>
    {
        public override void Bake(GoalAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<GoalComponent>(entity, 
                new GoalComponent
                {
                    PlayerType = authoring.PlayerType
                });
            
            AddComponent<PhysicsTriggerComponent>(entity,
                new PhysicsTriggerComponent
                {
                    MemberOfLayerMask = authoring.MemberOfLayerMask,
                    CollidesWithLayerMask = authoring.CollidesWithLayerMask
                });
        }
    }
}