namespace RubikSolver.WebInterface.Data.Models;

public enum Color
{
    White = 1,
    Blue,
    Red,
    Yellow,
    Green,
    Orange
}

public static class ColorExtensions
{
    public static string GetBackgroundClass(this Color color)
    {
        return color switch
        {
            Color.White => "bg-white",
            Color.Blue => "bg-blue-600",
            Color.Red => "bg-red-600",
            Color.Yellow => "bg-yellow-600",
            Color.Green => "bg-green-600",
            Color.Orange => "bg-orange-600",
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
        };
    }
}