using System;
using System.Collections.Generic;
using System.Linq;
using RubikSolver.Cube2X2.Application.Data;
using RubikSolver.Cube2X2.Core;
using RubikSolver.Cube2X2.Core.DefaultData;
using RubikSolver.Cube2X2.Core.Models;

namespace RubikSolver.Cube2X2.Application.Format;

public class CubeFormatManager : ICubeFormatManager
{
    private readonly IDefaultDataProvider _defaultDataProvider;

    public CubeFormatManager(IDefaultDataProvider defaultDataProvider)
    {
        ArgumentNullException.ThrowIfNull(defaultDataProvider);
        _defaultDataProvider = defaultDataProvider;
    }

    public Dictionary<Color, byte> DistributeColorIndexes(CubeView cubeView)
    {
        ArgumentNullException.ThrowIfNull(cubeView);

        var keyPieceColors = GetPieceColorsClockwise(cubeView, 4);
        var colorsToIndexes = new Dictionary<Color, byte>
        {
            { keyPieceColors[0], 3 },
            { keyPieceColors[0].GetOpposite(), 0 },
            { keyPieceColors[1], 5 },
            { keyPieceColors[1].GetOpposite(), 2 },
            { keyPieceColors[2], 4 },
            { keyPieceColors[2].GetOpposite(), 1 }
        };

        return colorsToIndexes;
    }

    public Cube GetCube(CubeView cubeView, Dictionary<Color, byte> colorIndexes)
    {
        ArgumentNullException.ThrowIfNull(cubeView);
        if (colorIndexes.Count != Constants.CubeColorsCount)
            throw new ArgumentException(ErrorMessages.InvalidColorsCount, nameof(colorIndexes));

        var pieces = new Piece[Constants.CubePiecesCount];
        for (byte positionIndex = 0; positionIndex < Constants.CubePiecesCount; positionIndex++)
        {
            var colors = GetPieceColorsClockwise(cubeView, positionIndex);
            var pieceRotationIndex = GetRotationIndex(colors, colorIndexes);
            byte[] pieceColorIndexes = [colorIndexes[colors[0]], colorIndexes[colors[1]], colorIndexes[colors[2]]];
            pieces[positionIndex] = _defaultDataProvider.GetPieceByColors(pieceColorIndexes);
            pieces[positionIndex].Move(positionIndex, pieceRotationIndex);
        }

        return new Cube(pieces);
    }

    public CubeView GetView(Cube cube, Dictionary<Color, byte> colorIndexes)
    {
        var colors = colorIndexes.ToDictionary(keyValue => keyValue.Value, keyValue => keyValue.Key);

        Side[] sides =
        [
            new Side(
                colors[cube.Pieces[0].ColorIndexes.Secondary],
                colors[cube.Pieces[3].ColorIndexes.Tertiary],
                colors[cube.Pieces[4].ColorIndexes.Tertiary],
                colors[cube.Pieces[7].ColorIndexes.Secondary]),
            new Side(
                colors[cube.Pieces[3].ColorIndexes.Secondary],
                colors[cube.Pieces[2].ColorIndexes.Tertiary],
                colors[cube.Pieces[7].ColorIndexes.Tertiary],
                colors[cube.Pieces[6].ColorIndexes.Secondary]),
            new Side(
                colors[cube.Pieces[2].ColorIndexes.Secondary],
                colors[cube.Pieces[1].ColorIndexes.Tertiary],
                colors[cube.Pieces[6].ColorIndexes.Tertiary],
                colors[cube.Pieces[5].ColorIndexes.Secondary]),
            new Side(
                colors[cube.Pieces[1].ColorIndexes.Secondary],
                colors[cube.Pieces[0].ColorIndexes.Tertiary],
                colors[cube.Pieces[5].ColorIndexes.Tertiary],
                colors[cube.Pieces[4].ColorIndexes.Secondary]),
            new Side(
                colors[cube.Pieces[0].ColorIndexes.Primary],
                colors[cube.Pieces[1].ColorIndexes.Primary],
                colors[cube.Pieces[3].ColorIndexes.Primary],
                colors[cube.Pieces[2].ColorIndexes.Primary]),
            new Side(
                colors[cube.Pieces[7].ColorIndexes.Primary],
                colors[cube.Pieces[6].ColorIndexes.Primary],
                colors[cube.Pieces[4].ColorIndexes.Primary],
                colors[cube.Pieces[5].ColorIndexes.Primary])
        ];

        return new CubeView(sides);
    }

    private Color[] GetPieceColorsClockwise(CubeView view, byte positionIndex)
    {
        return positionIndex switch
        {
            0 =>
            [
                view.TopSide.TopLeftTileColor,
                view.LeftSide.TopLeftTileColor,
                view.BackSide.TopRightTileColor
            ],
            1 =>
            [
                view.TopSide.TopRightTileColor,
                view.BackSide.TopLeftTileColor,
                view.RightSide.TopRightTileColor
            ],
            2 =>
            [
                view.TopSide.BottomRightTileColor,
                view.RightSide.TopLeftTileColor,
                view.FrontSide.TopRightTileColor
            ],
            3 =>
            [
                view.TopSide.BottomLeftTileColor,
                view.FrontSide.TopLeftTileColor,
                view.LeftSide.TopRightTileColor
            ],
            4 =>
            [
                view.BottomSide.BottomLeftTileColor,
                view.BackSide.BottomRightTileColor,
                view.LeftSide.BottomLeftTileColor
            ],
            5 =>
            [
                view.BottomSide.BottomRightTileColor,
                view.RightSide.BottomRightTileColor,
                view.BackSide.BottomLeftTileColor
            ],
            6 =>
            [
                view.BottomSide.TopRightTileColor,
                view.FrontSide.BottomRightTileColor,
                view.RightSide.BottomLeftTileColor
            ],
            7 =>
            [
                view.BottomSide.TopLeftTileColor,
                view.LeftSide.BottomRightTileColor,
                view.FrontSide.BottomLeftTileColor
            ],
            _ => throw new ArgumentOutOfRangeException(nameof(positionIndex), positionIndex,
                string.Format(ErrorMessages.InvalidPiecePositionIndex, positionIndex))
        };
    }

    private byte GetRotationIndex(Color[] colorsClockwise, Dictionary<Color, byte> colorIndexes)
    {
        for (byte i = 0; i < Constants.PieceColorsCount; i++)
        {
            var color = colorsClockwise[i];
            var colorIndex = colorIndexes[color];
            if (colorIndex is 0 or 3)
                return i;
        }

        throw new ArgumentException(
            "Primary (White or Yellow) color is not found in provided array",
            nameof(colorsClockwise));
    }
}