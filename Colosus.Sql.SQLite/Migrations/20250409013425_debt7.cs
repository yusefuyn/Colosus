using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Colosus.Sql.SQLite.Migrations
{
    /// <inheritdoc />
    public partial class debt7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentType",
                table: "DebtPays",
                newName: "PaymentTypePrivateKey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentTypePrivateKey",
                table: "DebtPays",
                newName: "PaymentType");
        }
    }
}
