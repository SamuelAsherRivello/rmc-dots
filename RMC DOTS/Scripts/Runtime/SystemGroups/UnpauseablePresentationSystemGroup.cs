using Unity.Entities;

namespace RMC.DOTS.SystemGroups
{
    [UpdateInGroup(typeof(PresentationSystemGroup), OrderFirst = true)]
    public partial class UnpauseablePresentationSystemGroup : ComponentSystemGroup
    {
   
    } 
}