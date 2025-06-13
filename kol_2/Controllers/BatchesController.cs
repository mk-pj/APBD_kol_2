using kol_2.DTOs;
using kol_2.Services;
using Microsoft.AspNetCore.Mvc;

namespace kol_2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BatchesController(IDbService service) : ControllerBase
{
    private readonly IDbService _service = service;

    [HttpPost]
    public async Task<IActionResult> AddBatch([FromBody] NewBatchDto newBatch)
    {
        await _service.AddNewBatchAsync(newBatch);
        return Ok();
    }
}