using System.Collections.Generic;
using RubikSolver.Cube2X2.Application.Data;
using RubikSolver.Cube2X2.Core.Models;

namespace RubikSolver.Cube2X2.Application.Format;

public interface ICubeFormatManager
{
    Dictionary<Color, byte> DistributeColorIndexes(CubeView cubeView);
    Cube GetCube(CubeView cubeView, Dictionary<Color, byte> colorIndexes);
    CubeView GetView(Cube cube, Dictionary<Color, byte> colorIndexes);
}