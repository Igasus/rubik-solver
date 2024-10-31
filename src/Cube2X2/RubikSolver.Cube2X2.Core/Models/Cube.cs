using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RubikSolver.Cube2X2.Core.Models;

public class Cube : IDisposable, IAsyncDisposable
{
    private readonly Dictionary<byte, Piece> _piecesByCurrentPositionIndex;

    public Piece[] Pieces => _piecesByCurrentPositionIndex.Values
        .OrderBy(x => x.CurrentPositionIndex)
        .ToArray();

    public Cube(Piece[] pieces)
    {
        if (pieces.Length != Constants.CubePiecesCount)
            throw new ArgumentException(ErrorMessages.InvalidPiecesCount, nameof(pieces));

        _piecesByCurrentPositionIndex = pieces.ToDictionary(x => x.CurrentPositionIndex, x => x);
    }

    public Cube Copy()
    {
        var pieces = Pieces.Select(x => x.Copy()).ToArray();
        return new Cube(pieces);
    }

    #region Rotation

    public void RotateUp()
    {
        _piecesByCurrentPositionIndex[0].Move(1, 0);
        _piecesByCurrentPositionIndex[1].Move(2, 0);
        _piecesByCurrentPositionIndex[2].Move(3, 0);
        _piecesByCurrentPositionIndex[3].Move(0, 0);
        RefreshDictionary();
    }

    public void RotateUpReverse()
    {
        _piecesByCurrentPositionIndex[1].Move(0, 0);
        _piecesByCurrentPositionIndex[2].Move(1, 0);
        _piecesByCurrentPositionIndex[3].Move(2, 0);
        _piecesByCurrentPositionIndex[0].Move(3, 0);
        RefreshDictionary();
    }

    public void RotateRight()
    {
        _piecesByCurrentPositionIndex[1].Move(5, -1);
        _piecesByCurrentPositionIndex[2].Move(1, 1);
        _piecesByCurrentPositionIndex[5].Move(6, 1);
        _piecesByCurrentPositionIndex[6].Move(2, -1);
        RefreshDictionary();
    }

    public void RotateRightReverse()
    {
        _piecesByCurrentPositionIndex[5].Move(1, 1);
        _piecesByCurrentPositionIndex[1].Move(2, -1);
        _piecesByCurrentPositionIndex[6].Move(5, -1);
        _piecesByCurrentPositionIndex[2].Move(6, 1);
        RefreshDictionary();
    }

    public void RotateFront()
    {
        _piecesByCurrentPositionIndex[2].Move(6, -1);
        _piecesByCurrentPositionIndex[3].Move(2, 1);
        _piecesByCurrentPositionIndex[6].Move(7, 1);
        _piecesByCurrentPositionIndex[7].Move(3, -1);
        RefreshDictionary();
    }

    public void RotateFrontReverse()
    {
        _piecesByCurrentPositionIndex[6].Move(2, 1);
        _piecesByCurrentPositionIndex[2].Move(3, -1);
        _piecesByCurrentPositionIndex[7].Move(6, -1);
        _piecesByCurrentPositionIndex[3].Move(7, 1);
        RefreshDictionary();
    }

    private void RefreshDictionary()
    {
        var allPieces = Pieces;
        for (byte positionIndex = 0; positionIndex < Constants.CubePiecesCount; positionIndex++)
        {
            _piecesByCurrentPositionIndex[positionIndex] =
                allPieces.First(x => x.CurrentPositionIndex == positionIndex);
        }
    }

    #endregion

    public void Dispose()
    {
        var pieces = _piecesByCurrentPositionIndex.Values.ToList();
        foreach (var piece in pieces)
            piece.Dispose();

        _piecesByCurrentPositionIndex.Clear();
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        var pieces = _piecesByCurrentPositionIndex.Values.ToList();
        foreach (var piece in pieces)
            await piece.DisposeAsync();

        _piecesByCurrentPositionIndex.Clear();
        GC.SuppressFinalize(this);
    }
}