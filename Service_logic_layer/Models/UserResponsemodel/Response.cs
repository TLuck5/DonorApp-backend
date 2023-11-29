using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service_logic_layer.Models.model;

namespace Service_logic_layer.Models.responsemodel
{
    public class Response
    {
        public int statuscode { get; set; }
        public UserModel User { get; set; }
        public ForgotPassModel ForgotPassData { get; set; }
        public loginModel LoginModel { get; set; }
        public string message { get; set; }
    }
}
