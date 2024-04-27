using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HiveAPI.Migrations
{
    /// <inheritdoc />
    public partial class addedTaskComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collaborators_Tasks_TaskId",
                table: "Collaborators");

            migrationBuilder.DropForeignKey(
                name: "FK_List_WorkSpaces_WorkSpaceId",
                table: "List");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_List_ListId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Collaborators_TaskId",
                table: "Collaborators");

            migrationBuilder.DropPrimaryKey(
                name: "PK_List",
                table: "List");

            migrationBuilder.DropColumn(
                name: "Label",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "Collaborators");

            migrationBuilder.RenameTable(
                name: "List",
                newName: "Lists");

            migrationBuilder.RenameIndex(
                name: "IX_List_WorkSpaceId",
                table: "Lists",
                newName: "IX_Lists_WorkSpaceId");

            migrationBuilder.AddColumn<int>(
                name: "priority",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "priority",
                table: "Tasks");

            migrationBuilder.RenameTable(
                name: "Lists",
                newName: "List");

            migrationBuilder.RenameIndex(
                name: "IX_Lists_WorkSpaceId",
                table: "List",
                newName: "IX_List_WorkSpaceId");

            migrationBuilder.AddColumn<string>(
                name: "Label",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TaskId",
                table: "Collaborators",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_List",
                table: "List",
                column: "ListId");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborators_TaskId",
                table: "Collaborators",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collaborators_Tasks_TaskId",
                table: "Collaborators",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "TaskId");

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
    }
}
