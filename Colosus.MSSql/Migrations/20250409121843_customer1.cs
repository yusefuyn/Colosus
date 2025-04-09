using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Colosus.Sql.MSSql.Migrations
{
    /// <inheritdoc />
    public partial class customer1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "VisibleFastOperation",
                table: "IndividualCustomers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "VisibleFastOperation",
                table: "CorporateCustomers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VisibleFastOperation",
                table: "IndividualCustomers");

            migrationBuilder.DropColumn(
                name: "VisibleFastOperation",
                table: "CorporateCustomers");
        }
    }
}
