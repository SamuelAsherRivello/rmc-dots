using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Samples.Templates.DOTSGameTemplate
{
    public class GoalTagAuthoring : MonoBehaviour
    {
        public class GoalTagAuthoringBaker : Baker<GoalTagAuthoring>
        {
            public override void Bake(GoalTagAuthoring componentAuthoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<GoalTag>(entity);
            }
        }
    }
}
