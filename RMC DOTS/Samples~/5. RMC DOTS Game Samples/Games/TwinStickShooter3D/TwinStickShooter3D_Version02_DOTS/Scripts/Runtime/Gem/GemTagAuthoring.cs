using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Samples.Games.TwinStickShooter3D.TwinStickShooter3D_Version02_DOTS
{
	public class GemTagAuthoring : MonoBehaviour
    {
        public class GemTagAuthoringBaker : Baker<GemTagAuthoring>
        {
            public override void Bake(GemTagAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                                
                AddComponent<GemTag>(entity);
            }
        }
    }
}