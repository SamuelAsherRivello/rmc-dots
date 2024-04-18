using Unity.Entities;
using Unity.Physics;
using UnityEngine;

namespace RMC.Playground3D.Pong2D_Version02_DOTS
{
    public enum PlayerType
    {
        Human,
        CPU
    }
    public class PaddleMoveAuthoring : MonoBehaviour
    {
        public float Speed = 50f;
        
        public PlayerType PlayerType = PlayerType.Human;
    }

    public class PaddleMoveBaker : Baker<PaddleMoveAuthoring>
    {
        public override void Bake(PaddleMoveAuthoring moveAuthoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            
            switch (moveAuthoring.PlayerType)
            {
                case PlayerType.Human:
                    AddComponent<PaddleHumanTag>(entity);
                    break;
                case PlayerType.CPU:
                    AddComponent<PaddleCPUTag>(entity);
                    break;
            }


            AddComponent(entity, new PaddleMoveComponent { Value = moveAuthoring.Speed });
        }
    }

    public class FixedList128<T>
    {
    }
}