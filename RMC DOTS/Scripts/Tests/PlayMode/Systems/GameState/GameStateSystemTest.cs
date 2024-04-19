// using NUnit.Framework;
// using Roguelite.Tests.Infrastructure;
//
// namespace RMC.DOTS.Systems.GameState
// {
//     [Category(nameof(GameStateSystem))]
//     [TestFixture]
//     public class GameStateSystemTest : CustomEcsTestsFixture
//     {
//
//         [SetUp]
//         public override void Setup()
//         {
//             base.Setup();
//             CreateSystemManaged<GameStateSystem>();
//             CreateEntity(typeof(GameStateSystemAuthoring.GameStateSystemIsEnabledTag));
//         }
//         
//         [Test]
//         public void Default_GameStateComponent_IsGamePausedFalse()
//         {
//             // Create an entity with default components
//             var entity = CreateEntity(typeof(GameStateComponent));
//             
//             // Add components with custom data
//             var component = new GameStateComponent
//             {   
//                 IsGamePaused = false,
//                 IsGameOver = false
//             };
//             Manager.AddComponentData(entity, component);
//  
//             // Assert that data is modified according to the test case
//             var result = Manager.GetComponentData<GameStateComponent>(entity);
//             Assert.That(result.IsGamePaused, Is.False);
//             Assert.That(result.IsGameOver, Is.False);
//         }
//     }
// }
