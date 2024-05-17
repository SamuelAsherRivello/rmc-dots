//DO NOT USE #if define in this file

using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Tweening.MagicTween
{
    public class PlayerTweenSystemAuthoring : MonoBehaviour
    {
        [SerializeField] 
        public bool IsSystemEnabled = true;
        
        public struct PlayerTweeningSystemIsEnabledTag : IComponentData {}
        
        
        public class PlayerTweenSystemAuthoringBaker : Baker<PlayerTweenSystemAuthoring>
        {
            public override void Bake(PlayerTweenSystemAuthoring authoring)
            {
                if (authoring.IsSystemEnabled)
                {
                    Entity inputEntity = GetEntity(TransformUsageFlags.Dynamic);
                    AddComponent<PlayerTweeningSystemIsEnabledTag>(inputEntity);
                }
            }
        }
    }
}
