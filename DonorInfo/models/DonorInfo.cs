namespace DonorInfoAPI.models
{
    public class DonorInfo
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? MobNo { get; set; }
        public string Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string BloodGroup { get; set; }
        public string ImageName { get; set; }

    }
}
