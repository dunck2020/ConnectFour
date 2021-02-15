using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectFour.Presentation;

namespace ConnectFour.Models
{
    //GameBoard
    public class GameBoard : ObservableObject
    {
        #region CONSTANTS AND ENUMS

        public const string PLAYER_RED = "Red";
        public const string PLAYER_BLACK = "Black";
        public const string PLAYER_NONE = "Azure";

        private const int MAX_NUM_ROWS = 6;
        private const int MAX_NUM_COLS = 7;

        public enum GameState
        {
            NewGame,
            RedTurn,
            BlackTurn,
            RedWin,
            BlackWin,
            TieGame
        }
        #endregion

        private string[][] _currentGameBoard;

        public string[][] CurrentGameBoard
        {
            get { return _currentGameBoard; }
            set 
            { 
                _currentGameBoard = value;
                OnPropertyChanged(nameof(CurrentGameBoard));
            }
        }
        public GameBoard()
        {
            CurrentGameBoard = new string[6][];
            CurrentGameBoard[0] = new string[7];
            CurrentGameBoard[1] = new string[7];
            CurrentGameBoard[2] = new string[7];
            CurrentGameBoard[3] = new string[7];
            CurrentGameBoard[4] = new string[7];
            CurrentGameBoard[5] = new string[7];

            InitializeGameboard();
        }

        /// <summary>
        /// sets current gameboard locations to none, initializes the game state to new game
        /// </summary>
        /// 
        public GameState CurrentRoundState { get; set; }
        public void InitializeGameboard()
        {
            CurrentRoundState = GameState.NewGame;

            for (int col = 0; col < MAX_NUM_COLS; col++)
            {
                for (int row = 0; row < MAX_NUM_ROWS; row++)
                {
                    CurrentGameBoard[row][col] = PLAYER_NONE;
                }
            }
        }

        /// <summary>
        /// used to place a color in a board location
        /// </summary>
        public struct GameBoardPostion
        {
            public int Row { get; set; }
            public int Column { get; set; }

            public GameBoardPostion(int row, int column)
            {
                Row = row;
                Column = column;
            }
        }

        /// <summary>
        /// determines if a position is available
        /// </summary>
        /// <param name="gameboardPosition"></param>
        /// <returns></returns>
        public bool IsPositionAvailable(GameBoardPostion gameboardPosition)
        {
            if (CurrentGameBoard[gameboardPosition.Row][gameboardPosition.Column] == "Azure")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// update game state for win or tie
        /// </summary>
        public void UpdateGameState()
        {
            if (ConnectFour(PLAYER_RED))
            {
                CurrentRoundState = GameState.RedWin;
            }
            else if (ConnectFour(PLAYER_BLACK))     
            {
                CurrentRoundState = GameState.BlackWin;

            }
            else if (TieGame())
            {
                CurrentRoundState = GameState.TieGame;
            }
        }

        /// <summary>
        /// check to see if there is a tie game
        /// </summary>
        /// <returns></returns>
        private bool TieGame()
        {
            for (int column = 0; column < 7; column++)
            {
                for (int row = 5; row >= 0; row--)
                {
                    if (CurrentGameBoard[row][column] == "Azure")
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// check to see if there is a winner
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private bool ConnectFour(string token)
        {
            //check each row to see if there is a horizontal winner   
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (CurrentGameBoard[row][col] == token &&
                        CurrentGameBoard[row][col + 1] == token &&
                        CurrentGameBoard[row][col + 2] == token &&
                        CurrentGameBoard[row][col + 3 ] == token)
                    {
                        return true;
                    }
                }
            }
            //check each column to see if there is a vertical winner
            for (int col = 0; col < 7; col++)
            {
                for (int row = 0; row < 3; row++)
                {                   
                    if (CurrentGameBoard[row][col] == token &&
                        CurrentGameBoard[row + 1][col] == token &&
                        CurrentGameBoard[row + 2][col] == token &&
                        CurrentGameBoard[row + 3][col] == token)
                    {
                        return true;
                    }
                }
            }
            //check each diag to see if there is a diagonal winner top down
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (CurrentGameBoard[row][col] == token &&
                        CurrentGameBoard[row + 1][col + 1] == token &&
                        CurrentGameBoard[row + 2][col + 2] == token &&
                        CurrentGameBoard[row + 3][col + 3] == token)
                    {
                        return true;
                    }
                }
            }
            //check diag bottom to top
            for (int row = 3; row < 6; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (CurrentGameBoard[row][col] == token &&
                        CurrentGameBoard[row - 1][col + 1] == token &&
                        CurrentGameBoard[row - 2][col + 2] == token &&
                        CurrentGameBoard[row - 3][col + 3] == token)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }
}
