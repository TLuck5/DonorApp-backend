using DonorInfoAPI.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service_logic_layer.Models.DonorRequestModel;
using Service_logic_layer.Services.donorInfo;
using System.Text.Json;

namespace DonorProjectAPI.Controllers
{
    [Route("api"),Authorize]
    [ApiController]
    public class DonorController : ControllerBase
    {
        private readonly IDonorServices _donorservices;

        public DonorController(IDonorServices donorservices)
        {
            _donorservices = donorservices;
        }

        [HttpPost("Add-DonorInfo")]

        public async Task<IActionResult> AddDonorInfo([FromBody]DonorRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var value = await _donorservices.AddDonorInfo(request);
            string jsonString = JsonSerializer.Serialize(value);
            return Ok(jsonString);
        }

        [HttpDelete("Delete-Donor/{id}"),Authorize]

        public async Task<IActionResult> DeleteDonorInfo(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var value = await _donorservices.DeleteDonorInfo(id);
            var jsonString = JsonSerializer.Serialize(value);
            return Ok(jsonString);
        }

        [HttpGet("GetDonorById/{id}"),Authorize]
        public async Task<IActionResult> GetDonorById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var value = _donorservices.GetDonorById(id);
            var jsonString = JsonSerializer.Serialize(value);
            return Ok(jsonString);  
        }

        [HttpGet("GetDonorsList")]
        public async Task<IActionResult> GetDonorList()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var value = _donorservices.GetDonorList();
            var jsonString = JsonSerializer.Serialize(value);
            return Ok(jsonString);
        }

        [HttpPut("UpdateDonorInfo/{id}"),Authorize]
        public async Task<IActionResult> UpdateDonorInfo(int id, [FromBody] DonorRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var value = await _donorservices.UpdateDonorInfo(id, request);
            var jsonString = JsonSerializer.Serialize(value);
            return Ok(jsonString);
        }
    }
}
