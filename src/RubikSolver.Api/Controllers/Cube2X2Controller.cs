using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RubikSolver.Api.Models;
using RubikSolver.Cube2X2.Application.Data;
using RubikSolver.Cube2X2.Application.Format;
using RubikSolver.Cube2X2.Application.Processing.Interfaces;
using RubikSolver.Cube2X2.Core.DefaultData;

namespace RubikSolver.Api.Controllers;

[ApiController]
[Route("api/2x2")]
public class Cube2X2Controller : ControllerBase
{
    private readonly IDefaultDataProvider _defaultDataProvider;
    private readonly ICubeFormatManager _cubeFormatManager;
    private readonly ISolutionFinder _solutionFinder;
    private readonly IFormulaService _formulaService;

    public Cube2X2Controller(
        IDefaultDataProvider defaultDataProvider,
        ICubeFormatManager cubeFormatManager,
        ISolutionFinder solutionFinder,
        IFormulaService formulaService)
    {
        ArgumentNullException.ThrowIfNull(defaultDataProvider);
        ArgumentNullException.ThrowIfNull(cubeFormatManager);
        ArgumentNullException.ThrowIfNull(solutionFinder);
        ArgumentNullException.ThrowIfNull(formulaService);

        _defaultDataProvider = defaultDataProvider;
        _cubeFormatManager = cubeFormatManager;
        _solutionFinder = solutionFinder;
        _formulaService = formulaService;
    }

    [HttpPost("solve")]
    public async Task<IActionResult> SolveAsync([FromBody] Find2X2SolutionRequest request)
    {
        if (request.Sides.Length != 6 ||
            request.Sides.Any(x => x.Colors.Length != 2 || x.Colors.Any(y => y.Length != 2)))
            return BadRequest();

        var sides = request.Sides.Select(x => new Side(
            x.Colors[0][0],
            x.Colors[0][1],
            x.Colors[1][0],
            x.Colors[1][1])).ToArray();
        var cubeView = new CubeView(sides);
        var colorsToIndexes = _cubeFormatManager.DistributeColorIndexes(cubeView);

        await using var initialCube = _cubeFormatManager.GetCube(cubeView, colorsToIndexes);
        await using var solvedCube = _defaultDataProvider.GetSolvedCube();

        var formula = _solutionFinder.FindFormula(initialCube, solvedCube);
        _formulaService.RemoveReverseSymbolFromDoubleMoves(formula);
        var response = new Find2X2SolutionResponse
        {
            Formula = formula.Select(x => x switch
            {
                MoveSign.Ur => "U'",
                MoveSign.Rr => "R'",
                MoveSign.Fr => "F'",
                _ => x.ToString()
            })
        };

        return Ok(response);
    }
}