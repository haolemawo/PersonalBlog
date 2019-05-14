using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Post.EntityFrameworkCore.Migrations
{
    public partial class InitDatabaseJYZ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArticleType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TypeName = table.Column<string>(maxLength: 50, nullable: false),
                    TypeValue = table.Column<int>(maxLength: 50, nullable: false),
                    TypeAvator = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeaveMessages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    UserName = table.Column<string>(maxLength: 50, nullable: false),
                    MessageContent = table.Column<string>(maxLength: 600, nullable: false),
                    UserAvator = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TypeId = table.Column<int>(nullable: false),
                    ArticleName = table.Column<string>(maxLength: 200, nullable: false),
                    ArticleUrl = table.Column<string>(maxLength: 200, nullable: false),
                    Author = table.Column<string>(maxLength: 50, nullable: false),
                    Introduce = table.Column<string>(maxLength: 300, nullable: false),
                    PageView = table.Column<int>(nullable: false),
                    IsRecommend = table.Column<string>(nullable: true),
                    Avator = table.Column<string>(nullable: true),
                    ArticleTags = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_ArticleType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "ArticleType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_TypeId",
                table: "Articles",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "LeaveMessages");

            migrationBuilder.DropTable(
                name: "ArticleType");
        }
    }
}
