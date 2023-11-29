using DonorInfoAPI.models;
using Service_logic_layer.Models.DonorModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_logic_layer.Models.DonorResponsemodel
{
    public class DonorResponse
    {
        public int statusCode {  get; set; }
        public string message { get; set; } =   string.Empty;
        public string errorMessage { get; set; } = string.Empty;
        public DModel donorInfo { get; set; }
    }
}
