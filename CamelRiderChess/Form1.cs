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
            InitializeComponent(); // Inițializare formular
            board = new Board();
            this.Controls.Add(board);
            DoubleBuffered = true;
            fullscreen = true;
            this.SizeChanged += new EventHandler(Form1_SizeChanged); // Înregistrare eveniment

            // Calculăm dimensiunile ferestrei pentru a se potrivi cu tabla
            int cellSize = 68; // Dimensiunea unei celule (68x68)
            int boardSize = cellSize * 10; // Tabla este 10x10

            // Setăm dimensiunea ferestrei pentru a se potrivi cu tabla
            this.Width = boardSize + 40; // Lăsăm un mic spațiu pentru margini
            this.Height = boardSize + 60; // Lăsăm un mic spațiu pentru margini (header, etc.)

            // Inițializăm tabla de șah
            board.resize_play_area(this.Width, this.Height);
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            board.resize_play_area(Width, Height);
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            board.resize_play_area(this.Width, this.Height);
        }
        

    }
}
