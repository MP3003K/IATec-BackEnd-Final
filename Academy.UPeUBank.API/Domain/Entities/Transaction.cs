using Domain.Entities.Base;

namespace Domain.Entities;

public class Transaction : Entity
{
    public int AccountId { get; private set; }
    public double Value { get; private set; }
    public DateTime Date { get; private set; }

    public virtual Account? Account { get; private set; }

    public Transaction(int accountId, double value)
    {
        AccountId = accountId;
        Value = value;
    }
}