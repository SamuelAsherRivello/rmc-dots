using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Systems.Scoring
{
    /// <summary>
    /// Place this MonoBehaviour on a GameObject in the Scene
    /// To enable the <see cref="ScoringSystem"/>
    /// </summary>
    public class ScoringSystemAuthoring : MonoBehaviour
    {
        public int ScoreMax;

        public class ScoringSystemBaker : Baker<ScoringSystemAuthoring>
        {
            public override void Bake(ScoringSystemAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity,
                    new ScoringComponent
                    {
                        ScoreComponent01 = new ScoreComponent {ScoreCurrent = 0, ScoreMax = authoring.ScoreMax},
                        ScoreComponent02 = new ScoreComponent {ScoreCurrent = 0, ScoreMax = authoring.ScoreMax}
                    });
            }
        }
    }
}