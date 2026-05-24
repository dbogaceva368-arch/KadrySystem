using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KadrySystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Должности",
                columns: table => new
                {
                    Код_должности = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Наименование = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Оклад = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Квалификационные_требования = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Должности", x => x.Код_должности);
                });

            migrationBuilder.CreateTable(
                name: "Подразделения",
                columns: table => new
                {
                    Код_подразделения = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Наименование = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Телефон = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Подразделения", x => x.Код_подразделения);
                });

            migrationBuilder.CreateTable(
                name: "ШтатноеРасписание",
                columns: table => new
                {
                    Код_позиции = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Код_должности = table.Column<int>(type: "int", nullable: false),
                    Количество_штатных_единиц = table.Column<int>(type: "int", nullable: false),
                    Количество_занятых_ставок = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ШтатноеРасписание", x => x.Код_позиции);
                    table.ForeignKey(
                        name: "FK_ШтатноеРасписание_Должности_Код_должности",
                        column: x => x.Код_должности,
                        principalTable: "Должности",
                        principalColumn: "Код_должности",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Сотрудники",
                columns: table => new
                {
                    Код_сотрудника = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Фамилия = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Имя = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Отчество = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Дата_рождения = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Паспортные_данные = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ИНН = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    СНИЛС = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Телефон = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Адрес_регистрации = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Дата_приема = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Дата_увольнения = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Код_должности = table.Column<int>(type: "int", nullable: false),
                    Код_подразделения = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Сотрудники", x => x.Код_сотрудника);
                    table.ForeignKey(
                        name: "FK_Сотрудники_Должности_Код_должности",
                        column: x => x.Код_должности,
                        principalTable: "Должности",
                        principalColumn: "Код_должности",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Сотрудники_Подразделения_Код_подразделения",
                        column: x => x.Код_подразделения,
                        principalTable: "Подразделения",
                        principalColumn: "Код_подразделения",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Приказы",
                columns: table => new
                {
                    Код_приказа = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Номер_приказа = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Дата_издания = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Тип_приказа = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Основание = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Код_сотрудника = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Приказы", x => x.Код_приказа);
                    table.ForeignKey(
                        name: "FK_Приказы_Сотрудники_Код_сотрудника",
                        column: x => x.Код_сотрудника,
                        principalTable: "Сотрудники",
                        principalColumn: "Код_сотрудника",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Приказы_Код_сотрудника",
                table: "Приказы",
                column: "Код_сотрудника");

            migrationBuilder.CreateIndex(
                name: "IX_Сотрудники_Код_должности",
                table: "Сотрудники",
                column: "Код_должности");

            migrationBuilder.CreateIndex(
                name: "IX_Сотрудники_Код_подразделения",
                table: "Сотрудники",
                column: "Код_подразделения");

            migrationBuilder.CreateIndex(
                name: "IX_ШтатноеРасписание_Код_должности",
                table: "ШтатноеРасписание",
                column: "Код_должности");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Приказы");

            migrationBuilder.DropTable(
                name: "ШтатноеРасписание");

            migrationBuilder.DropTable(
                name: "Сотрудники");

            migrationBuilder.DropTable(
                name: "Должности");

            migrationBuilder.DropTable(
                name: "Подразделения");
        }
    }
}
