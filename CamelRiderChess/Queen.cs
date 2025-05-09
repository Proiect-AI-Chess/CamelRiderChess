using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CamelRiderChess
{
    internal class Queen : Piece
    {
        public Queen(Board board, int x, int y, bool white) : base(board, x, y)
        {
            this.whitespace = false;
            this.white = white;
            if (white)
                this.Image = System.Drawing.Image.FromFile("images/queenWhite.png");
            else
                this.Image = System.Drawing.Image.FromFile("images/queenBlack.png");
        }

        public override void find_possible_moves()
        {
            int[] indexes = new int[2];
            indexes[0] = 1;
            indexes[1] = -1;
            int x_travel;
            int y_travel;
            int x_temp;
            int y_temp;
            for (int j = 0; j < 8; j++)
            {
                if (j < 4)
                {
                    x_travel = (int)(Math.Sin(j * 90 * Math.PI / 180) % 1.000000001);
                    y_travel = (int)(Math.Cos(j * 90 * Math.PI / 180) % 1.000000001);
                }
                else
                {
                    x_travel = indexes[(j - 4) % 2];
                    y_travel = indexes[(j - 4) / 2];
                }
                x_temp = x + x_travel;
                y_temp = y + y_travel;
                while (x_temp >= 0 && x_temp < board.play_board.Length && y_temp >= 0 && y_temp < board.play_board.Length)
                {
                    if (board.play_board[x_temp][y_temp].whitespace)
                        board.play_board[x_temp][y_temp].make_movable();
                    else
                    {
                        if (board.play_board[x_temp][y_temp].white != this.white)
                            board.play_board[x_temp][y_temp].make_victim();
                        break;
                    }
                    x_temp += x_travel;
                    y_temp += y_travel;
                }
            }
        }
    }
}
