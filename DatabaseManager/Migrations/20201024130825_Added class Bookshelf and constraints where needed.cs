using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseManager.Migrations
{
    public partial class AddedclassBookshelfandconstraintswhereneeded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(maxLength: 128, nullable: true),
                    Author = table.Column<string>(maxLength: 128, nullable: true),
                    Price = table.Column<float>(nullable: false),
                    BookShelfId = table.Column<int>(nullable: true),
                    OrderId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(maxLength: 128, nullable: true),
                    LastName = table.Column<string>(maxLength: 128, nullable: true),
                    Login = table.Column<string>(maxLength: 128, nullable: true),
                    Password = table.Column<string>(maxLength: 128, nullable: true),
                    AdminPermission = table.Column<bool>(nullable: false),
                    BookShelfFK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookShelves",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserFK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookShelves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookShelves_Users_UserFK",
                        column: x => x.UserFK,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserFK = table.Column<int>(nullable: false),
                    Bill = table.Column<double>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserFK",
                        column: x => x.UserFK,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookShelfId",
                table: "Books",
                column: "BookShelfId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_OrderId",
                table: "Books",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_BookShelves_UserFK",
                table: "BookShelves",
                column: "UserFK");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserFK",
                table: "Orders",
                column: "UserFK");

            migrationBuilder.CreateIndex(
                name: "IX_Users_BookShelfFK",
                table: "Users",
                column: "BookShelfFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookShelves_BookShelfId",
                table: "Books",
                column: "BookShelfId",
                principalTable: "BookShelves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Orders_OrderId",
                table: "Books",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_BookShelves_BookShelfFK",
                table: "Users",
                column: "BookShelfFK",
                principalTable: "BookShelves",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_BookShelves_BookShelfFK",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "BookShelves");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
