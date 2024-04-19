using NUnit.Framework;
using Roguelite.Tests.Infrastructure;

namespace RMC.DOTS.Systems.PhysicsTrigger
{
    [Category(nameof(PhysicsTriggerSystem))]
    [TestFixture]
    public class PhysicsTriggerSystemTest : CustomEcsTestsFixture
    {
        [SetUp]
        public override void Setup()
        {
            base.Setup();
            CreateSystem<PhysicsTriggerSystem>();
            CreateEntity(typeof(PhysicsTriggerSystemAuthoring.PhysicsTriggerSystemIsEnabledTag));
        }
        
        [Test]
        public void AfterUpdate_LoneEntity_HasNoPhysicsTriggerComponent()
        {
            // Create an entity with default components
            var entity = CreateEntity(typeof(PhysicsTriggerComponent));
            
            // Add components with custom data
            var component = new PhysicsTriggerComponent
            {   
                MemberOfLayerMask = 1,
                CollidesWithLayerMask = 1
            };
            Manager.AddComponentData(entity, component);
 
            // Update the system
            UpdateSystem<PhysicsTriggerSystem>();
 
            // Assert that data is modified according to the test case
            var hasComponent = Manager.HasComponent<PhysicsTriggerOutputTag>(entity);
            Assert.That(hasComponent, Is.False);
        }
    }
}
