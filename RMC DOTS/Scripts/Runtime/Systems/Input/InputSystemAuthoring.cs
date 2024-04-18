#if ENABLE_INPUT_SYSTEM
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.Input
{
    /// <summary>
    /// Place this MonoBehaviour on a GameObject in the Scene
    /// To enable the <see cref="InputSystem"/>
    /// </summary>
    public class InputSystemAuthoring : MonoBehaviour
    {
        [SerializeField] 
        public bool IsSystemEnabled = true;
        
        public struct InputSystemIsEnabledTag : IComponentData {}
        
        public class InputSystemAuthoringBaker : Baker<InputSystemAuthoring>
        {
            public override void Bake(InputSystemAuthoring systemAuthoring)
            {
                if (systemAuthoring.IsSystemEnabled)
                {
                    Entity inputEntity = GetEntity(TransformUsageFlags.Dynamic);
                    
                    //Turn system on
                    AddComponent<InputSystemIsEnabledTag>(inputEntity);
                    
                    //Create first component with default values
                    AddComponent<InputComponent>(inputEntity);
                }
            }
        }
    }
}
#endif //ENABLE_INPUT_SYSTEM