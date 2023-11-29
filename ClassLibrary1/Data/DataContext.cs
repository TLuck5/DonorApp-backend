
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using VerifyEmailPass.Models;

namespace VerifyEmailPass.Data
{
    public class DataContext:DbContext
    {
        private readonly IConfiguration _configuration;
        public DataContext(DbContextOptions<DataContext>options, IConfiguration configuration) : base(options)
        {

            _configuration = configuration;

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DbConn"));
        }
        public DbSet<User>Users { get; set; }
    }
}   
