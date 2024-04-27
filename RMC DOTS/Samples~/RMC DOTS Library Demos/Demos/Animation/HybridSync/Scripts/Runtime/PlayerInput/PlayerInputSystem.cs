using RMC.DOTS.SystemGroups;
using RMC.DOTS.Systems.Input;
using RMC.DOTS.Systems.Player;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace RMC.DOTS.Demos.HybridSync
{
    [UpdateInGroup(typeof(PauseableSystemGroup))]
    public partial struct PlayerInputSystem : ISystem 
    {
        //NOTE: Its not good practice to store state on a system
        private int _triggerIndex;
        private int _triggerIndexMin;
        private int _triggerIndexMax;
        
        public void OnCreate(ref SystemState state)
        {
            _triggerIndex = 0;
            _triggerIndexMin = 0;
            _triggerIndexMax = 3;
        
            state.RequireForUpdate<HybridSyncSystemAuthoring.HybridSyncSystemIsEnabledTag>();
            state.RequireForUpdate<InputComponent>();
        }

        public void OnUpdate(ref SystemState state)
        {
            // First get the current input value from the PlayerMoveInput component. This component is set in the
            // GetPlayerInputSystem that runs earlier in the frame.
            float2 move = SystemAPI.GetSingleton<InputComponent>().MoveFloat2;
            float2 look = SystemAPI.GetSingleton<InputComponent>().LookFloat2;
            bool isAction = SystemAPI.GetSingleton<InputComponent>().WasPressedThisFrameAction1 ||
                            SystemAPI.GetSingleton<InputComponent>().WasPressedThisFrameAction2;
            
            float deltaTime = SystemAPI.Time.DeltaTime;
            float linearSpeed = 10f;
            float angularSpeed = 30f;
            float2 moveComposite = float2.zero;
            
            // Here we support EITHER look or move to move around
            // Prioritize MOVE, if no MOVE is set, then use look
            if (move.x != 0)
            {
                moveComposite.x = move.x;
            }
            else
            {
                moveComposite.x = look.x;
            }

            if (move.y != 0)
            {
                moveComposite.y =  move.y;
            }
            else
            {
                moveComposite.y = look.y;
            }
            
            bool isMoving = math.length(moveComposite) > 0;

            // Triggers
            var triggers = new string[4];
            triggers[0] = "normal";
            triggers[1] = "angry";
            triggers[2] = "happy";
            triggers[3] = "dead";
            
            //Debug.Log("Next Trigger: " + moveComposite + " " + isAction + " " + triggers[_triggerIndex]);
            if (isAction)
            { 
                _triggerIndex++;
                if (_triggerIndex > _triggerIndexMax)
                {
                    _triggerIndex = _triggerIndexMin;
                    //Debug.Log("Next Trigger: " + triggers[_triggerIndex]);
                }
            }
            
            
            
            // Loop through all players. Add HybridSyncInputComponent to each
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach (var (localTransform, entity) in
                     SystemAPI.Query<RefRO<LocalTransform>>().
                         WithNone<HybridSyncInputComponent>().
                         WithAll<HybridSyncAnimatorReferenceComponent, PlayerTag>().WithEntityAccess())
             {
                 ecb.AddComponent<HybridSyncInputComponent>(entity);
             }
             ecb.Playback(state.EntityManager);
             ecb.Dispose();
             
             
            // Loop through all players. Update HybridSyncInputComponent on each
            foreach (var (toHybridSyncInputComponent, fromHybridSyncAnimatorReferenceComponent) in
                     SystemAPI.Query<RefRW<HybridSyncInputComponent>, HybridSyncAnimatorReferenceComponent>().
                         WithAll<LocalTransform, PlayerTag>())
            {
                
                // Keyframes
                toHybridSyncInputComponent.ValueRW.Blend = isMoving ? 1 : 0;

                // Trigger
                if (isAction)
                {
                    toHybridSyncInputComponent.ValueRW.Trigger = triggers[_triggerIndex];
                }
                
                // Move
                var movement = new Vector3(
                    moveComposite.x,
                    0,
                    moveComposite.y
                ) * deltaTime * linearSpeed;

                toHybridSyncInputComponent.ValueRW.Position =  
                    fromHybridSyncAnimatorReferenceComponent.Value.transform.position + movement;

                
                // Rotation
                if (math.length(movement) > 0.01f)
                {
                    // Face Movement when moving
                    var targetRotation =
                        quaternion.LookRotation(movement, Vector3.up);
                    
                    toHybridSyncInputComponent.ValueRW.Rotation =
                        math.slerp(fromHybridSyncAnimatorReferenceComponent.Value.transform.rotation, 
                            targetRotation,
                            angularSpeed * deltaTime);
                }
                else
                {
                    // Face camera when not moving
                    toHybridSyncInputComponent.ValueRW.Rotation = Quaternion.Euler(0, 180, 0);
                }
            }
        }
    }
}