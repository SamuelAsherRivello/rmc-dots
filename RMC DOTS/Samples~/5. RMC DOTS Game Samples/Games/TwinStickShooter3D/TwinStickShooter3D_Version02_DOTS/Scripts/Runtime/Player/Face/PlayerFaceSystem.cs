using RMC.DOTS.SystemGroups;
using RMC.DOTS.Systems.Input;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics.Aspects;

namespace RMC.DOTS.Samples.Games.TwinStickShooter3D.TwinStickShooter3D_Version02_DOTS
{
    /// <summary>
    /// This system moves the player in 3D space.
    /// </summary>
    [UpdateInGroup(typeof(PauseableSystemGroup))]
    public partial struct PlayerFaceSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<InputComponent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            // NOTE: First get the ArrowKeys for Look from the InputComponent. 
            float2 look = SystemAPI.GetSingleton<InputComponent>().LookFloat2;
            float3 lookComposite = new float3(look.x, 0, look.y);
            float deltaTime = SystemAPI.Time.DeltaTime;

            foreach (var (rigidBodyAspect, playerFaceComponent)
                in SystemAPI.Query<RigidBodyAspect, RefRO<PlayerFaceComponent>>())
            {
                
                // If lookComposite is zero or near zero, skip this iteration
                if (math.lengthsq(lookComposite) < 0.0001f)
                {
                    return;
                }
                
                // Calculate the angle of rotation
                float angle = math.atan2(lookComposite.z, -lookComposite.x); // Invert the x component

                // Add a 90 degrees offset to the angle
                angle += math.radians(90);

                // Create a quaternion from the angle
                quaternion targetRotation = quaternion.EulerXYZ(new float3(0, angle, 0));

                // Get the current rotation of the player
                quaternion currentRotation = rigidBodyAspect.Rotation;

                // Interpolate between the current rotation and the target rotation
                quaternion newRotation = math.slerp(currentRotation, targetRotation, 
                    playerFaceComponent.ValueRO.Value * deltaTime);

                // Set the new rotation of the player
                rigidBodyAspect.Rotation = newRotation;
            }
        }
    }
}
