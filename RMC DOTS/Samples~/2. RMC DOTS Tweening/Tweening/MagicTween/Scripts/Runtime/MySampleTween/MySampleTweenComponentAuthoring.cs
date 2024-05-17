//DO NOT USE #if define in this file

using Unity.Entities;
using UnityEngine;

namespace RMC.DOTS.Tweening.MagicTween
{
    public class MySampleTweenComponentAuthoring : MonoBehaviour
    {
        public class MySampleTweenComponentAuthoringBaker : Baker<MySampleTweenComponentAuthoring>
        {
            public override void Bake(MySampleTweenComponentAuthoring authoring)
            {
                Entity inputEntity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<MySampleTweenComponent>(inputEntity);
            }
        }
    }
}
