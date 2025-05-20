using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CamelRiderChess
{
    internal class Camel : Piece
    {
        int x_temp;
        int y_temp;
        public Camel(Board board, int x, int y, bool white) : base(board, x, y)
        {
            this.whitespace = false;
            this.white = white;
            if (white)
                this.Image = System.Drawing.Image.FromFile("images/camelWhite.png");
            else
                this.Image = System.Drawing.Image.FromFile("images/camelBlack.png");
        }
        void camel(int x, int y, bool mut, string caz)
        {
            switch (caz)
            {
                case "x-1 y-3":
                    if (y - 3 >= 0 && y - 3 < board.play_board.Length && x - 1 >= 0 && x - 1 < board.play_board.Length)
                    {
                        if (board.play_board[x - 1][y - 3].GetType().Name == "Piece")
                        {
                            board.play_board[x - 1][y - 3].make_movable();
                            if (mut)
                            {
                                x_temp = x - 1;
                                y_temp = y - 3;
                                camel(x_temp, y_temp, false, "x-1 y-3");
                            }
                        }
                        if (board.play_board[x - 1][y - 3].white != this.white && !board.play_board[x - 1][y - 3].whitespace)
                            board.play_board[x - 1][y - 3].make_victim();

                    }
                    break;

                case "x+1 y-3":
                    if (y - 3 >= 0 && y - 3 < board.play_board.Length && x + 1 >= 0 && x + 1 < board.play_board.Length)
                    {
                        if (board.play_board[x + 1][y - 3].GetType().Name == "Piece")
                        {
                            board.play_board[x + 1][y - 3].make_movable();
                            if (mut)
                            {
                                x_temp = x + 1;
                                y_temp = y - 3;
                                camel(x_temp, y_temp, false, "x+1 y-3");
                            }

                        }
                        if (board.play_board[x + 1][y - 3].white != this.white && !board.play_board[x + 1][y - 3].whitespace)
                            board.play_board[x + 1][y - 3].make_victim();

                    }
                    break;

                case "x-1 y+3":
                    if (y + 3 >= 0 && y + 3 < board.play_board.Length && x - 1 >= 0 && x - 1 < board.play_board.Length)
                    {
                        if (board.play_board[x - 1][y + 3].GetType().Name == "Piece")
                        {
                            board.play_board[x - 1][y + 3].make_movable();
                            if (mut)
                            {
                                x_temp = x - 1;
                                y_temp = y + 3;
                                camel(x_temp, y_temp, false, "x-1 y+3");
                            }
                        }
                        if (board.play_board[x - 1][y + 3].white != this.white && !board.play_board[x - 1][y + 3].whitespace)
                            board.play_board[x - 1][y + 3].make_victim();

                    }
                    break;

                case "x+1 y+3":
                    if (y + 3 >= 0 && y + 3 < board.play_board.Length && x + 1 >= 0 && x + 1 < board.play_board.Length)
                    {
                        if (board.play_board[x + 1][y + 3].GetType().Name == "Piece")
                        {
                            board.play_board[x + 1][y + 3].make_movable();
                            if (mut)
                            {
                                x_temp = x + 1;
                                y_temp = y + 3;
                                camel(x_temp, y_temp, false, "x+1 y+3");
                            }

                        }
                        if (board.play_board[x + 1][y + 3].white != this.white && !board.play_board[x + 1][y + 3].whitespace)
                            board.play_board[x + 1][y + 3].make_victim();

                    }
                    break;

                case "x-3 y-1":
                    if (y - 1 >= 0 && y - 1 < board.play_board.Length && x - 3 >= 0 && x - 3 < board.play_board.Length)
                    {
                        if (board.play_board[x - 3][y - 1].GetType().Name == "Piece")
                        {
                            board.play_board[x - 3][y - 1].make_movable();
                            if (mut)
                            {
                                x_temp = x - 3;
                                y_temp = y - 1;
                                camel(x_temp, y_temp, false, "x-3 y-1");
                            }
                        }
                        if (board.play_board[x - 3][y - 1].white != this.white && !board.play_board[x - 3][y - 1].whitespace)
                            board.play_board[x - 3][y - 1].make_victim();

                    }
                    break;

                case "x+3 y-1":
                    if (y - 1 >= 0 && y - 1 < board.play_board.Length && x + 3 >= 0 && x + 3 < board.play_board.Length)
                    {
                        if (board.play_board[x + 3][y - 1].GetType().Name == "Piece")
                        {
                            board.play_board[x + 3][y - 1].make_movable();
                            if (mut)
                            {
                                x_temp = x + 3;
                                y_temp = y - 1;
                                camel(x_temp, y_temp, false, "x+3 y-1");
                            }

                        }
                        if (board.play_board[x + 3][y - 1].white != this.white && !board.play_board[x + 3][y - 1].whitespace)
                            board.play_board[x + 3][y - 1].make_victim();

                    }
                    break;

                case "x-3 y+1":
                    if (y + 1 >= 0 && y + 1 < board.play_board.Length && x - 3 >= 0 && x - 3 < board.play_board.Length)
                    {
                        if (board.play_board[x - 3][y + 1].GetType().Name == "Piece")
                        {
                            board.play_board[x - 3][y + 1].make_movable();
                            if (mut)
                            {
                                x_temp = x - 3;
                                y_temp = y + 1;
                                camel(x_temp, y_temp, false, "x-3 y+1");
                            }

                        }
                        if (board.play_board[x - 3][y + 1].white != this.white && !board.play_board[x - 3][y + 1].whitespace)
                            board.play_board[x - 3][y + 1].make_victim();

                    }
                    break;

                case "x+3 y+1":
                    if (y + 1 >= 0 && y + 1 < board.play_board.Length && x + 3 >= 0 && x + 3 < board.play_board.Length)
                    {
                        if (board.play_board[x + 3][y + 1].GetType().Name == "Piece")
                        {
                            board.play_board[x + 3][y + 1].make_movable();
                            if (mut)
                            {
                                x_temp = x + 3;
                                y_temp = y + 1;
                                camel(x_temp, y_temp, false, "x-1 y-3");
                            }

                        }
                        if (board.play_board[x + 3][y + 1].white != this.white && !board.play_board[x + 3][y + 1].whitespace)
                            board.play_board[x + 3][y + 1].make_victim();

                    }
                    break;
            }

        }

        public override void find_possible_moves()
        {
            board.reset_play_board();



            if (y - 3 >= 0 && y - 3 < board.play_board.Length && x - 1 >= 0 && x - 1 < board.play_board.Length)
            {
                if (board.play_board[x - 1][y - 3].GetType().Name == "Piece")
                {
                    board.play_board[x - 1][y - 3].make_movable();
                    x_temp = x - 1;
                    y_temp = y - 3;
                    camel(x_temp, y_temp, true, "x-1 y-3");

                }
                if (board.play_board[x - 1][y - 3].white != this.white && !board.play_board[x - 1][y - 3].whitespace)
                    board.play_board[x - 1][y - 3].make_victim();
            }
            if (y - 3 >= 0 && y - 3 < board.play_board.Length && x + 1 >= 0 && x + 1 < board.play_board.Length)
            {
                if (board.play_board[x + 1][y - 3].GetType().Name == "Piece")
                {
                    board.play_board[x + 1][y - 3].make_movable();
                    x_temp = x + 1;
                    y_temp = y - 3;
                    camel(x_temp, y_temp, true, "x+1 y-3");

                }
                if (board.play_board[x + 1][y - 3].white != this.white && !board.play_board[x + 1][y - 3].whitespace)
                    board.play_board[x + 1][y - 3].make_victim();
            }
            if (y + 3 >= 0 && y + 3 < board.play_board.Length && x - 1 >= 0 && x - 1 < board.play_board.Length)
            {
                if (board.play_board[x - 1][y + 3].GetType().Name == "Piece")
                {
                    board.play_board[x - 1][y + 3].make_movable();
                    x_temp = x - 1;
                    y_temp = y + 3;
                    camel(x_temp, y_temp, true, "x-1 y+3");

                }
                if (board.play_board[x - 1][y + 3].white != this.white && !board.play_board[x - 1][y + 3].whitespace)
                    board.play_board[x - 1][y + 3].make_victim();
            }
            if (y + 3 >= 0 && y + 3 < board.play_board.Length && x + 1 >= 0 && x + 1 < board.play_board.Length)
            {
                if (board.play_board[x + 1][y + 3].GetType().Name == "Piece")
                {
                    board.play_board[x + 1][y + 3].make_movable();
                    x_temp = x + 1;
                    y_temp = y + 3;
                    camel(x_temp, y_temp, true, "x+1 y+3");

                }
                if (board.play_board[x + 1][y + 3].white != this.white && !board.play_board[x + 1][y + 3].whitespace)
                    board.play_board[x + 1][y + 3].make_victim();
            }


            if (y - 1 >= 0 && y - 1 < board.play_board.Length && x - 3 >= 0 && x - 3 < board.play_board.Length)
            {
                if (board.play_board[x - 3][y - 1].GetType().Name == "Piece")
                {
                    board.play_board[x - 3][y - 1].make_movable();
                    x_temp = x - 3;
                    y_temp = y - 1;
                    camel(x_temp, y_temp, true, "x-3 y-1");

                }
                if (board.play_board[x - 3][y - 1].white != this.white && !board.play_board[x - 3][y - 1].whitespace)
                    board.play_board[x - 3][y - 1].make_victim();
            }
            if (y - 1 >= 0 && y - 1 < board.play_board.Length && x + 3 >= 0 && x + 3 < board.play_board.Length)
            {
                if (board.play_board[x + 3][y - 1].GetType().Name == "Piece")
                {
                    board.play_board[x + 3][y - 1].make_movable();
                    x_temp = x + 3;
                    y_temp = y - 1;
                    camel(x_temp, y_temp, true, "x+3 y-1");

                }
                if (board.play_board[x + 3][y - 1].white != this.white && !board.play_board[x + 3][y - 1].whitespace)
                    board.play_board[x + 3][y - 1].make_victim();
            }
            if (y + 1 >= 0 && y + 1 < board.play_board.Length && x - 3 >= 0 && x - 3 < board.play_board.Length)
            {
                if (board.play_board[x - 3][y + 1].GetType().Name == "Piece")
                {
                    board.play_board[x - 3][y + 1].make_movable();
                    x_temp = x - 3;
                    y_temp = y + 1;
                    camel(x_temp, y_temp, true, "x-3 y+1");

                }
                if (board.play_board[x - 3][y + 1].white != this.white && !board.play_board[x - 3][y + 1].whitespace)
                    board.play_board[x - 3][y + 1].make_victim();
            }
            if (y + 1 >= 0 && y + 1 < board.play_board.Length && x + 3 >= 0 && x + 3 < board.play_board.Length)
            {
                if (board.play_board[x + 3][y + 1].GetType().Name == "Piece")
                {
                    board.play_board[x + 3][y + 1].make_movable();
                    x_temp = x + 3;
                    y_temp = y + 1;
                    camel(x_temp, y_temp, true, "x-1 y-3");

                }
                if (board.play_board[x + 3][y + 1].white != this.white && !board.play_board[x + 3][y + 1].whitespace)
                    board.play_board[x + 3][y + 1].make_victim();
            }
        }
        public override bool CanAttackPosition(int targetX, int targetY)
        {
            int dx = Math.Abs(targetX - this.x);
            int dy = Math.Abs(targetY - this.y);

            return (dx == 3 && dy == 1) || (dx == 1 && dy == 3);
        }
    }
}
