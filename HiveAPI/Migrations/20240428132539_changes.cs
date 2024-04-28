using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiveAPI.Migrations
{
    /// <inheritdoc />
    public partial class changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lists_WorkSpaces_WorkSpaceId",
                table: "Lists");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Lists_ListId",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lists",
                table: "Lists");

            migrationBuilder.RenameTable(
                name: "Lists",
                newName: "List");

            migrationBuilder.RenameIndex(
                name: "IX_Lists_WorkSpaceId",
                table: "List",
                newName: "IX_List_WorkSpaceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_List",
                table: "List",
                column: "ListId");

            migrationBuilder.AddForeignKey(
                name: "FK_List_WorkSpaces_WorkSpaceId",
                table: "List",
                column: "WorkSpaceId",
                principalTable: "WorkSpaces",
                principalColumn: "WId",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_List_WorkSpaces_WorkSpaceId",
                table: "List");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_List_ListId",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_List",
                table: "List");

            migrationBuilder.RenameTable(
                name: "List",
                newName: "Lists");

            migrationBuilder.RenameIndex(
                name: "IX_List_WorkSpaceId",
                table: "Lists",
                newName: "IX_Lists_WorkSpaceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lists",
                table: "Lists",
                column: "ListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lists_WorkSpaces_WorkSpaceId",
                table: "Lists",
                column: "WorkSpaceId",
                principalTable: "WorkSpaces",
                principalColumn: "WId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Lists_ListId",
                table: "Tasks",
                column: "ListId",
                principalTable: "Lists",
                principalColumn: "ListId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
