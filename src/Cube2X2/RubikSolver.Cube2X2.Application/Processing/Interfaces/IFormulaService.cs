using RubikSolver.Application.Cube2X2.Data;

namespace RubikSolver.Application.Cube2X2.Processing.Interfaces;

public interface IFormulaService
{
    MoveSign[] GetCompressedFormula(MoveSign[] moves);
    MoveSign[] GetFormulaByHashPath(long[] hashPath);
    void RemoveReverseSymbolFromDoubleMoves(MoveSign[] moves);
}