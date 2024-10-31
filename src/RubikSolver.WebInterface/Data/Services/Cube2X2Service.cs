using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using RubikSolver.WebInterface.Data.Models;
using RubikSolver.WebInterface.Data.Services.Interfaces;
using RubikSolver.WebInterface.Options;

namespace RubikSolver.WebInterface.Data.Services;

public class Cube2X2Service : ICube2X2Service
{
    private readonly CubeSolverApiOptions _options;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public Cube2X2Service(IOptions<CubeSolverApiOptions> options)
    {
        ArgumentNullException.ThrowIfNull(options);
        ArgumentException.ThrowIfNullOrWhiteSpace(options.Value.Address);
        _options = options.Value;

        _jsonSerializerOptions = new JsonSerializerOptions();
        _jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    }

    public async Task<string[]> SolveCubeAsync(Side[] sides)
    {
        const string route = "api/2x2/solve";
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri(_options.Address!)
        };

        var request = new HttpRequestMessage(HttpMethod.Post, route);
        var requestBody = new SolveCube2X2Request { Sides = sides };
        request.Content = JsonContent.Create(requestBody, options: _jsonSerializerOptions);

        var response = await httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode)
            return [];
        
        var responseBody = await response.Content.ReadFromJsonAsync<SolveCube2X2Response>();

        return responseBody?.Formula ?? [];
    }
}