namespace RubikSolver.WebInterface.Options;

public record CubeSolverApiOptions
{
    public const string Section = "CubeSolverApi";

    public string? Address { get; set; }
}