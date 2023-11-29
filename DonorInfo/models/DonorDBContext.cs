using Microsoft.EntityFrameworkCore;

namespace DonorInfoAPI.models
{
    public class DonorDBContext:DbContext
    {
        public DonorDBContext(DbContextOptions<DonorDBContext>options):base(options) 
        {
            
        }
        DbSet<DonorInfo>DonorInfo { get; set; }
    }
}
