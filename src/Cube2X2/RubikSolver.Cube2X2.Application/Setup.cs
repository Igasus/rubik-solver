using Microsoft.Extensions.DependencyInjection;
using RubikSolver.Application.Cube2X2.Format;
using RubikSolver.Application.Cube2X2.Hashing;
using RubikSolver.Application.Cube2X2.Processing;
using RubikSolver.Application.Cube2X2.Processing.Interfaces;
using RubikSolver.Cube2X2.Core.DefaultData;

namespace RubikSolver.Application.Cube2X2;

public static class Setup
{
    public static IServiceCollection Add2X2SolverServices(this IServiceCollection services)
    {
        services.AddSingleton<IDefaultDataProvider, DefaultDataProvider>();
        services.AddScoped<ICubeFormatManager, CubeFormatManager>();
        services.AddScoped<ICubeHashManager, CubeHashManager>();
        services.AddScoped<IFormulaService, FormulaService>();
        services.AddScoped<ISolutionFinder, SolutionFinder>();

        return services;
    }
}