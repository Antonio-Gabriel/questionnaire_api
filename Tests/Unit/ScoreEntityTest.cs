using NUnit.Framework;

namespace Tests.Unit
{
    public class ScoreEntityTest
    {
        Score _score;

        [SetUp]
        public void Setup()
        {
            _score = new Score
            {
                User = new User
                {
                    Name = "Antonio Gabriel",
                    Email = "antoniocamposgabriel@gmail.com",
                    CodeName = "AgDevCoder",                    
                }
            };
        }

        [Test]
        public void TestAddCorrectScore()
        {
            ScoreObject.AddCorrectScore(ref _score);
            ScoreObject.AddCorrectScore(ref _score);

            Assert.That(_score.Correct, Is.EqualTo(2));
        }

        [Test]
        public void TestAddWrongScore()
        {
            ScoreObject.AddWrongScore(ref _score);

            Assert.That(_score.Wrong, Is.EqualTo(1));
        }

        [Test]
        public void TestReachLimitOfScore()
        {
            for (int i = 0; i < 5; i++)
            {
                ScoreObject.AddWrongScore(ref _score);
                ScoreObject.AddCorrectScore(ref _score);
            }

            ScoreObject.AddWrongScore(ref _score);
            ScoreObject.AddCorrectScore(ref _score);

            Assert.That(_score.Correct, Is.EqualTo(5));
            Assert.That(_score.Wrong, Is.EqualTo(5));
        }

        [Test]
        public void TestCleanScore()
        {
            ScoreObject.AddWrongScore(ref _score);
            ScoreObject.AddCorrectScore(ref _score);

            ScoreObject.CleanScore(ref _score);

            Assert.That(_score.Correct, Is.EqualTo(0));
            Assert.That(_score.Wrong, Is.EqualTo(0));
        }
    }
}