using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChamCong.API.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "im_User_Group",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_User_Group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "im_User_Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_User_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "im_User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmplyeeId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PassWord = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GroudID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_im_User_im_User_Group_GroudID",
                        column: x => x.GroudID,
                        principalTable: "im_User_Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "im_User_Credential",
                columns: table => new
                {
                    UserGroupId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserRoleId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserGroupId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserRoleId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_User_Credential", x => new { x.UserGroupId, x.UserRoleId });
                    table.ForeignKey(
                        name: "FK_im_User_Credential_im_User_Group_UserGroupId1",
                        column: x => x.UserGroupId1,
                        principalTable: "im_User_Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_im_User_Credential_im_User_Role_UserRoleId1",
                        column: x => x.UserRoleId1,
                        principalTable: "im_User_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "im_Plan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsLate = table.Column<bool>(type: "bit", nullable: false),
                    CompletionPercentage = table.Column<float>(type: "real", nullable: false),
                    TotalTaskPlannedCount = table.Column<int>(type: "int", nullable: false),
                    TotalTaskComplete = table.Column<int>(type: "int", nullable: false),
                    TotalTaskOutStandingCount = table.Column<int>(type: "int", nullable: false),
                    TotalTimeWorkCount = table.Column<int>(type: "int", nullable: false),
                    TimeCheckIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeCheckOut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_Plan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_im_Plan_im_User_UserId",
                        column: x => x.UserId,
                        principalTable: "im_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "im_Task",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TypeTask = table.Column<int>(type: "int", nullable: false),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false),
                    PlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_im_Task", x => x.Id);
                    table.ForeignKey(
                        name: "FK_im_Task_im_Plan_PlanId",
                        column: x => x.PlanId,
                        principalTable: "im_Plan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_im_Plan_UserId",
                table: "im_Plan",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_im_Task_PlanId",
                table: "im_Task",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_im_User_EmplyeeId",
                table: "im_User",
                column: "EmplyeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_im_User_GroudID",
                table: "im_User",
                column: "GroudID");

            migrationBuilder.CreateIndex(
                name: "IX_im_User_UserName",
                table: "im_User",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_im_User_Credential_UserGroupId1",
                table: "im_User_Credential",
                column: "UserGroupId1");

            migrationBuilder.CreateIndex(
                name: "IX_im_User_Credential_UserRoleId1",
                table: "im_User_Credential",
                column: "UserRoleId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "im_Task");

            migrationBuilder.DropTable(
                name: "im_User_Credential");

            migrationBuilder.DropTable(
                name: "im_Plan");

            migrationBuilder.DropTable(
                name: "im_User_Role");

            migrationBuilder.DropTable(
                name: "im_User");

            migrationBuilder.DropTable(
                name: "im_User_Group");
        }
    }
}
