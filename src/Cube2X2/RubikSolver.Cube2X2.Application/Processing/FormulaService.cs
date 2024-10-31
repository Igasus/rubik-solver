using RubikSolver.Application.Cube2X2.Data;
using RubikSolver.Application.Cube2X2.Hashing;
using RubikSolver.Application.Cube2X2.Processing.Interfaces;
using RubikSolver.Cube2X2.Core;
using RubikSolver.Cube2X2.Core.Models;

namespace RubikSolver.Application.Cube2X2.Processing;

public class FormulaService : IFormulaService
{
    private readonly ICubeHashManager _cubeHashManager;

    public FormulaService(ICubeHashManager cubeHashManager)
    {
        ArgumentNullException.ThrowIfNull(cubeHashManager);
        _cubeHashManager = cubeHashManager;
    }

    public MoveSign[] GetCompressedFormula(MoveSign[] moves)
    {
        List<MoveSign> resultFormula = [moves[0]];

        for (var i = 1; i < moves.Length; i++)
        {
            switch (moves[i])
            {
                case MoveSign.U when resultFormula[^1] is MoveSign.U:
                    resultFormula[^1] = MoveSign.U2;
                    break;
                case MoveSign.U when resultFormula[^1] is MoveSign.U2:
                    resultFormula[^1] = MoveSign.Ur;
                    break;
                case MoveSign.Ur when resultFormula[^1] is MoveSign.Ur:
                    resultFormula[^1] = MoveSign.Ur2;
                    break;
                case MoveSign.Ur when resultFormula[^1] is MoveSign.Ur2:
                    resultFormula[^1] = MoveSign.U;
                    break;
                case MoveSign.R when resultFormula[^1] is MoveSign.R:
                    resultFormula[^1] = MoveSign.R2;
                    break;
                case MoveSign.R when resultFormula[^1] is MoveSign.R2:
                    resultFormula[^1] = MoveSign.Rr;
                    break;
                case MoveSign.Rr when resultFormula[^1] is MoveSign.Rr:
                    resultFormula[^1] = MoveSign.Rr2;
                    break;
                case MoveSign.Rr when resultFormula[^1] is MoveSign.Rr2:
                    resultFormula[^1] = MoveSign.R;
                    break;
                case MoveSign.F when resultFormula[^1] is MoveSign.F:
                    resultFormula[^1] = MoveSign.F2;
                    break;
                case MoveSign.F when resultFormula[^1] is MoveSign.F2:
                    resultFormula[^1] = MoveSign.Fr;
                    break;
                case MoveSign.Fr when resultFormula[^1] is MoveSign.Fr:
                    resultFormula[^1] = MoveSign.Fr2;
                    break;
                case MoveSign.Fr when resultFormula[^1] is MoveSign.Fr2:
                    resultFormula[^1] = MoveSign.F;
                    break;
                default:
                    resultFormula.Add(moves[i]);
                    break;
            }
        }

        return resultFormula.ToArray();
    }

    public MoveSign[] GetFormulaByHashPath(long[] hashPath)
    {
        if (hashPath.Length <= 1)
            throw new ArgumentException();

        var formula = new List<MoveSign>();
        for (var i = 0; i < hashPath.Length - 1; i++)
            formula.Add(GetMove(hashPath[i], hashPath[i + 1]));

        return formula.ToArray();
    }

    public void RemoveReverseSymbolFromDoubleMoves(MoveSign[] moves)
    {
        for (var i = 0; i < moves.Length; i++)
        {
            switch (moves[i])
            {
                case MoveSign.Ur2:
                    moves[i] = MoveSign.U2;
                    break;
                case MoveSign.Rr2:
                    moves[i] = MoveSign.R2;
                    break;
                case MoveSign.Fr2:
                    moves[i] = MoveSign.F2;
                    break;
            }
        }
    }

    private MoveSign GetMove(long startHash, long endHash)
    {
        using var startCube = _cubeHashManager.GetCube(startHash);
        using var endCube = _cubeHashManager.GetCube(endHash);

        if (RotationUpUsed(startCube, endCube))
            return MoveSign.U;
        if (RotationRightUsed(startCube, endCube))
            return MoveSign.R;
        if (RotationFrontUsed(startCube, endCube))
            return MoveSign.F;
        if (RotationUpReverseUsed(startCube, endCube))
            return MoveSign.Ur;
        if (RotationRightReverseUsed(startCube, endCube))
            return MoveSign.Rr;
        if (RotationFrontReverseUsed(startCube, endCube))
            return MoveSign.Fr;

        throw new Exception("Move not found.");
    }

    private bool RotationUpUsed(Cube startCube, Cube endCube)
    {
        using var cube = startCube.Copy();
        cube.RotateUp();
        return CubesEqual(cube, endCube);
    }

    private bool RotationRightUsed(Cube startCube, Cube endCube)
    {
        using var cube = startCube.Copy();
        cube.RotateRight();
        return CubesEqual(cube, endCube);
    }

    private bool RotationFrontUsed(Cube startCube, Cube endCube)
    {
        using var cube = startCube.Copy();
        cube.RotateFront();
        return CubesEqual(cube, endCube);
    }

    private bool RotationUpReverseUsed(Cube startCube, Cube endCube)
    {
        using var cube = startCube.Copy();
        cube.RotateUpReverse();
        return CubesEqual(cube, endCube);
    }

    private bool RotationRightReverseUsed(Cube startCube, Cube endCube)
    {
        using var cube = startCube.Copy();
        cube.RotateRightReverse();
        return CubesEqual(cube, endCube);
    }

    private bool RotationFrontReverseUsed(Cube startCube, Cube endCube)
    {
        using var cube = startCube.Copy();
        cube.RotateFrontReverse();
        return CubesEqual(cube, endCube);
    }
    
    private bool CubesEqual(Cube cube1, Cube cube2)
    {
        for (byte currentPositionIndex = 0; currentPositionIndex < Constants.CubePiecesCount; currentPositionIndex++)
        {
            var cube1Piece = cube1.Pieces[currentPositionIndex];
            var cube2Piece = cube2.Pieces[currentPositionIndex];
            if (cube1Piece.OriginalPositionIndex != cube2Piece.OriginalPositionIndex ||
                cube1Piece.RotationIndex != cube2Piece.RotationIndex)
                return false;
        }

        return true;
    }
}