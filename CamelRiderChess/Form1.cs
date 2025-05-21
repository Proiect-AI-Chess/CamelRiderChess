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

        public void UpdateChessBoard(string move)
        {
            try
            {
                // Parsăm mutarea primită
                string[] parts = move.Split('-');
                if (parts.Length != 2)
                {
                    MessageBox.Show("Mutare invalidă: " + move);
                    return;
                }

                // Obținem poziția de plecare și destinație din mutare
                string from = parts[0];
                string to = parts[1];

                // Convertim pozițiile de la text la coordonate pe tablă
                int fromX = from[0] - 'A';
                int fromY = int.Parse(from[1].ToString()) - 1;
                int toX = to[0] - 'A';
                int toY = int.Parse(to[1].ToString()) - 1;

                // Găsim piesa care trebuie mutată și o mutăm
                Piece pieceToMove = board.play_board[fromX][fromY];
                Piece pieceToRemove = board.play_board[toX][toY];

                // Verificăm dacă mutarea este validă
                if (pieceToMove != null)
                {
                    // Mutăm piesa pe noua poziție
                    board.play_board[fromX][fromY] = null;
                    board.play_board[toX][toY] = pieceToMove;
                    pieceToMove.Location = new Point(toX, toY);

                    // Dacă există o piesă pe poziția de destinație, o eliminăm
                    if (pieceToRemove != null)
                    {
                        board.Controls.Remove(pieceToRemove);
                    }

                    // Actualizăm starea vizuală a tablei
                    board.Invalidate();
                }
                else
                {
                    MessageBox.Show("Nu există piesă de mutat în poziția " + from);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare la actualizarea tablei de șah: " + ex.Message);
            }
        }
    }
}
