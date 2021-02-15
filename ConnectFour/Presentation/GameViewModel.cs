using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectFour.Models;

namespace ConnectFour.Presentation
{
    public class GameViewModel : ObservableObject
    {
        private Player _playerOne;
        private Player _playerTwo;
        private GameBoard _gameBoard;
        private string _gameMessage;

        public Player PlayerOne
        {
            get { return _playerOne; }
            set 
            { 
                _playerOne = value;
                OnPropertyChanged(nameof(PlayerOne));
            }
        }
        public Player PlayerTwo
        {
            get { return _playerTwo; }
            set 
            {
                _playerTwo = value;
                OnPropertyChanged(nameof(PlayerTwo));
            }
        }
        public GameBoard GameBoard
        {
            get { return _gameBoard; }
            set 
            {
                _gameBoard = value;
                OnPropertyChanged(nameof(GameBoard));
            }
        }
        public string GameMessage
        {
            get { return _gameMessage; }
            set 
            { 
                _gameMessage = value;
                OnPropertyChanged(nameof(GameMessage));
            }
        }

        public GameViewModel()
        {
            InitializeGame();
        }

        /// <summary>
        /// set players and game
        /// </summary>
        private void InitializeGame()
        {
            //set current players
            _playerOne = new Player();
            PlayerOne.Name = "Red";
            PlayerOne.Wins = 0;
            PlayerOne.Losses = 0;
            PlayerOne.Ties = 0;
            PlayerOne.Rank = "New Game";

            _playerTwo = new Player();
            PlayerTwo.Name = "Black";
            PlayerTwo.Wins = 0;
            PlayerTwo.Losses = 0;
            PlayerTwo.Ties = 0;
            PlayerTwo.Rank = "New Game";

            //initialize new game board
            _gameBoard = new GameBoard();
            GameMessage = "Player One Goes First!";
        }
        
        /// <summary>
        /// check to see if the columns are full before placing a token
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public bool ColumnCheck(int column)
        {
            for (int row = 5; row >= 0; row--)
            {
                if (GameBoard.CurrentGameBoard[row][column] == "Azure")
                {
                    //if column is open place token 
                    PlayerMove(row, column);

                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// place a token on the game board
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        public void PlayerMove(int row, int column)
        {
            if (_gameBoard.IsPositionAvailable(new GameBoard.GameBoardPostion(row,column)))
            {
                if (_gameBoard.CurrentRoundState == GameBoard.GameState.RedTurn || _gameBoard.CurrentRoundState == GameBoard.GameState.NewGame)
                {
                    GameBoard.CurrentGameBoard[row][column] = GameBoard.PLAYER_RED;
                    OnPropertyChanged(nameof(GameBoard));
                    
                    _gameBoard.CurrentRoundState = GameBoard.GameState.BlackTurn;
                    GameMessage = "Player Two's Turn";
                }
                else
                {
                    GameBoard.CurrentGameBoard[row][column] = GameBoard.PLAYER_BLACK;
                    OnPropertyChanged(nameof(GameBoard));
                    _gameBoard.CurrentRoundState = GameBoard.GameState.RedTurn;
                    GameMessage = "Player One's Turn";
                }
                CheckGameState();
                
            }
        }

        /// <summary>
        /// update the game state based on current round information
        /// </summary>
        public void CheckGameState()
        {
            _gameBoard.UpdateGameState();

            if (_gameBoard.CurrentRoundState == GameBoard.GameState.RedWin)
            {
                PlayerOne.Wins++;
                PlayerTwo.Losses++;
                GameMessage = "Red Wins!";
            }
            else if (_gameBoard.CurrentRoundState == GameBoard.GameState.BlackWin)
            {
                PlayerTwo.Wins++;
                PlayerOne.Losses++;
                GameMessage = "Black Wins!";
            }
            else if (_gameBoard.CurrentRoundState == GameBoard.GameState.TieGame)
            {
                PlayerOne.Ties++;
                PlayerTwo.Ties++;
                GameMessage = "It's a Tie!";
            }


            if (PlayerOne.Wins > PlayerTwo.Wins)
            {
                PlayerOne.Rank = "Champion";
                PlayerTwo.Rank = "Loser";
            }
            else if (PlayerTwo.Wins > PlayerOne.Wins)
            {
                PlayerTwo.Rank = "Champion";
                PlayerOne.Rank = "Loser";
            }
            else
            {
                PlayerTwo.Rank = "Tied up";
                PlayerOne.Rank = "Tied up";
            }
                
        }

        /// <summary>
        /// button menu commands from UI
        /// </summary>
        /// <param name="buttonName"></param>
        public void ButtonMenuCommand(string buttonName)
        {
            switch (buttonName)
            {
                case "NewGame":
                    _gameBoard.InitializeGameboard();
                    OnPropertyChanged(nameof(GameBoard));
                    UpdatePlayerStats();

                    break;
                case "ResetGame":
                    _gameBoard.InitializeGameboard();
                    OnPropertyChanged(nameof(GameBoard));
                    _gameBoard.CurrentRoundState = GameBoard.GameState.RedTurn;
                    break;
                case "Help":
                    HelpWindow helpWindow = new HelpWindow();
                    helpWindow.ShowDialog();
                    break;
            }

        }

        private void UpdatePlayerStats()
        {
            PlayerOne.Wins = 0;
            PlayerTwo.Wins = 0;
            PlayerOne.Losses = 0;
            PlayerTwo.Losses = 0;
            PlayerOne.Ties = 0;
            PlayerTwo.Ties = 0;
            PlayerOne.Rank = "New game";
            PlayerTwo.Rank = "New game";
        }
    }
}
