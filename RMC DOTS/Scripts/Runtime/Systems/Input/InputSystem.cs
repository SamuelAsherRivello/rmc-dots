#if ENABLE_INPUT_SYSTEM
using RMC.DOTS.SystemGroups;
using RMCDotsInputActionNamespace;
using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.Input
{
    [UpdateInGroup(typeof(PauseableSystemGroup))]
    public partial class InputSystem : SystemBase
    {
        // Reference to our custom input asset
        private RMCDotsInputAction _rmcDotsInputAction;

        protected override void OnCreate()
        {
            RequireForUpdate<InputSystemAuthoring.InputSystemIsEnabledTag>();
            _rmcDotsInputAction = new RMCDotsInputAction();
            _rmcDotsInputAction.Enable();
        }

        protected override void OnUpdate()
        {
            Vector2 playerMoveInput = _rmcDotsInputAction.Standard.Move.ReadValue<Vector2>();
            SystemAPI.SetSingleton(new InputComponent { Move = playerMoveInput });
        }
    }
}
#endif //ENABLE_INPUT_SYSTEM