using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Samples.Games.TwinStickShooter3D.TwinStickShooter3D_Version02_DOTS
{
	public class WallTagAuthoring : MonoBehaviour
    {
        public class WallTagAuthoringBaker : Baker<WallTagAuthoring>
        {
            public override void Bake(WallTagAuthoring tagAuthoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                                
                AddComponent<WallTag>(entity);
            }
        }
    }
}