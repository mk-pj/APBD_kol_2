using kol_2.Services;
using Microsoft.AspNetCore.Mvc;

namespace kol_2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NurseriesController(IDbService service) : ControllerBase
{
    
    private readonly IDbService _service = service;

    [HttpGet("{id:int}/batches")]
    public async Task<IActionResult> GetCustomerPurchases(int id)
    {
        var nursery = await _service.GetNurseryAsync(id);
        return Ok(nursery);
    }

}