using Unity.Entities;

namespace RMC.DOTS.Systems.PhysicsTrigger
{
    public enum PhysicsTriggerType
    {
        Null,
        Enter,
        Exit,
        Stay
    }
    
    public struct PhysicsTriggerOutputTag : IComponentData
    {
        //The from
        public Entity TheEntity;
        
        //The other being hit
        public Entity TheOtherEntity;
        
        public PhysicsTriggerType PhysicsTriggerType;

        public int TimeFrameCountForLastCollision;
    }
}