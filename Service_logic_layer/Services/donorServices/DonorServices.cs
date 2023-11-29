using DonorInfoAPI.models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Service_logic_layer.Models.DonorModel;
using Service_logic_layer.Models.DonorRequestModel;
using Service_logic_layer.Models.DonorResponsemodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_logic_layer.Services.donorInfo
{
    public class DonorServices : IDonorServices
    {
        private readonly DonorDBContext _db;

        public DonorServices(DonorDBContext db)
        {
            _db = db;
        }
        public async Task<DonorResponse> AddDonorInfo(DonorRequest request)
        {

            DonorResponse response = new DonorResponse();
            if(request == null) 
            {
                response.statusCode = 400;
                response.errorMessage = "Request is null";
                return response;
            }
            DonorInfo dinfo = new DonorInfo()
            {
                Name = request.Name,
                MobNo = request.MobNo,
                Address = request.Address,
                DateOfBirth = request.DateOfBirth,
                BloodGroup = request.BloodGroup,
                ImageName = request.ImageName,
            };
            _db.Add(dinfo);
            await _db.SaveChangesAsync();
            var dmodel = dinfo.Adapt<DModel>();
            response.statusCode = 200;
            response.message = "Added Donor Sucessfully";
            response.donorInfo = dmodel;
            return response;

        }

        public async Task<DonorResponse> DeleteDonorInfo(int id)
        {
            DonorResponse response = new DonorResponse();

            if(id==null)
            {
                response.statusCode = 500;
                response.errorMessage = "Enter the id you want to delete";
                return response;
            }
            var donor =await _db.DonorInfo.FirstOrDefaultAsync(d => d.Id == id);
            if(donor == null)
            {
                response.statusCode = 500;
                response.errorMessage = "Donor not found";
                return response;
            }
            _db.DonorInfo.Remove(donor);
            await _db.SaveChangesAsync();
            var dmodel = donor.Adapt<DModel>();
            response.statusCode = 200;
            response.message = "Donor Successfully deleted";
            response.donorInfo = dmodel;
            return response;
        }

        public async Task<DonorResponse> GetDonorById(int id)
        {
            DonorResponse response = new DonorResponse();

            if (id == null)
            {
                response.statusCode = 500;
                response.errorMessage = "Enter the valid id";
                return response;
            }
            var donor  =await _db.DonorInfo.FirstOrDefaultAsync(d => d.Id == id);
            if(donor == null)
            {
                response.statusCode = 500;
                response.errorMessage = "Donor not found";
                return response;
            }
            var dmodel = donor.Adapt<DModel>();
            response.statusCode = 200;
            response.message = $"Donor Info of id {id} is generated";
            response.donorInfo = dmodel;
            return response;
        }

        public async Task<IEnumerable<DonorInfo>> GetDonorList()
        {
            var d =await _db.DonorInfo.ToListAsync();
            return d;
        }

        public async Task<DonorResponse> UpdateDonorInfo(int id, DonorRequest request)
        {
            DonorResponse response = new DonorResponse();
            if(id==null ||  request==null)
            {
                response.statusCode=500;
                response.errorMessage = "Invalid Parameters";
                return response;
            }
            var donor = _db.DonorInfo.FirstOrDefault(d => d.Id == id);
            if(donor == null)
            {
                response.statusCode = 500;
                response.errorMessage = "Donor not exists";
                return response;
            }
            donor.Name = request.Name;
            donor.MobNo = request.MobNo;
            donor.Address = request.Address;
            donor.DateOfBirth = request.DateOfBirth;
            donor.BloodGroup = request.BloodGroup;
            donor.ImageName = request.ImageName;
            _db.DonorInfo.Update(donor);
            _db.SaveChangesAsync();
            var dmodel = donor.Adapt<DModel>();
            response.statusCode = 200;
            response.message = $"User with id {id} Updated successfully";
            response.donorInfo = dmodel;
            return response;
        }
    }
}
