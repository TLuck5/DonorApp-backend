using DonorInfoAPI.models;
using Service_logic_layer.Models.DonorRequestModel;
using Service_logic_layer.Models.DonorResponsemodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_logic_layer.Services.donorInfo
{
    public interface IDonorServices
    {
        public Task<IEnumerable<DonorInfo>>GetDonorList();
        public Task<DonorResponse> GetDonorById(int id);
        public Task<DonorResponse> AddDonorInfo(DonorRequest request);
        public Task<DonorResponse> DeleteDonorInfo(int id);
        public Task<DonorResponse> UpdateDonorInfo(int id, DonorRequest request);
    }
}
