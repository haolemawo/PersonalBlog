using Microsoft.EntityFrameworkCore.Migrations;

namespace Post.EntityFrameworkCore.Migrations
{
    public partial class AddAuthorAvatorJYZ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorAvator",
                table: "Articles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorAvator",
                table: "Articles");
        }
    }
}
