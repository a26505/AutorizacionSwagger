using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RestauranteAPI.Services;
using RestauranteAPI.Models;

namespace RestauranteAPI.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class ComboController : ControllerBase
{
    private readonly ILogger<ComboController> _logger;
    private readonly IComboService _service;

    public ComboController(ILogger<ComboController> logger, IComboService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public IActionResult GetCombos()
    {
        try
        {
            var combos = _service.GetAll();
            return Ok(combos);
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetCombo(int id)
    {
        try
        {
            var combo = _service.GetById(id);
            if (combo == null) return NotFound();
            return Ok(combo);
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public IActionResult CreateCombo([FromBody] Combo combo)
    {
        try
        {
            var created = _service.CreateCombo(combo);
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
    public IActionResult UpdateCombo(int id, [FromBody] Combo updated)
    {
        try
        {
            var existing = _service.GetById(id);
            if (existing == null) return NotFound();

            _service.UpdateCombo(id, updated);
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
    public IActionResult DeleteCombo(int id)
    {
        try
        {
            var existing = _service.GetById(id);
            if (existing == null) return NotFound();

            _service.DeleteCombo(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
            return BadRequest(ex.Message);
        }
    }
}
