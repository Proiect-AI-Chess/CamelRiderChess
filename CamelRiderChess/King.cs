using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CamelRiderChess
{
    internal class King : Piece
    {
        public King(Board board, int x, int y, bool white) : base(board, x, y)
        {
            this.whitespace = false;
            this.white = white;
            if (white)
                this.Image = System.Drawing.Image.FromFile("images/kingWhite.png");
            else
                this.Image = System.Drawing.Image.FromFile("images/kingBlack.png");
        }

        void king(int x, int y, int a, int b)
        {
            if (y + b >= 0 && y + b < board.play_board.Length && x + a >= 0 && x + a < board.play_board.Length)
            {
                if (board.play_board[x + a][y + b].GetType().Name == "Piesa")
                {
                    board.play_board[x + a][y + b].make_movable();

                }
                if (board.play_board[x + a][y + b].white != this.white && !board.play_board[x + a][y + b].whitespace)
                    board.play_board[x + a][y + b].make_victim();
            }
        }

        public override void find_possible_moves()
        {
            board.reset_play_board();
            king(x, y, 1, 1);
            king(x, y, -1, 1);
            king(x, y, 1, -1);
            king(x, y, -1, -1);
            king(x, y, 0, 1);
            king(x, y, 1, 0);
            king(x, y, -1, 0);
            king(x, y, 0, -1);

        }
    }
}
