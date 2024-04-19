using RMC.DOTS.Systems.PhysicsTrigger;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Samples.Pong2D.Pong2D_Version02_DOTS
{
    public class GoalAuthoring : MonoBehaviour
    {
        public PlayerType PlayerType;
    }

    public class GoalAuthoringBaker : Baker<GoalAuthoring>
    {
        public override void Bake(GoalAuthoring moveAuthoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<GoalComponent>(entity, new GoalComponent { PlayerType = moveAuthoring.PlayerType });
            AddComponent<PhysicsTriggerInput2Tag>(entity);
        }
    }
}