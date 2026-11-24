using Microsoft.AspNetCore.Mvc;
using QTKD.Api.DTOs;
using QTKD.Api.Services;

namespace QTKD.Api.Controllers;

[ApiController]
[Route("api/contracts")]
public class ContractsController : ControllerBase
{
    private readonly QtkdQueryService _service;

    public ContractsController(QtkdQueryService service) => _service = service;

    // GET /api/contracts/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ContractResponse>> GetById(int id)
    {
        var contract = await _service.GetContractAsync(id);
        return contract is null ? NotFound() : Ok(contract);
    }
}
