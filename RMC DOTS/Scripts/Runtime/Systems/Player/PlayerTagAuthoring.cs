using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.Player
{
    public class PlayerTagAuthoring : MonoBehaviour
    {
        public class PlayerTagAuthoringBaker : Baker<PlayerTagAuthoring>
        {
            public override void Bake(PlayerTagAuthoring tagAuthoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                                
                AddComponent<PlayerTag>(entity);
            }
        }
    }
}