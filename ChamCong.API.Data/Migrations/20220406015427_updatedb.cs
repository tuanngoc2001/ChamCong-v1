using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChamCong.API.Data.Migrations
{
    public partial class updatedb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "im_User_Role",
                newName: "Office");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "im_User_Group",
                newName: "Office");

            migrationBuilder.InsertData(
                table: "im_User_Group",
                columns: new[] { "Id", "Name", "Office" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "ADMIN", "Quản trị" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "MEMBER", "Thành Viên" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "MOD", "Moderator" }
                });

            migrationBuilder.InsertData(
                table: "im_User_Role",
                columns: new[] { "Id", "Name", "Office" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "VIEW_USER", "Xem danh sách user" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "EDIT_USER", "Sửa user" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "DELETE_USER", "Xóa user" },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "ADD_USER", "Thêm user" },
                    { new Guid("00000000-0000-0000-0000-000000000005"), "VIEW_TIMESHEET", "Xem danh sách timesheet" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_im_User_Role_Name",
                table: "im_User_Role",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_im_User_Group_Name",
                table: "im_User_Group",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_im_User_Role_Name",
                table: "im_User_Role");

            migrationBuilder.DropIndex(
                name: "IX_im_User_Group_Name",
                table: "im_User_Group");

            migrationBuilder.DeleteData(
                table: "im_User_Group",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "im_User_Group",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "im_User_Group",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "im_User_Role",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "im_User_Role",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "im_User_Role",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "im_User_Role",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "im_User_Role",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"));

            migrationBuilder.RenameColumn(
                name: "Office",
                table: "im_User_Role",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Office",
                table: "im_User_Group",
                newName: "Title");
        }
    }
}
