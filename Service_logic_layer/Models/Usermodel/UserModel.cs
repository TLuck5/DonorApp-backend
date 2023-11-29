using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_logic_layer.Models.model
{
    public class UserModel
    {
        public string Email { get; set; } = string.Empty;
        public string? VerificationToken { get; set; }
    }
}
