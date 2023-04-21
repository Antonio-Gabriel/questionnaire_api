namespace QuestionaryApp.Domain.ValueObject
{
    public struct ScoreObject
    {
        private static int _scoreObserver = 0;
        public static void AddCorrectScore(ref Score _score)
        {
            if (_scoreObserver < (int)QuestionnaireRangeLimits.Questions)
            {
                _score.Correct += 1;

                ++_scoreObserver;
            }
        }

        public static void AddWrongScore(ref Score _score)
        {
            if (_scoreObserver < (int)QuestionnaireRangeLimits.Questions)
            {
                _score.Wrong += 1;

                ++_scoreObserver;
            }
        }

        public static void CleanScore(ref Score _score)
        {
            _score.Wrong = 0;
            _scoreObserver = 0;
            _score.Correct = 0;
        }
    }
}