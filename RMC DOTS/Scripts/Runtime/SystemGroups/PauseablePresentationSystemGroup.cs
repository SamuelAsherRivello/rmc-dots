using Unity.Entities;

namespace RMC.DOTS.SystemGroups
{
    [UpdateInGroup(typeof(PresentationSystemGroup))]
    [UpdateAfter(typeof(UnpauseablePresentationSystemGroup))]
    public partial class PauseablePresentationSystemGroup : ComponentSystemGroup
    {
   
    } 
}