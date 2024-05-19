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
    
    public struct PhysicsTriggerOutputComponent : IComponentData
    { 
        //The from
        public Entity TheEntity;
        
        //The other being hit
        public Entity TheOtherEntity;
        
        public PhysicsTriggerType PhysicsTriggerType;

        public int TimeFrameCountForLastCollision;
        
        //KLUGE: Should not be needed. Due to testing, I think it helps
        //TODO: why not lower it to '0'? I guess this sysetm runs one frame after the last one?
        public const int FramesToWait = 2; // I tried 1 (ok), 2 (best), 3 (ok), and 4-5 (zero triggers)
    }
}