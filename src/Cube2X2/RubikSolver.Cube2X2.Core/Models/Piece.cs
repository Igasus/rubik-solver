using System;
using System.Threading.Tasks;

namespace RubikSolver.Cube2X2.Core.Models;

public class Piece : IDisposable, IAsyncDisposable
{
    public byte OriginalPositionIndex { get; }
    public byte CurrentPositionIndex { get; private set; }
    public byte RotationIndex { get; private set; }
    public PieceColorIndexes ColorIndexes { get; }

    public Piece(byte originalPositionIndex, PieceColorIndexes colorIndexes)
    {
        if (originalPositionIndex >= Constants.CubePiecesCount)
        {
            throw new ArgumentException(
                string.Format(ErrorMessages.InvalidPiecePositionIndex, originalPositionIndex),
                nameof(originalPositionIndex));
        }

        ArgumentNullException.ThrowIfNull(colorIndexes);

        OriginalPositionIndex = originalPositionIndex;
        ColorIndexes = colorIndexes;
        CurrentPositionIndex = originalPositionIndex;
        RotationIndex = 0;
    }

    public Piece Copy()
    {
        return new Piece(OriginalPositionIndex, ColorIndexes.Copy())
        {
            CurrentPositionIndex = CurrentPositionIndex,
            RotationIndex = RotationIndex
        };
    }

    public void Move(byte positionIndex, int rotationIndexShift)
    {
        if (positionIndex >= Constants.CubePiecesCount)
        {
            throw new ArgumentException(
                string.Format(ErrorMessages.InvalidPiecePositionIndex, positionIndex),
                nameof(positionIndex));
        }

        var shortcutRotationIndexShift = rotationIndexShift % Constants.PieceColorsCount;
        var tempRotationIndex = RotationIndex + shortcutRotationIndexShift;
        RotationIndex = (byte)((tempRotationIndex + Constants.PieceColorsCount) % Constants.PieceColorsCount);
        CurrentPositionIndex = positionIndex;
    }

    public void Dispose()
    {
        ColorIndexes.Dispose();
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await ColorIndexes.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}