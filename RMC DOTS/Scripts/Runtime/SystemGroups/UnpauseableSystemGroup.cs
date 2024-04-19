using Unity.Entities;

namespace RMC.DOTS.SystemGroups
{
    [UpdateInGroup(typeof(PresentationSystemGroup), OrderLast = true)]
    public partial class UnpauseableSystemGroup : ComponentSystemGroup
    {
   
    } 
}