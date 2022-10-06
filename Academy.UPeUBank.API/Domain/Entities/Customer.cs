using Domain.Entities.Base;

namespace Domain.Entities;

public class Customer : Entity
{
    public string Name { get; private set; }

    public virtual IList<Account>? Accounts { get; private set; }

    public Customer(string name)
    {
        Name = name;
    }

    public void UpdateName(string name)
    {
        Name = name;
    }

    public void Update(string name)
    {
        UpdateName(name);
    }
}