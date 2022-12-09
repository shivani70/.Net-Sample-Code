using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OMTDal.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace OMTDal.Configuration
{
    public class OmtContext : DbContext
    {
        private IConfiguration _configuration;
        public OmtContext(IConfiguration configuration)
        {
            _configuration = configuration; 
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetSection("ConnectionStrings").GetSection("DefaultConnnection").Value);
        }
        public virtual DbSet<User> Users { get; set; }
    }
}
