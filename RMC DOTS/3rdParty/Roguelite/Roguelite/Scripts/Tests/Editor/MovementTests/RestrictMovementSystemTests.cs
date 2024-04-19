using NUnit.Framework;
using Roguelite.Movement;
using Roguelite.Physics;
using Roguelite.Tests.Infrastructure;
using Unity.Mathematics;

namespace Roguelite.Tests.Editor.MovementTests
{
    [TestFixture]
    public class RestrictMovementSystemTests : CustomEcsTestsFixture
    {
        [SetUp]
        public override void Setup()
        {
            base.Setup();
            CreateSystem<RestrictMovementSystem>();
        }

        [Test]
        public void Update_HasCollisionsOnRightAndMovingRight_HorizontalMovementIsReset()
        {
            var entity = CreateEntity(typeof(RightWallCollision));
            var movement = new MovementDirectionData
            {
                Movement = new float3(1)
            };
            Manager.AddComponentData(entity, movement);

            UpdateSystem<RestrictMovementSystem>();

            var movementData = Manager.GetComponentData<MovementDirectionData>(entity);
            Assert.AreEqual(0, movementData.Movement.x);
        }

        [Test]
        public void Update_HasCollisionsOnLeftAndMovingLeft_HorizontalMovementIsReset()
        {
            var entity = CreateEntity(typeof(LeftWallCollision));
            var movement = new MovementDirectionData
            {
                Movement = new float3(-1)
            };
            Manager.AddComponentData(entity, movement);

            UpdateSystem<RestrictMovementSystem>();

            var movementData = Manager.GetComponentData<MovementDirectionData>(entity);
            Assert.AreEqual(0, movementData.Movement.x);
        }

        [Test]
        public void Update_HasCollisionsOnRightAndMovingLeft_HorizontalMovementIsNotChanged()
        {
            const int directionX = -1;
            var entity = CreateEntity(typeof(RightWallCollision));
            var movement = new MovementDirectionData
            {
                Movement = new float3(directionX)
            };
            Manager.AddComponentData(entity, movement);

            UpdateSystem<RestrictMovementSystem>();

            var movementData = Manager.GetComponentData<MovementDirectionData>(entity);
            Assert.AreEqual(directionX, movementData.Movement.x);
        }

        [Test]
        public void Update_NoCollisions_HorizontalMovementIsNotChanged()
        {
            const int directionX = 1;
            var entity = CreateEntity();
            var movement = new MovementDirectionData
            {
                Movement = new float3(directionX)
            };
            Manager.AddComponentData(entity, movement);

            UpdateSystem<RestrictMovementSystem>();

            var movementData = Manager.GetComponentData<MovementDirectionData>(entity);
            Assert.AreEqual(directionX, movementData.Movement.x);
        }
    }
}