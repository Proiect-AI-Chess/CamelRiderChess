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
        public bool gameWithAI;
        private AiPlayer aiPlayer;
        public Form1(bool gameWithAI = false)
        {
            InitializeComponent(); // Inițializare formular
            board = new Board();
            this.Controls.Add(board);
            DoubleBuffered = true;
            fullscreen = true;
            this.SizeChanged += new EventHandler(Form1_SizeChanged); // Înregistrare eveniment
            board.menuForm = (Menu)Application.OpenForms["Menu"]; // sau o referință salvată
            board.gameWithAI = gameWithAI;
            board.player_turn = true;   // începe albul
            board.isWhitePlayer = true;
            if (gameWithAI)                   
            {
                board.aiPlayer = new AiPlayer(board);  
            }

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

        public void UpdateChessBoard(string move)
        {
            var parts = move.Split('|');
            if (parts.Length < 3) return;

            var from = parts[1].Split(',');
            var to = parts[2].Split(',');

            int fromX = int.Parse(from[0]);
            int fromY = int.Parse(from[1]);
            int toX = int.Parse(to[0]);
            int toY = int.Parse(to[1]);

            // Ex: Simulezi click pe piesa și mutare
            var piece = board.play_board[fromX][fromY];
            board.selected = piece;

            var target = board.play_board[toX][toY];
            target.movable = true;
            target.takeable = parts.Length >= 4 && parts[3] == "TAKE";

            piece.move();


        }
        
    }
}
