using Microsoft.EntityFrameworkCore.Migrations;

namespace lesson.Data.Migrations
{
    public partial class identityExtended : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "lessonUserId",
                table: "Summaries",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Job",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Summaries_lessonUserId",
                table: "Summaries",
                column: "lessonUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Summaries_AspNetUsers_lessonUserId",
                table: "Summaries",
                column: "lessonUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Summaries_AspNetUsers_lessonUserId",
                table: "Summaries");

            migrationBuilder.DropIndex(
                name: "IX_Summaries_lessonUserId",
                table: "Summaries");

            migrationBuilder.DropColumn(
                name: "lessonUserId",
                table: "Summaries");

            migrationBuilder.DropColumn(
                name: "Job",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "AspNetUsers");
        }
    }
}
