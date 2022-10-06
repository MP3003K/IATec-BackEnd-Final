using Domain.Exceptions.Base;

namespace Application.Exceptions;

public class ThereIsNotEnoughBalanceForThisAccountException : BaseException
{
    private const string MessageKey = "There Is Not Enough Balance For This Account";

    public ThereIsNotEnoughBalanceForThisAccountException() : base(MessageKey)
    {
    }
}