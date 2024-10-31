using RubikSolver.Application.Cube2X2.Data;
using RubikSolver.Cube2X2.Core.Models;

namespace RubikSolver.Application.Cube2X2.Format;

public interface ICubeFormatManager
{
    Dictionary<Color, byte> DistributeColorIndexes(CubeView cubeView);
    Cube GetCube(CubeView cubeView, Dictionary<Color, byte> colorIndexes);
    CubeView GetView(Cube cube, Dictionary<Color, byte> colorIndexes);
}