using System.Collections.Generic;

namespace RubikSolver.Api.Models;

public record Find2X2SolutionResponse
{
    public required IEnumerable<string> Formula { get; set; }
}