
using ChamCong.Common.Utils;
using ChamCong.Data.Data.Entities;
using ChamCong.Data01.Data.Entities.Profile;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong.Data.Entities
{
    public class ImDbContext:DbContext
    {
        public ImDbContext(DbContextOptions<ImDbContext> options) : base(options) { }
        public virtual DbSet<im_Plan> im_Plan { get; set; }
        public virtual DbSet<im_Task> im_Task { get; set; }
        public virtual DbSet<im_User> im_User { get; set; }
        public virtual DbSet<im_User_Role> im_User_Role { get; set; }
        public virtual DbSet<im_User_Group> im_User_Group { get; set; }
        public virtual DbSet<im_User_Credential> im_Credential { get; set; }
        private string connectionString=Utils.GetConfig("ConnectionStrings:MyDb");
        public ImDbContext()
        {
            connectionString = Utils.GetConfig("ConnectionStrings:MyDb");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-35OQRMO;Initial Catalog=ChamCongAPI;Integrated Security=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<im_User>(entity => { entity.HasIndex(e => e.UserName).IsUnique(); });
            modelBuilder.Entity<im_User>(entity => { entity.HasIndex(e => e.EmplyeeId).IsUnique(); });
            //set bảng 2 key
            modelBuilder.Entity<im_User_Credential>().HasKey(c => new { c.UserGroupId, c.UserRoleId });
        }
    }
}
