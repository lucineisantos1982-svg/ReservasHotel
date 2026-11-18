using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservasHotel.Migrations
{
    /// <inheritdoc />
    public partial class QuartoAtivo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Quartos",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Quartos");
        }
    }
}
