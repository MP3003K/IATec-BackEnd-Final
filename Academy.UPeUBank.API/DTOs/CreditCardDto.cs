using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class CreditCardDto
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Number { get; set; } = string.Empty;
        public double Limit { get; set; }
    }
}
