using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RestauranteAPI.Services;
using RestauranteAPI.Models;

namespace RestauranteAPI.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class PlatoPrincipalController : ControllerBase
{
    private readonly ILogger<PlatoPrincipalController> _logger;
    private readonly IPlatoPrincipalService _service;

    public PlatoPrincipalController(ILogger<PlatoPrincipalController> logger, IPlatoPrincipalService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public IActionResult GetPlatos()
    {
        try
        {
            var platos = _service.GetAll();
            return Ok(platos);
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetPlato(int id)
    {
        try
        {
            var plato = _service.GetById(id);
            if (plato == null) return NotFound();
            return Ok(plato);
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public IActionResult CreatePlato([FromBody] PlatoPrincipal plato)
    {
        try
        {
            var created = _service.CreatePlato(plato);
            return Ok(created.Id);
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult UpdatePlato(int id, [FromBody] PlatoPrincipal updated)
    {
        try
        {
            var existing = _service.GetById(id);
            if (existing == null) return NotFound();

            _service.UpdatePlato(id, updated);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("{id}")]
    public IActionResult DeletePlato(int id)
    {
        try
        {
            var existing = _service.GetById(id);
            if (existing == null) return NotFound();

            _service.DeletePlato(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
            return BadRequest(ex.Message);
        }
    }
}
