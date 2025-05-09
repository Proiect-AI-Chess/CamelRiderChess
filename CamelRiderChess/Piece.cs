using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CamelRiderChess
{
    public class Piece : System.Windows.Forms.Button
    {
        public bool white;
        public int x;
        public int y;
        public bool whitespace;
        public Board board;
        public bool movable;
        public bool takeable;
        public int no_moves;


        public Piece(Board board, int x, int y)
        {
            square(x, y);
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Margin = new Padding(0);
            this.Padding = new Padding(0);
            this.Dock = DockStyle.Fill;
            this.Margin = new Padding(0);
            this.Padding = new Padding(0);
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;

            this.board = board;
            this.x = x;
            this.y = y;
            this.whitespace = true;
        }

        void square(int x, int y)
        {
            if ((x + y) % 2 == 0)
                this.BackColor = Color.Beige;
            else
                this.BackColor = Color.SaddleBrown;
        }

        public void make_movable()
        {
            this.BackColor = System.Drawing.Color.FromArgb(100, 64, 224, 208);
            this.movable = true;
        }
        public void make_unmovable(int x, int y)
        {
            square(x, y);
            this.movable = false;
        }
        public void make_untakeable(int x, int y)
        {
            square(x, y);
            this.takeable = false;
        }
        public void make_victim()
        {

            this.BackColor = System.Drawing.Color.FromArgb(100, 150, 0, 0);
            this.takeable = true;

        }

        public virtual void find_possible_moves() { /*.....*/ }

    }
}
