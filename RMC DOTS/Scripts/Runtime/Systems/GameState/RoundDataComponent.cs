namespace RMC.DOTS.Systems.GameState
{
    public struct RoundData
    {
        public int RoundCurrent;
        public int RoundMax;
        
        public override string ToString()
        {
            return $"[RoundData (RoundCurrent: {RoundCurrent}, RoundMax: {RoundMax})]";
        }
    }
}