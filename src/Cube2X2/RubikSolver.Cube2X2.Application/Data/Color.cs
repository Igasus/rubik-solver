using System;
using RubikSolver.Cube2X2.Core;

namespace RubikSolver.Cube2X2.Application.Data;

public enum Color
{
    White = 1,
    Yellow = 2,
    Green = 3,
    Blue = 4,
    Red = 5,
    Orange = 6
}

public static class ColorExtensions
{
    public static Color GetOpposite(this Color color)
    {
        return color switch
        {
            Color.White => Color.Yellow,
            Color.Yellow => Color.White,
            Color.Green => Color.Blue,
            Color.Blue => Color.Green,
            Color.Red => Color.Orange,
            Color.Orange => Color.Red,
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, ErrorMessages.UndefinedColor)
        };
    }
}