using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using ResumeRegister.datalayer.Entities.Permissions;
using ResumeRegister.datalayer.Entities.Resume;
using ResumeRegister.datalayer.Entities.Users;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace ResumeRegister.datalayer.Context
{
    public class ResumeRegisterContext : DbContext
    {
        public ResumeRegisterContext(DbContextOptions<ResumeRegisterContext> options) : base(options)
        {

        }

        #region Users

        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        #endregion

        #region Resume

        public DbSet<ResumeInfo> ResumeInfos { get; set; }

        #endregion

        #region Seed Data


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>().HasData(new Permission()
            {
                PermissionId = 1,
                PermissionTitle = "مدیر سایت",
                EnPermissionTitle = "SiteAdmin"
            });
            modelBuilder.Entity<Role>().HasData(new Role()
            {
                RoleId = 1,
                RoleTitle = "مدیر"
            });
            modelBuilder.Entity<RolePermission>().HasData(new RolePermission()
            {
                RP_Id = 1,
                PermissionId = 1,
                RoleId = 1
            });
            modelBuilder.Entity<UserRole>().HasData(new UserRole()
            {
                UserId = 1,
                RoleId = 1,
                UR_Id = 1
            });
            modelBuilder.Entity<UserRole>().HasData(new UserRole()
            {
                UserId = 2,
                RoleId = 1,
                UR_Id = 2
            });
            modelBuilder.Entity<UserInfo>().HasData(new UserInfo()
            {
                Email = "asadi_hamzeh@hotmail.com",
                FirstNameUser = "siteManager",
                IsActive = true,
                LastNameUser = "siteManager",
                Password = "@site@Manager",
                RegisterDate = DateTime.Now,
                UserId = 1,
                UserName = "siteManager"
            });
            modelBuilder.Entity<UserInfo>().HasData(new UserInfo()
            {
                Email = "asadi_hamzeh@yahoo.com",
                FirstNameUser = "حمزه",
                IsActive = true,
                LastNameUser = "اسدی",
                Password = "a",
                RegisterDate = DateTime.Now,
                UserId = 2,
                UserName = "ha"
            });
        }

        #endregion
    }
}
