using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class create_database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_tarefas",
                columns: table => new
                {
                    TarefaCodigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "tb_tarefas_historico")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "data_fim_vigencia")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "data_inicio_vigencia"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "tb_tarefas_historico")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "data_fim_vigencia")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "data_inicio_vigencia"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "tb_tarefas_historico")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "data_fim_vigencia")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "data_inicio_vigencia"),
                    data_fim_vigencia = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "tb_tarefas_historico")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "data_fim_vigencia")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "data_inicio_vigencia"),
                    data_inicio_vigencia = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "tb_tarefas_historico")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "data_fim_vigencia")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "data_inicio_vigencia")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_tarefas", x => x.TarefaCodigo);
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "tb_tarefas_historico")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "data_fim_vigencia")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "data_inicio_vigencia");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_tarefas")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "tb_tarefas_historico")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "data_fim_vigencia")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "data_inicio_vigencia");
        }
    }
}
