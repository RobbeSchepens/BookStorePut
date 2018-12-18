using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BookStore.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class AccountsController : ODataController
    {
        private static IList<Account> _accounts = null;
        public AccountsController()
        {
            if (_accounts == null)
            {
                _accounts = InitAccounts();
            }
        }

        // PUT ~/Accounts(100)/PayinPIs(101)         
        [ODataRoute("Accounts({accountId})/PayinPIs({paymentInstrumentId})")]
        [HttpPut]
        public IActionResult PutToPayinPI(int accountId, int paymentInstrumentId, [FromBody]PaymentInstrument paymentInstrument)
        {
            var account = _accounts.Single(a => a.AccountID == accountId);
            var originalPi = account.PayinPIs.Single(p => p.PaymentInstrumentID == paymentInstrumentId);
            originalPi.FriendlyName = paymentInstrument.FriendlyName;
            return Ok(paymentInstrument);
        }

        private static IList<Account> InitAccounts()
        {
            var accounts = new List<Account>()
            {
                new Account()
                {
                    AccountID = 100,
                    Name="Name100",
                    PayinPIs = new List<PaymentInstrument>()
                    {
                        new PaymentInstrument()
                        {
                            PaymentInstrumentID = 101,
                            FriendlyName = "101 first PI",
                        },
                        new PaymentInstrument()
                        {
                            PaymentInstrumentID = 102,
                            FriendlyName = "102 second PI",
                        },
                    },
                },
            };
            return accounts;
        }
    }
}
