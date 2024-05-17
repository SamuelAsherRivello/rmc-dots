using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Samples.Games.TwinStickShooter3D.TwinStickShooter3D_Version02_DOTS
{
    public class GemSpawnComponentAuthoring : MonoBehaviour
    {
        public GameObject BulletPrefab;
        public float GemSpeed = 10;
        
		public class GemSpawnComponentAuthoringBaker : Baker<GemSpawnComponentAuthoring>
        {
            public override void Bake(GemSpawnComponentAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                                
                //NOTE: CONSTRUCTOR is used to specify the subset of values that is required
				AddComponent(entity, new GemSpawnComponent 
				(
					GetEntity(authoring.BulletPrefab, TransformUsageFlags.Dynamic), 
					authoring.GemSpeed)
				);
			}
        }
    }
}