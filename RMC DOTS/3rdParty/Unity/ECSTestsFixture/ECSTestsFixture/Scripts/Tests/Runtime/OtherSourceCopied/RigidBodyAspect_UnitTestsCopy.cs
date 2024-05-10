using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Unity.Physics.Tests.Aspects
{
    public static class AspectTestUtils
    {
        internal static float3 DefaultPos => new float3(1.0f, 0.0f, 0.0f);
        internal static quaternion DefaultRot => quaternion.AxisAngle(new float3(0.0f, 1.0f, 0.0f), math.radians(45.0f));
        internal static PhysicsVelocity DefaultVelocity => new PhysicsVelocity { Angular = new float3(0.2f, 0.5f, 0.1f), Linear = new float3(0.1f, 0.2f, 0.3f) };
        internal static PhysicsDamping DefaultDamping => new PhysicsDamping { Angular = 0.75f, Linear = 0.5f };
        internal static PhysicsMass DefaultMass => PhysicsMass.CreateDynamic(MassProperties.UnitSphere, 3.0f);
        internal static PhysicsGravityFactor DefaultGravityFactor => new PhysicsGravityFactor { Value = 0.50f };
        internal static float NonIdentityScale => 2.0f;
        internal static Material Material1 => new Material { CollisionResponse = CollisionResponsePolicy.Collide, CustomTags = 2, Friction = 0.197f, Restitution = 0.732f, FrictionCombinePolicy = Material.CombinePolicy.Minimum, RestitutionCombinePolicy = Material.CombinePolicy.ArithmeticMean };
        internal static Material Material2 => new Material { CollisionResponse = CollisionResponsePolicy.CollideRaiseCollisionEvents, CustomTags = 4, Friction = 0.127f, Restitution = 0.332f, FrictionCombinePolicy = Material.CombinePolicy.Maximum, RestitutionCombinePolicy = Material.CombinePolicy.GeometricMean};
        internal static CollisionFilter NonDefaultFilter => new CollisionFilter { BelongsTo = 123, CollidesWith = 567, GroupIndex = 442 };
        internal static CollisionFilter DefaultFilter => CollisionFilter.Default;
        internal static CollisionFilter ModificationFilter => new CollisionFilter { BelongsTo = 234, CollidesWith = 123, GroupIndex = 221 };
    }

    public partial class RigidBodyAspect_UnitTestsCopy
    {
        public enum BodyType
        {
            STATIC,
            DYNAMIC,
            INFINITE_MASS,
            INFINITE_INERTIA,
            KINEMATIC_NO_MASS_OVERRIDE,
            KINEMATIC_MASS_OVERRIDE,
            SET_VELOCITY_TO_ZERO,
            SCALED_DYNAMIC
        }

        public static Entity CreateBodyComponents(BodyType type, EntityManager manager)
        {
            // Create default components - index, transform, body, scale
            PhysicsWorldIndex worldIndex = new PhysicsWorldIndex { Value = 0 };

            PhysicsCollider pc = new PhysicsCollider();
            {
                BoxGeometry geometry = new BoxGeometry
                {
                    BevelRadius = 0.0015f,
                    Center = float3.zero,
                    Orientation = quaternion.identity,
                    Size = new float3(1.0f, 2.0f, 3.0f)
                };

                pc.Value = BoxCollider.Create(geometry);
            }

            LocalTransform tl = LocalTransform.FromPositionRotationScale(AspectTestUtils.DefaultPos, AspectTestUtils.DefaultRot, 1.0f);
            LocalToWorld ltw = new LocalToWorld { Value = tl.ToMatrix() };

            PhysicsVelocity pv = AspectTestUtils.DefaultVelocity;
            PhysicsMass pm = AspectTestUtils.DefaultMass;
            PhysicsGravityFactor pgf = AspectTestUtils.DefaultGravityFactor;
            PhysicsDamping pd = AspectTestUtils.DefaultDamping;
            PhysicsMassOverride pmo = new PhysicsMassOverride { IsKinematic = 0, SetVelocityToZero = 0 };

            Entity body = manager.CreateEntity();

            // Add index, transform, scale, localToWorld, collider
            manager.AddSharedComponent<PhysicsWorldIndex>(body, worldIndex);
            manager.AddComponentData<LocalTransform>(body, tl);
            manager.AddComponentData<LocalToWorld>(body, ltw);
            manager.AddComponentData<PhysicsCollider>(body, pc);
            manager.AddComponentData<PhysicsDamping>(body, pd);
            manager.AddComponentData<PhysicsGravityFactor>(body, pgf);

            switch (type)
            {
                case BodyType.INFINITE_MASS:
                    pm.InverseMass = 0.0f;
                    goto case BodyType.DYNAMIC;

                case BodyType.INFINITE_INERTIA:
                    pm.InverseInertia = float3.zero;
                    goto case BodyType.DYNAMIC;

                case BodyType.KINEMATIC_NO_MASS_OVERRIDE:
                    pm.InverseInertia = float3.zero;
                    pm.InverseMass = 0.0f;
                    goto case BodyType.DYNAMIC;

                case BodyType.SET_VELOCITY_TO_ZERO:
                    pmo.SetVelocityToZero = 1;
                    goto case BodyType.KINEMATIC_MASS_OVERRIDE;

                case BodyType.SCALED_DYNAMIC:

                    var localTransform = manager.GetComponentData<LocalTransform>(body);
                    localTransform.Scale = AspectTestUtils.NonIdentityScale;
                    manager.SetComponentData<LocalTransform>(body, localTransform);
                    goto case BodyType.DYNAMIC;

                case BodyType.KINEMATIC_MASS_OVERRIDE:
                    pmo.IsKinematic = 1;
                    manager.AddComponentData<PhysicsMassOverride>(body, pmo);
                    goto case BodyType.DYNAMIC;

                case BodyType.DYNAMIC:
                    manager.AddComponentData<PhysicsVelocity>(body, pv);
                    manager.AddComponentData<PhysicsMass>(body, pm);
                    break;

                case BodyType.STATIC:
                default:
                    break;
            }
            return body;
        }
    }
}
