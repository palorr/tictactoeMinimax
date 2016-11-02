using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tictactoe
{
    class Program
    {
        static void Main(string[] args)
        {
            //We create the players and the board
            //MaxDepth for the MiniMax algorithm is set to 2; feel free to change the values
            GamePlayer XPlayer = new GamePlayer(9, Board.X);
            GamePlayer OPlayer = new GamePlayer(9, Board.O);
            Board board = new Board();

            //Put this out of comments for the O to play first
            //board.setLastLetterPlayed(Board.X);

            board.print();
            //While the game has not finished
            while (!board.isTerminal())
            {
                Console.WriteLine();
                switch (board.getLastLetterPlayed())
                {
                    //If X played last, then 0 plays now
                    case Board.X:
                        Console.WriteLine("O moves");
                        Move OMove = OPlayer.MiniMax(board);
                        board.makeMove(OMove.getRow(), OMove.getCol(), Board.O);
                        break;
                    //If O played last, then X plays now
                    case Board.O:
                        Console.WriteLine("X moves");
                        Move XMove = XPlayer.MiniMax(board);
                        board.makeMove(XMove.getRow(), XMove.getCol(), Board.X);
                        break;
                    default:
                        break;
                }
                board.print();
                
            }
            Console.Read();
        }
    }
}
