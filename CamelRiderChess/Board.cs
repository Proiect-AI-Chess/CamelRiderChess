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
        public Piece selected;
        public List<Piece> discarded;
        public bool player_turn;
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

        public void resize_play_area(int width, int height)
        {
            this.Height = (int)(0.85 * ((double)height));
            this.Width = this.Height;
            this.Top = (height - this.Height) / 2;
            this.Left = (width - this.Width) / 2;

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
                }
            }
        }
        
    }
}