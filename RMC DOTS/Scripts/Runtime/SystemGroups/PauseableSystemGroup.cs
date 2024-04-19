using Unity.Entities;

namespace RMC.DOTS.SystemGroups
{
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderLast = true)]
    public partial class PauseableSystemGroup : ComponentSystemGroup
    {
   
    } 
}