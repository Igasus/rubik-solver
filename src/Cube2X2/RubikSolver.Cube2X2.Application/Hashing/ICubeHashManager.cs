using RubikSolver.Cube2X2.Core.Models;

namespace RubikSolver.Application.Cube2X2.Hashing;

public interface ICubeHashManager
{
    long GetHash(Cube cube);
    Cube GetCube(long hash);
}