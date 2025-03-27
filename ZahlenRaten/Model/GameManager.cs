namespace ZahlenRaten.Model
{
    public class GameManager
    {
        private readonly Random _random = new Random();
        public int MinNumber { get; private set; }
        public int MaxNumber { get; private set; }
        public int AttemptCount { get; private set; }
        private int _generatedNumber;

        public GameManager(int min, int max)
        {
            MinNumber = min;
            MaxNumber = max;
        }

        public void GenerateNewNumber()
        {
            _generatedNumber = _random.Next(MinNumber, MaxNumber + 1);
            AttemptCount = 0;
        }

        public void SetRange(int min, int max)
        {
            MinNumber = min;
            MaxNumber = max;
        }

        public GuessResult CheckGuess(int guess)
        {
            AttemptCount++;
            if (guess < _generatedNumber)
                return new GuessResult("Die gesuchte Zahl ist größer! Versuche es erneut.", false);
            if (guess > _generatedNumber)
                return new GuessResult("Die gesuchte Zahl ist kleiner! Versuche es erneut.", false);
            return new GuessResult("Richtig! Sie haben die Zahl erraten!", true);
        }
    }
}
