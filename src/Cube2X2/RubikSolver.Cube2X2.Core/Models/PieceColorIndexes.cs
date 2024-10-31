using System;
using System.Threading.Tasks;

namespace RubikSolver.Cube2X2.Core.Models;

public class PieceColorIndexes : IDisposable, IAsyncDisposable
{
    public byte Primary { get; }
    public byte Secondary { get; }
    public byte Tertiary { get; }

    public PieceColorIndexes(byte primary, byte secondary, byte tertiary)
    {
        if (primary >= Constants.CubeColorsCount)
            throw new ArgumentException(ErrorMessages.InvalidColorIndex, nameof(primary));

        if (secondary >= Constants.CubeColorsCount)
            throw new ArgumentException(ErrorMessages.InvalidColorIndex, nameof(secondary));

        if (tertiary >= Constants.CubeColorsCount)
            throw new ArgumentException(ErrorMessages.InvalidColorIndex, nameof(tertiary));
                
        Primary = primary;
        Secondary = secondary;
        Tertiary = tertiary;
    }

    public PieceColorIndexes Copy()
    {
        return new PieceColorIndexes(Primary, Secondary, Tertiary);
    }

    public byte this[byte index]
    {
        get
        {
            return index switch
            {
                0 => Primary,
                1 => Secondary,
                2 => Tertiary,
                _ => throw new ArgumentOutOfRangeException(nameof(index), index, ErrorMessages.InvalidPieceColorIndex)
            };
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return default;
    }
}