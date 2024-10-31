namespace RubikSolver.Cube2X2.Core;

public static class ErrorMessages
{
    public const string InvalidPieceColorIndex = "Piece Color Index is expected to be in range [0-2]";
    public const string InvalidPieceColorsCount = "Piece Colors Count is expected to be 3";
    public const string InvalidColorIndex = "Color Index is expected to be in range [0-5]";
    public const string InvalidPiecePositionIndex = "Piece Position Index is expected to be in range [0-7]. Received {0}";
    public const string InvalidPiecesCount = "Pieces count is expected to be 8";
    public const string InvalidPieceColorIndexes = "Piece with provided Color Indexes and their order does not exist";
    public const string InvalidSidesCount = "Sides Count is expected to be 6";
    public const string InvalidColorsCount = "Colors Count is expected to be 6";
    public const string UndefinedColor = "Provided Color is not defined in enum";
}