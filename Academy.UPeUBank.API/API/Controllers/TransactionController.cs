using API.Controllers.Base;
using Application.Features.Transaction.Commands;
using Application.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class TransactionController : BaseController
{
    /// <summary>
    /// Deposit Value
    /// </summary>
    /// <remarks>
    ///     Any additional text you want
    /// </remarks>
    /// <returns>This is the list of results</returns>
    /// <response code="400">
    ///     <para>
    ///         If property Account is empty
    ///         <example>
    ///             <br /> 
    ///             <code>
    ///                 {
    ///                     "success":false
    ///                     "message": "The Property Account Cannot Be Null"
    ///                 }
    ///             </code>
    ///         </example>
    ///     </para>
    ///     <para>
    ///         If Entity Account not found by Account Code
    ///         <example>
    ///             <br /> 
    ///             <code>
    ///                 {
    ///                     "success":false
    ///                     "message": "The Entity Account Cannot Be Found"
    ///                 }
    ///             </code>
    ///         </example>
    ///     </para>
    ///     <para>
    ///         If property Value less than zero
    ///         <example>
    ///             <br /> 
    ///             <code>
    ///                 {
    ///                     "success":false
    ///                     "message": "The Sent Value Needs Be Greater Than Zero"
    ///                 }
    ///             </code>
    ///         </example>
    ///     </para>
    /// </response>
    [HttpPost]
    [Route("deposit")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Response<bool>>> AddDeposit([FromBody] AddDepositRequest request)
    {
        return Created("", await Mediator.Send(request));
    }

    /// <summary>
    /// Debit Value
    /// </summary>
    /// <remarks>
    ///     Any additional text you want
    /// </remarks>
    /// <returns>This is the list of results</returns>
    /// <response code="400">
    ///     <para>
    ///         If property Account is empty
    ///         <example>
    ///            <br /> 
    ///             <code>
    ///                 {
    ///                     "success":false
    ///                     "message": "The Property Account Cannot Be Null"
    ///                 }
    ///             </code>
    ///         </example>
    ///     </para>
    ///     <para>
    ///         If Entity Account not found by Account Code
    ///         <example>
    ///            <br /> 
    ///             <code>
    ///                 {
    ///                     "success":false
    ///                     "message": "The Entity Account Cannot Be Found"
    ///                 }
    ///             </code>
    ///         </example>
    ///     </para>
    ///     <para>
    ///         If property Value less than zero
    ///         <example>
    ///            <br /> 
    ///             <code>
    ///                 {
    ///                     "success":false
    ///                     "message": "The Sent Value Needs Be Greater Than Zero"
    ///                 }
    ///             </code>
    ///         </example>
    ///     </para>
    ///     <para>
    ///         If Entity Account has not balance
    ///         <example>s
    ///            <br /> 
    ///             <code>
    ///                 {
    ///                     "success":false
    ///                     "message": "There Is Not Enough Balance For This Account"
    ///                 }
    ///             </code>
    ///         </example>
    ///     </para>
    /// </response>
    [HttpPost]
    [Route("debit")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Response<bool>>> AddDebit([FromBody] AddDebitRequest request)
    {
        return Created("", await Mediator.Send(request));
    }

    /// <summary>
    /// Transfer Value
    /// </summary>
    /// <remarks>
    ///     Any additional text you want
    /// </remarks>
    /// <returns>This is the list of results</returns>
    /// <response code="400">
    ///     <para>
    ///         If property From is empty
    ///         <example>
    ///            <br /> 
    ///             <code>
    ///                 {
    ///                     "success":false
    ///                     "message": "The Property From Cannot Be Null"
    ///                 }
    ///             </code>
    ///         </example>
    ///     </para>
    ///      <para>
    ///         If property To is empty
    ///         <example>
    ///            <br /> 
    ///             <code>
    ///                 {
    ///                     "success":false
    ///                     "message": "The Property To Cannot Be Null"
    ///                 }
    ///             </code>
    ///         </example>
    ///     </para>
    ///     <para>
    ///         If property Value less than zero
    ///         <example>
    ///            <br /> 
    ///             <code>
    ///                 {
    ///                     "success":false
    ///                     "message": "The Sent Value Needs Be Greater Than Zero"
    ///                 }
    ///             </code>
    ///         </example>
    ///     </para>
    ///     <para>
    ///         If Entity Account not found by Account Code
    ///         <example>
    ///            <br /> 
    ///             <code>
    ///                 {
    ///                     "success":false
    ///                     "message": "The Entity Account Cannot Be Found"
    ///                 }
    ///             </code>
    ///         </example>
    ///     </para>
    ///     <para>
    ///         If Entity From Account has not balance
    ///         <example>s
    ///            <br /> 
    ///             <code>
    ///                 {
    ///                     "success":false
    ///                     "message": "There Is Not Enough Balance For This Account"
    ///                 }
    ///             </code>
    ///         </example>
    ///     </para>
    /// </response>
    [HttpPost]
    [Route("transfer")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Response<bool>>> AddTransfer([FromBody] AddTransferRequest request)
    {
        return Created("", await Mediator.Send(request));
    }

    /// <summary>
    /// Send Pix
    /// </summary>
    /// <remarks>
    ///     Any additional text you want
    /// </remarks>
    /// <returns>This is the list of results</returns>
    /// <response code="400">
    ///     <para>
    ///         If property Account is empty
    ///         <example>
    ///            <br /> 
    ///             <code>
    ///                 {
    ///                     "success":false
    ///                     "message": "The Property Account Cannot Be Null"
    ///                 }
    ///             </code>
    ///         </example>
    ///     </para>
    ///      <para>
    ///         If property Key is empty
    ///         <example>
    ///            <br /> 
    ///             <code>
    ///                 {
    ///                     "success":false
    ///                     "message": "The Property Key Be Null"
    ///                 }
    ///             </code>
    ///         </example>
    ///     </para>
    ///     <para>
    ///         If property Value less than zero
    ///         <example>
    ///            <br /> 
    ///             <code>
    ///                 {
    ///                     "success":false
    ///                     "message": "The Sent Value Needs Be Greater Than Zero"
    ///                 }
    ///             </code>
    ///         </example>
    ///     </para>
    ///     <para>
    ///         If Entity Account not found by Account Code
    ///         <example>
    ///            <br /> 
    ///             <code>
    ///                 {
    ///                     "success":false
    ///                     "message": "The Entity Account Cannot Be Found"
    ///                 }
    ///             </code>
    ///         </example>
    ///     </para>
    ///     <para>
    ///         If Entity From Account has not balance
    ///         <example>s
    ///            <br /> 
    ///             <code>
    ///                 {
    ///                     "success":false
    ///                     "message": "There Is Not Enough Balance For This Account"
    ///                 }
    ///             </code>
    ///         </example>
    ///     </para>
    /// </response>
    [HttpPost]
    [Route("pix")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Response<bool>>> SendPix([FromBody] SendPixRequest request)
    {
        return Created("", await Mediator.Send(request));
    }
}