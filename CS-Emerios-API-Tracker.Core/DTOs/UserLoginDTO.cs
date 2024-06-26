using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Emerios_API_Tracker.Core.DTOs
{
    public class UserLoginDTO
    {
        public string UsrUsername { get; set; } = null!;
        public string UsrPassword { get; set; } = null!;
        public int? empid { get; set; }
        public DateTime SessionDate { get; set; }
    }
}
