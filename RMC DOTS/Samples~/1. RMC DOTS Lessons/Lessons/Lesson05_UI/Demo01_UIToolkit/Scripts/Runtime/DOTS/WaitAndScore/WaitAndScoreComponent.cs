using System;
using Unity.Entities;

namespace RMC.DOTS.Lessons.UI.UIToolkit
{
    public struct WaitAndScoreComponent : IComponentData
    {
        public float WaitForSeconds;
        public int ScoreDelta;
    }
}