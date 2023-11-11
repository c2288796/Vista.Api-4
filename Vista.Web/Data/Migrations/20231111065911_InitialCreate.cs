using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vista.Web.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    StaffId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.StaffId);
                });

            migrationBuilder.CreateTable(
                name: "Workshops",
                columns: table => new
                {
                    WorkshopId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    DateAndTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CategoryCode = table.Column<string>(type: "TEXT", nullable: true),
                    BookingRef = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workshops", x => x.WorkshopId);
                });

            migrationBuilder.CreateTable(
                name: "WorkshopStaff",
                columns: table => new
                {
                    WorkshopId = table.Column<int>(type: "INTEGER", nullable: false),
                    StaffId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkshopStaff", x => new { x.WorkshopId, x.StaffId });
                    table.ForeignKey(
                        name: "FK_WorkshopStaff_Staff_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkshopStaff_Workshops_WorkshopId",
                        column: x => x.WorkshopId,
                        principalTable: "Workshops",
                        principalColumn: "WorkshopId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "StaffId", "FirstName", "LastName" },
                values: new object[] { 1, "Shripati", "MacGrory" });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "StaffId", "FirstName", "LastName" },
                values: new object[] { 2, "Nani", "Martinsson" });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "StaffId", "FirstName", "LastName" },
                values: new object[] { 3, "Harrison", "Presley" });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "StaffId", "FirstName", "LastName" },
                values: new object[] { 4, "Theo", "Orr" });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "StaffId", "FirstName", "LastName" },
                values: new object[] { 5, "Drew", "Metcalfe" });

            migrationBuilder.InsertData(
                table: "Workshops",
                columns: new[] { "WorkshopId", "BookingRef", "CategoryCode", "DateAndTime", "Name" },
                values: new object[] { 1, null, null, new DateTime(2023, 1, 10, 10, 0, 0, 0, DateTimeKind.Unspecified), "Excel (Beginner)" });

            migrationBuilder.InsertData(
                table: "Workshops",
                columns: new[] { "WorkshopId", "BookingRef", "CategoryCode", "DateAndTime", "Name" },
                values: new object[] { 2, null, null, new DateTime(2023, 1, 11, 10, 0, 0, 0, DateTimeKind.Unspecified), "Excel (Intermediate)" });

            migrationBuilder.InsertData(
                table: "Workshops",
                columns: new[] { "WorkshopId", "BookingRef", "CategoryCode", "DateAndTime", "Name" },
                values: new object[] { 3, null, null, new DateTime(2023, 9, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), "Word (Beginner)" });

            migrationBuilder.InsertData(
                table: "WorkshopStaff",
                columns: new[] { "StaffId", "WorkshopId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "WorkshopStaff",
                columns: new[] { "StaffId", "WorkshopId" },
                values: new object[] { 2, 1 });

            migrationBuilder.InsertData(
                table: "WorkshopStaff",
                columns: new[] { "StaffId", "WorkshopId" },
                values: new object[] { 1, 2 });

            migrationBuilder.InsertData(
                table: "WorkshopStaff",
                columns: new[] { "StaffId", "WorkshopId" },
                values: new object[] { 2, 2 });

            migrationBuilder.InsertData(
                table: "WorkshopStaff",
                columns: new[] { "StaffId", "WorkshopId" },
                values: new object[] { 4, 2 });

            migrationBuilder.InsertData(
                table: "WorkshopStaff",
                columns: new[] { "StaffId", "WorkshopId" },
                values: new object[] { 4, 3 });

            migrationBuilder.CreateIndex(
                name: "IX_WorkshopStaff_StaffId",
                table: "WorkshopStaff",
                column: "StaffId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkshopStaff");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "Workshops");
        }
    }
}
