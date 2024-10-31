using System;
using RubikSolver.Cube2X2.Core;
using RubikSolver.Cube2X2.Core.DefaultData;
using RubikSolver.Cube2X2.Core.Models;

namespace RubikSolver.Cube2X2.Application.Hashing;

public class CubeHashManager : ICubeHashManager
{
    private readonly IDefaultDataProvider _defaultDataProvider;

    public CubeHashManager(IDefaultDataProvider defaultDataProvider)
    {
        ArgumentNullException.ThrowIfNull(defaultDataProvider);
        _defaultDataProvider = defaultDataProvider;
    }

    public long GetHash(Cube cube)
    {
        ArgumentNullException.ThrowIfNull(cube);

        long hash = 0;

        foreach (var piece in cube.Pieces)
        {
            var pieceHash = 3 * piece.OriginalPositionIndex + piece.RotationIndex;
            hash = hash * 24 + pieceHash;
        }

        return hash;
    }

    public Cube GetCube(long hash)
    {
        var pieces = new Piece[Constants.CubePiecesCount];

        for (var currentPositionIndex = pieces.Length - 1; currentPositionIndex >= 0; currentPositionIndex--)
        {
            var pieceHash = hash % 24;
            var pieceRotationIndex = (byte)(pieceHash % 3);
            var pieceOriginalPositionIndex = (byte)(pieceHash / 3);
            pieces[currentPositionIndex] = _defaultDataProvider.GetPieceByOriginalPositionIndex(pieceOriginalPositionIndex);
            pieces[currentPositionIndex].Move((byte)currentPositionIndex, pieceRotationIndex);
            hash /= 24;
        }

        return new Cube(pieces);
    }
}