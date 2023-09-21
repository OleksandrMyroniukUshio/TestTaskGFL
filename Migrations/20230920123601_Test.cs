using Microsoft.EntityFrameworkCore.Migrations;

namespace TestTaskGFL.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Folders",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Folders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Folders_Folders_ParentID",
                        column: x => x.ParentID,
                        principalTable: "Folders",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Folders_ParentID",
                table: "Folders",
                column: "ParentID");

            // seeding data
            migrationBuilder.InsertData(
                table: "Folders",
                columns: new[] { "Name", "ParentID" },
                values: new object[] { "Creating digital Images", null });

            migrationBuilder.InsertData(
                table: "Folders",
                columns: new[] { "Name", "ParentID" },
                values: new object[,]
                {
                    { "Resources", 1 },
                    { "Evidence", 1 },
                    { "Graphic Products", 1 }
                });

            migrationBuilder.InsertData(
                table: "Folders",
                columns: new[] { "Name", "ParentID" },
                values: new object[,]
                {
                    { "Primary Sources", 2 },
                    { "Secondary Sources", 2 }
                });

            migrationBuilder.InsertData(
                table: "Folders",
                columns: new[] { "Name", "ParentID" },
                values: new object[,]
                {
                    { "Process", 4 },
                    { "Final Product", 4 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Folders");
        }
    }
}
