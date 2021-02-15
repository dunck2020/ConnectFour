using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ConnectFour.Models;
using ConnectFour.Presentation;

namespace ConnectFour.Presentation
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : Window
    {
        GameViewModel _gameViewModel;
        public GameView(GameViewModel gameViewModel)
        {
            _gameViewModel = gameViewModel;
          
            InitializeComponent();
        }
        private void TokenDrop(object sender, RoutedEventArgs e)
        {
            Button tokenColumn = sender as Button;
            int column = int.Parse(tokenColumn.Tag.ToString());
            if (!_gameViewModel.ColumnCheck(column))
            {
                _gameViewModel.GameMessage = "That column is full";
            }

        }

        private void GameBoardButton_Click(object sender, RoutedEventArgs e)
        {
            Button gameBoardButtonPressed = sender as Button;

            switch (gameBoardButtonPressed.Name)
            {
                case "NewGame":
                    _gameViewModel.ButtonMenuCommand(gameBoardButtonPressed.Name);
                    break;
                case "ResetGame":
                    _gameViewModel.ButtonMenuCommand(gameBoardButtonPressed.Name);
                    break;
                case "Help":
                    _gameViewModel.ButtonMenuCommand(gameBoardButtonPressed.Name);
                    break;
                case "QuitGame":
                    Close();
                    break;
            }
        }
    }
}
