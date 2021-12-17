using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Financeiro.Data.Migrations
{
    public partial class Inicio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CentroCusto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    DtCadastro = table.Column<DateTime>(type: "datetime", nullable: false),
                    DtAtualizacao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentroCusto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContaFinanceira",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    DtCadastro = table.Column<DateTime>(type: "datetime", nullable: false),
                    DtAtualizacao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContaFinanceira", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fornecedores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    DtCadastro = table.Column<DateTime>(type: "datetime", nullable: false),
                    DtAtualizacao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    DtCadastro = table.Column<DateTime>(type: "datetime", nullable: false),
                    DtAtualizacao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    Login = table.Column<string>(type: "varchar(50)", nullable: false),
                    Senha = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    RefreshToken = table.Column<string>(type: "varchar(100)", nullable: true),
                    DtCadastro = table.Column<DateTime>(type: "datetime", nullable: false),
                    DtAtualizacao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movimentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(500)", nullable: false),
                    ValorMovimento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Observacao = table.Column<string>(type: "varchar(1000)", nullable: true),
                    IsPago = table.Column<bool>(type: "bit", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataMovimento = table.Column<DateTime>(type: "datetime", nullable: false),
                    TipoMovimento = table.Column<int>(type: "int", nullable: false),
                    ContaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CentroCustoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FornecedorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PessoaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PessoaPagadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DtCadastro = table.Column<DateTime>(type: "datetime", nullable: false),
                    DtAtualizacao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movimentos_CentroCusto_CentroCustoId",
                        column: x => x.CentroCustoId,
                        principalTable: "CentroCusto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movimentos_ContaFinanceira_ContaId",
                        column: x => x.ContaId,
                        principalTable: "ContaFinanceira",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movimentos_Fornecedores_FornecedorId",
                        column: x => x.FornecedorId,
                        principalTable: "Fornecedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movimentos_Pessoas_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movimentos_Pessoas_PessoaPagadorId",
                        column: x => x.PessoaPagadorId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PessoaCentroCusto",
                columns: table => new
                {
                    PessoaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CentroCustoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValorMensal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PessoaCentroCusto", x => new { x.PessoaId, x.CentroCustoId });
                    table.ForeignKey(
                        name: "FK_PessoaCentroCusto_CentroCusto_CentroCustoId",
                        column: x => x.CentroCustoId,
                        principalTable: "CentroCusto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PessoaCentroCusto_Pessoas_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemMovimento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Historico = table.Column<string>(type: "varchar(500)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChaveLancamento = table.Column<string>(type: "varchar(100)", nullable: false),
                    NumeroParcela = table.Column<int>(type: "int", nullable: false),
                    TotalParcela = table.Column<int>(type: "int", nullable: false),
                    MovimentoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PessoaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CentroCustoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PessoaPagadorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DtCadastro = table.Column<DateTime>(type: "datetime", nullable: false),
                    DtAtualizacao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemMovimento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemMovimento_CentroCusto_CentroCustoId",
                        column: x => x.CentroCustoId,
                        principalTable: "CentroCusto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemMovimento_Movimentos_MovimentoId",
                        column: x => x.MovimentoId,
                        principalTable: "Movimentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemMovimento_Pessoas_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemMovimento_Pessoas_PessoaPagadorId",
                        column: x => x.PessoaPagadorId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemMovimento_CentroCustoId",
                table: "ItemMovimento",
                column: "CentroCustoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemMovimento_MovimentoId",
                table: "ItemMovimento",
                column: "MovimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemMovimento_PessoaId",
                table: "ItemMovimento",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemMovimento_PessoaPagadorId",
                table: "ItemMovimento",
                column: "PessoaPagadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentos_CentroCustoId",
                table: "Movimentos",
                column: "CentroCustoId");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentos_ContaId",
                table: "Movimentos",
                column: "ContaId");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentos_FornecedorId",
                table: "Movimentos",
                column: "FornecedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentos_PessoaId",
                table: "Movimentos",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentos_PessoaPagadorId",
                table: "Movimentos",
                column: "PessoaPagadorId");

            migrationBuilder.CreateIndex(
                name: "IX_PessoaCentroCusto_CentroCustoId",
                table: "PessoaCentroCusto",
                column: "CentroCustoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemMovimento");

            migrationBuilder.DropTable(
                name: "PessoaCentroCusto");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Movimentos");

            migrationBuilder.DropTable(
                name: "CentroCusto");

            migrationBuilder.DropTable(
                name: "ContaFinanceira");

            migrationBuilder.DropTable(
                name: "Fornecedores");

            migrationBuilder.DropTable(
                name: "Pessoas");
        }
    }
}
