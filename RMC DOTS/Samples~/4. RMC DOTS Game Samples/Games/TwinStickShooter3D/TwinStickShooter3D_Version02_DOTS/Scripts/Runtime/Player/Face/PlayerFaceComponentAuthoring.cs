using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Samples.Games.TwinStickShooter3D.TwinStickShooter3D_Version02_DOTS
{
    public class PlayerFaceComponentAuthoring : MonoBehaviour
    {
        public float Speed = 7.5f;
        
        public class PlayerFaceComponentAuthoringBaker : Baker<PlayerFaceComponentAuthoring>
        {
            public override void Bake(PlayerFaceComponentAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);
                                
                AddComponent<PlayerFaceComponent>(entity, 
                    new PlayerFaceComponent { Value = authoring.Speed });
            }
        }
    }
}