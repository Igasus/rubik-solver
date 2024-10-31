using RubikSolver.Application.Cube2X2.Data;
using RubikSolver.Cube2X2.Core.Models;

namespace RubikSolver.Application.Cube2X2.Processing.Interfaces;

public interface ISolutionFinder
{
    MoveSign[] FindFormula(Cube cubeStart, Cube cubeEnd);
}