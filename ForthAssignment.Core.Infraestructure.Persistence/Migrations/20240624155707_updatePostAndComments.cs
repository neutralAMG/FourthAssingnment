using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForthAssignment.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updatePostAndComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments");

            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentRespondingTo",
                table: "Comments",
                column: "CommentRespondingTo");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_CommentRespondingTo",
                table: "Comments",
                column: "CommentRespondingTo",
                principalTable: "Comments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_CommentRespondingTo",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_CommentRespondingTo",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "Posts");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
