using RubikSolver.WebInterface.Data.Models;

namespace RubikSolver.WebInterface.Data.Services.Interfaces;

public interface ICube2X2Service
{
    Task<string[]> SolveCubeAsync(Side[] sides);
}