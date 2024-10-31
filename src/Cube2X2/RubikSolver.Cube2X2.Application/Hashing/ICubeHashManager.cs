using RubikSolver.Cube2X2.Core.Models;

namespace RubikSolver.Cube2X2.Application.Hashing;

public interface ICubeHashManager
{
    long GetHash(Cube cube);
    Cube GetCube(long hash);
}