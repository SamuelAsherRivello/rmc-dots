using RMC.DOTS.Systems.PhysicsTrigger;
using Unity.Entities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RMC.DOTS.Samples.Pong2D.Pong2D_Version02_DOTS
{
    public class ProjectileAuthoring : MonoBehaviour
    {
        [Header("PhysicsTrigger System")]
        public LayerMask MemberOfLayerMask;
        public LayerMask CollidesWithLayerMask;
        
        [Header("ApplyLinearImpulse System")]
        public bool IsSupportNegative = true;
        public Vector3 MinLinearImpulse = new Vector3(1,1, 0);
        public Vector3 MaxLinearImpulse = new Vector3(1,1, 0);
    }

    public class ProjectileBaker : Baker<ProjectileAuthoring>
    {
        public override void Bake(ProjectileAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<ProjectileTag>(entity);
            
            Vector3 linearImpulse = new Vector3(
                GenerateRandomComponent(
                    authoring.MinLinearImpulse.x, 
                    authoring.MaxLinearImpulse.x, 
                    authoring.IsSupportNegative),
                GenerateRandomComponent(
                    authoring.MinLinearImpulse.y, 
                    authoring.MaxLinearImpulse.y, 
                    authoring.IsSupportNegative),
                GenerateRandomComponent(
                    authoring.MinLinearImpulse.z, 
                    authoring.MaxLinearImpulse.z, 
                    authoring.IsSupportNegative)
            );

            AddComponent(entity, new ApplyLinearImpulseComponent { Value = linearImpulse });
    
            
            AddComponent<PhysicsTriggerComponent>(entity,
                new PhysicsTriggerComponent
                {
                    MemberOfLayerMask = authoring.MemberOfLayerMask,
                    CollidesWithLayerMask = authoring.CollidesWithLayerMask
                });
        }

        private float GenerateRandomComponent(float min, float max, bool isSupportNegative)
        {
            float magnitude = Random.Range(min, max);
            bool isNegative = isSupportNegative && Random.Range(0, 2) == 0; 
            return isNegative ? -magnitude : magnitude;
        }
    }
}