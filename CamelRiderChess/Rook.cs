using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CamelRiderChess
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
    }
}