using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Lessons.UI.UIToolkit
{
    /// <summary>
    /// Place this MonoBehaviour on a GameObject in the Scene
    /// To enable the <see cref="WaitAndScoreSystem"/>
    /// </summary>
    public class SimpleScoreSystemAuthoring : MonoBehaviour
    {
        public int Score;

        [SerializeField] 
        public bool IsSystemEnabled = true;
        
        public struct SimpleScoreSystemIsEnabled : IComponentData {}
        
        public class SimpleScoreSystemAuthoringBaker : Baker<SimpleScoreSystemAuthoring>
        {
            public override void Bake(SimpleScoreSystemAuthoring authoring)
            {
                if (authoring.IsSystemEnabled)
                {
                    var entity = GetEntity(TransformUsageFlags.None);
                    
                    AddComponent<SimpleScoreSystemIsEnabled>(entity);
                    
                    AddComponent(entity,
                        new SimpleScoreComponent()
                        {
                            Score = authoring.Score,
                        });
                }
            }
        }
    }
}