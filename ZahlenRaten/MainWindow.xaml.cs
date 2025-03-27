using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using ZahlenRaten.Model;
using ZahlenRaten.Repositories;

namespace ZahlenRaten
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameManager _gameManager;



        public MainWindow()
        {
            InitializeComponent();
            _gameManager = new GameManager(new RandomNumberGenerator());
            UpdateUI();
        }
        private void UpdateUI()
        {
            lbl_Title.Content = $"Bitte rate eine Zahl zwischen {_gameManager.MinNumber} und {_gameManager.MaxNumber}";
            lblAttemptCount.Content = "Versuche: 0";
            btnRestartGame.Visibility = Visibility.Hidden;
            txtGuessNumber.Clear();
            txtGuessNumber.Focus();
        }

        private void btnGuess_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtGuessNumber.Text, out int guessedNumber))
            {
                MessageBox.Show("Bitte eine gültige Zahl eingeben.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string result = _gameManager.CheckGuess(guessedNumber);
            lblResult.Content = result;
            lblAttemptCount.Content = $"Versuche: {_gameManager.AttemptCount}";

            if (result.StartsWith("Richtig"))
            {
                lblResult.Foreground = new SolidColorBrush(Colors.Green);
                btnRestartGame.Visibility = Visibility.Visible;
            }
            else
            {
                lblResult.Foreground = new SolidColorBrush(Colors.Red);
                txtGuessNumber.SelectAll();
                txtGuessNumber.Focus();
            }
        }

        private void btnRestartGame_Click(object sender, RoutedEventArgs e)
        {
            _gameManager.RestartGame();
            UpdateUI();
        }

        private void btn_Range_Change_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txt_Min_Number.Text, out int newMin) && int.TryParse(txt_Max_Number.Text, out int newMax))
            {
                try
                {
                    _gameManager.UpdateRange(newMin, newMax);
                    MessageBox.Show($"Neue Range: {newMin} - {newMax}", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    UpdateUI();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Ungültige Eingabe! Bitte Zahlen eingeben.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}