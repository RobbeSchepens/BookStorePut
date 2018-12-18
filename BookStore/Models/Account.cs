using Microsoft.AspNet.OData.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Account
    {
        public int AccountID { get; set; }
        public string Name { get; set; }
        [Contained]
        public IList<PaymentInstrument> PayinPIs { get; set; }
    }

    public class PaymentInstrument
    {
        public int PaymentInstrumentID { get; set; }
        public string FriendlyName { get; set; }
    }
}
