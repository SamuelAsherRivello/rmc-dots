using NUnit.Framework;

namespace RMC.DOTS.World
{
    /// <summary>
    /// Here, in a rare case, we want this test to run EXACTLY
    /// the same here in editor as in play mode. So we inherit from play mode
    /// </summary>
    [Category(nameof(RMC.DOTS.World))]
    [TestFixture]
    public class WorldDefaultTest : WorldDefaultPlayModeTest
    {
    }
}
