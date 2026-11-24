using Microsoft.AspNetCore.Mvc;
using QTKD.Api.DTOs;
using QTKD.Api.Services;

namespace QTKD.Api.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomersController : ControllerBase
{
    private readonly QtkdQueryService _service;

    public CustomersController(QtkdQueryService service) => _service = service;

    // GET /api/customers
    [HttpGet]
    public async Task<ActionResult<List<CustomerResponse>>> GetAll() =>
        Ok(await _service.GetCustomersAsync());
}
