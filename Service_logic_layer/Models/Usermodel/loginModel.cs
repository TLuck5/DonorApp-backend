using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_logic_layer.Models.model
{
    public class loginModel
    {
        public string Email { get; set; } = string.Empty;
        public string JwtToken { get; set; } 

        public DateTime? VerifiedAt { get; set; }
    }
}
