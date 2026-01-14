using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RestauranteAPI.Services;
using RestauranteAPI.Models;

namespace RestauranteAPI.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class PostreController : ControllerBase
{
    private readonly ILogger<PostreController> _logger;
    private readonly IPostreService _service;

    public PostreController(ILogger<PostreController> logger, IPostreService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public IActionResult GetPostres()
    {
        try
        {
            var postres = _service.GetAll();
            return Ok(postres);
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetPostre(int id)
    {
        try
        {
            var postre = _service.GetById(id);
            if (postre == null) return NotFound();
            return Ok(postre);
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public IActionResult CreatePostre([FromBody] Postre postre)
    {
        try
        {
            var created = _service.CreatePostre(postre);
            return Ok(created.Id);
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult UpdatePostre(int id, [FromBody] Postre updated)
    {
        try
        {
            var existing = _service.GetById(id);
            if (existing == null) return NotFound();

            _service.UpdatePostre(id, updated);
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
    public IActionResult DeletePostre(int id)
    {
        try
        {
            var existing = _service.GetById(id);
            if (existing == null) return NotFound();

            _service.DeletePostre(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.Message);
            return BadRequest(ex.Message);
        }
    }
}
