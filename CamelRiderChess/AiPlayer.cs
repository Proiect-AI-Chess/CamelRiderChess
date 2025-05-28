using CamelRiderChess;
using System;
using System.Collections.Generic;

public class AiPlayer
{
    private Board board;

    public AiPlayer(Board board)
    {
        this.board = board;
    }

    public void MakeMove()
    {
        int bestFromX = -1, bestFromY = -1, bestToX = -1, bestToY = -1;
        int maxEval = int.MinValue;
        int depth = 3;

        // Generăm toate mutările negre
        foreach (var move in GenerateAllMoves(false)) // false = piesele negre
        {
            // Aplicăm mutarea
            ApplyMove(move.fromX, move.fromY, move.toX, move.toY, out var capturedPiece);

            // Calculăm evaluarea folosind Minimax
            int eval = Minimax(depth - 1, true, int.MinValue, int.MaxValue);

            

            if (eval > maxEval)
            {
                maxEval = eval;
                bestFromX = move.fromX;
                bestFromY = move.fromY;
                bestToX = move.toX;
                bestToY = move.toY;
            }
        }
        if (bestFromX != -1)
        {
            ApplyMove(bestFromX, bestFromY, bestToX, bestToY, out _);
            board.redraw_play_area(board.play_board[bestToX][bestToY]); // forțează redraw (sau un alt mecanism)
        }


    }

    private int Minimax(int depth, bool isMaximizing, int alpha, int beta)
    {
        if (depth == 0)
            return EvaluateBoard();

        var moves = GenerateAllMoves(isMaximizing);

        if (moves.Count == 0)
            return isMaximizing ? int.MinValue : int.MaxValue;

        if (isMaximizing)
        {
            int maxEval = int.MinValue;
            foreach (var move in moves)
            {
                ApplyMove(move.fromX, move.fromY, move.toX, move.toY, out var capturedPiece);
                int eval = Minimax(depth - 1, false, alpha, beta);

                maxEval = Math.Max(maxEval, eval);
                alpha = Math.Max(alpha, eval);
                if (beta <= alpha) break;
            }
            return maxEval;
        }
        else
        {
            int minEval = int.MaxValue;
            foreach (var move in moves)
            {
                ApplyMove(move.fromX, move.fromY, move.toX, move.toY, out var capturedPiece);
                int eval = Minimax(depth - 1, true, alpha, beta);

                minEval = Math.Min(minEval, eval);
                beta = Math.Min(beta, eval);
                if (beta <= alpha) break;
            }
            return minEval;
        }
    }

    private int EvaluateBoard()
    {
        int score = 0;
        foreach (var row in board.play_board)
        {
            foreach (var piece in row)
            {
                if (piece.whitespace) continue;
                int value = 0;
                if (piece is Pawn) value = 10;
                else if (piece is Knight) value = 30;
                else if (piece is Bishop) value = 30;
                else if (piece is Rook) value = 50;
                else if (piece is Queen) value = 90;
                else if (piece is King) value = 1000;
                else if (piece is Camel) value = 40;


                score += piece.white ? -value : value;
            }
        }
        return score;
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

    private List<SimpleMove> GenerateAllMoves(bool forWhite)
    {
        var moves = new List<SimpleMove>();

        foreach (var row in board.play_board)
        {
            foreach (var piece in row)
            {
                if (!piece.whitespace && piece.white == forWhite)
                {
                    piece.find_possible_moves();
                    piece.find_possible_victims();

                    for (int x = 0; x < 10; x++)
                    {
                        for (int y = 0; y < 10; y++)
                        {
                            if (board.play_board[x][y].movable || board.play_board[x][y].takeable)
                            {
                                moves.Add(new SimpleMove(piece.x, piece.y, x, y));
                            }
                        }
                    }
                }
            }
        }
        return moves;
    }

    private void ApplyMove(int fromX, int fromY, int toX, int toY, out Piece capturedPiece)
    {
        capturedPiece = board.play_board[toX][toY];
        var piece = board.play_board[fromX][fromY];

        board.play_board[toX][toY] = piece;
        board.play_board[fromX][fromY] = new Piece(board, fromX, fromY); // piesa goală

        piece.x = toX;
        piece.y = toY;
    }

    
}
