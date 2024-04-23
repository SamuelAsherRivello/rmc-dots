// using NUnit.Framework;
// using Roguelite.Tests.Infrastructure;
//
// namespace RMC.DOTS.Systems.PhysicsTrigger
// {
//     [Category(nameof(WaypointsSystem))]
//     [TestFixture]
//     public class WaypointsFollowerSystemTest : CustomEcsTestsFixture
//     {
//         [SetUp]
//         public override void Setup()
//         {
//             base.Setup();
//             CreateSystem<WaypointsSystem>();
//             CreateEntity(typeof(PhysicsTriggerSystemAuthoring.PhysicsTriggerSystemIsEnabledTag));
//         }
//         
//         [Test]
//         public void AfterUpdate_LoneEntity_HasNoPhysicsTriggerComponent()
//         {
//             // Create an entity with default components
//             var entity = CreateEntity(typeof(PhysicsTriggerComponent));
//             
//             // Add components with custom data
//             var component = new PhysicsTriggerComponent
//             {   
//                 MemberOfLayerMask = 1,
//                 CollidesWithLayerMask = 1
//             };
//             Manager.AddComponentData(entity, component);
//  
//             // Update the system
//             UpdateSystem<WaypointsSystem>();
//  
//             // Assert that data is modified according to the test case
//             var hasComponent = Manager.HasComponent<PhysicsTriggerOutputComponent>(entity);
//             Assert.That(hasComponent, Is.False);
//         }
//     }
// }
