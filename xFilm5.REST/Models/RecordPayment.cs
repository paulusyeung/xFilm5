using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace xFilm5.REST.Models
{
    public class RecordPayment
    {
        public int ClientId { get; set; }
        public int InvoiceId { get; set; }
        public int InvoiceNumber { get; set; }
        public decimal InvoiceAmount { get; set; }
        public DateTime? PaidOn { get; set; }
        public string PaidBy { get; set; }
        public string Remarks { get; set; }
        public int UserId { get; set; }
    }
}