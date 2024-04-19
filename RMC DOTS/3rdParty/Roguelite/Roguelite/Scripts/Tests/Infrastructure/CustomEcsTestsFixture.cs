using Unity.Entities;
using Unity.Entities.Tests;

namespace Roguelite.Tests.Infrastructure
{
    public class CustomEcsTestsFixture : ECSTestsFixture
    {
        private const int EntityCount = 1000;

        /// <summary>
        /// A proxy property just for the sake of following my codestyle regarding naming in my tests
        /// </summary>
        protected EntityManager Manager => m_Manager;

        protected void UpdateSystem<T>() where T : unmanaged, ISystem
        {
            World.GetExistingSystem<T>().Update(World.Unmanaged);
        }

        protected SystemHandle CreateSystem<T>() where T : unmanaged, ISystem => World.CreateSystem<T>();

        protected Entity CreateEntity(params ComponentType[] types) => Manager.CreateEntity(types);

        protected void CreateEntities(ComponentType[] types, int entityCount = EntityCount)
        {
            for (var i = 0; i < entityCount; i++)
            {
                Manager.CreateEntity(types);
            }
        }

        protected void CreateEntityCommandBufferSystem()
        {
            World.CreateSystem<EndSimulationEntityCommandBufferSystem>();
        }
    }
}