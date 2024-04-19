using NUnit.Framework;
using Roguelite.Movement;
using Roguelite.Tests.Infrastructure;
using Unity.PerformanceTesting;
using ResetMovementSystem = Roguelite.Movement.ResetMovementSystem;

namespace Roguelite.Tests.Editor
{
    [TestFixture]
    public class SystemsWithJobsPerformanceTests : PerformanceEcsTestsFixture
    {
        private static int[] entitiesCount = { 1, 10, 100, 1000, 10000, 100000 };

        [Test]
        [Performance]
        [TestCaseSource(nameof(entitiesCount))]
        public void ScheduleParallelJobResetMovementSystemTest(int entityCount)
        {
            MeasureUpdate<ResetMovementSystem>(entityCount, typeof(MovementDirectionData));
        }

        [Test]
        [Performance]
        [TestCaseSource(nameof(entitiesCount))]
        public void RunJobResetMovementSystemTest(int entityCount)
        {
            MeasureUpdate<RunJobResetMovementSystem>(entityCount, typeof(MovementDirectionData));
        }

        [Test]
        [Performance]
        [TestCaseSource(nameof(entitiesCount))]
        public void ScheduleJobResetMovementSystemTest(int entityCount)
        {
            MeasureUpdate<JobResetMovementSystem>(entityCount, typeof(MovementDirectionData));
        }

        [Test]
        [Performance]
        [TestCaseSource(nameof(entitiesCount))]
        public void MainThreadResetMovementSystemTest(int entityCount)
        {
            MeasureUpdate<MainThreadResetMovementSystem>(entityCount, typeof(MovementDirectionData));
        }
    }
}