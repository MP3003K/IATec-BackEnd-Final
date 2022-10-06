using Domain.Entities.Base;

namespace Domain.Entities
{
    public class CreditCard : Entity
    {
        public int AccountId { get; private set; }
        public string Number { get; private set; } = string.Empty;
        public double Limit { get; private set; }
        public virtual Account? Account { get; private set; }
        public virtual IList<CreditCardTransaction>? CreditCardTransactions { get; private set; }

        public CreditCard(int accountId, double limit)
        {   
            AccountId = accountId;
            Limit = limit;
            UpdateNumber();
        }

        public void UpdateNumber()
        {
            Number = GenerateNumber();
        }

        public void UpdateAccountId(int accountId)
        {
            AccountId = accountId;
        }

        public void UpdateLimit(double limit)
        {
            Limit = limit;
        }

        public void Update(int accountId, double limit)
        {
            UpdateAccountId(accountId);
            UpdateLimit(limit);
        }

        private string GenerateNumber()
        {
            var number = "";

            for (int i = 0; i < 12; i++)
            { 
                number += GenerateRandomNumber().ToString();
            }

            return number;

        }
        private int GenerateRandomNumber()
        {
            var guid = Guid.NewGuid();
            var justNumbers = new String(guid.ToString().Where(Char.IsDigit).ToArray());
            var seed = int.Parse(justNumbers.Substring(0, 4));

            var random = new Random(seed);
            var value = random.Next(0, 9);

            return value;
        }
    }
}
