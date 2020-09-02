using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EldoradoService.Migrations
{
    public partial class entidadesobrasimagens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cronogramas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cronogramas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Terrenos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    EnderecoCompleto = table.Column<string>(nullable: true),
                    DataAquisicao = table.Column<DateTime>(nullable: false),
                    MetragemTerreno = table.Column<decimal>(nullable: false),
                    Latitude = table.Column<string>(nullable: true),
                    Longitude = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terrenos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Obras",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DescricaoObra = table.Column<string>(nullable: true),
                    TerrenoId = table.Column<Guid>(nullable: true),
                    DataInicioObras = table.Column<DateTime>(nullable: false),
                    DataEntregaEmpreendimento = table.Column<DateTime>(nullable: false),
                    DataValidadeGarantia = table.Column<DateTime>(nullable: false),
                    ValorTotalAquisicao = table.Column<decimal>(nullable: false),
                    PorcentagemParticipacao = table.Column<decimal>(nullable: false),
                    StatusObra = table.Column<int>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    DataUltimaModificacao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Obras_Terrenos_TerrenoId",
                        column: x => x.TerrenoId,
                        principalTable: "Terrenos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Imagens",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    UrlImagem = table.Column<string>(nullable: true),
                    IdObra = table.Column<Guid>(nullable: false),
                    ObraId = table.Column<Guid>(nullable: true),
                    Capa = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imagens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Imagens_Obras_ObraId",
                        column: x => x.ObraId,
                        principalTable: "Obras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItensCronograma",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DescricaoAtividade = table.Column<string>(nullable: true),
                    InicioAtividade = table.Column<DateTime>(nullable: false),
                    FimAtividade = table.Column<DateTime>(nullable: false),
                    Responsavel = table.Column<string>(nullable: true),
                    StatusItem = table.Column<int>(nullable: false),
                    CronogramaId = table.Column<Guid>(nullable: true),
                    ObraId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensCronograma", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensCronograma_Cronogramas_CronogramaId",
                        column: x => x.CronogramaId,
                        principalTable: "Cronogramas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItensCronograma_Obras_ObraId",
                        column: x => x.ObraId,
                        principalTable: "Obras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Socios",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Documento = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(nullable: true),
                    ObraId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Socios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Socios_Obras_ObraId",
                        column: x => x.ObraId,
                        principalTable: "Obras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Imagens_ObraId",
                table: "Imagens",
                column: "ObraId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensCronograma_CronogramaId",
                table: "ItensCronograma",
                column: "CronogramaId");

            migrationBuilder.CreateIndex(
                name: "IX_ItensCronograma_ObraId",
                table: "ItensCronograma",
                column: "ObraId");

            migrationBuilder.CreateIndex(
                name: "IX_Obras_TerrenoId",
                table: "Obras",
                column: "TerrenoId");

            migrationBuilder.CreateIndex(
                name: "IX_Socios_ObraId",
                table: "Socios",
                column: "ObraId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Imagens");

            migrationBuilder.DropTable(
                name: "ItensCronograma");

            migrationBuilder.DropTable(
                name: "Socios");

            migrationBuilder.DropTable(
                name: "Cronogramas");

            migrationBuilder.DropTable(
                name: "Obras");

            migrationBuilder.DropTable(
                name: "Terrenos");
        }
    }
}
