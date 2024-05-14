using Unity.Entities;

namespace RMC.DOTS.Systems.Culling
{
    /// <summary>
    /// </summary>
    public struct CullingComponent : IComponentData
    {
        // Nullable value is used so that on the VERy first frame we capture 
        // a 'has changed' moment
        public bool? IsOffscreen;

        public readonly bool WillDestroyWhenCulled;

        public CullingComponent(bool willDestroyWhenCulled = false)
        {
            IsOffscreen = null;
            WillDestroyWhenCulled = willDestroyWhenCulled;
        }

    }
}