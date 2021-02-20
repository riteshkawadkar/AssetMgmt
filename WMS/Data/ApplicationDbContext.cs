using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WMS.Data.Request;
using WMS.Data.Worfklow;
using WMS.Data.Workflow;
using WMS.Models;

namespace WMS.Data
{
    public class ApplicationDbContext : IdentityDbContext<Employee>
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<ITAsset>(q =>
            {
                q.HasIndex(x => x.HostName).IsUnique(true);
                q.HasIndex(x => x.IPAddress).IsUnique(true);
            });

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        public DbSet<Employee> Employees { get; set; }



        public DbSet<Company> Companies { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<SubDepartment> SubDepartments { get; set; }
        public DbSet<DArea> DAreas { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<ITAsset> ITAssets { get; set; }
        public DbSet<EquipmentType> EquipmentTypes { get; set; }



        public DbSet<RequestType> RequestTypes { get; set; }
        public DbSet<MyRequest> MyRequests { get; set; }



        public DbSet<WorkflowPasswordChange> WorkflowPasswordChanges { get; set; }
        public DbSet<WorkflowGrantAccess> WorkflowGrantAccesses { get; set; }


        public DbSet<RequestWorkflow> RequestWorkflows { get; set; }



  



    }
}
