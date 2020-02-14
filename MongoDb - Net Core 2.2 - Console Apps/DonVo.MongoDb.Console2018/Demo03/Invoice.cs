using System;
using System.Collections.Generic;

namespace DonVo.MongoDb.Console2018.Demo03
{
    public class Invoice
    {
        public string Id { get; set; }
        public string Buyer { get; set; }
        public DateTime Date { get; set; }
        public int PayableInDays { get; set; }

        public IList<InvoiceLine> Lines { get; set; }
    }
}
