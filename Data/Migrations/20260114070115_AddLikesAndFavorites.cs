using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AulorAudio.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLikesAndFavorites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteSongs_Songs_SongId",
                table: "FavoriteSongs");

            migrationBuilder.DropForeignKey(
                name: "FK_SongLikes_Songs_SongId",
                table: "SongLikes");

            migrationBuilder.DropIndex(
                name: "IX_SongLikes_SongId",
                table: "SongLikes");

            migrationBuilder.DropIndex(
                name: "IX_FavoriteSongs_SongId",
                table: "FavoriteSongs");

            migrationBuilder.DropColumn(
                name: "SongId",
                table: "SongLikes");

            migrationBuilder.DropColumn(
                name: "SongId",
                table: "FavoriteSongs");

            migrationBuilder.AddColumn<string>(
                name: "SongFile",
                table: "SongLikes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SongFile",
                table: "FavoriteSongs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SongFile",
                table: "SongLikes");

            migrationBuilder.DropColumn(
                name: "SongFile",
                table: "FavoriteSongs");

            migrationBuilder.AddColumn<int>(
                name: "SongId",
                table: "SongLikes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SongId",
                table: "FavoriteSongs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SongLikes_SongId",
                table: "SongLikes",
                column: "SongId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteSongs_SongId",
                table: "FavoriteSongs",
                column: "SongId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteSongs_Songs_SongId",
                table: "FavoriteSongs",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SongLikes_Songs_SongId",
                table: "SongLikes",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
