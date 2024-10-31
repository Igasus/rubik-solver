using Microsoft.Extensions.DependencyInjection;
using RubikSolver.Cube2X2.Application.Format;
using RubikSolver.Cube2X2.Application.Hashing;
using RubikSolver.Cube2X2.Application.Processing;
using RubikSolver.Cube2X2.Application.Processing.Interfaces;
using RubikSolver.Cube2X2.Core.DefaultData;

namespace RubikSolver.Cube2X2.Application;

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