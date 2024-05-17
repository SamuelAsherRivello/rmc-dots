#if MAGICTWEEN_ENABLE_TRANSFORM_JOBS && MAGICTWEEN_ENABLE_DEMO

using System.Numerics;
using MagicTween;
using MagicTween.Core;
using MagicTween.Core.Systems;
using Unity.Entities;
using Unity.Transforms;
using MagicTween.Translators;
using RMC.DOTS.Systems.Audio;
using RMC.DOTS.Systems.Player;
using Unity.Mathematics;
using Unity.Rendering;
using UnityEngine;

namespace RMC.DOTS.Tweening.MagicTween
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    public partial struct PlayerTweenSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerTweenSystemAuthoring.PlayerTweeningSystemIsEnabledTag>();
            state.RequireForUpdate<EndInitializationEntityCommandBufferSystem.Singleton>();
        }

        
        /// <summary>
        /// <see cref="PositionTranslator"/>
        /// </summary>
        /// <param name="state"></param>
        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.
                GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>().
                CreateCommandBuffer(state.WorldUnmanaged);
            
            /////////////////////////////////////
            // 1. NOT Tweened?
            foreach (var (localTransform, entity) in
                     SystemAPI.Query<RefRW<LocalTransform>>().WithAll<PlayerTag>().WithNone<PlayerTweenExecuteOnceTag>()
                         .WithEntityAccess())
            {


                /////////////////////////////////////
                // 2. Do the tween sequence
                const float duration = 0.5f;
                var tween1 = Tween.Entity.To<LocalTransform, ScaleTranslator>(entity, 0.1f, duration);
                var tween2 = Tween.Entity.To<LocalTransform, ScaleTranslator>(entity, 1f, duration);
                var tween3 = Tween.Entity.To<LocalTransform, EulerAnglesYTranslator>(entity, 90f, duration);
                var tween4 = Tween.Entity.To<LocalTransform, EulerAnglesXTranslator>(entity, 90f, duration);
                var tween5 = Tween.Entity.To<LocalTransform, EulerAnglesZTranslator>(entity, 89f, duration);
                var tween6 = Tween.Entity.To<MySampleTweenComponent, MySampleTweenTranslator>(entity, 0.1f, duration);


                var sequence = Sequence.Create()
                    .Append(tween1)
                    .Append(tween2)
                    .Append(tween3)
                    .Append(tween4)
                    .Append(tween5)
                    .Append(tween6);


                // 3. Do the tween in parallel (to the above
                float longDuration = (duration * 6);
                var longTween1 = Tween.Entity.FromTo<LocalTransform, PositionTranslator>
                (
                    entity,
                    new float3(0, 0, 0),
                    new float3(2, 2, 2),
                    longDuration / 2
                ).SetEase(Ease.InOutCubic).SetLoops(2, LoopType.Yoyo);

                longTween1.Play();

            }
            
            
            
            /////////////////////////////////////
            // 4. NOT Tweened?
            foreach (var (urpMaterialPropertyBaseColor, entity) in 
                     SystemAPI.Query<RefRW<URPMaterialPropertyBaseColor>>().
                         WithAll<PlayerTag>().
                         WithNone<PlayerTweenExecuteOnceTag>().
                         WithEntityAccess())
            {
 
                /////////////////////////////////////
                // 5. Change color
                Color fromColorOld = new Color(0, 255, 0, 1);
                float4 fromColor = new float4();
                fromColor.x = fromColorOld.linear.r;
                fromColor.y = fromColorOld.linear.g;
                fromColor.z = fromColorOld.linear.b;
                fromColor.w = fromColorOld.linear.a;
                urpMaterialPropertyBaseColor.ValueRW.Value = fromColor;
            }
            
            
            
            /////////////////////////////////////
            // 6. NOT Tweened?
            foreach (var (urpMaterialPropertyBaseColor, entity) in 
                     SystemAPI.Query<RefRW<LocalTransform>>().
                         WithAll<PlayerTag>().
                         WithNone<PlayerTweenExecuteOnceTag>().
                         WithEntityAccess())
            {
            
                /////////////////////////////////////
                // 7. Mark as Tweened!
                ecb.AddComponent<PlayerTweenExecuteOnceTag>(entity);
            }
        }
    }
}

#endif //#if MAGICTWEEN_ENABLE_TRANSFORM_JOBS && MAGICTWEEN_ENABLE_DEMO
