using NUnit.Framework;
using Unity.Collections;
using UnityEngine;
using WayneGames.ECSTestsFixture;

namespace RMC.DOTS.World
{
    [Category(nameof (RMC.DOTS.World))]
    [TestFixture]
    public class WorldEmptyPlayModeTest : ECSTestsFixture
    {
        [SetUp]
        public override void Setup()
        {
            CreateDefaultWorld = false;
            base.Setup();
        }
        
        [Test]
        public void World_SystemsCountIs0_WhenInitial()
        {
            // Arrange
            
            // Act
            int result = World.Systems.Count;

            // Assert
            Assert.That(result, Is.EqualTo(0));
        }
        
        [Test]
        public void World_GetAllUnmanagedSystemsCountIs0_WhenInitial()
        {
            // Arrange
            
            // Act
            int result = World.Unmanaged.GetAllUnmanagedSystems(Allocator.Temp).Length;

            // Assert
            Assert.That(result, Is.EqualTo(0));
        }
        
        [Test]
        public void World_EntityCountIs0_WhenInitial()
        {
            // Arrange
            
            // Act
            int result = World.EntityManager.UniversalQuery.CalculateEntityCount();

            // Assert
            Assert.That(result, Is.EqualTo(0));
        }
        
        [Test]
        public void World_QuitUpdateIsFalse_WhenInitial()
        {
            // Arrange
            
            // Act

            // Assert
            Assert.That(World.QuitUpdate, Is.False);
        }
        
        [Test]
        public void World_UpdateThrowsNowException_WhenRepeated()
        {
            // Arrange
            var timesToUpdate = 5;
            var counter = 0;
            
            // Act
            for (int i = 0; i < timesToUpdate; i++)
            {
                World.Update();
                counter++;
            }
            
            // Assert
            Assert.That(counter, Is.EqualTo(timesToUpdate));
        }
    }   
}
