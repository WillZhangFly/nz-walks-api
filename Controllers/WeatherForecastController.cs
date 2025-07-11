using Microsoft.AspNetCore.Mvc;
using NZWalks.Models;

namespace NZWalks.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Gets weather forecast data for the next 5 days
    /// </summary>
    /// <returns>A list of weather forecasts</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<WeatherForecast>> Get()
    {
        _logger.LogInformation("Getting weather forecast data");

        var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();

        return Ok(forecasts);
    }

    /// <summary>
    /// Gets weather forecast for a specific date
    /// </summary>
    /// <param name="date">The date to get forecast for</param>
    /// <returns>Weather forecast for the specified date</returns>
    [HttpGet("{date}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<WeatherForecast> GetByDate(DateOnly date)
    {
        _logger.LogInformation("Getting weather forecast for date: {Date}", date);

        if (date < DateOnly.FromDateTime(DateTime.Now) || date > DateOnly.FromDateTime(DateTime.Now.AddDays(30)))
        {
            return NotFound("Weather forecast is only available for the next 30 days");
        }

        var forecast = new WeatherForecast
        {
            Date = date,
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        };

        return Ok(forecast);
    }

    /// <summary>
    /// Creates a new weather forecast entry
    /// </summary>
    /// <param name="forecast">The weather forecast to create</param>
    /// <returns>The created weather forecast</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<WeatherForecast> Post([FromBody] WeatherForecast forecast)
    {
        _logger.LogInformation("Creating new weather forecast for date: {Date}", forecast.Date);

        if (forecast.Date < DateOnly.FromDateTime(DateTime.Now))
        {
            return BadRequest("Cannot create forecast for past dates");
        }

        // In a real application, you would save this to a database
        return CreatedAtAction(nameof(GetByDate), new { date = forecast.Date }, forecast);
    }
}
