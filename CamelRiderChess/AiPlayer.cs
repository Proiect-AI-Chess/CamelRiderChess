using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CamelRiderChess
{
    public class AiPlayer
    {
        public bool IsWhite { get; private set; }

        public AiPlayer(bool isWhite)
        {
            IsWhite = isWhite;
        }
        public void MakeMove(Board board)
        {
            // Generăm toate mutările valide pentru AI
            var allMoves = new List<(Piece piece, int targetX, int targetY)>();

            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    var piece = board.play_board[x][y];
                    if (piece.white == IsWhite)
                    {
                        // Resetăm highlight mutări posibile pentru a le regenera
                        board.reset_play_board();

                        piece.find_possible_moves();
                        piece.find_possible_victims();

                        // Adunăm mutările disponibile
                        for (int i = 0; i < 10; i++)
                        {
                            for (int j = 0; j < 10; j++)
                            {
                                if ((board.play_board[i][j].movable || board.play_board[i][j].takeable)
                                    && !board.WouldBeInCheckAfterMove(piece, i, j))
                                {
                                    allMoves.Add((piece, i, j));
                                }
                            }
                        }
                    }
                }
            }
            if (allMoves.Count == 0)
            {
                MessageBox.Show("AI nu are mutări valide, ai câștigat!");
                return;
            }

            // Alege o mutare aleatorie (sau cea mai bună, dacă vrei)
            var rand = new Random();
            var (chosenPiece, targetX, targetY) = allMoves[rand.Next(allMoves.Count)];

            // Execută mutarea pe tabla de joc
            // Facem o simulare ca și cum AI-ul ar "clicka" pe piesă și apoi pe țintă

            board.selected = chosenPiece;
            var targetPiece = board.play_board[targetX][targetY];

            // Setăm dacă mutarea e de capturare
            targetPiece.takeable = (targetPiece.white != IsWhite);
            targetPiece.movable = (targetPiece.white == IsWhite);

            var movedPiece = targetPiece.move();

            if (movedPiece != null)
            {
                board.redraw_play_area(movedPiece);
                board.player_turn = !board.player_turn;
                MessageBox.Show("Rândul jucătorului");
            }
        }
    }
}