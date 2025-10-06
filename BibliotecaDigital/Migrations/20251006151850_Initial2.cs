using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaDigital.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
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

            migrationBuilder.AlterColumn<string>(
                name: "Telefono",
                table: "Usuarios",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Prestamos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "LibroId",
                table: "Prestamos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaDevolucion",
                table: "Prestamos",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaPrestamo",
                table: "Prestamos",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "Libros",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prestamos_Libros_LibroId",
                table: "Prestamos");

            migrationBuilder.DropForeignKey(
                name: "FK_Prestamos_Usuarios_UsuarioId",
                table: "Prestamos");

            migrationBuilder.DropColumn(
                name: "FechaPrestamo",
                table: "Prestamos");

            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Libros");

            migrationBuilder.AlterColumn<int>(
                name: "Telefono",
                table: "Usuarios",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Prestamos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LibroId",
                table: "Prestamos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "FechaDevolucion",
                table: "Prestamos",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Prestamos_Libros_LibroId",
                table: "Prestamos",
                column: "LibroId",
                principalTable: "Libros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prestamos_Usuarios_UsuarioId",
                table: "Prestamos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
