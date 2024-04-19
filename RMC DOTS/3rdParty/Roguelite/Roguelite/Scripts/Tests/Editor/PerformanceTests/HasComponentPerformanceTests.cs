using NUnit.Framework;
using Roguelite.Movement;
using Roguelite.Physics;
using Roguelite.Tests.Infrastructure;
using Unity.PerformanceTesting;

namespace Roguelite.Tests.Editor
{
    [TestFixture]
    public class HasComponentPerformanceTests : PerformanceEcsTestsFixture
    {
        [Test]
        [Performance]
        public void EntityManagerHasComponentTest()
        {
            MeasureUpdate<EntityManagerHasComponentSystem>(typeof(GravityTag), typeof(MovementStatsData),
                typeof(MovementDirectionData), typeof(GroundCollision));
        }

        [Test]
        [Performance]
        public void SystemApiHasComponentTest()
        {
            MeasureUpdate<SystemApiHasComponentSystem>(typeof(GravityTag), typeof(MovementStatsData),
                typeof(MovementDirectionData));
        }
    }
}