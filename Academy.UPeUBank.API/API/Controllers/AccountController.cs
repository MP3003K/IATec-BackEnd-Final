using API.Controllers.Base;
using Application.Features.Account.Commands;
using Application.Features.Account.Queries;
using Application.Wrappers;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("[controller]")]
[ApiController]
public class AccountController : BaseController
{
    /// <summary>
    /// Get account by id
    /// </summary>
    /// <param name="id"></param>
    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Response<AccountDto>>> GetAccount(int id)
    {
        return Ok(await Mediator.Send(new GetAccountQuery() {Id = id}));
    }

    /// <summary>
    /// List all account
    /// </summary>
    [HttpGet]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Response<IList<AccountDto>>>> ListAllAccount()
    {
        return Ok(await Mediator.Send(new ListAccountQuery()));
    }

    /// <summary>
    /// Add new account
    /// </summary>
    /// <remarks>
    ///     Any additional text you want
    /// </remarks>
    /// <returns>This is the list of results</returns>
    /// <response code="400">
    ///     <para>
    ///         If property CustomerId is empty
    ///         <example>
    ///             <br /> 
    ///             <code>
    ///                 {
    ///                     "success":false
    ///                     "message": "The Property CustomerId Cannot Be Null"
    ///                 }
    ///             </code>
    ///         </example>
    ///     </para>
    ///     <para>
    ///         If Entity Customer not found by CustomerId
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
    ///         If property Code is empty
    ///         <example>
    ///             <br /> 
    ///             <code>
    ///                 {
    ///                     "success":false
    ///                     "message": "The Property Code Cannot Be Null"
    ///                 }
    ///             </code>
    ///         </example>
    ///     </para>
    /// </response>
    [HttpPost]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Response<AccountDto>>> AddAccount([FromBody] AddAccountRequest request)
    {
        return Created("", await Mediator.Send(request));
    }

    /// <summary>
    /// Delete account
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
    ///                     "message": "The Entity Account Cannot Found"
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
    public async Task<ActionResult<Response<AccountDto>>> DeleteAccount([FromRoute] int id)
    {
        return Ok(await Mediator.Send(new DeleteAccountRequest() {Id = id}));
    }
}