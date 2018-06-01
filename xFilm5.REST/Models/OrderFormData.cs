using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xFilm5.EF6;

namespace xFilm5.REST.Models
{
    public class OrderFormData_Plate
    {
        public int ClientId { get; set; }
        public List<vwPrintQueueVpsList_AvailablePlate> Items { get; set; }
        public int Priority { get; set; }
        public string Workshop { get; set; }
        public string Remarks { get; set; }
        public bool Pickup { get; set; }
        public bool Deliver { get; set; }
        public int DeliverTo { get; set; }
    }

    public class OrderFormData_Film
    {
        public int ClientId { get; set; }
        public List<vwPrintQueueVpsList_AvailableFilm> Items { get; set; }
        public int Priority { get; set; }
        public string Workshop { get; set; }
        public string Remarks { get; set; }
        public bool Pickup { get; set; }
        public bool Deliver { get; set; }
        public int DeliverTo { get; set; }
    }

    public class OrderFormData_Blueprint
    {
        public int ClientId { get; set; }
        public List<vwPrintQueueVpsList_AvailablePlate> Items { get; set; }
        public int Priority { get; set; }
        public string Workshop { get; set; }
        public string Remarks { get; set; }
        public bool Pickup { get; set; }
        public bool Deliver { get; set; }
        public int DeliverTo { get; set; }
    }
}
