using RubikSolver.Application.Cube2X2.Data;

namespace RubikSolver.Api.Models;

public record Find2X2SolutionRequest
{
    public Find2X2SolutionRequestSide[] Sides { get; set; } = [];
}

public record Find2X2SolutionRequestSide
{
    public Color[][] Colors { get; set; } = [];
}