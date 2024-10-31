using System;
using RubikSolver.Cube2X2.Core;

namespace RubikSolver.Cube2X2.Application.Data;

public class CubeView
{
    public Side[] Sides { get; }

    public CubeView(Side[] sides)
    {
        if (sides.Length != Constants.CubeColorsCount)
            throw new ArgumentException(ErrorMessages.InvalidSidesCount, nameof(sides));

        Sides = sides;
    }

    public Side LeftSide => Sides[0];
    public Side FrontSide => Sides[1];
    public Side RightSide => Sides[2];
    public Side BackSide => Sides[3];
    public Side TopSide => Sides[4];
    public Side BottomSide => Sides[5];
}