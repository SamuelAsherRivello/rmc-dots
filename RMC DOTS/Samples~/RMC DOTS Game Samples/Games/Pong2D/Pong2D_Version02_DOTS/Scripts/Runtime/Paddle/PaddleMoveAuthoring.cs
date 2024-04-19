using Unity.Entities;
using Unity.Physics;
using UnityEngine;

namespace RMC.DOTS.Samples.Pong2D.Pong2D_Version02_DOTS
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
        public override void Bake(PaddleMoveAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            
            switch (authoring.PlayerType)
            {
                case PlayerType.Human:
                    AddComponent<PaddleHumanTag>(entity);
                    break;
                case PlayerType.CPU:
                    AddComponent<PaddleCPUTag>(entity);
                    break;
            }


            AddComponent(entity, new PaddleMoveComponent { Value = authoring.Speed });
        }
    }

    public class FixedList128<T>
    {
    }
}