namespace RubikSolver.WebInterface.Data.Models;

public record SolveCube2X2Request
{
    public required Side[] Sides { get; init; }
}