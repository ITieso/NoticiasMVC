using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArchitectureMvc.Infra.Migrations
{
    /// <inheritdoc />
    public partial class noticiaTagUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NoticiaTags",
                table: "NoticiaTags");

            migrationBuilder.DropIndex(
                name: "IX_NoticiaTags_NoticiaId",
                table: "NoticiaTags");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "NoticiaTags",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NoticiaTags",
                table: "NoticiaTags",
                columns: new[] { "NoticiaId", "TagId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NoticiaTags",
                table: "NoticiaTags");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "NoticiaTags",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NoticiaTags",
                table: "NoticiaTags",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_NoticiaTags_NoticiaId",
                table: "NoticiaTags",
                column: "NoticiaId");
        }
    }
}
