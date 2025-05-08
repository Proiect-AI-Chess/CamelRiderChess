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
            this.white = white;
            if (white)
                this.Image = System.Drawing.Image.FromFile("images/pawnWhite.png");
            else
                this.Image = System.Drawing.Image.FromFile("images/pawnBlack.png");
            this.whitespace = false;
        }
    }
}
