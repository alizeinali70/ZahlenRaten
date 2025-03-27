namespace ZahlenRaten.Model
{
    public class GuessResult
    {
        public string Message { get; }
        public bool IsCorrect { get; }

        public GuessResult(string message, bool isCorrect)
        {
            Message = message;
            IsCorrect = isCorrect;
        }
    }
}
