using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.PhysicsVelocityImpulse
{
    public class PhysicsVelocityImpulseComponentAuthoring : MonoBehaviour
    {
        
        public bool IsRandomForce = false;
        
        // Not Random
        public Vector3 Force = new Vector3(1,1, 1);
         
        // Random
        public bool RandomCanBeNegative = true;
        public Vector3 RandomMinForce = new Vector3(1,1, 1);
        public Vector3 RandomMaxForce = new Vector3(2,2, 2);
        
         public class PhysicsVelocityImpulseComponentAuthoringBaker : Baker<PhysicsVelocityImpulseComponentAuthoring>
        {
            public override void Bake(PhysicsVelocityImpulseComponentAuthoring authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);

                if (authoring.IsRandomForce)
                {
                    //Pass along data for a random choice later
                    AddComponent<PhysicsVelocityImpulseComponent>(entity,
                        new PhysicsVelocityImpulseComponent
                        {
                            CanBeNegative = authoring.RandomCanBeNegative,
                            MinForce = authoring.RandomMinForce,
                            MaxForce = authoring.RandomMaxForce
                        });

                }
                else
                {
                    
                    //Pass along non-random values for a specific choice now
                    AddComponent<PhysicsVelocityImpulseComponent>(entity,
                        new PhysicsVelocityImpulseComponent
                        {
                            CanBeNegative = false,
                            MinForce = authoring.Force,
                            MaxForce = authoring.Force
                        });

                }
    
            }
        }
    }
}
