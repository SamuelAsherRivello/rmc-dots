using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.PhysicsVelocityImpulse
{
    public class PhysicsVelocityImpulseComponentAuthoring : MonoBehaviour
    {
        //If you don't want random, just put the same values for min/max and false for canBeNegative
        [Header("ApplyLinearImpulse System")]
        public bool CanBeNegative = true;
        public Vector3 MinForce = new Vector3(1,1, 0);
        public Vector3 MaxForce = new Vector3(1,1, 0);
        
        public class PhysicsVelocityImpulseComponentAuthoringBaker : Baker<PhysicsVelocityImpulseComponentAuthoring>
        {
            public override void Bake(PhysicsVelocityImpulseComponentAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent<PhysicsVelocityImpulseComponent>(entity,
                    new PhysicsVelocityImpulseComponent
                    {
                        CanBeNegative = authoring.CanBeNegative,
                        MinForce = authoring.MinForce,
                        MaxForce = authoring.MaxForce
                    });

            }
        }
    }
}
