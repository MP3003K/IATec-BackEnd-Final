using Domain.Exceptions.Base;

namespace Application.Exceptions;

public class CreditCardWithInsufficientBalanceException : BaseException
{
    private const string MessageKey = "Your Credit Card Does Not Have Sufficient Funds To Complete The Transaction";

    public CreditCardWithInsufficientBalanceException() : base(MessageKey)
    {
       
    }
}