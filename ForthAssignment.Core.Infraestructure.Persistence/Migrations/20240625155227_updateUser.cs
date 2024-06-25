using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForthAssignment.Infraestructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersFriends_Users_UserId1",
                table: "UsersFriends");

            migrationBuilder.DropIndex(
                name: "IX_UsersFriends_UserId1",
                table: "UsersFriends");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UsersFriends");

            migrationBuilder.CreateIndex(
                name: "IX_UsersFriends_UserFriendId",
                table: "UsersFriends",
                column: "UserFriendId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersFriends_Users_UserFriendId",
                table: "UsersFriends",
                column: "UserFriendId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersFriends_Users_UserFriendId",
                table: "UsersFriends");

            migrationBuilder.DropIndex(
                name: "IX_UsersFriends_UserFriendId",
                table: "UsersFriends");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "UsersFriends",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UsersFriends_UserId1",
                table: "UsersFriends",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersFriends_Users_UserId1",
                table: "UsersFriends",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
