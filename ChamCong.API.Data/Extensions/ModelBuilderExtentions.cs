using ChamCong.API.Data.Data.Profile;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong.API.Data.Extensions
{
    public static class ModelBuilderExtentions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            //phân quyền cho user
            modelBuilder.Entity<im_User_Group>().HasData(
                new im_User_Group() { Id = new Guid("00000000-0000-0000-0000-000000000001"), Name = "ADMIN", Office = "Quản trị" },
                new im_User_Group() { Id = new Guid("00000000-0000-0000-0000-000000000002"), Name ="MEMBER",Office="Thành Viên"},
                new im_User_Group() { Id = new Guid("00000000-0000-0000-0000-000000000003"), Name ="MOD",Office= "Moderator" }
                );
            modelBuilder.Entity<im_User_Role>().HasData(
                new im_User_Role() { Id = new Guid("00000000-0000-0000-0000-000000000001"), Name ="VIEW_USER",Office="Xem danh sách user"},
                new im_User_Role() { Id = new Guid("00000000-0000-0000-0000-000000000002"), Name ="EDIT_USER",Office="Sửa user"},
                new im_User_Role() { Id = new Guid("00000000-0000-0000-0000-000000000003"), Name = "DELETE_USER", Office = "Xóa user" },
                new im_User_Role() { Id = new Guid("00000000-0000-0000-0000-000000000004"), Name = "ADD_USER", Office = "Thêm user" },
                new im_User_Role() { Id = new Guid("00000000-0000-0000-0000-000000000005"), Name = "VIEW_TIMESHEET", Office = "Xem danh sách timesheet" }
                );
        }
    }
}
