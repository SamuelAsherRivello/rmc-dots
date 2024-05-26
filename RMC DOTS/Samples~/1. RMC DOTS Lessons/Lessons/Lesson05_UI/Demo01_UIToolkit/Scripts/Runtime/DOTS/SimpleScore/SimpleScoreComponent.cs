using Unity.Entities;

namespace RMC.DOTS.Lessons.UI.UIToolkit
{
    /// <summary>
    /// Store data for a single player
    /// </summary>
    public struct SimpleScoreComponent : IComponentData
    {
        public int Score;

        public override bool Equals(object obj)
        {
            return obj is SimpleScoreComponent component &&
                   Score == component.Score;
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(Score);
        }

        public override string ToString()
        {
            return $"[ScoreComponent (Score: {Score})]";
        }
    }
}