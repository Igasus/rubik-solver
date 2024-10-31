namespace RubikSolver.Application.Cube2X2.Data;

public class Side
{
    public Color[][] TileColors { get; }

    public Side(
        Color topLeftTileColor,
        Color topRightTileColor,
        Color bottomLeftTileColor,
        Color bottomRightTileColor)
    {
        TileColors =
        [
            [topLeftTileColor, topRightTileColor],
            [bottomLeftTileColor, bottomRightTileColor]
        ];
    }

    public Color TopLeftTileColor => TileColors[0][0];
    public Color TopRightTileColor => TileColors[0][1];
    public Color BottomLeftTileColor => TileColors[1][0];
    public Color BottomRightTileColor => TileColors[1][1];
}