using RubikSolver.Application.Cube2X2.Format;
using RubikSolver.Application.Cube2X2.Hashing;
using RubikSolver.Application.Cube2X2.Processing;
using RubikSolver.Console.IO;
using RubikSolver.Cube2X2.Core.DefaultData;

var dataReader = new DataReader();
var dataWriter = new DataWriter();

var defaultDataProvider = new DefaultDataProvider();
var cubeViewManager = new CubeFormatManager(defaultDataProvider);
var cubeHashManager = new CubeHashManager(defaultDataProvider);
var formulaService = new FormulaService(cubeHashManager);
var solutionFinder = new SolutionFinder(cubeHashManager, formulaService);

var cubeView = dataReader.ReadCubeView();
var colorIndexes = cubeViewManager.DistributeColorIndexes(cubeView);
var initialCube = cubeViewManager.GetCube(cubeView, colorIndexes);
var solvedCube = defaultDataProvider.GetSolvedCube();
var formula = solutionFinder.FindFormula(initialCube, solvedCube);

foreach (var sign in formula)
{
    Console.WriteLine(sign.ToString());
}
