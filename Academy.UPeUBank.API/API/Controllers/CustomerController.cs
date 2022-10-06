using API.Controllers.Base;
using API.Exceptions;
using Application.Features.Customer.Commands;
using Application.Features.Customer.Queries;
using Application.Wrappers;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("[controller]")]
[ApiController]
public class CustomerController : BaseController
{
    /// <summary>
    /// Get customer by id
    /// </summary>
    /// <param name="id"></param>
    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Response<CustomerDto>>> GetCustomer(int id)
    {
        return Ok(await Mediator.Send(new GetCustomerQuery() {Id = id}));
    }

    /// <summary>
    /// List all customer
    /// </summary>
    [HttpGet]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Response<IList<CustomerDto>>>> ListAllCustomer()
    {
        return Ok(await Mediator.Send(new ListCustomerQuery()));
    }

    /// <summary>
    /// Add new customer
    /// </summary>
    /// <remarks>
    ///     Any additional text you want
    /// </remarks>
    /// <returns>This is the list of results</returns>
    /// <response code="400">
    ///     <para>
    ///         If property Name is empty
    ///         <example>
    ///             <br /> 
    ///             <code>
    ///                 {
    ///                     "success":false
    ///                     "message": "The Property Name Cannot Be Null"
    ///                 }
    ///             </code>
    ///         </example>
    ///     </para>
    /// </response>
    [HttpPost]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Response<CustomerDto>>> AddCustomer([FromBody] AddCustomerRequest request)
    {
        return Created("", await Mediator.Send(request));
    }

    /// <summary>
    /// Update customer
    /// </summary>
    /// <remarks>
    ///     Any additional text you want
    /// </remarks>
    /// <returns>This is the list of results</returns>
    /// <response code="400">
    ///     <para>
    ///         If the sent id by route didn't match with id from request body
    ///         <example>
    ///             <br /> 
    ///             <code>
    ///                 {
    ///                     "success":false
    ///                     "message": "The Sent Id {Id} By Route Didn't Match With Id From Request Body"
    ///                 }
    ///             </code>
    ///         </example>
    ///     </para>
    ///     <para>
    ///         If property Name is empty
    ///         <example>
    ///             <br /> 
    ///             <code>
    ///                 {
    ///                     "success":false
    ///                     "message": "The Property Name Cannot Be Null"
    ///                 }
    ///             </code>
    ///         </example>
    ///     </para>
    ///     <para>
    ///         If Entity not found by id
    ///         <example>
    ///             <br /> 
    ///             <code>
    ///                 {
    ///                     "success":false
    ///                     "message": "The Entity Customer Cannot Found"
    ///                 }
    ///             </code>
    ///         </example>
    ///     </para>
    /// </response>
    [HttpPut]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Response<CustomerDto>>> UpdateCustomer([FromRoute] int id,
        [FromBody] UpdateCustomerRequest request)
    {
        if (id != request.Id)
        {
            throw new SentRouteIdNotMatchRequestEntityIdException(id);
        }

        return Accepted("", await Mediator.Send(request));
    }

    /// <summary>
    /// Delete customer
    /// </summary>
    /// <remarks>
    ///     Any additional text you want
    /// </remarks>
    /// <returns>This is the list of results</returns>
    /// <response code="400">
    ///     <para>
    ///         If Entity not found by id
    ///         <example>
    ///             <br /> 
    ///             <code>
    ///                 {
    ///                     "success":false
    ///                     "message": "The Entity Customer Cannot Found"
    ///                 }
    ///             </code>
    ///         </example>
    ///     </para>
    ///     <para>
    ///         If there are some entity dependencies
    ///         <example>
    ///             <br /> 
    ///             <code>
    ///                 {
    ///                     "success":false
    ///                     "message": "The Entity Customer Cannot Be Remove Because There Are Some Dependencies"
    ///                 }
    ///             </code>
    ///         </example>
    ///     </para>
    /// </response>
    [HttpDelete]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Response<CustomerDto>>> DeleteCustomer([FromRoute] int id)
    {
        return Ok(await Mediator.Send(new DeleteCustomerRequest() {Id = id}));
    }
}