using RMC.Components;
using UnityEngine;

namespace RMC.DOTS.Components
{
    public enum DividerType
    {
        Null,
        SharedSystems,
        Systems,
        Instances
    }
    
    public class DemoHierarchyDivider : HierarchyDivider
    {
        [SerializeField] 
        public DividerType DividerType;
        
        protected override string Title
        {
            get
            {
                if (DividerType == DividerType.Null)
                {
                    return "";
                }
                return DividerType.ToString();
            }
        }
    }
}
