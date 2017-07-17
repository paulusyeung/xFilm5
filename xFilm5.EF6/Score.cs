using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xFilm5.EF6
{
    public partial class Score
    {
        public string date { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
        public decimal paid { get; set; }
        public decimal unpaid { get; set; }
        public decimal subtotal { get; set; }
    }
}
