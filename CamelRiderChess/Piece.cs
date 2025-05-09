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

        public Piece move()
        {
            if (takeable)
            {
                int temp_x = board.selected.x;
                int temp_y = board.selected.y;
                board.selected.x = x;
                board.selected.y = y;
                board.Controls.Remove(this);
                this.takeable = false;

                Piece piece_whitespace = new Piece(board, temp_x, temp_y);
                piece_whitespace.Click += new EventHandler((sender, e) =>
                {
                    Piece piece = (Piece)sender;

                    if (!piece.movable && !piece.takeable)
                    {
                        if (!piece.whitespace)
                            board.reset_play_board();
                        piece.find_possible_moves();
                    }

                    Piece temp_piece = piece.move();
                    if (temp_piece != null)
                    {
                        board.redraw_play_area(temp_piece);
                    }

                    board.selected = piece;
                });

                board.play_board[temp_x][temp_y] = piece_whitespace;
                board.Controls.Add(piece_whitespace);
                board.play_board[x][y] = board.selected;
                board.discarded.Add(this);
                board.reset_play_board();
                board.player_turn = !board.player_turn;
                return piece_whitespace;
            }

            if (movable)
            {
                board.play_board[x][y] = board.selected;
                int temp_x = board.selected.x;
                int temp_y = board.selected.y;
                board.selected.x = x;
                board.selected.y = y;
                board.selected.no_moves++;
                x = temp_x;
                y = temp_y;
                board.play_board[x][y] = this;
                board.reset_play_board();
                board.player_turn = !board.player_turn;
                return this;
            }
            return null;
        }
    }
}
