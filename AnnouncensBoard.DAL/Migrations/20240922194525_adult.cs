using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnnouncensBoard.DAL.Migrations
{
    /// <inheritdoc />
    public partial class adult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AdultOnly",
                table: "Subject_table",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdultOnly",
                table: "Subject_table");
        }
    }
}
