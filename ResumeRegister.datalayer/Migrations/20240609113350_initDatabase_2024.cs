using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ResumeRegister.datalayer.Migrations
{
    public partial class initDatabase_2024 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserInfos",
                keyColumn: "UserId",
                keyValue: 1,
                column: "RegisterDate",
                value: new DateTime(2024, 6, 9, 15, 3, 49, 402, DateTimeKind.Local).AddTicks(5576));

            migrationBuilder.UpdateData(
                table: "UserInfos",
                keyColumn: "UserId",
                keyValue: 2,
                column: "RegisterDate",
                value: new DateTime(2024, 6, 9, 15, 3, 49, 404, DateTimeKind.Local).AddTicks(8376));

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UR_Id", "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "UR_Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "UR_Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "UserInfos",
                keyColumn: "UserId",
                keyValue: 1,
                column: "RegisterDate",
                value: new DateTime(2021, 7, 26, 11, 56, 53, 845, DateTimeKind.Local).AddTicks(9123));

            migrationBuilder.UpdateData(
                table: "UserInfos",
                keyColumn: "UserId",
                keyValue: 2,
                column: "RegisterDate",
                value: new DateTime(2021, 7, 26, 11, 56, 53, 849, DateTimeKind.Local).AddTicks(1200));
        }
    }
}
