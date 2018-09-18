using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace xFilm5.REST.Models
{
    public class Registration
    {
        public string Locale { get; set; }
        public string Workshop { get; set; }
        public string BusinessName { get; set; }
        public string BusinessAddress { get; set; }
        public string BusinessTel { get; set; }
        public string ContactPerson { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}