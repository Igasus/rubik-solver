namespace RubikSolver.WebInterface.Data.Models;

public record Side
{
    public Color[][] Colors { get; set; } = [[Color.White, Color.White], [Color.White, Color.White]];
}