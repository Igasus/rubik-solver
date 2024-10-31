using RubikSolver.Cube2X2.Application.Data;

namespace RubikSolver.Cube2X2.Application.Processing.Interfaces;

public interface IFormulaService
{
    MoveSign[] GetCompressedFormula(MoveSign[] moves);
    MoveSign[] GetFormulaByHashPath(long[] hashPath);
    void RemoveReverseSymbolFromDoubleMoves(MoveSign[] moves);
}