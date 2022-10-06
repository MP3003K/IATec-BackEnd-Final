using API.Controllers.Base;
using Application.Features.CreditCard.Commands;
using Application.Wrappers;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CreditCardController : BaseController
    {
        /// <summary>
        ///  Add new credit card
        /// </summary>
        /// <remarks>
        /// Any additional text you want
        /// </remarks>
        /// <param name="request"></param>
        /// <returns>this is the list of results</returns>
        /// <response code="400">
        ///     <para>
        ///         If property AccountId is empty
        ///         <example>
        ///         <br />
        ///         <code>
        ///                 {
        ///                     "success":false
        ///                     "message": "The Property AccountId Cannot Be Null"
        ///                 }
        ///         </code>
        ///         </example>
        ///     </para>
        ///     <para>
        ///         If property Limit is empty
        ///         <example>
        ///         <br />
        ///         <code>
        ///                 {
        ///                     "success":false
        ///                     "message": "The Property Limit Cannot Be Null"
        ///                 }
        ///         </code>
        ///         </example>
        ///     </para>
        ///     <para>
        ///         If Entity Account not found by AccountId
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
        ///         If property Limit is less than zero.
        ///         <example>
        ///             <br /> 
        ///             <code>
        ///                 {
        ///                     "success":false
        ///                     "message": "The Sent Limit Needs Be Greater Than Zero"
        ///                 }
        ///             </code>
        ///         </example>
        ///     </para>
        /// </response>
        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<CreditCardDto>>> AddCreditCard([FromBody] AddCreditCardRequest request)
        {
            return Created("", await Mediator.Send(request));
        }
    }
}
