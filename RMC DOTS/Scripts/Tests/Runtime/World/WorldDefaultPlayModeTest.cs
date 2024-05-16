using NUnit.Framework;
using Unity.Collections;
using Unity.Hack.ECSTestsFixture;

namespace RMC.DOTS.World
{
    [Category(nameof (RMC.DOTS.World))]
    [TestFixture]
    public class WorldDefaultPlayModeTest : ECSTestsFixture
    {
        [SetUp]
        public override void Setup()
        {
            CreateDefaultWorld = true;
            base.Setup();
        }
        
        [Test]
        public void World_SystemsCountIsGT71_WhenInitial()
        {
            // Arrange
            
            // Act
            int result = World.Systems.Count;

            // Assert
            Assert.That(result, Is.GreaterThan(71));
        }
        
        [Test]
        public void World_GetAllUnmanagedSystemsCountIsGT45_WhenInitial()
        {
            // Arrange
            
            // Act
            int result = World.Unmanaged.GetAllUnmanagedSystems(Allocator.Temp).Length;

            var list = World.Unmanaged.GetAllUnmanagedSystems(Allocator.Temp);
            foreach (var system in list)
            {
               // Debug.Log(system);
            }

            // Assert
            Assert.That(result, Is.GreaterThan(45));
        }

        [Test]
        public void World_EntityCountIsGT4_WhenInitial()
        {
            // Arrange
            
            // Act
            int result = World.EntityManager.UniversalQuery.CalculateEntityCount();

            // Assert
            Assert.That(result, Is.GreaterThan(4));
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
