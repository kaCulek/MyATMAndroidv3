using System.Collections.Generic;
using System.Linq;

namespace MyATMAndroid
{
    public class AtmUser
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string Pin { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public List<AtmUserTransaction> Transactions { get; set; }
        public decimal GetCurrentBalance()
        {
            var lastTransaction = Transactions.OrderByDescending(i => i.Date).First();
            return lastTransaction.AccountBalance;
        }
    }
}