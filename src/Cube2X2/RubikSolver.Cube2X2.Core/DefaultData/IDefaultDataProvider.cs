using RubikSolver.Cube2X2.Core.Models;

namespace RubikSolver.Cube2X2.Core.DefaultData;

public interface IDefaultDataProvider
{
    Piece GetPieceByOriginalPositionIndex(byte originalPositionIndex);
    Piece GetPieceByColors(byte[] colorIndexesClockwise);
    Cube GetSolvedCube();
}