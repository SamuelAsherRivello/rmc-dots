using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Samples.Games.TwinStickShooter3D.TwinStickShooter3D_Version02_DOTS
{
    public class PlayerShootComponentAuthoring : MonoBehaviour
    {
        public GameObject BulletPrefab;
        public float BulletSpeed = 10;
        public float BulletFireRate = 10;
        
		public class PlayerShootComponentAuthoringBaker : Baker<PlayerShootComponentAuthoring>
        {
            public override void Bake(PlayerShootComponentAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                                
                //NOTE: CONSTRUCTOR is used to specify the subset of values that is required
				AddComponent(entity, new PlayerShootComponent 
				(
					GetEntity(authoring.BulletPrefab, 
							TransformUsageFlags.Dynamic), 
							authoring.BulletSpeed, 
							authoring.BulletFireRate)
				);
			}
        }
    }
}