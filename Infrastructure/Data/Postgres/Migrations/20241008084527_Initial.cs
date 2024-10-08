using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Data.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeID = table.Column<int>(type: "integer", nullable: false),
                    RoleID = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IsAdmin = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    DepartmentId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 8, 11, 45, 27, 300, DateTimeKind.Utc).AddTicks(9849), false, "Information Tech.", null },
                    { 2, new DateTime(2024, 10, 8, 11, 45, 27, 300, DateTimeKind.Utc).AddTicks(9858), false, "Human Researcher", null },
                    { 3, new DateTime(2024, 10, 8, 11, 45, 27, 300, DateTimeKind.Utc).AddTicks(9864), false, "Accounting", null },
                    { 4, new DateTime(2024, 10, 8, 11, 45, 27, 300, DateTimeKind.Utc).AddTicks(9870), false, "Sales", null },
                    { 5, new DateTime(2024, 10, 8, 11, 45, 27, 300, DateTimeKind.Utc).AddTicks(9876), false, "Purchasing", null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "IsAdmin", "IsDeleted", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 8, 11, 45, 27, 301, DateTimeKind.Utc).AddTicks(957), true, false, "Manager", null },
                    { 2, new DateTime(2024, 10, 8, 11, 45, 27, 301, DateTimeKind.Utc).AddTicks(964), true, false, "Director", null },
                    { 3, new DateTime(2024, 10, 8, 11, 45, 27, 301, DateTimeKind.Utc).AddTicks(970), false, false, "Software Developer", null },
                    { 4, new DateTime(2024, 10, 8, 11, 45, 27, 301, DateTimeKind.Utc).AddTicks(976), false, false, "Sales Representative", null },
                    { 5, new DateTime(2024, 10, 8, 11, 45, 27, 301, DateTimeKind.Utc).AddTicks(1022), false, false, "Purchasing Officer", null }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CreatedAt", "DepartmentId", "Email", "FullName", "IsDeleted", "Password", "Phone", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 8, 11, 45, 27, 301, DateTimeKind.Utc).AddTicks(285), 1, "batuhan@", "Batuhan Yavuz", false, "123", "0544", null },
                    { 2, new DateTime(2024, 10, 8, 11, 45, 27, 301, DateTimeKind.Utc).AddTicks(292), 1, "utku@", "Utku Yavuz", false, "123", "0545", null },
                    { 3, new DateTime(2024, 10, 8, 11, 45, 27, 301, DateTimeKind.Utc).AddTicks(299), 2, "alperen@", "Alperen Karakuş", false, "123", "0546", null },
                    { 4, new DateTime(2024, 10, 8, 11, 45, 27, 301, DateTimeKind.Utc).AddTicks(305), 3, "berke@", "Berke Taşkur", false, "123", "0547", null },
                    { 5, new DateTime(2024, 10, 8, 11, 45, 27, 301, DateTimeKind.Utc).AddTicks(382), 4, "berkay@", "Berkay Öztürk", false, "123", "0548", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeRoles");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
