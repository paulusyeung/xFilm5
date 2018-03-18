using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xFilm5.EF6;

namespace xFilm5.REST.Models
{
    public partial class UserEx : User
    {
        public int UserRole { get; set; }
        public String UserRoleName{ get; set; }
    }
}
