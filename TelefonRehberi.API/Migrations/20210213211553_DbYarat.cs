using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TelefonRehberi.API.Migrations
{
    public partial class DbYarat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kisi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EklenmeTarihi = table.Column<DateTime>(nullable: true),
                    GuncellenmeTarihi = table.Column<DateTime>(nullable: true),
                    SilindiMi = table.Column<bool>(nullable: false),
                    UUID = table.Column<int>(nullable: false),
                    Ad = table.Column<string>(maxLength: 50, nullable: false),
                    Soyad = table.Column<string>(maxLength: 50, nullable: true),
                    Firma = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kisi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Iletisim",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EklenmeTarihi = table.Column<DateTime>(nullable: true),
                    GuncellenmeTarihi = table.Column<DateTime>(nullable: true),
                    SilindiMi = table.Column<bool>(nullable: false),
                    BilgiTipi = table.Column<int>(nullable: false),
                    BilgiIcerigi = table.Column<string>(nullable: true),
                    KisiId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Iletisim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Iletisim_Kisi_KisiId",
                        column: x => x.KisiId,
                        principalTable: "Kisi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Iletisim_KisiId",
                table: "Iletisim",
                column: "KisiId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Iletisim");

            migrationBuilder.DropTable(
                name: "Kisi");
        }
    }
}
