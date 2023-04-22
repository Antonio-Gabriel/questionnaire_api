using NUnit.Framework;

namespace Tests.Integration
{
    public class ScoreRepositoryTest
    {
        private readonly ScoreRepository _scoreRepository;

        public ScoreRepositoryTest()
        {
            var _context = DataContextFactory.create();
            _scoreRepository = new ScoreRepository(_context);
        }

        [Test]
        public async Task ReturnScoreLists()
        {
            var scores = await _scoreRepository.GetAll();
            Assert.IsNotNull(scores);
        }

        [Test]
        public async Task ReturnTrueAfterInsertScoreFromDb()
        {
            var score = new Score
            {
                Correct = 0,
                Wrong = 0,
                UserId = new Guid("782055fc-b8b2-4d77-bc0c-dd2c98f91c0d"),
            };

            ScoreObject.AddWrongScore(ref score);
            ScoreObject.AddCorrectScore(ref score);

            bool result = await _scoreRepository.Create(score);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task ReturnTrueAfterUpdateScoreFromDb()
        {
            // Don't forget to change this ID for an existent
            var scoreId = new Guid("540bfaad-3599-4948-89e0-35d5007893c4");

            var score = await _scoreRepository.Get(scoreId);
            score.Correct = 6;
            score.Wrong = 4;
            score.LastModified = DateTime.UtcNow;

            bool result = await _scoreRepository.Update(score);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task ReturnTrueAfterDeleteScoreFromDb()
        {
            // Don't forget to change this ID for an existent
            var scoreId = new Guid("12a3c73f-b63c-4e63-b2b5-1a4c457d3194");
            var score = await _scoreRepository.Get(scoreId);

            bool result = await _scoreRepository.Delete(score);

            Assert.IsTrue(result);
        }
    }
}