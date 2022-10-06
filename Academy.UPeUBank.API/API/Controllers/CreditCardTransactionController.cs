using API.Controllers.Base;
using Application.Features.CreditCardTransaction.Commands;
using Application.Features.Transaction.Commands;
using Application.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CreditCardTransactionController : BaseController
    {

        /// <summary>
        /// Debit Value
        /// </summary>
        /// <remarks>
        ///     Any additional text you want
        /// </remarks>
        /// <returns>This is the list of results</returns>
        /// <response code="400">
        ///     <para>
        ///         If property Number is empty
        ///         <example>
        ///            <br /> 
        ///             <code>
        ///                 {
        ///                     "success":false
        ///                     "message": "The Property Number Cannot Be Null"
        ///                 }
        ///             </code>
        ///         </example>
        ///     </para>
        ///     <para>
        ///         If property Value is empty
        ///         <example>
        ///            <br /> 
        ///             <code>
        ///                 {
        ///                     "success":false
        ///                     "message": "The Property Value Cannot Be Null"
        ///                 }
        ///             </code>
        ///         </example>
        ///     </para>
        ///     <para>
        ///         If property Value is less or equal than zero.
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
        ///     <para>
        ///         If Entity CreditCard not found by CreditCard Number
        ///         <example>
        ///            <br /> 
        ///             <code>
        ///                 {
        ///                     "success":false
        ///                     "message": "The Entity CreditCard Cannot Be Found"
        ///                 }
        ///             </code>
        ///         </example>
        ///     </para>
        ///     
        ///     <para>
        ///         If CreditCard has not credit
        ///         <example>
        ///            <br /> 
        ///             <code>
        ///                 {
        ///                     "success":false
        ///                     "message": "Your Credit Card Does Not Have Sufficient Funds To Complete The Transaction"
        ///                 }
        ///             </code>
        ///         </example>
        ///     </para>
        /// </response>
        [HttpPost]
        [Route("debitCreditCard")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Response<bool>>> AddCreditCard([FromBody] AddDebitToCreditCardRequest request)
        {
            return Created("", await Mediator.Send(request));
        }
    }
}
