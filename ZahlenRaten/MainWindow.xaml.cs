using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using ZahlenRaten.Model;

namespace ZahlenRaten
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly GameManager _gameManager;
        private readonly Stopwatch _stopwatch = new Stopwatch();


        public MainWindow()
        {
            InitializeComponent();
            _gameManager = new GameManager(1, 10);
            StartNewGame();
        }
        private void StartNewGame()
        {
            _gameManager.GenerateNewNumber();
            txtGuessNumber.IsEnabled = true;
            btnGuess.IsEnabled = true;
            btnRestartGame.Visibility = Visibility.Hidden;
            lblResult.Content = "";
            lbl_Title.Content = $"Bitte raten Sie eine Zahl zwischen {_gameManager.MinNumber} und {_gameManager.MaxNumber} ";
            lblAttemptCount.Content = "Versuche: 0";
            txtGuessNumber.Clear();
            txtGuessNumber.Focus();
            _stopwatch.Restart();
        }

        private void btnGuess_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtGuessNumber.Text, out int guessedNumber))
            {
                MessageBox.Show("Bitte geben Sie eine gültige Zahl ein.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var result = _gameManager.CheckGuess(guessedNumber);
            lblResult.Content = result.Message;
            lblResult.Foreground = new SolidColorBrush(result.IsCorrect ? Colors.Green : Colors.Red);
            lblAttemptCount.Content = $"Versuche: {_gameManager.AttemptCount}";

            if (result.IsCorrect)
            {
                txtGuessNumber.IsEnabled = false;
                btnGuess.IsEnabled = false;
                btnRestartGame.Visibility = Visibility.Visible;
                _stopwatch.Stop();
                lblResult.Content += $"\nBenötigte Zeit: {_stopwatch.Elapsed.Seconds} Sekunden";
            }
            else
            {
                txtGuessNumber.SelectAll();
                txtGuessNumber.Focus();
            }
        }

        private void btnRestartGame_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame();
        }

        private void btn_Range_Change_Click(object sender, RoutedEventArgs e)
        {

            if (int.TryParse(txt_Min_Number.Text, out int newMin) && int.TryParse(txt_Max_Number.Text, out int newMax) && newMin < newMax)
            {
                _gameManager.SetRange(newMin, newMax);
                MessageBox.Show($"Neuer Zahlenbereich: {newMin} - {newMax}", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                StartNewGame();
            }
            else
            {
                MessageBox.Show("Ungültiger Bereich! Der Min-Wert muss kleiner als der Max-Wert sein.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }



    }
}