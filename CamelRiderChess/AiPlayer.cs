using CamelRiderChess;
using System;
using System.Collections.Generic;

public class AiPlayer
{
    //private Board board;

    //public AiPlayer(Board board)
    //{
    //    this.board = board;
    //}

    //public void MakeMove()
    //{
    //    int bestFromX = -1, bestFromY = -1, bestToX = -1, bestToY = -1;
    //    int maxEval = int.MinValue;
    //    int depth = 3;

    //    // Generăm toate mutările negre
    //    foreach (var move in GenerateAllMoves(false)) // false = piesele negre
    //    {
    //        // Aplicăm mutarea
    //        ApplyMove(move.fromX, move.fromY, move.toX, move.toY, out var capturedPiece);

    //        // Calculăm evaluarea folosind Minimax
    //        int eval = Minimax(depth - 1, true, int.MinValue, int.MaxValue);



    //        if (eval > maxEval)
    //        {
    //            maxEval = eval;
    //            bestFromX = move.fromX;
    //            bestFromY = move.fromY;
    //            bestToX = move.toX;
    //            bestToY = move.toY;
    //        }
    //    }
    //    if (bestFromX != -1)
    //    {
    //        ApplyMove(bestFromX, bestFromY, bestToX, bestToY, out _);
    //        board.redraw_play_area(board.play_board[bestToX][bestToY]); // forțează redraw (sau un alt mecanism)
    //    }


    //}

    //private int Minimax(int depth, bool isMaximizing, int alpha, int beta)
    //{
    //    if (depth == 0)
    //        return EvaluateBoard();

    //    var moves = GenerateAllMoves(isMaximizing);

    //    if (moves.Count == 0)
    //        return isMaximizing ? int.MinValue : int.MaxValue;

    //    if (isMaximizing)
    //    {
    //        int maxEval = int.MinValue;
    //        foreach (var move in moves)
    //        {
    //            ApplyMove(move.fromX, move.fromY, move.toX, move.toY, out var capturedPiece);
    //            int eval = Minimax(depth - 1, false, alpha, beta);

    //            maxEval = Math.Max(maxEval, eval);
    //            alpha = Math.Max(alpha, eval);
    //            if (beta <= alpha) break;
    //        }
    //        return maxEval;
    //    }
    //    else
    //    {
    //        int minEval = int.MaxValue;
    //        foreach (var move in moves)
    //        {
    //            ApplyMove(move.fromX, move.fromY, move.toX, move.toY, out var capturedPiece);
    //            int eval = Minimax(depth - 1, true, alpha, beta);

    //            minEval = Math.Min(minEval, eval);
    //            beta = Math.Min(beta, eval);
    //            if (beta <= alpha) break;
    //        }
    //        return minEval;
    //    }
    //}

    //private int EvaluateBoard()
    //{
    //    int score = 0;
    //    foreach (var row in board.play_board)
    //    {
    //        foreach (var piece in row)
    //        {
    //            if (piece.whitespace) continue;
    //            int value = 0;
    //            if (piece is Pawn) value = 10;
    //            else if (piece is Knight) value = 30;
    //            else if (piece is Bishop) value = 30;
    //            else if (piece is Rook) value = 50;
    //            else if (piece is Queen) value = 90;
    //            else if (piece is King) value = 1000;
    //            else if (piece is Camel) value = 40;


    //            score += piece.white ? -value : value;
    //        }
    //    }
    //    return score;
    //}

    //private struct SimpleMove
    //{
    //    public int fromX, fromY, toX, toY;
    //    public SimpleMove(int fx, int fy, int tx, int ty)
    //    {
    //        fromX = fx;
    //        fromY = fy;
    //        toX = tx;
    //        toY = ty;
    //    }
    //}

    //private List<SimpleMove> GenerateAllMoves(bool forWhite)
    //{
    //    var moves = new List<SimpleMove>();

    //    foreach (var row in board.play_board)
    //    {
    //        foreach (var piece in row)
    //        {
    //            if (!piece.whitespace && piece.white == forWhite)
    //            {
    //                piece.find_possible_moves();
    //                piece.find_possible_victims();

    //                for (int x = 0; x < 10; x++)
    //                {
    //                    for (int y = 0; y < 10; y++)
    //                    {
    //                        if (board.play_board[x][y].movable || board.play_board[x][y].takeable)
    //                        {
    //                            moves.Add(new SimpleMove(piece.x, piece.y, x, y));
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    return moves;
    //}

    //private void ApplyMove(int fromX, int fromY, int toX, int toY, out Piece capturedPiece)
    //{
    //    capturedPiece = board.play_board[toX][toY];
    //    var piece = board.play_board[fromX][fromY];

    //    board.play_board[toX][toY] = piece;
    //    board.play_board[fromX][fromY] = new Piece(board, fromX, fromY); // piesa goală

    //    piece.x = toX;
    //    piece.y = toY;
    //}


        private Board board;
        private int maxDepth = 3; // Poți ajusta pentru dificultate

        public AiPlayer(Board board)
        {
            this.board = board;
        }

        public void MakeMove()
        {
            var bestMove = GetBestMove();
            if (bestMove != null)
            {
                // Simulăm mutarea pe interfață
                var piece = board.play_board[bestMove.FromX][bestMove.FromY];
                var target = board.play_board[bestMove.ToX][bestMove.ToY];

                board.selected = piece;
                target.movable = true;
                target.takeable = bestMove.IsCapture;

                piece.move();
            }
        }

        private Move GetBestMove()
        {
            List<Move> possibleMoves = GenerateAllMoves(false); // AI e negru
            Move bestMove = null;
            int bestScore = int.MaxValue;

            foreach (var move in possibleMoves)
            {
                var state = SimulateMove(move);
                int score = Minimax(maxDepth - 1, true, int.MinValue, int.MaxValue);
                if (score < bestScore)
                {
                    bestScore = score;
                    bestMove = move;
                }
            }

            return bestMove;
        }

        private int Minimax(int depth, bool isMaximizing, int alpha, int beta)
        {
            if (depth == 0 || IsGameOver())
            {
                return EvaluateBoard();
            }

            List<Move> moves = GenerateAllMoves(isMaximizing);

            if (isMaximizing)
            {
                int maxEval = int.MinValue;
                foreach (var move in moves)
                {
                    var state = SimulateMove(move);
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
                    var state = SimulateMove(move);
                    int eval = Minimax(depth - 1, true, alpha, beta);
                    minEval = Math.Min(minEval, eval);
                    beta = Math.Min(beta, eval);
                    if (beta <= alpha) break;
                }
                return minEval;
            }
        }

        private List<Move> GenerateAllMoves(bool forWhite)
        {
            List<Move> moves = new List<Move>();

            foreach (var row in board.play_board)
            {
                foreach (var piece in row)
                {
                    if (piece.whitespace || piece.white != forWhite)
                        continue;

                    piece.find_possible_moves();
                    piece.find_possible_victims();

                    for (int i = 0; i < 10; i++)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            if (board.play_board[i][j].movable || board.play_board[i][j].takeable)
                            {
                                if (!board.WouldBeInCheckAfterMove(piece, i, j))
                                {
                                    moves.Add(new Move
                                    {
                                        FromX = piece.x,
                                        FromY = piece.y,
                                        ToX = i,
                                        ToY = j,
                                        IsCapture = board.play_board[i][j].takeable
                                    });
                                }
                            }
                        }
                    }

                    board.reset_play_board(); // resetăm starea
                }
            }

            return moves;
        }

        private (Piece[][] state, Piece capturedPiece) SimulateMove(Move move)
        {
            var piece = board.play_board[move.FromX][move.FromY];
            var target = board.play_board[move.ToX][move.ToY];

            Piece captured = target.whitespace ? null : target;
            board.play_board[move.ToX][move.ToY] = piece;
            board.play_board[move.FromX][move.FromY] = new Piece(board, move.FromX, move.FromY);
            piece.x = move.ToX;
            piece.y = move.ToY;

            return (board.play_board, captured);
        }

        

        private bool IsGameOver()
        {
            return !board.HasAnyValidMove(true) || !board.HasAnyValidMove(false);
        }

        private int EvaluateBoard()
        {
            int score = 0;

            foreach (var row in board.play_board)
            {
                foreach (var piece in row)
                {
                    if (piece.whitespace) continue;

                    int value = GetPieceValue(piece);

                    if (piece.white)
                        score += value;
                    else
                        score -= value;
                }
            }

            return score;
        }

        private int GetPieceValue(Piece piece)
        {
            if (piece is King) return 10000;
            if (piece is Queen) return 900;
            if (piece is Rook) return 500;
            if (piece is Bishop) return 300;
            if (piece is Knight) return 300;
            if (piece is Camel) return 250;
            if (piece is Pawn) return 100;
            return 0;
        }

        private class Move
        {
            public int FromX, FromY, ToX, ToY;
            public bool IsCapture;
        }
    }




