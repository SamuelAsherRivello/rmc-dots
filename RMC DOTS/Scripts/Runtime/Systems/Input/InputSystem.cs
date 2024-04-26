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
                    Move = _rmcDotsInputAction.Standard.Move.ReadValue<Vector2>(),
                    Look = _rmcDotsInputAction.Standard.Look.ReadValue<Vector2>(),
                    Action1 = _rmcDotsInputAction.Standard.Action1.IsPressed(),
                    Action2 = _rmcDotsInputAction.Standard.Action2.IsPressed()
                });
        }
    }
}
#endif //ENABLE_INPUT_SYSTEM