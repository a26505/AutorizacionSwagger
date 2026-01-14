using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RestauranteAPI.Services;
using RestauranteAPI.Models;

namespace RestauranteAPI.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class BebidaController : ControllerBase
{
    private readonly ILogger<BebidaController> _logger;
    private readonly IBebidaService _service;

    public BebidaController(ILogger<BebidaController> logger, IBebidaService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public IActionResult GetBebidas()
    {
        try
        {
            var bebidas = _service.GetAll();
            return Ok(bebidas);
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetBebida(int id)
    {
        try
        {
            var bebida = _service.GetById(id);
            if (bebida == null) return NotFound();
            return Ok(bebida);
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public IActionResult CreateBebida([FromBody] Bebida bebida)
    {
        try
        {
            var created = _service.CreateBebida(bebida);
            return Ok(created.Id);
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
            return BadRequest(ex.Message);
        }
    }
    [Authorize(Roles = Roles.Admin)]
    [HttpPut("{id}")]
    public IActionResult UpdateBebida(int id, [FromBody] Bebida updated)
    {
        try
        {
            var existing = _service.GetById(id);
            if (existing == null) return NotFound();

            _service.UpdateBebida(id, updated);
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
    public IActionResult DeleteBebida(int id)
    {
        try
        {
            var existing = _service.GetById(id);
            if (existing == null) return NotFound();

            _service.DeleteBebida(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
            return BadRequest(ex.Message);
        }
    }
}
