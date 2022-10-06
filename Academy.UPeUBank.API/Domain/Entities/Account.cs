using Domain.Entities.Base;

namespace Domain.Entities;

public class Account : Entity
{
    public int CustomerId { get; private set; }
    public string Code { get; private set; }
    public double Balance { get; private set; }

    public virtual Customer? Customer { get; private set; }
    public virtual IList<Transaction>? Transactions { get; private set; }
    public virtual IList<Pix>? Pixes { get; private set; }
    public virtual IList<CreditCard>? CreditCards { get; private set; }

    public Account(int customerId, string code, double balance)
    {
        CustomerId = customerId;
        Code = code;
        Balance = balance;
    }

    public void UpdateCustomerId(int customerId)
    {
        CustomerId = customerId;
    }

    public void UpdateCode(string code)
    {
        Code = code;
    }

    public void UpdateBalance(double balance)
    {
        Balance = balance;
    }

    public void Update(int customerId, string code, double balance)
    {
        UpdateCustomerId(customerId);
        UpdateCode(code);
        UpdateBalance(balance);
    }
}