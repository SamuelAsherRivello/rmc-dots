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
            SystemAPI.SetSingleton(
                new InputComponent
                {
                    MoveFloat2 = _rmcDotsInputAction.Standard.Move.ReadValue<Vector2>(),
                    LookFloat2 = _rmcDotsInputAction.Standard.Look.ReadValue<Vector2>(),
                    IsPressedAction1 = _rmcDotsInputAction.Standard.Action1.IsPressed(),
                    IsPressedAction2 = _rmcDotsInputAction.Standard.Action2.IsPressed(),
                    WasPressedThisFrameAction1 = _rmcDotsInputAction.Standard.Action1.WasPressedThisFrame(),
                    WasPressedThisFrameAction2 = _rmcDotsInputAction.Standard.Action2.WasPressedThisFrame()
                });
        }
    }
}
#endif //ENABLE_INPUT_SYSTEM