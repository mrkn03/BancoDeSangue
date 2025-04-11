using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BancoDeSangue.Migrations
{
    /// <inheritdoc />
    public partial class AtualizacoesDeEntidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Idade",
                table: "Doador");

            migrationBuilder.RenameColumn(
                name: "Cpf",
                table: "Doador",
                newName: "CpfDoador");

            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "Doador",
                type: "varchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataNascimento",
                table: "Doador",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "Doador");

            migrationBuilder.RenameColumn(
                name: "CpfDoador",
                table: "Doador",
                newName: "Cpf");

            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "Doador",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldMaxLength: 15)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Idade",
                table: "Doador",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
