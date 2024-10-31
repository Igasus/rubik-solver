using RubikSolver.Application.Cube2X2.Data;

namespace RubikSolver.Console.IO;

public class DataWriter
{
    public void DisplayView(CubeView cubeView)
    {
        ArgumentNullException.ThrowIfNull(cubeView);
        
        System.Console.WriteLine($"..{SymbolOf(cubeView.Sides[4].TileColors[0][0])}{SymbolOf(cubeView.Sides[4].TileColors[0][1])}");
        System.Console.WriteLine($"..{SymbolOf(cubeView.Sides[4].TileColors[1][0])}{SymbolOf(cubeView.Sides[4].TileColors[1][1])}");
        System.Console.Write($"{SymbolOf(cubeView.Sides[0].TileColors[0][0])}{SymbolOf(cubeView.Sides[0].TileColors[0][1])}");
        System.Console.Write($"{SymbolOf(cubeView.Sides[1].TileColors[0][0])}{SymbolOf(cubeView.Sides[1].TileColors[0][1])}");
        System.Console.Write($"{SymbolOf(cubeView.Sides[2].TileColors[0][0])}{SymbolOf(cubeView.Sides[2].TileColors[0][1])}");
        System.Console.WriteLine($"{SymbolOf(cubeView.Sides[3].TileColors[0][0])}{SymbolOf(cubeView.Sides[3].TileColors[0][1])}");
        System.Console.Write($"{SymbolOf(cubeView.Sides[0].TileColors[1][0])}{SymbolOf(cubeView.Sides[0].TileColors[1][1])}");
        System.Console.Write($"{SymbolOf(cubeView.Sides[1].TileColors[1][0])}{SymbolOf(cubeView.Sides[1].TileColors[1][1])}");
        System.Console.Write($"{SymbolOf(cubeView.Sides[2].TileColors[1][0])}{SymbolOf(cubeView.Sides[2].TileColors[1][1])}");
        System.Console.WriteLine($"{SymbolOf(cubeView.Sides[3].TileColors[1][0])}{SymbolOf(cubeView.Sides[3].TileColors[1][1])}");
        System.Console.WriteLine($"..{SymbolOf(cubeView.Sides[5].TileColors[0][0])}{SymbolOf(cubeView.Sides[5].TileColors[0][1])}");
        System.Console.WriteLine($"..{SymbolOf(cubeView.Sides[5].TileColors[1][0])}{SymbolOf(cubeView.Sides[5].TileColors[1][1])}");
    }

    private char SymbolOf(Color color)
    {
        return color switch
        {
            Color.White => 'w',
            Color.Yellow => 'y',
            Color.Green => 'g',
            Color.Blue => 'b',
            Color.Red => 'r',
            Color.Orange => 'o',
            _ => throw new ArgumentOutOfRangeException(nameof(color))
        };
    }
}