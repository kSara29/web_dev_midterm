using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web_dev_midterm.Migrations
{
    /// <inheritdoc />
    public partial class DeleteImageFromPostModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Posts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Posts",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
