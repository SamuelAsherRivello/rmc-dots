using System;
using NUnit.Framework;
using RMC.DOTS.Systems.Random;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics.Tests.Aspects;
using Unity.Transforms;
using UnityEngine;
using WayneGames.ECSTestsFixture;

namespace RMC.DOTS.Systems.PhysicsVelocityImpulse
{
    [Category(nameof(PhysicsVelocityImpulse))]
    [TestFixture]
    public class PhysicsVelocityImpulseSystemPlayModeTest : ECSTestsFixture
    {
        [SetUp]
        public override void Setup()
        {
            CreateDefaultWorld = true;
            base.Setup();
            
            // Dependencies
            //TODO:     Sometimes bakers do complex setup. Can I move the following into
            //          the baker so it's not repeated in every test?
            Entity randomEntity = EntityManager.CreateEntity(typeof(RandomSystemAuthoring.RandomSystemIsEnabledTag));
            uint seed = (uint)UnityEngine.Random.Range(0, Int32.MaxValue);
            EntityManager.AddComponentData<RandomComponent>(randomEntity,
                new RandomComponent
                {
                    Random = Unity.Mathematics.Random.CreateFromIndex(seed)
                });
            
            //System under test
            World.CreateSystem(typeof(PhysicsVelocityImpulseSystem));
            EntityManager.CreateEntity(typeof(PhysicsVelocityImpulseSystemAuthoring.PhysicsVelocityImpulseSystemIsEnabledTag));
        }
        
        [Test]
        public void LocalTransform_PositionIsSame_After00Updates()
        {
            // Arrange
            Entity entity = RigidBodyAspect_UnitTestsCopy.CreateBodyComponents(RigidBodyAspect_UnitTestsCopy.BodyType.DYNAMIC, EntityManager);
            
            var localTransform = EntityManager.GetComponentData<LocalTransform>(entity);
            localTransform.Position = new float3(0,0,0);
            EntityManager.SetComponentData<LocalTransform>(entity, localTransform);
            
            var physicsVelocityImpulseComponent = new PhysicsVelocityImpulseComponent
            {   
                CanBeNegative = false,
                MinForce = new Vector3(10,0,0),
                MaxForce = new Vector3(10,0,0),
            };
            EntityManager.AddComponentData<PhysicsVelocityImpulseComponent>(entity, physicsVelocityImpulseComponent);
            
            // Act
 
            // Assert 
            var result = EntityManager.GetComponentData<LocalTransform>(entity);
            Assert.That(result.Position, Is.EqualTo(localTransform.Position));
        }
        
        [Test]
        public void LocalTransform_PositionIsNotSame_After01Updates()
        {
            // Arrange
            
            // TODO: Discuss1 how knowing what components you need (physics) for a given system (physics) is difficult
            Entity entity = RigidBodyAspect_UnitTestsCopy.CreateBodyComponents(RigidBodyAspect_UnitTestsCopy.BodyType.DYNAMIC, EntityManager);
            
            // TODO: Discuss2 tools to help find out what components you need
            // var t = EntityManager.GetComponentTypes(entity, Allocator.Temp);
            // foreach (var x in t)
            // {
            //     Debug.Log(x);
            // }
            
            var localTransform = EntityManager.GetComponentData<LocalTransform>(entity);
            localTransform.Position = new float3(0,0,0);
            EntityManager.SetComponentData<LocalTransform>(entity, localTransform);
            
            var physicsVelocityImpulseComponent = new PhysicsVelocityImpulseComponent
            {   
                CanBeNegative = false,
                MinForce = new Vector3(10,0,0),
                MaxForce = new Vector3(10,0,0),
            };
            EntityManager.AddComponentData<PhysicsVelocityImpulseComponent>(entity, physicsVelocityImpulseComponent);
            
            // Act
            World.Update();
 
            // Assert 
            var result = EntityManager.GetComponentData<LocalTransform>(entity);
            Assert.That(result.Position, Is.Not.EqualTo(localTransform.Position));
        }
    }
}
