using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZahlenRaten.Interface;

namespace ZahlenRaten.Model
{
    public class GameManager
    {
        private readonly INumberGenerator _numberGenerator;
        public int TargetNumber { get; private set; }
        public int AttemptCount { get; private set; } = 0;
        public int MinNumber { get; private set; }
        public int MaxNumber { get; private set; }
        private Stopwatch _stopwatch;

        public GameManager(INumberGenerator numberGenerator, int min = 1, int max = 10)
        {
            _numberGenerator = numberGenerator;
            MinNumber = min;
            MaxNumber = max;
            RestartGame();
        }

        public void RestartGame()
        {
            TargetNumber = _numberGenerator.GenerateNumber(MinNumber, MaxNumber);
            AttemptCount = 0;
            _stopwatch = Stopwatch.StartNew();
        }

        public string CheckGuess(int guess)
        {
            AttemptCount++;
            if (guess < TargetNumber) return "Zu niedrig! Versuche es erneut.";
            if (guess > TargetNumber) return "Zu hoch! Versuche es erneut.";
            _stopwatch.Stop();
            return $"Richtig! Du hast {AttemptCount} Versuche gebraucht in {_stopwatch.Elapsed.Seconds} Sekunden.";
        }

        public void UpdateRange(int min, int max)
        {
            if (min >= max) throw new ArgumentException("Min muss kleiner als Max sein.");
            MinNumber = min;
            MaxNumber = max;
            RestartGame();
        }
    }
}
