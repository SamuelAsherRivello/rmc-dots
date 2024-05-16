using NUnit.Framework;

namespace RMC.DOTS.Systems.Scoring
{
    [Category(nameof (RMC.DOTS.Systems.Scoring))]
    [TestFixture]
    public class ScoringComponentTest
    {
        [Test]
        public void ScoringComponent_AreEqual_WhenDefault()
        {
            // Arrange
            ScoringComponent scoringComponent01 = new ScoringComponent();
            scoringComponent01.ScoreComponent01 = new ScoreComponent();
            scoringComponent01.ScoreComponent02 = new ScoreComponent();
            
            ScoringComponent scoringComponent02 = new ScoringComponent();
            scoringComponent02.ScoreComponent01 = new ScoreComponent();
            scoringComponent02.ScoreComponent02 = new ScoreComponent();
            
            // Act

            // Assert
            Assert.That(scoringComponent01, Is.EqualTo(scoringComponent02));
        }

        [Test]
        public void ScoringComponent_AreEqual_WhenMatchingValues()
        {
            // Arrange
            ScoringComponent scoringComponent01 = new ScoringComponent();
            scoringComponent01.ScoreComponent01 = new ScoreComponent
            {
                ScoreCurrent = 10,
                ScoreMax = 11
            };
            scoringComponent01.ScoreComponent02 = new ScoreComponent();

            ScoringComponent scoringComponent02 = new ScoringComponent();
            scoringComponent02.ScoreComponent01 = new ScoreComponent
            {
                ScoreCurrent = 10,
                ScoreMax = 11
            };
            scoringComponent02.ScoreComponent02 = new ScoreComponent();

            // Act

            // Assert
            Assert.That(scoringComponent01, Is.EqualTo(scoringComponent02));
        }

        [Test]
        public void ScoringComponent_AreNotEqual_WhenUniqueValues()
        {
            // Arrange
            ScoringComponent scoringComponent01 = new ScoringComponent();
            scoringComponent01.ScoreComponent01 = new ScoreComponent
            {
                ScoreCurrent = 10,
                ScoreMax = 11
            };
            scoringComponent01.ScoreComponent02 = new ScoreComponent();
            
            ScoringComponent scoringComponent02 = new ScoringComponent();
            scoringComponent02.ScoreComponent01 = new ScoreComponent
            {
                ScoreCurrent = 12,
                ScoreMax = 13
            };
            scoringComponent02.ScoreComponent02 = new ScoreComponent();
            
            // Act

            // Assert
            Assert.That(scoringComponent01, Is.Not.EqualTo(scoringComponent02));
        }
    }   
}
