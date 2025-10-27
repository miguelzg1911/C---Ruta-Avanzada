using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaDigital.Migrations
{
    /// <inheritdoc />
    public partial class ActualizacionRelaciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prestamos_Libros_LibroId",
                table: "Prestamos");

            migrationBuilder.DropForeignKey(
                name: "FK_Prestamos_Usuarios_UsuarioId",
                table: "Prestamos");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Prestamos",
                newName: "UsuariosId");

            migrationBuilder.RenameColumn(
                name: "LibroId",
                table: "Prestamos",
                newName: "LibrosId");

            migrationBuilder.RenameIndex(
                name: "IX_Prestamos_UsuarioId",
                table: "Prestamos",
                newName: "IX_Prestamos_UsuariosId");

            migrationBuilder.RenameIndex(
                name: "IX_Prestamos_LibroId",
                table: "Prestamos",
                newName: "IX_Prestamos_LibrosId");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_IdLibro",
                table: "Prestamos",
                column: "IdLibro");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_IdUsuario",
                table: "Prestamos",
                column: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Prestamos_Libros_IdLibro",
                table: "Prestamos",
                column: "IdLibro",
                principalTable: "Libros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prestamos_Libros_LibrosId",
                table: "Prestamos",
                column: "LibrosId",
                principalTable: "Libros",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prestamos_Usuarios_IdUsuario",
                table: "Prestamos",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prestamos_Usuarios_UsuariosId",
                table: "Prestamos",
                column: "UsuariosId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prestamos_Libros_IdLibro",
                table: "Prestamos");

            migrationBuilder.DropForeignKey(
                name: "FK_Prestamos_Libros_LibrosId",
                table: "Prestamos");

            migrationBuilder.DropForeignKey(
                name: "FK_Prestamos_Usuarios_IdUsuario",
                table: "Prestamos");

            migrationBuilder.DropForeignKey(
                name: "FK_Prestamos_Usuarios_UsuariosId",
                table: "Prestamos");

            migrationBuilder.DropIndex(
                name: "IX_Prestamos_IdLibro",
                table: "Prestamos");

            migrationBuilder.DropIndex(
                name: "IX_Prestamos_IdUsuario",
                table: "Prestamos");

            migrationBuilder.RenameColumn(
                name: "UsuariosId",
                table: "Prestamos",
                newName: "UsuarioId");

            migrationBuilder.RenameColumn(
                name: "LibrosId",
                table: "Prestamos",
                newName: "LibroId");

            migrationBuilder.RenameIndex(
                name: "IX_Prestamos_UsuariosId",
                table: "Prestamos",
                newName: "IX_Prestamos_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Prestamos_LibrosId",
                table: "Prestamos",
                newName: "IX_Prestamos_LibroId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prestamos_Libros_LibroId",
                table: "Prestamos",
                column: "LibroId",
                principalTable: "Libros",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prestamos_Usuarios_UsuarioId",
                table: "Prestamos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }
    }
}
