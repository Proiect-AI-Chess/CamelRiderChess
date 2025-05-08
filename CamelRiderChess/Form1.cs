using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CamelRiderChess
{
    public partial class Form1 : Form
    {
        Board board;
        bool fullscreen;
        public Form1()
        {
            InitializeComponent();
            board = new Board();
            this.Controls.Add(board);
            DoubleBuffered = true;
            fullscreen = true;
           
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            board.resize_play_area(Width, Height);
        }

    }
}
