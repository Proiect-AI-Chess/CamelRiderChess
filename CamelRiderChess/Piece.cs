using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public void square(int x, int y)
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
            if(this is King) { return; }
            this.takeable = true;

        }

        public virtual void find_possible_moves() { /*.....*/ }

        public virtual void find_possible_victims() { /*.....*/ }

        public Piece move()
        {
            // Dacă piesa nu poate fi mutată/take-uită, nu facem nimic
            if (!takeable && !movable)
                return null;

            // Verificăm dacă mutarea ar pune regele în șah (nu permitem mutarea)
            if (board.WouldBeInCheckAfterMove(board.selected, x, y))
            {
                MessageBox.Show("Mutare invalidă: regele tău ar fi în șah!");
                return null;
            }
            int fromX = board.selected.x;
            int fromY = board.selected.y;
            int toX = x;
            int toY = y;
            string moveString = "";
            if (takeable)
            {
                board.selected.x = x;
                board.selected.y = y;
                board.Controls.Remove(this);
                this.takeable = false;

                Piece piece_whitespace = new Piece(board, fromX, fromY);
                piece_whitespace.Click += new EventHandler((sender, e) =>
                {
                    Piece piece = (Piece)sender;

                    if (!piece.movable && !piece.takeable)
                    {
                        if (!piece.whitespace)
                            board.reset_play_board();
                        piece.find_possible_moves();
                        piece.find_possible_victims();
                    }

                    Piece temp_piece = piece.move();
                    if (temp_piece != null)
                    {
                        board.redraw_play_area(temp_piece);
                    }

                    board.selected = piece;
                });

                board.play_board[fromX][fromY] = piece_whitespace;
                board.Controls.Add(piece_whitespace);
                board.play_board[toX][toY] = board.selected;
                board.discarded.Add(this);
                board.reset_play_board();
                board.player_turn = !board.player_turn;
                MessageBox.Show("Acum e rândul " + (board.player_turn ? "albului" : "negrului"));

                moveString = $"MOVE|{fromX},{fromY}|{toX},{toY}|TAKE";
                board.SendMoveToServer(moveString);
                // După fiecare mutare, verificăm dacă regele e în șah
                foreach (var row in board.play_board)
                {
                    foreach (var piece in row)
                    {
                        if (piece is King king && king.white == board.player_turn)
                        {
                            if (king.IsCheckmate())
                            {
                                MessageBox.Show("Regele e în șah mat!");
                                king.BackColor = System.Drawing.Color.Red; // Fundal roșu pentru șah-mat
                            }
                            else if (king.IsInCheck())
                            {
                                MessageBox.Show("Regele e în șah!");
                                king.BackColor = System.Drawing.Color.Yellow;
                            }
                            else
                            {
                                king.square(king.x, king.y); // resetăm culoarea
                            }
                        }
                    }
                }

                return piece_whitespace;
            }

            if (movable)
            {
                board.play_board[toX][toY] = board.selected;
                board.selected.x = toX;
                board.selected.y = toY;
                board.selected.no_moves++;
                board.play_board[fromX][fromY] = this;

                x = fromX;
                y = fromY;

                board.reset_play_board();
                board.player_turn = !board.player_turn;
                MessageBox.Show("Acum e rândul " + (board.player_turn ? "albului" : "negrului"));

                moveString = $"MOVE|{fromX},{fromY}|{toX},{toY}|MOVE";
                board.SendMoveToServer(moveString);
                // După fiecare mutare, verificăm dacă regele e în șah
                foreach (var row in board.play_board)
                {
                    foreach (var piece in row)
                    {
                        if (piece is King king && king.white == board.player_turn)
                        {
                            if (king.IsCheckmate())
                            {
                                MessageBox.Show("Regele e în șah mat!");
                                king.BackColor = System.Drawing.Color.Red; // Fundal roșu pentru șah-mat
                            }
                            else if (king.IsInCheck())
                            {
                                MessageBox.Show("Regele e în șah!");
                                king.BackColor = System.Drawing.Color.Yellow;
                            }
                            else
                            {
                                king.square(king.x, king.y); // resetăm culoarea
                            }
                        }
                    }
                }

                return this;
            }
            return null;
        }

        public virtual bool CanAttackPosition(int targetX, int targetY)
        {
            // Implicit, o piesă generică nu atacă nimic
            return false;
        }

        

    }
}
