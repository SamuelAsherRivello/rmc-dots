using Unity.Entities;

namespace RMC.DOTS.Lessons.SharedComponent
{
    //  Component  ------------------------------------
    public struct ClickCountSharedComponent : ISharedComponentData
    {
        public int Value; 

    }
}