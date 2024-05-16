using NUnit.Framework;

namespace RMC.DOTS.Systems.Scoring
{
    [Category(nameof (RMC.DOTS.Systems.Scoring))]
    [TestFixture]
    public class ScoreComponentTest
    {
        [Test]
        public void ScoreComponent_AreEqual_WhenDefault()
        {
            // Arrange
            ScoreComponent scoreComponent01 = new ScoreComponent();
            ScoreComponent scoreComponent02 = new ScoreComponent();
            
            // Act

            // Assert
            Assert.That(scoreComponent01, Is.EqualTo(scoreComponent02));
        }
        
        [Test]
        public void ScoreComponent_AreEqual_WhenMatchingValues()
        {
            // Arrange
            ScoreComponent scoreComponent01 = new ScoreComponent
            {
                ScoreCurrent = 10,
                ScoreMax = 11
            };
            ScoreComponent scoreComponent02 = new ScoreComponent
            {
                ScoreCurrent = 10,
                ScoreMax = 11
            };
            
            // Act

            // Assert
            Assert.That(scoreComponent01, Is.EqualTo(scoreComponent02));
        }
        
        [Test]
        public void ScoreComponent_AreNotEqual_WhenUniqueValues01()
        {
            // Arrange
            ScoreComponent scoreComponent01 = new ScoreComponent
            {
                ScoreCurrent = 1,
                ScoreMax = 3
            };
            ScoreComponent scoreComponent02 = new ScoreComponent
            {
                ScoreCurrent = 2,
                ScoreMax = 3
            };
            
            // Act

            // Assert
            Assert.That(scoreComponent01, Is.Not.EqualTo(scoreComponent02));
        }
        
        [Test]
        public void ScoreComponent_AreNotEqual_WhenUniqueValues02()
        {
            // Arrange
            ScoreComponent scoreComponent01 = new ScoreComponent
            {
                ScoreCurrent = 10,
                ScoreMax = 11
            };
            ScoreComponent scoreComponent02 = new ScoreComponent
            {
                ScoreCurrent = 12,
                ScoreMax = 13
            };
            
            // Act

            // Assert
            Assert.That(scoreComponent01, Is.Not.EqualTo(scoreComponent02));
        }
    }   
}
