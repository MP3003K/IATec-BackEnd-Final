using Domain.Exceptions.Base;

namespace Application.Exceptions;

public class LimitNeedsBeGreaterThanZeroException : BaseException
{
    private const string MessageKey = "The Send Limit Needs Be Greater Than Zero";

    public LimitNeedsBeGreaterThanZeroException() : base(MessageKey)
    {
    }
}