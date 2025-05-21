using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CamelRiderChessServer
{
    internal class Bishop : Piece
    {
        public Bishop(Board board, int x, int y, bool white) : base(board, x, y)
        {
            this.whitespace = false;
            this.white = white;
            if (white)
                this.Image = System.Drawing.Image.FromFile("images/bishopWhite.png");
            else
                this.Image = System.Drawing.Image.FromFile("images/bishopBlack.png");
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
            for (int j = 0; j < 4; j++)
            {
                x_travel = indexes[j % 2];
                y_travel = indexes[j / 2];
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
        public override bool CanAttackPosition(int targetX, int targetY)
        {
            int dx = targetX - this.x;
            int dy = targetY - this.y;

            if (Math.Abs(dx) != Math.Abs(dy))
                return false; // Nu e diagonală

            int stepX = dx > 0 ? 1 : -1;
            int stepY = dy > 0 ? 1 : -1;

            int currentX = this.x + stepX;
            int currentY = this.y + stepY;

            while (currentX != targetX && currentY != targetY)
            {
                if (!board.play_board[currentX][currentY].whitespace)
                    return false;
                currentX += stepX;
                currentY += stepY;
            }

            var targetPiece = board.play_board[targetX][targetY];
            return targetPiece.whitespace || targetPiece.white != this.white;
        }
    }
}

