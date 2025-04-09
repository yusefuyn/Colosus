using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Colosus.Sql.MSSql.Migrations
{
    /// <inheritdoc />
    public partial class category1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "VisibleWeb",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VisibleWeb",
                table: "Categories");
        }
    }
}
