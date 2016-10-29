using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tictactoe
{
    class GamePlayer
    {
        //Variable that holds the maximum depth the MiniMax algorithm will reach for this player
        private int maxDepth;
        //Variable that holds which letter this player controls
        private int playerLetter;

        public GamePlayer()
        {
            maxDepth = 2;
            playerLetter = Board.X;
        } //ctor

        public GamePlayer(int maxDepth, int playerLetter)
        {
            this.maxDepth = maxDepth;
            this.playerLetter = playerLetter;
        } //ctor

        //Initiates the MiniMax algorithm
        public Move MiniMax(Board board)
        {
            //If the X plays then it wants to MAXimize the heuristics value
            if (playerLetter == Board.X)
            {
                return max(new Board(board), 0);
            }
            //If the O plays then it wants to MINimize the heuristics value
            else
            {
                return min(new Board(board), 0);
            }
        }

        // The max and min functions are called interchangingly, one after another until a max depth is reached
        public Move max(Board board, int depth)
        {
            Random r = new Random();

            /* If MAX is called on a state that is terminal or after a maximum depth is reached,
             * then a heuristic is calculated on the state and the move returned.
             */
            if ((board.isTerminal()) || (depth == maxDepth))
            {
                Move lastMove = new Move(board.getLastMove().getRow(), board.getLastMove().getCol(), board.evaluate());
                return lastMove;
            }
            //The children-moves of the state are calculated
            List<Board> children = new List<Board>(board.getChildren(Board.X));
            Move maxMove = new Move(Int32.MinValue);
            foreach (Board child in children)
            {
                //And for each child min is called, on a lower depth
                Move move = min(child, depth + 1);
                //The child-move with the greatest value is selected and returned by max
                if (move.getValue() >= maxMove.getValue())
                {
                    if ((move.getValue() == maxMove.getValue()))
                    {
                        //If the heuristic has the same value then we randomly choose one of the two moves
                        if (r.Next(2) == 0)
                        {
                            maxMove.setRow(child.getLastMove().getRow());
                            maxMove.setCol(child.getLastMove().getCol());
                            maxMove.setValue(move.getValue());
                        }
                    }
                    else
                    {
                        maxMove.setRow(child.getLastMove().getRow());
                        maxMove.setCol(child.getLastMove().getCol());
                        maxMove.setValue(move.getValue());
                    }
                }
            }
            return maxMove;
        }

        //Min works similarly to max
        public Move min(Board board, int depth)
        {
            Random r = new Random();

            if ((board.isTerminal()) || (depth == maxDepth))
            {
                Move lastMove = new Move(board.getLastMove().getRow(), board.getLastMove().getCol(), board.evaluate());
                return lastMove;
            }
            List<Board> children = new List<Board>(board.getChildren(Board.O));
            Move minMove = new Move(Int32.MaxValue);
            foreach (Board child in children)
            {
                Move move = max(child, depth + 1);
                if (move.getValue() <= minMove.getValue())
                {
                    if ((move.getValue() == minMove.getValue()))
                    {
                        if (r.Next(2) == 0)
                        {
                            minMove.setRow(child.getLastMove().getRow());
                            minMove.setCol(child.getLastMove().getCol());
                            minMove.setValue(move.getValue());
                        }
                    }
                    else
                    {
                        minMove.setRow(child.getLastMove().getRow());
                        minMove.setCol(child.getLastMove().getCol());
                        minMove.setValue(move.getValue());
                    }
                }
            }
            return minMove;
        }
    }
}
