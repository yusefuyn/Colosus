using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Colosus.Sql.SQLite.Migrations
{
    /// <inheritdoc />
    public partial class customer2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "IndividualCustomers",
                newName: "Name");

            migrationBuilder.CreateTable(
                name: "FastCustomers",
                columns: table => new
                {
                    Key = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PrivateKey = table.Column<string>(type: "TEXT", nullable: false),
                    PublicKey = table.Column<string>(type: "TEXT", nullable: false),
                    CustomerKey = table.Column<string>(type: "TEXT", nullable: false),
                    ContactGroupKey = table.Column<string>(type: "TEXT", nullable: false),
                    PaymentGroupKey = table.Column<string>(type: "TEXT", nullable: false),
                    VisibleFastOperation = table.Column<bool>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FastCustomers", x => x.Key);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FastCustomers");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "IndividualCustomers",
                newName: "FirstName");
        }
    }
}
