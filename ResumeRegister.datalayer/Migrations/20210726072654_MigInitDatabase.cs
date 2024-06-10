using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ResumeRegister.datalayer.Migrations
{
    public partial class MigInitDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EnPermissionTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ParentID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.PermissionId);
                    table.ForeignKey(
                        name: "FK_Permissions_Permissions_ParentID",
                        column: x => x.ParentID,
                        principalTable: "Permissions",
                        principalColumn: "PermissionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "UserInfos",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstNameUser = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastNameUser = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfos", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    RP_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.RP_Id);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "PermissionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResumeInfos",
                columns: table => new
                {
                    ResumeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MajorStudy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MinorStudy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    WorkBackground = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Expertise = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResumeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: false),
                    IsChecked = table.Column<bool>(type: "bit", nullable: false),
                    Birthday = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    ProvinceOfBirth = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CityOfBirth = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProvinceOfResidence = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CityOfResidence = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    University = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PictureName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumeInfos", x => x.ResumeId);
                    table.ForeignKey(
                        name: "FK_ResumeInfos_UserInfos_UserId",
                        column: x => x.UserId,
                        principalTable: "UserInfos",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UR_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.UR_Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_UserInfos_UserId",
                        column: x => x.UserId,
                        principalTable: "UserInfos",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "PermissionId", "EnPermissionTitle", "ParentID", "PermissionTitle" },
                values: new object[] { 1, "SiteAdmin", null, "مدیر سایت" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "IsDelete", "RoleTitle" },
                values: new object[] { 1, false, "مدیر" });

            migrationBuilder.InsertData(
                table: "UserInfos",
                columns: new[] { "UserId", "Email", "FirstNameUser", "IsActive", "LastNameUser", "Password", "RegisterDate", "UserName" },
                values: new object[,]
                {
                    { 1, "asadi_hamzeh@hotmail.com", "siteManager", true, "siteManager", "@site@Manager", new DateTime(2021, 7, 26, 11, 56, 53, 845, DateTimeKind.Local).AddTicks(9123), "siteManager" },
                    { 2, "asadi_hamzeh@yahoo.com", "حمزه", true, "اسدی", "a", new DateTime(2021, 7, 26, 11, 56, 53, 849, DateTimeKind.Local).AddTicks(1200), "ha" }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "RP_Id", "PermissionId", "RoleId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_ParentID",
                table: "Permissions",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_ResumeInfos_UserId",
                table: "ResumeInfos",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId",
                table: "RolePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResumeInfos");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "UserInfos");
        }
    }
}
