using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OfBugsAndDevs.Migrations
{
    /// <inheritdoc />
    public partial class _008 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "CategoryName", "ShowOnNavBar", "Slug" },
                values: new object[,]
                {
                    { 1, "C#", true, "c-sharp" },
                    { 2, "ASP.NET Core", true, "aspnet-core" },
                    { 3, "Blazor", true, "blazor" },
                    { 4, "SQL Server", true, "sql-server" },
                    { 5, "Entity Framework", true, "entity-framework" },
                    { 6, "Data Structures and Algorithms", true, "datastructures-and-algorithms" },
                    { 7, "Developer Life", true, "dev-life" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 7);
        }
    }
}
