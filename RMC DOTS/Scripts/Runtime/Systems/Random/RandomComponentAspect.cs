using System.Numerics;
using Unity.Entities;

namespace RMC.DOTS.Systems.Random
{
    /// <summary>
    /// An aspect is an object-like wrapper that you can use to group together
    /// a subset of an entity's components into a single C# struct.
    /// <see cref="https://docs.unity3d.com/Packages/com.unity.entities@1.0/manual/aspects-concepts.html"/>
    /// </summary>
    public readonly partial struct RandomComponentAspect : IAspect
    {
        private readonly RefRW<RandomComponent> _randomComponent;
        
        public float NextInt(int minInclusive, int maxExclusive)
        {
            Randomize();
            return _randomComponent.ValueRW.Random.NextInt(minInclusive, maxExclusive);
        }

        public float NextFloat(float minInclusive, float maxExclusive)
        {
            Randomize();
            return _randomComponent.ValueRW.Random.NextFloat(minInclusive, maxExclusive);
        }
  
        public float NextFloat (float minInclusive, float maxInclusive, bool canBeNegative)
        {
            Randomize();
            float magnitude = NextFloat(minInclusive, maxInclusive);
            
            //
            bool isNegative = canBeNegative && NextInt(0,2) == 0; 
            return isNegative ? -magnitude : magnitude;
        }

        public Vector3 NextFloat3 (Vector3 minInclusive, Vector3 maxInclusive, bool canBeNegative)
        {
            Randomize();
            return new Vector3(
                NextFloat(
                    minInclusive.X,
                    maxInclusive.X,
                    canBeNegative),
                NextFloat(
                    minInclusive.Y,
                    maxInclusive.Y,
                    canBeNegative),
                NextFloat(
                    minInclusive.Z,
                    maxInclusive.Z,
                    canBeNegative));
        }
        
        /// <summary>
        /// All public methods must call this once to re-randomize
        /// </summary>
        private void Randomize()
        {
            uint index = _randomComponent.ValueRW.Random.NextUInt(uint.MaxValue);
            _randomComponent.ValueRW.Random = Unity.Mathematics.Random.CreateFromIndex(index);
        }
    }
}