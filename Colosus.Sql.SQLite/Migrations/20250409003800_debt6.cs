using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Colosus.Sql.SQLite.Migrations
{
    /// <inheritdoc />
    public partial class debt6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Payed",
                table: "Debts");

            migrationBuilder.AddColumn<string>(
                name: "PaymentType",
                table: "DebtPays",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "DebtPays");

            migrationBuilder.AddColumn<bool>(
                name: "Payed",
                table: "Debts",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
