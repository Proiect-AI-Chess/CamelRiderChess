using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CamelRiderChess
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
    }
}
