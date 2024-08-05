using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArizaBildirimProject.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArizaTurler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArizaTurler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departman",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departman", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roller",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roller", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArizaKisaTanimlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArizaTurId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArizaKisaTanimlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArizaKisaTanimlar_ArizaTurler_ArizaTurId",
                        column: x => x.ArizaTurId,
                        principalTable: "ArizaTurler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bolum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bolum", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bolum_Departman_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departman",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DepartmanId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Departman_DepartmanId",
                        column: x => x.DepartmanId,
                        principalTable: "Departman",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Roller_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roller",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cihaz",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BolumId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cihaz", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cihaz_Bolum_BolumId",
                        column: x => x.BolumId,
                        principalTable: "Bolum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rapor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmanId = table.Column<int>(type: "int", nullable: false),
                    BolumId = table.Column<int>(type: "int", nullable: false),
                    CihazId = table.Column<int>(type: "int", nullable: false),
                    ArizaKisaTanimId = table.Column<int>(type: "int", nullable: false),
                    ArizaTurId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rapor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rapor_ArizaKisaTanimlar_ArizaKisaTanimId",
                        column: x => x.ArizaKisaTanimId,
                        principalTable: "ArizaKisaTanimlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rapor_ArizaTurler_ArizaTurId",
                        column: x => x.ArizaTurId,
                        principalTable: "ArizaTurler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rapor_Bolum_BolumId",
                        column: x => x.BolumId,
                        principalTable: "Bolum",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rapor_Cihaz_CihazId",
                        column: x => x.CihazId,
                        principalTable: "Cihaz",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rapor_Departman_DepartmanId",
                        column: x => x.DepartmanId,
                        principalTable: "Departman",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Aktiviteler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RaporId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aktiviteler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aktiviteler_Rapor_RaporId",
                        column: x => x.RaporId,
                        principalTable: "Rapor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Statuler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RaporId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Statuler_Rapor_RaporId",
                        column: x => x.RaporId,
                        principalTable: "Rapor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teshisler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RaporId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teshisler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teshisler_Rapor_RaporId",
                        column: x => x.RaporId,
                        principalTable: "Rapor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aktiviteler_RaporId",
                table: "Aktiviteler",
                column: "RaporId");

            migrationBuilder.CreateIndex(
                name: "IX_ArizaKisaTanimlar_ArizaTurId",
                table: "ArizaKisaTanimlar",
                column: "ArizaTurId");

            migrationBuilder.CreateIndex(
                name: "IX_Bolum_DepartmentId",
                table: "Bolum",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Cihaz_BolumId",
                table: "Cihaz",
                column: "BolumId");

            migrationBuilder.CreateIndex(
                name: "IX_Rapor_ArizaKisaTanimId",
                table: "Rapor",
                column: "ArizaKisaTanimId");

            migrationBuilder.CreateIndex(
                name: "IX_Rapor_ArizaTurId",
                table: "Rapor",
                column: "ArizaTurId");

            migrationBuilder.CreateIndex(
                name: "IX_Rapor_BolumId",
                table: "Rapor",
                column: "BolumId");

            migrationBuilder.CreateIndex(
                name: "IX_Rapor_CihazId",
                table: "Rapor",
                column: "CihazId");

            migrationBuilder.CreateIndex(
                name: "IX_Rapor_DepartmanId",
                table: "Rapor",
                column: "DepartmanId");

            migrationBuilder.CreateIndex(
                name: "IX_Statuler_RaporId",
                table: "Statuler",
                column: "RaporId");

            migrationBuilder.CreateIndex(
                name: "IX_Teshisler_RaporId",
                table: "Teshisler",
                column: "RaporId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmanId",
                table: "Users",
                column: "DepartmanId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aktiviteler");

            migrationBuilder.DropTable(
                name: "Statuler");

            migrationBuilder.DropTable(
                name: "Teshisler");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Rapor");

            migrationBuilder.DropTable(
                name: "Roller");

            migrationBuilder.DropTable(
                name: "ArizaKisaTanimlar");

            migrationBuilder.DropTable(
                name: "Cihaz");

            migrationBuilder.DropTable(
                name: "ArizaTurler");

            migrationBuilder.DropTable(
                name: "Bolum");

            migrationBuilder.DropTable(
                name: "Departman");
        }
    }
}
