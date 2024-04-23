using RMC.DOTS.Systems.PhysicsTrigger;
using RMC.DOTS.Systems.PhysicsVelocityImpulse;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Samples.Pong2D.Pong2D_Version02_DOTS
{
    public class ProjectileAuthoring : MonoBehaviour
    {
        [Header("PhysicsTrigger System")]
        public LayerMask MemberOfLayerMask;
        public LayerMask CollidesWithLayerMask;
        
        [Header("ApplyLinearImpulse System")]
        public bool CanBeNegative = true;
        public Vector3 MinForce = new Vector3(1,1, 0);
        public Vector3 MaxForce = new Vector3(1,1, 0);
    }

    public class ProjectileBaker : Baker<ProjectileAuthoring>
    {
        public override void Bake(ProjectileAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<ProjectileTag>(entity);

            AddComponent<PhysicsVelocityImpulseComponent>(entity,
                new PhysicsVelocityImpulseComponent
                {
                    CanBeNegative = authoring.CanBeNegative,
                    MinForce = authoring.MinForce,
                    MaxForce = authoring.MaxForce
                });

            AddComponent<PhysicsTriggerComponent>(entity,
                new PhysicsTriggerComponent
                {
                    MemberOfLayerMask = authoring.MemberOfLayerMask,
                    CollidesWithLayerMask = authoring.CollidesWithLayerMask
                });
        }
    }
}