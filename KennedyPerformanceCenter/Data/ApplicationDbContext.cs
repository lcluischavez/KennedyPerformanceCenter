using System;
using System.Collections.Generic;
using System.Text;
using KennedyPerformanceCenter.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KennedyPerformanceCenter.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<DrivetrainType> DrivetrainType { get; set; }
        public DbSet<TransmissionType> TransmissionType { get; set; }
        public DbSet<EngineType> EngineType { get; set; }
        public DbSet<Part> Part { get; set; }
        public DbSet<PartType> PartType { get; set; }
        public DbSet<Service> Service { get; set; }
    }
}
