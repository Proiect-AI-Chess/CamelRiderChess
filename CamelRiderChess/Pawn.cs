using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CamelRiderChess
{
    internal class Pawn : Piece
    {
        public Pawn(Board board, int x, int y, bool white) : base(board, x, y)
        {
            no_moves = 0;
            this.white = white;
            if (white)
                this.Image = System.Drawing.Image.FromFile("images/pawnWhite.png");
            else
                this.Image = System.Drawing.Image.FromFile("images/pawnBlack.png");
            this.whitespace = false;
        }

        /*public override void find_possible_moves() //varianta veche
        {
            board.reset_play_board();
            if (no_moves == 0)
            {
                if (white)
                {
                    if (y - 1 >= 0 && board.play_board[x][y - 1].GetType().Name == "Piece")
                        board.play_board[x][y - 1].make_movable();

                    if (y - 2 >= 0 && board.play_board[x][y - 2].GetType().Name == "Piece")
                        board.play_board[x][y - 2].make_movable();

                }
                else
                {
                    if (y + 1 < board.play_board.Length && board.play_board[x][y + 1].GetType().Name == "Piece")
                        board.play_board[x][y + 1].make_movable();
                    if (y + 2 < board.play_board.Length && board.play_board[x][y + 2].GetType().Name == "Piece")
                        board.play_board[x][y + 2].make_movable();
                }

            }
            else
            {
                if (white)
                {
                    if (board.play_board[x][y - 1].GetType().Name == "Piece")
                        board.play_board[x][y - 1].make_movable();
                }
                else
                {
                    if (board.play_board[x][y + 1].GetType().Name == "Piece")
                        board.play_board[x][y + 1].make_movable();
                }

            }
        }*/
        public override void find_possible_moves()
        {
            board.reset_play_board();

            if (no_moves == 0)
            {
                if (white)
                {
                    if (y - 1 >= 0 && board.play_board[x][y - 1].whitespace)
                        board.play_board[x][y - 1].make_movable();

                    if (y - 2 >= 0 && board.play_board[x][y - 1].whitespace && board.play_board[x][y - 2].whitespace)
                        board.play_board[x][y - 2].make_movable();
                }
                else
                {
                    if (y + 1 < board.play_board.Length && board.play_board[x][y + 1].whitespace)
                        board.play_board[x][y + 1].make_movable();

                    if (y + 2 < board.play_board.Length &&
                        board.play_board[x][y + 1].whitespace && board.play_board[x][y + 2].whitespace)
                        board.play_board[x][y + 2].make_movable();
                }
            }
            else
            {
                if (white)
                {
                    if (y - 1 >= 0 && board.play_board[x][y - 1].whitespace)
                        board.play_board[x][y - 1].make_movable();
                }
                else
                {
                    if (y + 1 < board.play_board.Length && board.play_board[x][y + 1].whitespace)
                        board.play_board[x][y + 1].make_movable();
                }
            }
        }
        public override void find_possible_victims()
        {
            if (white)
            {
                if (y - 1 >= 0 && x - 1 >= 0 && board.play_board[x - 1][y - 1].white != this.white && !board.play_board[x - 1][y - 1].whitespace)
                    board.play_board[x - 1][y - 1].make_victim();
                if (y - 1 >= 0 && x + 1 < board.play_board.Length && board.play_board[x + 1][y - 1].white != this.white && !board.play_board[x + 1][y - 1].whitespace)
                    board.play_board[x + 1][y - 1].make_victim();
            }
            else
            {
                if (y + 1 < board.play_board.Length && x - 1 >= 0 && board.play_board[x - 1][y + 1].white != this.white && !board.play_board[x - 1][y + 1].whitespace)
                    board.play_board[x - 1][y + 1].make_victim();
                if (y + 1 < board.play_board.Length && x + 1 < board.play_board.Length && board.play_board[x + 1][y + 1].white != this.white && !board.play_board[x + 1][y + 1].whitespace)
                    board.play_board[x + 1][y + 1].make_victim();
            }
        }

    }
}
