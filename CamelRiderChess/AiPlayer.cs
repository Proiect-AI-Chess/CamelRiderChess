using CamelRiderChess;
using System;
using System.Collections.Generic;

public class AiPlayer
{
    private Board board;
    private const int MAX_DEPTH = 1 ;

    public AiPlayer(Board board)
    {
        this.board = board;
    }

    public void MakeMove()
    {
        SimpleMove bestMove = FindBestMove();
        if (bestMove.fromX != -1)
        {
            // Select the piece
            Piece pieceToMove = board.play_board[bestMove.fromX][bestMove.fromY];
            board.selected = pieceToMove;

            // Find and apply possible moves
            pieceToMove.find_possible_moves();
            pieceToMove.find_possible_victims();

            // Move to target position
            Piece targetPiece = board.play_board[bestMove.toX][bestMove.toY];
            Piece result = targetPiece.move();

            if (result != null)
            {
                board.redraw_play_area(result);
            }
        }
    }

    private SimpleMove FindBestMove()
    {
        int bestValue = int.MinValue;
        SimpleMove bestMove = new SimpleMove(-1, -1, -1, -1);

        List<SimpleMove> possibleMoves = GenerateAllMoves();

        foreach (var move in possibleMoves)
        {
            // Save current state
            Piece capturedPiece = SimulateMove(move);

            // Evaluate move
            int moveValue = Minimax(MAX_DEPTH - 1, false, int.MinValue, int.MaxValue);

            // Restore board state
            UndoMove(move, capturedPiece);

            if (moveValue > bestValue)
            {
                bestValue = moveValue;
                bestMove = move;
            }
        }

        return bestMove;
    }

    private int Minimax(int depth, bool maximizingPlayer, int alpha, int beta)
    {
        if (depth == 0)
            return EvaluateBoard();

        List<SimpleMove> moves = GenerateAllMoves();

        if (maximizingPlayer)
        {
            int maxEval = int.MinValue;
            foreach (var move in moves)
            {
                Piece captured = SimulateMove(move);
                int eval = Minimax(depth - 1, false, alpha, beta);
                UndoMove(move, captured);

                maxEval = Math.Max(maxEval, eval);
                alpha = Math.Max(alpha, eval);
                if (beta <= alpha)
                    break;
            }
            return maxEval;
        }
        else
        {
            int minEval = int.MaxValue;
            foreach (var move in moves)
            {
                Piece captured = SimulateMove(move);
                int eval = Minimax(depth - 1, true, alpha, beta);
                UndoMove(move, captured);

                minEval = Math.Min(minEval, eval);
                beta = Math.Min(beta, eval);
                if (beta <= alpha)
                    break;
            }
            return minEval;
        }
    }

    private struct SimpleMove
    {
        public int fromX, fromY, toX, toY;
        public SimpleMove(int fx, int fy, int tx, int ty)
        {
            fromX = fx;
            fromY = fy;
            toX = tx;
            toY = ty;
        }
    }

    private List<SimpleMove> GenerateAllMoves()
    {
        List<SimpleMove> moves = new List<SimpleMove>();

        // Generate moves for all black pieces
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Piece piece = board.play_board[i][j];
                if (!piece.whitespace && !piece.white) // Black pieces
                {
                    piece.find_possible_moves();
                    piece.find_possible_victims();

                    // Check all possible positions
                    for (int x = 0; x < 10; x++)
                    {
                        for (int y = 0; y < 10; y++)
                        {
                            if ((board.play_board[x][y].movable || board.play_board[x][y].takeable) &&
                                !board.WouldBeInCheckAfterMove(piece, x, y))
                            {
                                moves.Add(new SimpleMove(i, j, x, y));
                            }
                        }
                    }

                    board.reset_play_board();
                }
            }
        }
        return moves;
    }

    private Piece SimulateMove(SimpleMove move)
    {
        Piece movingPiece = board.play_board[move.fromX][move.fromY];
        Piece capturedPiece = board.play_board[move.toX][move.toY];

        // Store the piece at destination
        board.play_board[move.toX][move.toY] = movingPiece;
        board.play_board[move.fromX][move.fromY] = new Piece(board, move.fromX, move.fromY);

        // Update piece coordinates
        movingPiece.x = move.toX;
        movingPiece.y = move.toY;

        return capturedPiece;
    }

    private void UndoMove(SimpleMove move, Piece capturedPiece)
    {
        Piece movingPiece = board.play_board[move.toX][move.toY];

        // Restore original position
        board.play_board[move.fromX][move.fromY] = movingPiece;
        board.play_board[move.toX][move.toY] = capturedPiece;

        // Restore piece coordinates
        movingPiece.x = move.fromX;
        movingPiece.y = move.fromY;
    }

    private int EvaluateBoard()
    {
        int score = 0;

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Piece piece = board.play_board[i][j];
                if (!piece.whitespace)
                {
                    int pieceValue = GetPieceValue(piece);
                    if (!piece.white) // Black pieces (AI)
                        score += pieceValue;
                    else // White pieces (opponent)
                        score -= pieceValue;
                }
            }
        }
        return score;
    }

    private int GetPieceValue(Piece piece)
    {
        if (piece is Pawn) return 10;
        if (piece is Knight) return 30;
        if (piece is Bishop) return 30;
        if (piece is Rook) return 50;
        if (piece is Queen) return 90;
        if (piece is King) return 900;
        if (piece is Camel) return 40;
        return 0;
    }
}
