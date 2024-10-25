using System.Net;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using CreditProposal.Application.ViewModels;
using CreditProposal.Application.Queries.GetAllCustomers;
using CreditProposal.Application.Queries.GetCustomersById;
using Swashbuckle.AspNetCore.Annotations;

namespace CreditProposal.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "v1")]
[Route("api/v{version:apiVersion}/credit-proposal")]
public class CreditProposalController : ControllerBase
{
    private readonly IMediator _mediator;

    public CreditProposalController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("customer")]
    [SwaggerOperation(Summary = "Busca todos as propostas de crédito do cleinte (obs, de acordo com a regra atual de avaliação de crédito, só terá 1 único registro para cada cliente)).")]
    [ProducesResponseType(typeof(IEnumerable<CustomerViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetAll([FromQuery] Guid customerId)
    {
        var result = await _mediator.Send(new GetCreditProposalsByCustomerIdQuery(customerId));
        return Ok(result);
    }

    [HttpGet("customers")]
    [SwaggerOperation(Summary = "Busca todos os clientes registrados.")]
    [ProducesResponseType(typeof(IEnumerable<CustomerViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllCustomersQuery());
        return Ok(result);
    }

    [HttpGet("customers")]
    [SwaggerOperation(Summary = "Busca um cliente registrado.")]
    [ProducesResponseType(typeof(IEnumerable<CustomerViewModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetCustomerDetail([FromQuery] Guid customerId)
    {
        var result = await _mediator.Send(new GetCustomerByIdQuery(customerId));
        return result != null ? Ok(result) : NotFound();
    }
}
