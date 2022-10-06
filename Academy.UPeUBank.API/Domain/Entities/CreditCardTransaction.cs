using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CreditCardTransaction : Entity
    {
        public int CreditCardId { get; private set; }
        public double Value { get; private set; }
        public DateTime Date { get; private set; }
        public virtual CreditCard? CreditCard { get; private set; }


        public CreditCardTransaction(int creditCardId, double value)
        {
            CreditCardId = creditCardId;
            Value = value;

        }
    }
}
