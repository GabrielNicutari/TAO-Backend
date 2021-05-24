using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace TAO_Backend.Migrations
{
    public partial class IdentityDBUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "houses",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    address = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false),
                    zip = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false),
                    area = table.Column<int>(type: "int(11)", nullable: false),
                    year_built = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_houses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "daily_readings",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    timestamp = table.Column<DateTime>(type: "date", nullable: false),
                    energy = table.Column<int>(type: "int(11)", nullable: false),
                    volume = table.Column<double>(type: "double", nullable: false),
                    hour_counter = table.Column<int>(type: "int(11)", nullable: false),
                    temp_forward = table.Column<double>(type: "double", nullable: false),
                    temp_return = table.Column<double>(type: "double", nullable: false),
                    power = table.Column<double>(type: "double", nullable: false),
                    flow = table.Column<int>(type: "int(11)", nullable: false),
                    house_reading_id = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_daily_readings", x => x.id);
                    table.ForeignKey(
                        name: "house_reading_id",
                        column: x => x.house_reading_id,
                        principalTable: "houses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    username = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false),
                    password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    house_id = table.Column<int>(type: "int(11)", nullable: false),
                    phone_number = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                    table.ForeignKey(
                        name: "house_id",
                        column: x => x.house_id,
                        principalTable: "houses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "house_id_idx",
                table: "daily_readings",
                column: "house_reading_id");

            migrationBuilder.CreateIndex(
                name: "house_id_UNIQUE",
                table: "users",
                column: "house_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "daily_readings");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "houses");
        }
    }
}
