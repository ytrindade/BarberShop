using BarberShop.Application.UseCases.Revenues.Delete;
using BarberShop.Application.UseCases.Revenues.GetAll;
using BarberShop.Application.UseCases.Revenues.GetById;
using BarberShop.Application.UseCases.Revenues.Register;
using BarberShop.Application.UseCases.Revenues.Update;
using BarberShop.Communication.Requests;
using BarberShop.Communication.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RevenueController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredRevenueJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromServices] IRegisterRevenueUseCase useCase,
        [FromBody] RequestRevenueJson request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseRegisteredRevenueJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAll([FromServices] IGetRevenuesUseCase useCase)
    {
        var response = await useCase.Execute();

        if(response.Revenues.Count == 0)
            return NoContent();

        return Ok(response);
    }


    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseRegisteredRevenueJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll([FromServices] IGetRevenueByIdUseCase useCase,
        [FromRoute] long id)
    {
        var response = await useCase.Execute(id);

        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromServices] IDeleteRevenueUseCase useCase,
        [FromRoute] long id)
    {
        await useCase.Execute(id);

        return NoContent();
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromServices] IUpdateRevenueUseCase useCase,
        [FromRoute] long id, [FromBody] RequestRevenueJson request)
    {
        await useCase.Execute(id, request);

        return NoContent();
    }

}
