using Microsoft.AspNetCore.Mvc;
using SQLSanitizorNator.Data.Models;
using SQLSanitizorNator.Logic.CRUD;
using SQLSanitizorNator.Logic.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SQLSanitizorNator.Controllers;

[Route("api/sanitize")]
[ApiController]
public class SanitizeController : ControllerBase
{
    private readonly INaughtyWordService<NaughtyWordServiceCRUD> _service;
    private readonly ILogger<SanitizeController> _logger;
    public SanitizeController(INaughtyWordService<NaughtyWordServiceCRUD> service, ILogger<SanitizeController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpPost("sanitize")]
    public async Task<ActionResult<string>> Sanitize([FromBody] string toSanitize, [FromQuery] int severity = 10)
    {       
        string sanitizedString =  await _service.Sanitize(toSanitize, severity);
        return sanitizedString;
    }

    [HttpGet]
    public async Task<ActionResult<List<NaughtyWord>>> Get()
    {
        try
        {
            return Ok(await _service.GetAll());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the naughty word");
            return Problem();
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<List<NaughtyWord>>> Get(Guid id)
    {
        try
        {
            return Ok(await _service.GetById(id));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the naughty word");
            return Problem();
        }
    }

    [HttpPost]
    public async Task<ActionResult<NaughtyWord?>> Create([FromBody] NaughtyWord word)
    {
        try
        {
            return Ok(await _service.Create(word));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the naughty word");
            return Problem();
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<NaughtyWord?>> Delete(Guid id)
    {
        try
        {
            return Ok(await _service.DeleteById(id));
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogError(ex, ex.Message, id);
            return NotFound($"Naughty word with ID {id} not found.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting the naughty word with ID {Id}", id);
            return Problem();         
        }
    }
}
