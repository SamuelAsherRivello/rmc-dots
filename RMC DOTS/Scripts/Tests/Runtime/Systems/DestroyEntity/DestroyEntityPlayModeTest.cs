using NUnit.Framework;
using Unity.Collections;
using Unity.Hack.ECSTestsFixture;

namespace RMC.DOTS.Systems.DestroyEntity
{   
    [Category(nameof(RMC.DOTS.Systems.DestroyEntity))]
    [TestFixture]
    public class DestroyEntityPlayModeTest : ECSTestsFixture
    {
        [SetUp]
        public override void Setup()
        {
            CreateDefaultWorld = true;
            base.Setup();
            
            World.CreateSystem(typeof(DestroyEntitySystem));
            EntityManager.CreateEntity(typeof(DestroyEntitySystemAuthoring.DestroyEntitySystemIsEnabledTag));
            
        }

        [Test]
        public void CreateEntity_EntityCountIncrementsBy01_AfterCreation()
        {
            // Arrange
            var entityCountBefore = EntityManager.GetAllEntities(Allocator.Temp).Length;
            
            // Act
            EntityManager.CreateEntity();
            var entityCountAfterCreate = EntityManager.GetAllEntities(Allocator.Temp).Length;
            
            // Assert 
            Assert.That(entityCountAfterCreate, Is.EqualTo(entityCountBefore + 1));
        }
        
        [Test]
        public void DestroyEntity_EntityCountIsSame_AfterDestroyWith00SecondsWith00Updates ()
        {
            // Arrange
            var entityCountBefore = EntityManager.GetAllEntities(Allocator.Temp).Length;
            var entity = EntityManager.CreateEntity();

            // Act
            EntityManager.AddComponentData<DestroyEntityComponent>(entity, new DestroyEntityComponent
            {
                TimeTillDestroyInSeconds = 0
            });
 
            // Assert 
            var entityCountFinal = EntityManager.GetAllEntities(Allocator.Temp).Length;
            Assert.That(entityCountFinal, Is.EqualTo(entityCountFinal));
        }
        
        [Test]
        public void DestroyEntity_EntityCountIsSame_AfterDestroyWith01SecondsWith00Updates ()
        {
            // Arrange
            var entityCountBefore = EntityManager.GetAllEntities(Allocator.Temp).Length;
            var entity = EntityManager.CreateEntity();

            // Act
            EntityManager.AddComponentData<DestroyEntityComponent>(entity, new DestroyEntityComponent
            {
                TimeTillDestroyInSeconds = 1
            });
 
            // Assert 
            var entityCountFinal = EntityManager.GetAllEntities(Allocator.Temp).Length;
            Assert.That(entityCountFinal, Is.EqualTo(entityCountFinal));
        }
        
        [Test]
        public void DestroyEntity_EntityCountDecrements_AfterDestroyWith00SecondsWith01Updates ()
        {
            // Arrange
            var entityCountBefore = EntityManager.GetAllEntities(Allocator.Temp).Length;
            var entity = EntityManager.CreateEntity();

            // Act
            EntityManager.AddComponentData<DestroyEntityComponent>(entity, new DestroyEntityComponent
            {
                TimeTillDestroyInSeconds = 0
            });
            World.Update();
 
            // Assert 
            var entityCountFinal = EntityManager.GetAllEntities(Allocator.Temp).Length;
            Assert.That(entityCountFinal, Is.EqualTo(entityCountFinal));
        }
        
        [Test]
        public void DestroyEntity_EntityCountIsSame_AfterDestroyWith01SecondsWith01Updates ()
        {
            // Arrange
            var entityCountBefore = EntityManager.GetAllEntities(Allocator.Temp).Length;
            var entity = EntityManager.CreateEntity();

            // Act
            EntityManager.AddComponentData<DestroyEntityComponent>(entity, new DestroyEntityComponent
            {
                TimeTillDestroyInSeconds = 1
            });
            World.Update();
 
            // Assert 
            var entityCountFinal = EntityManager.GetAllEntities(Allocator.Temp).Length;
            Assert.That(entityCountFinal, Is.EqualTo(entityCountFinal));
        }
        
    }
}
