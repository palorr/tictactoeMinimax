using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tictactoe
{
    class Board
    {
        //Variables for the Boards values
        public const int X = 1;
        public const int O = -1;
        public const int EMPTY = 0;

        //Immediate move that lead to this board
        private Move lastMove;

        /* Variable containing who played last; whose turn resulted in this board
         * Even a new Board has lastLetterPlayed value; it denotes which player will play first
         */
        private int lastLetterPlayed;

        private int[,] gameBoard;

        public Board()
        {
            lastMove = new Move();
            lastLetterPlayed = O;
            gameBoard = new int[3,3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    gameBoard[i,j] = EMPTY;
                }
            }
        }

        public Board(Board board)
        {
            lastMove = board.lastMove;
            lastLetterPlayed = board.lastLetterPlayed;
            gameBoard = new int[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    gameBoard[i,j] = board.gameBoard[i,j];
                }
            }
        }

        public Move getLastMove()
        {
            return lastMove;
        }

        public int getLastLetterPlayed()
        {
            return lastLetterPlayed;
        }

        public int[,] getGameBoard()
        {
            return gameBoard;
        }

        public void setLastMove(Move lastMove)
        {
            this.lastMove.setRow(lastMove.getRow());
            this.lastMove.setCol(lastMove.getCol());
            this.lastMove.setValue(lastMove.getValue());
        }

        public void setLastLetterPlayed(int lastLetterPlayed)
        {
            this.lastLetterPlayed = lastLetterPlayed;
        }

        public void setGameBoard(int[,] gameBoard)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    this.gameBoard[i,j] = gameBoard[i,j];
                }
            }
        }

        //Make a move; it places a letter in the board
        public void makeMove(int row, int col, int letter)
        {
            gameBoard[row,col] = letter;
            lastMove = new Move(row, col);
            lastLetterPlayed = letter;
        }

        //Checks whether a move is valid; whether a square is empty
        public bool isValidMove(int row, int col)
        {
            if ((row == -1) || (col == -1) || (row > 2) || (col > 2))
            {
                return false;
            }
            if (gameBoard[row,col] != EMPTY)
            {
                return false;
            }
            return true;
        }

        /* Generates the children of the state
         * Any square in the board that is empty results to a child
         */
        public List<Board> getChildren(int letter)
        {
            List<Board> children = new List<Board>();
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (isValidMove(row, col))
                    {
                        Board child = new Board(this);
                        child.makeMove(row, col, letter);
                        children.Add(child);
                    }
                }
            }
            return children;
        }

        /*
         * The heuristic we use to evaluate is
         * the number our almost complete tic-tac-toes (having 2 letter in a row, column or diagonal)
         * minus the number of the opponent's almost complete tic-tac-toes
         * Special case: if a complete tic-tac-toe is present it counts as ten
         */
        public int evaluate()
        {
            int Xlines = 0;
            int Olines = 0;
            int sum;

            //Checking rows
            for (int row = 0; row < 3; row++)
            {
                sum = gameBoard[row,0] + gameBoard[row,1] + gameBoard[row,2];
                if (sum == 3)
                {
                    Xlines = Xlines + 10;
                }
                else if (sum == 2)
                {
                    Xlines++;
                }
                else if (sum == -3)
                {
                    Olines = Olines + 10;
                }
                else if (sum == -2)
                {
                    Olines++;
                }
            }

            //Checking columns
            for (int col = 0; col < 3; col++)
            {
                sum = gameBoard[0,col] + gameBoard[1,col] + gameBoard[2,col];
                if (sum == 3)
                {
                    Xlines = Xlines + 10;
                }
                else if (sum == 2)
                {
                    Xlines++;
                }
                else if (sum == -3)
                {
                    Olines = Olines + 10;
                }
                else if (sum == -2)
                {
                    Olines++;
                }
            }

            //Checking  diagonals
            sum = gameBoard[0,0] + gameBoard[1,1] + gameBoard[2,2];
            if (sum == 3)
            {
                Xlines = Xlines + 10;
            }
            else if (sum == 2)
            {
                Xlines++;
            }
            else if (sum == -3)
            {
                Olines = Olines + 10;
            }
            else if (sum == -2)
            {
                Olines++;
            }
            sum = gameBoard[0,2] + gameBoard[1,1] + gameBoard[2,0];
            if (sum == 3)
            {
                Xlines = Xlines + 10;
            }
            else if (sum == 2)
            {
                Xlines++;
            }
            else if (sum == -3)
            {
                Olines = Olines + 10;
            }
            else if (sum == -2)
            {
                Olines++;
            }

            return Xlines - Olines;
        }

        /*
         * A state is terminal if there is a tic-tac-toe
         * or no empty tiles are available
         */
        public bool isTerminal()
        {
            //Checking if there is a horizontal tic-tac-toe
            for (int row = 0; row < 3; row++)
            {
                if ((gameBoard[row,0] == gameBoard[row,1]) && (gameBoard[row,1] == gameBoard[row,2]) && (gameBoard[row,0] != EMPTY))
                {
                    return true;
                }
            }

            //Checking if there is a vertical tic-tac-toe
            for (int col = 0; col < 3; col++)
            {
                if ((gameBoard[0,col] == gameBoard[1,col]) && (gameBoard[1,col] == gameBoard[2,col]) && (gameBoard[0,col] != EMPTY))
                {
                    return true;
                }
            }

            //Checking if there is a diagonal tic-tac-toe
            if ((gameBoard[0,0] == gameBoard[1,1]) && (gameBoard[1,1] == gameBoard[2,2]) && (gameBoard[1,1] != EMPTY))
            {
                return true;
            }
            if ((gameBoard[0,2] == gameBoard[1,1]) && (gameBoard[1,1] == gameBoard[2,0]) && (gameBoard[1,1] != EMPTY))
            {
                return true;
            }

            //Checking if there is at least one empty tile
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (gameBoard[row,col] == EMPTY)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        //Prints the board
        public void print()
        {
            Console.WriteLine("*********");
            for (int row = 0; row < 3; row++)
            {
                Console.Write("* ");
                for (int col = 0; col < 3; col++)
                {
                    switch (gameBoard[row,col])
                    {
                        case X:
                            Console.Write("X ");
                            break;
                        case O:
                            Console.Write("O ");
                            break;
                        case EMPTY:
                            Console.Write("- ");
                            break;
                        default:
                            break;
                    }
                }
                Console.WriteLine("*");
            }
            Console.WriteLine("*********");
        }
    }
}
