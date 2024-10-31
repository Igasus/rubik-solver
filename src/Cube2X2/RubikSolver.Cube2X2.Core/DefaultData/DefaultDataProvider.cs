using System;
using RubikSolver.Cube2X2.Core.Models;

namespace RubikSolver.Cube2X2.Core.DefaultData;

public class DefaultDataProvider : IDefaultDataProvider
{
    private static readonly byte[][] DefaultPieceColorIndexes =
    [
        [0, 4, 5],
        [0, 5, 1],
        [0, 1, 2],
        [0, 2, 4],
        [3, 5, 4],
        [3, 1, 5],
        [3, 2, 1],
        [3, 4, 2]
    ];

    public Piece GetPieceByOriginalPositionIndex(byte originalPositionIndex)
    {
        if (originalPositionIndex >= Constants.CubePiecesCount)
            throw new ArgumentException(ErrorMessages.InvalidPiecePositionIndex, nameof(originalPositionIndex));

        var colorIndexes = new PieceColorIndexes(
            DefaultPieceColorIndexes[originalPositionIndex][0],
            DefaultPieceColorIndexes[originalPositionIndex][1],
            DefaultPieceColorIndexes[originalPositionIndex][2]);

        return new Piece(originalPositionIndex, colorIndexes);
    }

    public Piece GetPieceByColors(byte[] colorIndexesClockwise)
    {
        if (colorIndexesClockwise.Length != Constants.PieceColorsCount)
            throw new ArgumentException(ErrorMessages.InvalidPieceColorsCount, nameof(colorIndexesClockwise));

        for (byte i = 0; i < Constants.PieceColorsCount; i++)
        {
            var colorIndex = colorIndexesClockwise[i];
            if (colorIndex >= Constants.CubeColorsCount)
                throw new ArgumentException(ErrorMessages.InvalidColorIndex, $"{nameof(colorIndexesClockwise)}[{i}]");
        }

        for (byte positionIndex = 0; positionIndex < Constants.CubePiecesCount; positionIndex++)
        {
            var colorIndexes = DefaultPieceColorIndexes[positionIndex];
            if (ColorsMatchClockwise(colorIndexesClockwise, colorIndexes))
            {
                var pieceColorIndexes = new PieceColorIndexes(colorIndexes[0], colorIndexes[1], colorIndexes[2]);
                return new Piece(positionIndex, pieceColorIndexes);
            }
        }

        throw new ArgumentException(ErrorMessages.InvalidPieceColorIndexes, nameof(colorIndexesClockwise));
    }

    public Cube GetSolvedCube()
    {
        var pieces = new Piece[Constants.CubePiecesCount];
        for (byte positionIndex = 0; positionIndex < pieces.Length; positionIndex++)
        {
            pieces[positionIndex] = GetPieceByOriginalPositionIndex(positionIndex);
        }

        return new Cube(pieces);
    }

    private bool ColorsMatchClockwise(byte[] actualColorIndexes, byte[] expectedColorIndexes)
    {
        var firstMatchColorIndex = Array.IndexOf(actualColorIndexes, expectedColorIndexes[0]);
        if (firstMatchColorIndex < 0)
            return false;

        for (var i = 1; i < actualColorIndexes.Length; i++)
        {
            var index = (firstMatchColorIndex + i) % actualColorIndexes.Length;
            if (actualColorIndexes[index] != expectedColorIndexes[i])
                return false;
        }

        return true;
    }
}