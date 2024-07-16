using AdminCenter.Application.WeatherForecasts.Queries.GetWeatherForecasts;

namespace AdminCenter.Web.Endpoints;

public class WeatherForecasts : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
              .MapGet(GetWeatherForecasts);
    }

    public async Task<IEnumerable<WeatherForecast>> GetWeatherForecasts(ISender sender)
    {
        return await sender.Send(new GetWeatherForecastsQuery());
    }
}
