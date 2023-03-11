using CleanArchitecture.WebUI.Endpoints.Base;
using CleanArchitecture.Application.WeatherForecasts.Queries;
using FastEndpoints;
using CleanArchitecture.Application.WeatherForecasts.Queries.GetWeatherForecasts;

namespace CleanArchitecture.WebUI.Endpoints.WeatherForecast.WeatherForecastGet;

public class GetWeatherForecastEndpoint : AbstractEndpointWithoutRequest<IEnumerable<CleanArchitecture.Application.WeatherForecasts.Queries.GetWeatherForecasts.WeatherForecast>>
{
    public override void Configure()
    {
        Get("/WeatherForecast");
        AllowAnonymous();
    }

    public override async Task HandleAsync(EmptyRequest req, CancellationToken ct)
    {
        await SendAsync(await Mediator.Send(new GetWeatherForecastsQuery()));
    }
}
