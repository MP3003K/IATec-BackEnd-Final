using Domain.Entities.Base;

namespace Domain.Entities;

public class Pix : Entity
{
    public int AccountId { get; private set; }
    public string Key { get; private set; }

    public virtual Account? Account { get; private set; }

    public Pix(int accountId, string key)
    {
        AccountId = accountId;
        Key = key;
    }

    public void UpdateAccountId(int accountId)
    {
        AccountId = accountId;
    }

    public void UpdateKey(string key)
    {
        Key = key;
    }

    public void Update(int accountId, string key)
    {
        UpdateAccountId(accountId);
        UpdateKey(key);
    }
}