using NUnit.Framework;
using Unity.Hack.ECSTestsFixture;

namespace RMC.DOTS.Systems.GameState
{
    [Category(nameof(GameStateSystem))]
    [TestFixture]
    public class GameStateSystemPlayModeTest : ECSTestsFixture
    {

        [SetUp]
        public override void Setup()
        {
            base.Setup();
            World.CreateSystemManaged<GameStateSystem>();
            EntityManager.CreateEntity(typeof(GameStateSystemAuthoring.GameStateSystemIsEnabledTag));
        }
        
        [Test]
        public void Default_GameStateComponent_HasDefaultValues()
        {
            // Create an entity with default components
            var entity = EntityManager.CreateEntity(typeof(GameStateSystemAuthoring.GameStateSystemIsEnabledTag));
            
            // Add components with custom data
            var component = new GameStateComponent
            {   
                IsGamePaused = false,
                IsGameOver = false
            };
            EntityManager.AddComponentData(entity, component);
 
            // Assert that data is modified according to the test case
            var result = EntityManager.GetComponentData<GameStateComponent>(entity);
            Assert.That(result.IsGamePaused, Is.False);
            Assert.That(result.IsGameOver, Is.False);
        }
    }
}
