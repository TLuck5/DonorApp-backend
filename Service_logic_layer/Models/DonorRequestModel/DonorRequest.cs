    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_logic_layer.Models.DonorRequestModel
{
    public class DonorRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? MobNo { get; set; }
        public string Address { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public string BloodGroup { get; set; } = string.Empty;
        public string ImageName { get; set; } = string.Empty;
    }
}
