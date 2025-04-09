using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Colosus.Sql.MSSql.Migrations
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
                    Key = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrivateKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublicKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactGroupKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentGroupKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisibleFastOperation = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
