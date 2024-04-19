using Unity.Entities;
using Unity.PerformanceTesting;

namespace Roguelite.Tests.Infrastructure
{
    public class PerformanceEcsTestsFixture : CustomEcsTestsFixture
    {
        private readonly SampleGroup _sampleGroup = new("Time", SampleUnit.Microsecond);
        private const int WarmupCount = 5;
        private const int MeasurementCount = 10;
        private const int IterationsPerMeasurement = 5;

        protected void MeasureUpdate<T>(params ComponentType[] types) where T : unmanaged, ISystem
        {
            Measure.Method(UpdateSystem<T>)
                .SetUp(() =>
                {
                    CreateSystem<T>();
                    CreateEntities(types);
                })
                .WarmupCount(WarmupCount)
                .MeasurementCount(MeasurementCount)
                .IterationsPerMeasurement(IterationsPerMeasurement)
                .SampleGroup(_sampleGroup)
                .Run();
        }

        protected void MeasureUpdate<T>(int entityCount, params ComponentType[] types) where T : unmanaged, ISystem
        {
            Measure.Method(UpdateSystem<T>)
                .SetUp(() =>
                {
                    CreateSystem<T>();
                    CreateEntities(types, entityCount);
                })
                .WarmupCount(WarmupCount)
                .MeasurementCount(MeasurementCount)
                .IterationsPerMeasurement(IterationsPerMeasurement)
                .SampleGroup(_sampleGroup)
                .Run();
        }
    }
}