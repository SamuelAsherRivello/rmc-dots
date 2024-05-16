using Unity.Entities;

namespace RMC.DOTS.Systems.Scoring
{
    /// <summary>
    /// Store data for a single player
    /// </summary>
    public struct ScoreComponent : IComponentData
    {
        public int ScoreCurrent;
        public int ScoreMax;

        public override bool Equals(object obj)
        {
            return obj is ScoreComponent component &&
                   ScoreCurrent == component.ScoreCurrent &&
                   ScoreMax == component.ScoreMax;
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(ScoreCurrent, ScoreMax);
        }

        public override string ToString()
        {
            return $"[ScoreComponent (ScoreCurrent: {ScoreCurrent}, ScoreMax: {ScoreMax})]";
        }
    }
    
    /// <summary>
    /// This system manages two separate scores. This is to cover most
    /// common use cases for single player games like RollABall,
    /// and player-vs-cpu games like pong.
    /// </summary>
    public struct ScoringComponent : IComponentData
    {
        public ScoreComponent ScoreComponent01;
        public ScoreComponent ScoreComponent02;
        
        public override bool Equals(object obj)
        {
            return obj is ScoringComponent component &&
                   ScoreComponent01.Equals(component.ScoreComponent01) &&
                   ScoreComponent02.Equals(component.ScoreComponent02);
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(ScoreComponent01, ScoreComponent02);
        }

        public override string ToString()
        {
            return $"[ScoringComponent (ScoreComponent01: {ScoreComponent01}, ScoreComponent02: {ScoreComponent02})]";
        }
    }
}