using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CamelRiderChess
{
    internal class Knight : Piece
    {
        public Knight(Board board, int x, int y, bool white) : base(board, x, y)
        {
            this.whitespace = false;
            this.white = white;
            if (white)
                this.Image = System.Drawing.Image.FromFile("images/knightWhite.png");
            else
                this.Image = System.Drawing.Image.FromFile("images/knightBlack.png");
        }

        public override void find_possible_moves()
        {
            board.reset_play_board();

            if (y - 2 >= 0 && y - 2 < board.play_board.Length && x - 1 >= 0 && x - 1 < board.play_board.Length)
            {
                if (board.play_board[x - 1][y - 2].GetType().Name == "Piece")
                    board.play_board[x - 1][y - 2].make_movable();
                if (board.play_board[x - 1][y - 2].white != this.white && !board.play_board[x - 1][y - 2].whitespace)
                    board.play_board[x - 1][y - 2].make_victim();
            }
            if (y - 2 >= 0 && y - 2 < board.play_board.Length && x + 1 >= 0 && x + 1 < board.play_board.Length)
            {
                if (board.play_board[x + 1][y - 2].GetType().Name == "Piece")
                    board.play_board[x + 1][y - 2].make_movable();
                if (board.play_board[x + 1][y - 2].white != this.white && !board.play_board[x + 1][y - 2].whitespace)
                    board.play_board[x + 1][y - 2].make_victim();
            }
            if (y + 2 >= 0 && y + 2 < board.play_board.Length && x - 1 >= 0 && x - 1 < board.play_board.Length)
            {
                if (board.play_board[x - 1][y + 2].GetType().Name == "Piece")
                    board.play_board[x - 1][y + 2].make_movable();
                if (board.play_board[x - 1][y + 2].white != this.white && !board.play_board[x - 1][y + 2].whitespace)
                    board.play_board[x - 1][y + 2].make_victim();
            }
            if (y + 2 >= 0 && y + 2 < board.play_board.Length && x + 1 >= 0 && x + 1 < board.play_board.Length)
            {
                if (board.play_board[x + 1][y + 2].GetType().Name == "Piece")
                    board.play_board[x + 1][y + 2].make_movable();
                if (board.play_board[x + 1][y + 2].white != this.white && !board.play_board[x + 1][y + 2].whitespace)
                    board.play_board[x + 1][y + 2].make_victim();
            }


            if (y - 1 >= 0 && y - 1 < board.play_board.Length && x - 2 >= 0 && x - 2 < board.play_board.Length)
            {
                if (board.play_board[x - 2][y - 1].GetType().Name == "Piece")
                    board.play_board[x - 2][y - 1].make_movable();
                if (board.play_board[x - 2][y - 1].white != this.white && !board.play_board[x - 2][y - 1].whitespace)
                    board.play_board[x - 2][y - 1].make_victim();
            }
            if (y - 1 >= 0 && y - 1 < board.play_board.Length && x + 2 >= 0 && x + 2 < board.play_board.Length)
            {
                if (board.play_board[x + 2][y - 1].GetType().Name == "Piece")
                    board.play_board[x + 2][y - 1].make_movable();
                if (board.play_board[x + 2][y - 1].white != this.white && !board.play_board[x + 2][y - 1].whitespace)
                    board.play_board[x + 2][y - 1].make_victim();
            }
            if (y + 1 >= 0 && y + 1 < board.play_board.Length && x - 2 >= 0 && x - 2 < board.play_board.Length)
            {
                if (board.play_board[x - 2][y + 1].GetType().Name == "Piece")
                    board.play_board[x - 2][y + 1].make_movable();
                if (board.play_board[x - 2][y + 1].white != this.white && !board.play_board[x - 2][y + 1].whitespace)
                    board.play_board[x - 2][y + 1].make_victim();
            }
            if (y + 1 >= 0 && y + 1 < board.play_board.Length && x + 2 >= 0 && x + 2 < board.play_board.Length)
            {
                if (board.play_board[x + 2][y + 1].GetType().Name == "Piece")
                    board.play_board[x + 2][y + 1].make_movable();
                if (board.play_board[x + 2][y + 1].white != this.white && !board.play_board[x + 2][y + 1].whitespace)
                    board.play_board[x + 2][y + 1].make_victim();
            }
        }
        public override bool CanAttackPosition(int targetX, int targetY)
        {
            int dx = Math.Abs(targetX - this.x);
            int dy = Math.Abs(targetY - this.y);

            return (dx == 2 && dy == 1) || (dx == 1 && dy == 2);
        }

    }
}
