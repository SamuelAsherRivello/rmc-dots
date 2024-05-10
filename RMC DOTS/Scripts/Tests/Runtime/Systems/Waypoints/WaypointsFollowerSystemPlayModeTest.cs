using NUnit.Framework;
using WayneGames.ECSTestsFixture;

namespace RMC.DOTS.Systems.Waypoints
{
    [Category(nameof(WaypointsFollowerSystem))]
    [TestFixture]
    public class WaypointsFollowerSystemPlayModeTest : ECSTestsFixture
    {
        [SetUp]
        public override void Setup()
        {
            base.Setup();
            World.CreateSystemManaged<WaypointsFollowerSystem>();
            EntityManager.CreateEntity(typeof(WaypointsFollowerSystemAuthoring.WaypointsSystemIsEnabledTag));
        }
        
        [Test]
        public void Default_WaypointsFollowerComponent_HasDefaultValues()
        {
            // Create an entity with default components
            var entity = EntityManager.CreateEntity(typeof(WaypointsFollowerComponent));
            
            // Add components with custom data
            var component = new WaypointsFollowerComponent
            {   
                LinearSpeed = 10,
                AngularSpeed = 10,
                NextWaypointIndex = 0
            };
            EntityManager.AddComponentData(entity, component);
 
            // Assert that data is modified according to the test case
            var result = EntityManager.GetComponentData<WaypointsFollowerComponent>(entity);
            Assert.That(result.LinearSpeed, Is.EqualTo(10));
            Assert.That(result.AngularSpeed, Is.EqualTo(10));
        }
    }
}
