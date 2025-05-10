using CamelRiderChess;

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
                if (piece.white != this.white && !piece.whitespace && piece.takeable)
                {
                    if (piece.movable && piece.x == this.x && piece.y == this.y)
                    {
                        return true; // Dacă există o piesă care poate captura regele
                    }
                }
            }
        }
        return false; // Dacă nu există piesă care poate captura regele
    }

    // Verifică dacă regele este în șah-mat
    public bool IsCheckmate()
    {
        if (!IsInCheck())
            return false; // Dacă nu este în șah, nu poate fi șah-mat

        // Verificăm dacă există o mutare care ar putea salva regele
        foreach (var row in board.play_board)
        {
            foreach (var piece in row)
            {
                if (piece.white == this.white && piece.movable)
                {
                    piece.find_possible_moves();
                    foreach (var targetPiece in board.play_board)
                    {
                        if (!IsInCheck())
                            return false; // Dacă regele nu mai este în șah după mutare
                    }
                }
            }
        }

        return true; // Dacă nu există mutări care pot salva regele
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
}