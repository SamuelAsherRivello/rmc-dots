using RMC.DOTS.Systems.PhysicsTrigger;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RMC.Playground3D.Pong2D_Version02_DOTS
{
    public class ProjectileAuthoring : MonoBehaviour
    {
        public bool IsSupportNegative = true;
        public Vector3 MinLinearImpulse = new Vector3(1,1, 0);
        public Vector3 MaxLinearImpulse = new Vector3(1,1, 0);
    }


    public class ProjectileBaker : Baker<ProjectileAuthoring>
    {
        public override void Bake(ProjectileAuthoring moveAuthoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            Vector3 linearImpulse = new Vector3(
                GenerateRandomComponent(
                    moveAuthoring.MinLinearImpulse.x, 
                    moveAuthoring.MaxLinearImpulse.x, 
                    moveAuthoring.IsSupportNegative),
                GenerateRandomComponent(
                    moveAuthoring.MinLinearImpulse.y, 
                    moveAuthoring.MaxLinearImpulse.y, 
                    moveAuthoring.IsSupportNegative),
                GenerateRandomComponent(
                    moveAuthoring.MinLinearImpulse.z, 
                    moveAuthoring.MaxLinearImpulse.z, 
                    moveAuthoring.IsSupportNegative)
            );

            AddComponent(entity, new ApplyLinearImpulseComponent { Value = linearImpulse });
            AddComponent<ProjectileTag>(entity);
        }

        private float GenerateRandomComponent(float min, float max, bool isSupportNegative)
        {
            float magnitude = Random.Range(min, max);
            bool isNegative = isSupportNegative && Random.Range(0, 2) == 0; 
            return isNegative ? -magnitude : magnitude;
        }
    }
}