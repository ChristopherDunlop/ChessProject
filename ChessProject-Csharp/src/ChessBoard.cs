using System;

namespace SolarWinds.MSP.Chess
{
    public class ChessBoard
    {
        public static readonly int MaxBoardWidth = 7; //Board should be 8 wide not 7. Highest array element is 7 this could be better named.
        public static readonly int MaxBoardHeight = 7; //Same as above.
        private static readonly int MaxPawnCount = 8;

        private Pawn[,] pieces;

        public ChessBoard()
        {
            pieces = new Pawn[MaxBoardWidth + 1, MaxBoardHeight + 1];
        }

        ///<summary>
        ///Validates the requested add then adds a Pawn to the board.
        ///</summary>
        public void Add(Pawn pawn, int xCoordinate, int yCoordinate, PieceColor pieceColor) //PieceColor parameter not required. Tests should be altered as piece colour private so adds nothing here.
        {
            if (IsLegalBoardPosition(xCoordinate, yCoordinate)
                && (PositionEmpty(xCoordinate, yCoordinate))
                && ((xCoordinate == 6 && pawn.PieceColor == PieceColor.Black)
                || (xCoordinate == 1 && pawn.PieceColor == PieceColor.White)))
            {
                pawn.ChessBoard = this;
                pawn.XCoordinate = xCoordinate;
                pawn.YCoordinate = yCoordinate;
                pieces[xCoordinate, yCoordinate] = pawn;
                Console.WriteLine(pawn.ToString());
            }
            else
            {
                pawn.XCoordinate = -1;
                pawn.YCoordinate = -1;
            }
        }

        ///<summary>
        ///Returns if the position on the board is empty.
        ///</summary>
        public bool PositionEmpty(int xCoordinate, int yCoordinate)
        {
            return pieces[xCoordinate, yCoordinate] == null;
        }

        ///<summary>
        ///Returns if the requested positions is withing the bounds of the board.
        ///</summary>
        public bool IsLegalBoardPosition(int xCoordinate, int yCoordinate)
        {
            return (CoordinateWithinRange_(xCoordinate) && CoordinateWithinRange_(yCoordinate));
        }

        ///<summary>
        ///Returns if the requested position is withing the bounds of the board.
        ///</summary>
        private bool CoordinateWithinRange_(int coordinate)
        {
            return (coordinate >= 0 && coordinate <= MaxBoardWidth);
        }

        ///<summary>
        ///Returns if the maximum number of pawns has been reached.
        ///</summary>
        private bool MaximumPiecesReached_(PieceColor pieceColor)
        {
            int pieceCount = 0;

            for (int x = 0; x < MaxBoardWidth; x++)
            {
                for (int y = 0; y < MaxBoardHeight; y++)
                {
                    if (pieces[x, y] != null)
                    {
                        if (pieces[x, y].PieceColor == pieceColor)
                            pieceCount++;
                    }
                }
            }
            return (pieceCount == MaxPawnCount);
        }
    }
}
