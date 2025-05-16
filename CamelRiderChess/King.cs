using CamelRiderChess;
using System;

internal class King : Piece
{
    public King(Board board, int x, int y, bool white) : base(board, x, y)
    {
        this.whitespace = false;
        this.white = white;

        if (white)
            this.Image = System.Drawing.Image.FromFile("images/kingWhite.png");
        else
            this.Image = System.Drawing.Image.FromFile("images/kingBlack.png");
    }

    // Verifică dacă regele este în șah
    public bool IsInCheck()
    {
        foreach (var row in board.play_board)
        {
            foreach (var piece in row)
            {
                if (!piece.whitespace && piece.white != this.white)
                {
                    if (piece.CanAttackPosition(this.x, this.y))
                        return true;
                }
            }
        }
        return false;
    }

    public bool IsCheckmate()
    {
        if (!IsInCheck())
            return false; // Dacă nu e în șah, nu poate fi șah-mat

        // Pentru fiecare piesă a jucătorului curent
        foreach (var row in board.play_board)
        {
            foreach (var piece in row)
            {
                if (piece.white == this.white && !piece.whitespace)
                {
                    // Găsim mutările posibile ale piesei
                    piece.find_possible_moves();

                    // Pentru fiecare pătrat de pe tabla de joc, verificăm dacă e mutabil
                    for (int i = 0; i < board.play_board.Length; i++)
                    {
                        for (int j = 0; j < board.play_board[i].Length; j++)
                        {
                            var targetSquare = board.play_board[i][j];

                            if (targetSquare.movable || targetSquare.takeable)
                            {
                                // Simulăm mutarea
                                var originalX = piece.x;
                                var originalY = piece.y;

                                var capturedPiece = board.play_board[i][j];
                                bool capturedWasWhitespace = capturedPiece.whitespace;

                                // Mutăm piesa
                                board.play_board[i][j] = piece;
                                board.play_board[originalX][originalY] = new Piece(board, originalX, originalY); // pătrat gol

                                piece.x = i;
                                piece.y = j;

                                bool stillInCheck = IsInCheck();

                                // Revenim mutarea
                                board.play_board[originalX][originalY] = piece;
                                board.play_board[i][j] = capturedPiece;

                                piece.x = originalX;
                                piece.y = originalY;

                                if (!stillInCheck)
                                {
                                    return false; // Regele poate fi salvat cu această mutare
                                }
                            }
                        }
                    }
                }
            }
        }

        return true; // Nu există mutări care să scoată regele din șah -> șah-mat
    }


    // Funcție pentru a marca mutările posibile ale regelui
    void king(int x, int y, int a, int b)
    {
        if (y + b >= 0 && y + b < board.play_board.Length && x + a >= 0 && x + a < board.play_board.Length)
        {
            if (board.play_board[x + a][y + b].GetType().Name == "Piece")
            {
                board.play_board[x + a][y + b].make_movable();
            }
            if (board.play_board[x + a][y + b].white != this.white && !board.play_board[x + a][y + b].whitespace)
                board.play_board[x + a][y + b].make_victim();
        }
    }

    // Găsim mutările posibile ale regelui și evidențiem dacă este în șah sau șah-mat
    public override void find_possible_moves()
    {
        board.reset_play_board();
        king(x, y, 1, 1);
        king(x, y, -1, 1);
        king(x, y, 1, -1);
        king(x, y, -1, -1);
        king(x, y, 0, 1);
        king(x, y, 1, 0);
        king(x, y, -1, 0);
        king(x, y, 0, -1);

        // Schimbăm fundalul la galben dacă regele este în șah
        if (IsInCheck())
        {
            this.BackColor = System.Drawing.Color.Yellow;  // Evidentiem regele in cazul unui șah
        }
        else if (IsCheckmate())
        {
            this.BackColor = System.Drawing.Color.Red;  // Schimbăm fundalul la roșu dacă este în șah-mat
        }
        else
        {
            this.BackColor = System.Drawing.Color.Transparent;  // Fundal normal dacă nu este în șah sau șah-mat
        }
    }

    public override bool CanAttackPosition(int targetX, int targetY)
    {
        int dx = Math.Abs(targetX - this.x);
        int dy = Math.Abs(targetY - this.y);

        // Regele poate ataca poziții la o distanță de maxim 1 pe orizontală, verticală sau diagonală
        return (dx <= 1 && dy <= 1) && !(dx == 0 && dy == 0);
    }
}