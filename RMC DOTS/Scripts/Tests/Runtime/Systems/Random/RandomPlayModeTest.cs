using System;
using NUnit.Framework;
using Unity.Entities;
using Unity.Hack.ECSTestsFixture;

namespace RMC.DOTS.Systems.Random
{   
    [Category(nameof(RMC.DOTS.Systems.Random))]
    [TestFixture]
    public class RandomPlayModeTest : ECSTestsFixture
    {
        private Entity _randomComponentEntity;

        private void SetupWithRandomSeed(uint seed)
        {
            EntityManager.AddComponentData<RandomComponent>(_randomComponentEntity,
                new RandomComponent
                {
                    Random = Unity.Mathematics.Random.CreateFromIndex(seed)
                });
        }
        
        [SetUp]
        public override void Setup()
        {
            CreateDefaultWorld = true;
            base.Setup();
            
            _randomComponentEntity = EntityManager.CreateEntity
            (
                typeof(RandomSystemAuthoring.RandomSystemIsEnabledTag),
                typeof(RandomComponent)
            );

        }

        [TearDown]
        public override void TearDown()
        {
            base.TearDown();
            _randomComponentEntity = Entity.Null;
        }

        [Test]
        public void Aspect_NextFloatResulIsSame_WhenSeedIs00()
        {
            // Arrange
            uint seed = 0;

            // Act
            SetupWithRandomSeed(seed);
            var randomComponentAspect1 = EntityManager.GetAspect<RandomComponentAspect>(_randomComponentEntity);
            var result1 =  randomComponentAspect1.NextFloat(0, 10000);
        
            SetupWithRandomSeed(seed);
            var randomComponentAspect2 = EntityManager.GetAspect<RandomComponentAspect>(_randomComponentEntity);
            var result2 =  randomComponentAspect2.NextFloat(0, 10000);
 
            // Assert 
            Assert.That(result1, Is.EqualTo(result2));
        }
        
        [Test]
        public void Aspect_NextFloatResulIsSame_WhenSeedIs100()
        {
            // Arrange
            uint seed = 100;

            // Act
            SetupWithRandomSeed(seed);
            var randomComponentAspect1 = EntityManager.GetAspect<RandomComponentAspect>(_randomComponentEntity);
            var result1 =  randomComponentAspect1.NextFloat(0, 10000);
        
            SetupWithRandomSeed(seed);
            var randomComponentAspect2 = EntityManager.GetAspect<RandomComponentAspect>(_randomComponentEntity);
            var result2 =  randomComponentAspect2.NextFloat(0, 10000);
 
            // Assert 
            Assert.That(result1, Is.EqualTo(result2));
        }
        
        [Test]
        public void Aspect_NextFloatResulIsNotSame_WhenSeedIsRandom()
        {
            // Arrange
            uint seed1 = (uint)UnityEngine.Random.Range(0, Int32.MaxValue);
            uint seed2 = (uint)UnityEngine.Random.Range(0, Int32.MaxValue);
            
            // Act
            SetupWithRandomSeed(seed1);
            var randomComponentAspect1 = EntityManager.GetAspect<RandomComponentAspect>(_randomComponentEntity);
            var result1 =  randomComponentAspect1.NextFloat(0, 10000);
        
            SetupWithRandomSeed(seed2);
            var randomComponentAspect2 = EntityManager.GetAspect<RandomComponentAspect>(_randomComponentEntity);
            var result2 =  randomComponentAspect2.NextFloat(0, 10000);
 
            // Assert 
            Assert.That(result1, Is.Not.EqualTo(result2));
        }
    }
}
