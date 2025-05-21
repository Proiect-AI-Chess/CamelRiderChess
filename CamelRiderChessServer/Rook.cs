using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CamelRiderChessServer
{
    internal class Rook : Piece
    {
        public Rook(Board board, int x, int y, bool white) : base(board, x, y)
        {
            this.white = white;
            this.whitespace = false;
            if (white)
                this.Image = System.Drawing.Image.FromFile("images/rookWhite.png");
            else
                this.Image = System.Drawing.Image.FromFile("images/rookBlack.png");
        }
        public override void find_possible_moves()
        {
            int x_travel;
            int y_travel;
            int x_temp;
            int y_temp;
            for (int j = 0; j < 4; j++)
            {
                x_travel = (int)(Math.Sin(j * 90 * Math.PI / 180) % 1.000000001);
                y_travel = (int)(Math.Cos(j * 90 * Math.PI / 180) % 1.000000001);
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
            if (targetX != this.x && targetY != this.y)
                return false; // mutare invalidă pentru tură

            int stepX = targetX == this.x ? 0 : (targetX > this.x ? 1 : -1);
            int stepY = targetY == this.y ? 0 : (targetY > this.y ? 1 : -1);

            int currentX = this.x + stepX;
            int currentY = this.y + stepY;

            while (currentX != targetX || currentY != targetY)
            {
                if (!board.play_board[currentX][currentY].whitespace) // Calea este blocată
                    return false;
                currentX += stepX;
                currentY += stepY;
            }

            // Poate ataca dacă ținta este ocupată de piesă adversă sau liberă (captură sau mutare)
            var targetPiece = board.play_board[targetX][targetY];
            return targetPiece.whitespace || targetPiece.white != this.white;
        }
    }
}