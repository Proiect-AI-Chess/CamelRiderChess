using CamelRiderChess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CamelRiderChess
{
    public class Board : TableLayoutPanel
    {
        public String[][] board_game;
        public Piece[][] play_board;
        public Piece selected = null;
        public List<Piece> discarded;
        public Menu menuForm;
        public bool player_turn = true;   
        public bool isWhitePlayer = true; 
        public bool gameWithAI;
        public AiPlayer aiPlayer;


        public Board()
        {
            player_turn = true;
            discarded = new List<Piece>();


            for (int i = 0; i < 10; i++)
            {

                this.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
                this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
            }


            this.AutoSize = true;

            board_game = new String[10][];
            play_board = new Piece[10][];
            for (int i = 0; i < 10; i++)
            {
                play_board[i] = new Piece[10];
            }

            this.build_play_area();
        }

        public void resize_play_area(int formWidth, int formHeight)
        {
            int cellSize = 68; // Dimensiunea unei celule (68x68)
            int boardSize = cellSize * 10; // Tabla este 10x10

            // Ajustăm dimensiunea tablei la dimensiunea ferestrei
            this.Width = boardSize;
            this.Height = boardSize;

            // Ajustăm fiecare celulă la dimensiunea de 68x68
            foreach (Control control in this.Controls)
            {
                control.Width = cellSize;
                control.Height = cellSize;
            }

            // Centrează tabla în fereastră
            this.Top = (formHeight - this.Height) / 2;
            this.Left = (formWidth - this.Width) / 2;
        }

        public void build_play_area()
        {
            board_game = JsonSerializer.Deserialize<String[][]>(System.IO.File.ReadAllText("board.json"));
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Piece piece;
                    switch (board_game[i][j])
                    {
                        case "pawnWhite":
                            piece = new Pawn(this, j, i, true);
                            break;
                        case "pawnBlack":
                            piece = new Pawn(this, j, i, false);
                            break;
                        case "rookWhite":
                            piece = new Rook(this, j, i, true);
                            break;
                        case "rookBlack":
                            piece = new Rook(this, j, i, false);
                            break;
                        case "knightWhite":
                            piece = new Knight(this, j, i, true);
                            break;
                        case "knightBlack":
                            piece = new Knight(this, j, i, false);
                            break;
                        case "bishopWhite":
                            piece = new Bishop(this, j, i, true);
                            break;
                        case "bishopBlack":
                            piece = new Bishop(this, j, i, false);
                            break;
                        case "queenWhite":
                            piece = new Queen(this, j, i, true);
                            break;
                        case "queenBlack":
                            piece = new Queen(this, j, i, false);
                            break;
                        case "kingWhite":
                            piece = new King(this, j, i, true);
                            break;
                        case "kingBlack":
                            piece = new King(this, j, i, false);
                            break;
                        case "camelWhite":
                            piece = new Camel(this, j, i, true);
                            break;
                        case "camelBlack":
                            piece = new Camel(this, j, i, false);
                            break;
                        default:
                            piece = new Piece(this, j, i);
                            break;
                    }
                    play_board[j][i] = piece;
                    this.Controls.Add(piece, j, i);

                    piece.Click += new EventHandler((sender, e) =>
                    {
                        Piece piece1 = (Piece)sender;

                        // Verificăm dacă e rândul jucătorului și dacă piesa selectată este a lui (albă)
                        if (player_turn != isWhitePlayer )
                            return;

                        if (!piece1.movable && !piece1.takeable)
                        {
                            if (!piece1.whitespace)
                                reset_play_board();
                            piece1.find_possible_moves();
                            piece1.find_possible_victims();
                        }

                        Piece temp_piece = piece1.move();
                        if (temp_piece != null)
                        {
                            redraw_play_area(temp_piece);
                            ToggleTurnAndRunAI();         // noul apel
                        }


                        selected = piece1;
                    });


                }
            }
        }
        private void ToggleTurnAndRunAI()
        {

            if (gameWithAI && !player_turn)  // acum e rândul negrului (AI)
            {
                aiPlayer.MakeMove();         // AI mută
                player_turn = !player_turn;  // după mutarea AI-ului revine la alb
            }
            player_turn = !player_turn;      // inversăm tura

        }
        public void reset_play_board()
        {
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                {
                    play_board[i][j].make_unmovable(i, j);
                    play_board[i][j].make_untakeable(i, j);
                }
        }

        public void redraw_play_area(Piece p1)
        {
            if (selected != null)
            {
                this.Controls.Remove(p1);
                this.Controls.Remove(selected);
                this.Controls.Add(p1, p1.x, p1.y);
                this.Controls.Add(selected, selected.x, selected.y);
            }
            reset_play_board();
        }

        internal King FindKing(bool white)
        {
            foreach (var row in play_board)
            {
                foreach (var piece in row)
                {
                    if (piece is King king && king.white == white)
                        return king;
                }
            }
            throw new Exception("King not found on board for color: " + (white ? "White" : "Black"));

        }


        public bool WouldBeInCheckAfterMove(Piece movingPiece, int targetX, int targetY)
        {
            int originalX = movingPiece.x;
            int originalY = movingPiece.y;

            Piece targetPiece = play_board[targetX][targetY];

            // Mutăm temporar piesa
            play_board[targetX][targetY] = movingPiece;
            play_board[originalX][originalY] = new Piece(this, originalX, originalY); // pătrat gol

            int oldX = movingPiece.x;
            int oldY = movingPiece.y;
            movingPiece.x = targetX;
            movingPiece.y = targetY;

            // Verificăm dacă regele jucătorului e în șah
            King king = FindKing(movingPiece.white);
            bool inCheck = king.IsInCheck();

            // Revenim la poziția inițială
            play_board[originalX][originalY] = movingPiece;
            play_board[targetX][targetY] = targetPiece;
            movingPiece.x = oldX;
            movingPiece.y = oldY;

            return inCheck;
        }

        public bool HasAnyValidMove(bool whiteToMove)
        {
            foreach (var row in play_board)
            {
                foreach (var piece in row)
                {
                    if (!piece.whitespace && piece.white == whiteToMove)
                    {
                        piece.find_possible_moves();
                        piece.find_possible_victims();

                        for (int i = 0; i < play_board.Length; i++)
                        {
                            for (int j = 0; j < play_board[i].Length; j++)
                            {
                                if ((play_board[i][j].movable || play_board[i][j].takeable) &&
                                    !WouldBeInCheckAfterMove(piece, i, j))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
        public void SendMoveToServer(string move)
        {
            try
            {
                menuForm.SendMove(move);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Eroare trimitere mutare: " + ex.Message);
            }
        }
       

    }
}
