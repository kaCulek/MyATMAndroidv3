using System;

namespace MyATMAndroid
{
    public class AtmUserTransaction
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public decimal AccountBalance { get; set; }
    }
}