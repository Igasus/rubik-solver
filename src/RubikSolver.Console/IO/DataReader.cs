using RubikSolver.Application.Cube2X2.Data;

namespace RubikSolver.Console.IO;

public class DataReader
{
    public CubeView ReadCubeView()
    {
        System.Console.WriteLine("Enter cube colors in format:");
        System.Console.WriteLine(
            """
            ..ee
            ..ee
            aabbccdd
            aabbccdd
            ..ff
            ..ff

            """);
        
        System.Console.Write("..");
        var s4Top = System.Console.ReadLine();
        System.Console.Write("..");
        var s4Bottom = System.Console.ReadLine();
        var s0123Top = System.Console.ReadLine();
        var s0123Bottom = System.Console.ReadLine();
        System.Console.Write("..");
        var s5Top = System.Console.ReadLine();
        System.Console.Write("..");
        var s5Bottom = System.Console.ReadLine();

        if (string.IsNullOrWhiteSpace(s4Top) || s4Top.Length != 2 ||
            string.IsNullOrWhiteSpace(s4Bottom) || s4Bottom.Length != 2 ||
            string.IsNullOrWhiteSpace(s0123Top) || s0123Top.Length != 8 ||
            string.IsNullOrWhiteSpace(s0123Bottom) || s0123Bottom.Length != 8 ||
            string.IsNullOrWhiteSpace(s5Top) || s5Top.Length != 2 ||
            string.IsNullOrWhiteSpace(s5Bottom) || s5Bottom.Length != 2)
            throw new Exception("Invalid input format.");

        Side[] sides =
        [
            new(ColorOf(s0123Top[0]), ColorOf(s0123Top[1]), ColorOf(s0123Bottom[0]), ColorOf(s0123Bottom[1])),
            new(ColorOf(s0123Top[2]), ColorOf(s0123Top[3]), ColorOf(s0123Bottom[2]), ColorOf(s0123Bottom[3])),
            new(ColorOf(s0123Top[4]), ColorOf(s0123Top[5]), ColorOf(s0123Bottom[4]), ColorOf(s0123Bottom[5])),
            new(ColorOf(s0123Top[6]), ColorOf(s0123Top[7]), ColorOf(s0123Bottom[6]), ColorOf(s0123Bottom[7])),
            new(ColorOf(s4Top[0]), ColorOf(s4Top[1]), ColorOf(s4Bottom[0]), ColorOf(s4Bottom[1])),
            new(ColorOf(s5Top[0]), ColorOf(s5Top[1]), ColorOf(s5Bottom[0]), ColorOf(s5Bottom[1]))
        ];

        return new CubeView(sides);
    }

    private Color ColorOf(char symbol)
    {
        return symbol switch
        {
            'w' => Color.White,
            'y' => Color.Yellow,
            'g' => Color.Green,
            'b' => Color.Blue,
            'r' => Color.Red,
            'o' => Color.Orange,
            _ => throw new ArgumentOutOfRangeException(nameof(symbol), $"Symbol '{symbol}' represents undefined color.")
        };
    }
}