using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiveAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangesInTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_WorkSpaces_WorkSpaceId",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "WorkSpaceId",
                table: "Tasks",
                newName: "ListId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_WorkSpaceId",
                table: "Tasks",
                newName: "IX_Tasks_ListId");

            migrationBuilder.CreateTable(
                name: "List",
                columns: table => new
                {
                    ListId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkSpaceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_List", x => x.ListId);
                    table.ForeignKey(
                        name: "FK_List_WorkSpaces_WorkSpaceId",
                        column: x => x.WorkSpaceId,
                        principalTable: "WorkSpaces",
                        principalColumn: "WId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_List_WorkSpaceId",
                table: "List",
                column: "WorkSpaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_List_ListId",
                table: "Tasks",
                column: "ListId",
                principalTable: "List",
                principalColumn: "ListId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_List_ListId",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "List");

            migrationBuilder.RenameColumn(
                name: "ListId",
                table: "Tasks",
                newName: "WorkSpaceId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_ListId",
                table: "Tasks",
                newName: "IX_Tasks_WorkSpaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_WorkSpaces_WorkSpaceId",
                table: "Tasks",
                column: "WorkSpaceId",
                principalTable: "WorkSpaces",
                principalColumn: "WId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
