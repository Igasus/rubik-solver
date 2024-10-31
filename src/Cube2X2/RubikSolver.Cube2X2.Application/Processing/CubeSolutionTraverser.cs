using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RubikSolver.Cube2X2.Application.Hashing;
using RubikSolver.Cube2X2.Core.Models;

namespace RubikSolver.Cube2X2.Application.Processing;

public class CubeSolutionTraverser : IDisposable, IAsyncDisposable
{
    private readonly ICubeHashManager _cubeHashManager;
    private readonly Queue<long> _stateQueue;

    public Dictionary<long, long> SolutionTree { get; }

    public CubeSolutionTraverser(ICubeHashManager cubeHashManager, Cube initialCube)
    {
        ArgumentNullException.ThrowIfNull(cubeHashManager);
        _cubeHashManager = cubeHashManager;

        ArgumentNullException.ThrowIfNull(initialCube);
        var initialCubeHash = cubeHashManager.GetHash(initialCube);

        _stateQueue = [];
        _stateQueue.Enqueue(initialCubeHash);
        SolutionTree = [];
        SolutionTree.Add(initialCubeHash, -1);
    }

    public bool Next(in List<long> nextMoveCubeHashes)
    {
        nextMoveCubeHashes.Clear();

        if (_stateQueue.Count == 0)
            return false;

        var currentCubeHash = _stateQueue.Dequeue();
        using var cube = _cubeHashManager.GetCube(currentCubeHash);

        nextMoveCubeHashes.AddRange([
            GetCubeHashAfterMoveUp(cube),
            GetCubeHashAfterMoveRight(cube),
            GetCubeHashAfterMoveFront(cube)
        ]);

        for (var i = nextMoveCubeHashes.Count - 1; i >= 0; i--)
        {
            var hash = nextMoveCubeHashes[i];
            if (!SolutionTree.TryAdd(hash, currentCubeHash))
                nextMoveCubeHashes.RemoveAt(i);
            else
                _stateQueue.Enqueue(hash);
        }

        return true;
    }

    private long GetCubeHashAfterMoveUp(Cube cube)
    {
        using var resultCube = cube.Copy();
        resultCube.RotateUp();
        var resultCubeHash = _cubeHashManager.GetHash(resultCube);

        return resultCubeHash;
    }

    private long GetCubeHashAfterMoveRight(Cube cube)
    {
        using var resultCube = cube.Copy();
        resultCube.RotateRight();
        var resultCubeHash = _cubeHashManager.GetHash(resultCube);

        return resultCubeHash;
    }

    private long GetCubeHashAfterMoveFront(Cube cube)
    {
        using var resultCube = cube.Copy();
        resultCube.RotateFront();
        var resultCubeHash = _cubeHashManager.GetHash(resultCube);

        return resultCubeHash;
    }

    public void Dispose()
    {
        _stateQueue.Clear();
        SolutionTree.Clear();
        GC.SuppressFinalize(this);
    }

    public ValueTask DisposeAsync()
    {
        _stateQueue.Clear();
        SolutionTree.Clear();
        GC.SuppressFinalize(this);
        return default;
    }
}