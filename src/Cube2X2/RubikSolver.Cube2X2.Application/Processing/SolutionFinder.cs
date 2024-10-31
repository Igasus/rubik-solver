using RubikSolver.Application.Cube2X2.Data;
using RubikSolver.Application.Cube2X2.Hashing;
using RubikSolver.Application.Cube2X2.Processing.Interfaces;
using RubikSolver.Cube2X2.Core.Models;

namespace RubikSolver.Application.Cube2X2.Processing;

public class SolutionFinder : ISolutionFinder
{
    private readonly ICubeHashManager _cubeHashManager;
    private readonly IFormulaService _formulaService;

    public SolutionFinder(ICubeHashManager cubeHashManager, IFormulaService formulaService)
    {
        ArgumentNullException.ThrowIfNull(cubeHashManager);
        ArgumentNullException.ThrowIfNull(formulaService);
        
        _cubeHashManager = cubeHashManager;
        _formulaService = formulaService;
    }

    public MoveSign[] FindFormula(Cube cubeStart, Cube cubeEnd)
    {
        var hashPath = GetHashPath(cubeStart, cubeEnd);
        var formula = _formulaService.GetFormulaByHashPath(hashPath);
        formula = _formulaService.GetCompressedFormula(formula);

        return formula;
    }

    private long[] GetHashPath(Cube cubeStart, Cube cubeEnd)
    {
        using var directFlowTraverser = new CubeSolutionTraverser(_cubeHashManager, cubeStart);
        using var counterFlowTraverser = new CubeSolutionTraverser(_cubeHashManager, cubeEnd);

        var directMoves = new List<long>();
        var counterMoves = new List<long>();

        while (true)
        {
            var directMovesFound = directFlowTraverser.Next(in directMoves);
            if (directMovesFound)
            {
                foreach (var move in directMoves)
                {
                    if (counterFlowTraverser.SolutionTree.ContainsKey(move))
                        return BuildHashPath(directFlowTraverser.SolutionTree, counterFlowTraverser.SolutionTree, move);
                }
            }

            var counterMovesFound = counterFlowTraverser.Next(in counterMoves);
            if (counterMovesFound)
            {
                foreach (var move in counterMoves)
                {
                    if (directFlowTraverser.SolutionTree.ContainsKey(move))
                        return BuildHashPath(directFlowTraverser.SolutionTree, counterFlowTraverser.SolutionTree, move);
                }
            }

            if (!directMovesFound && !counterMovesFound)
                break;
        }

        throw new Exception("Solution not found");
    }

    private long[] BuildHashPath(
        Dictionary<long, long> directFlowSolutionTree,
        Dictionary<long, long> counterFlowSolutionTree,
        long keyCubeHash)
    {
        var directPath = BuildReversedHashPath(directFlowSolutionTree, keyCubeHash).Reverse();
        var counterPath = BuildReversedHashPath(counterFlowSolutionTree, keyCubeHash).ToList();
        long[] fullPath = [..directPath, ..counterPath[1..]];

        return fullPath;
    }

    private IEnumerable<long> BuildReversedHashPath(Dictionary<long, long> solutionTree, long startPoint)
    {
        while (startPoint != -1)
        {
            yield return startPoint;
            startPoint = solutionTree[startPoint];
        }
    }
}