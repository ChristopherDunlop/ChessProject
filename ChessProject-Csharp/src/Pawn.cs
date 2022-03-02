using System;

namespace SolarWinds.MSP.Chess
{
    public class Pawn
    {
        private ChessBoard chessBoard;
        private int xCoordinate;
        private int yCoordinate;
        private PieceColor pieceColor;

        public ChessBoard ChessBoard
        {
            get { return chessBoard; }
            set { chessBoard = value; }
        }

        public int XCoordinate
        {
            get { return xCoordinate; }
            set { xCoordinate = value; }
        }

        public int YCoordinate
        {
            get { return yCoordinate; }
            set { yCoordinate = value; }
        }

        public PieceColor PieceColor
        {
            get { return pieceColor; }
            private set { pieceColor = value; }
        }

        public Pawn(PieceColor pieceColor)
        {
            this.pieceColor = pieceColor;
        }

        ///<summary>
        ///Validates the requested move then updates the Pawns Location.
        ///</summary>
        public void Move(MovementType movementType, int newX, int newY)
        {
            if (ChessBoard.IsLegalBoardPosition(newX, newY) && ChessBoard.PositionEmpty(YCoordinate, XCoordinate))
            {
                switch (movementType)
                {
                    case MovementType.Move:
                        if (LegalMove_(newX,newY))
                        {
                            XCoordinate = newX;
                            YCoordinate = newY;
                            Console.WriteLine(ToString());
                        }
                        break;
                    case MovementType.Capture:
                        throw new NotImplementedException("Movement Type not implemented!");
                        break;
                    default:
                        throw new NotImplementedException("Incorrect Movement Type!");
                        break;
                }
            }
        }

        ///<summary>
        ///Returns if the requested coordinates equates to a legal pawn move.
        ///</summary>
        private bool LegalMove_(int newX, int newY)
        {
            bool bLegal = false;
            if (ChessBoard.IsLegalBoardPosition(newX, newY)
                && ChessBoard.PositionEmpty(YCoordinate, XCoordinate)
                && XCoordinate == newX)
            {
                int requestedDistance = YCoordinate - newY;
                int allowedDistance = 1;
                //Checks if this is the pawns first move.
                if ((pieceColor == PieceColor.Black && XCoordinate == 6)
                    || (pieceColor == PieceColor.White && XCoordinate == 1))
                {
                    allowedDistance = 2;
                }

                //Check if the requested movement is Legal taking into account the correct direction for the black and white pieces to move.
                if ((pieceColor == PieceColor.Black && requestedDistance > 0 && requestedDistance <= allowedDistance)
                    || (pieceColor == PieceColor.White && requestedDistance < 0 && requestedDistance >= (allowedDistance * -1)))
                    bLegal = true;
             }

            return bLegal;
        }

        //Implement LegalCapture_() here.

        public override string ToString()
        {
            return CurrentPositionAsString();
        }

        protected string CurrentPositionAsString()
        {
            return string.Format("Current X: {1}{0}Current Y: {2}{0}Piece Color: {3}", Environment.NewLine, XCoordinate, YCoordinate, PieceColor);
        }

    }
}