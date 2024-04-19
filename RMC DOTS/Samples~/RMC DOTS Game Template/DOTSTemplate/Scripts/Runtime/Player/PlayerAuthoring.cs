using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Samples.DOTSTemplate
{
    public class PlayerAuthoring : MonoBehaviour
    {
        public class PlayerAuthoringBaker : Baker<PlayerAuthoring>
        {
            public override void Bake(PlayerAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                                
                AddComponent<PlayerTag>(entity);
            }
        }
    }
}