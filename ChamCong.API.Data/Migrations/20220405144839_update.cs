using Microsoft.EntityFrameworkCore.Migrations;

namespace ChamCong.API.Data.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_im_User_Credential_im_User_Group_UserGroupId1",
                table: "im_User_Credential");

            migrationBuilder.DropForeignKey(
                name: "FK_im_User_Credential_im_User_Role_UserRoleId1",
                table: "im_User_Credential");

            migrationBuilder.RenameColumn(
                name: "UserRoleId1",
                table: "im_User_Credential",
                newName: "im_User_RoleId");

            migrationBuilder.RenameColumn(
                name: "UserGroupId1",
                table: "im_User_Credential",
                newName: "im_User_GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_im_User_Credential_UserRoleId1",
                table: "im_User_Credential",
                newName: "IX_im_User_Credential_im_User_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_im_User_Credential_UserGroupId1",
                table: "im_User_Credential",
                newName: "IX_im_User_Credential_im_User_GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_im_User_Credential_im_User_Group_im_User_GroupId",
                table: "im_User_Credential",
                column: "im_User_GroupId",
                principalTable: "im_User_Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_im_User_Credential_im_User_Role_im_User_RoleId",
                table: "im_User_Credential",
                column: "im_User_RoleId",
                principalTable: "im_User_Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_im_User_Credential_im_User_Group_im_User_GroupId",
                table: "im_User_Credential");

            migrationBuilder.DropForeignKey(
                name: "FK_im_User_Credential_im_User_Role_im_User_RoleId",
                table: "im_User_Credential");

            migrationBuilder.RenameColumn(
                name: "im_User_RoleId",
                table: "im_User_Credential",
                newName: "UserRoleId1");

            migrationBuilder.RenameColumn(
                name: "im_User_GroupId",
                table: "im_User_Credential",
                newName: "UserGroupId1");

            migrationBuilder.RenameIndex(
                name: "IX_im_User_Credential_im_User_RoleId",
                table: "im_User_Credential",
                newName: "IX_im_User_Credential_UserRoleId1");

            migrationBuilder.RenameIndex(
                name: "IX_im_User_Credential_im_User_GroupId",
                table: "im_User_Credential",
                newName: "IX_im_User_Credential_UserGroupId1");

            migrationBuilder.AddForeignKey(
                name: "FK_im_User_Credential_im_User_Group_UserGroupId1",
                table: "im_User_Credential",
                column: "UserGroupId1",
                principalTable: "im_User_Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_im_User_Credential_im_User_Role_UserRoleId1",
                table: "im_User_Credential",
                column: "UserRoleId1",
                principalTable: "im_User_Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
