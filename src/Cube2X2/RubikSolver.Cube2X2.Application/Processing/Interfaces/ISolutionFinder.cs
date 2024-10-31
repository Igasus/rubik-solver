using RubikSolver.Cube2X2.Application.Data;
using RubikSolver.Cube2X2.Core.Models;

namespace RubikSolver.Cube2X2.Application.Processing.Interfaces;

public interface ISolutionFinder
{
    MoveSign[] FindFormula(Cube cubeStart, Cube cubeEnd);
}