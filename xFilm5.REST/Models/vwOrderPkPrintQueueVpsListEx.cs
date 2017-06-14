using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xFilm5.EF6;

namespace xFilm5.REST.Models
{
    public partial class vwOrderPkPrintQueueVpsListEx: vwOrderPkPrintQueueVpsList
    {
        /// <summary>
        /// 1 = Blueprint, 2 = Film, 3 = Plate
        /// </summary>
        public int OrderType { get; set; }
    }
}
