using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace myCoreMvc.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Reference = table.Column<string>(nullable: true),
                    Priority = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    WorkPlanId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkItems_WorkPlans_WorkPlanId",
                        column: x => x.WorkPlanId,
                        principalTable: "WorkPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkItems_WorkPlanId",
                table: "WorkItems",
                column: "WorkPlanId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkItems");

            migrationBuilder.DropTable(
                name: "WorkPlans");
        }
    }
}
