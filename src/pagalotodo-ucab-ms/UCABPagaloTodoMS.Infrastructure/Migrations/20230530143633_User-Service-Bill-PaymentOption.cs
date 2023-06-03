using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UCABPagaloTodoMS.Infrastructure.Migrations
{
    public partial class UserServiceBillPaymentOption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Dni = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Lastname = table.Column<string>(type: "text", nullable: true),
                    Username = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<bool>(type: "boolean", nullable: true),
                    VerificationCode = table.Column<string>(type: "text", nullable: true),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    isAdmin = table.Column<bool>(type: "boolean", nullable: true),
                    CompanyName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceName = table.Column<string>(type: "text", nullable: true),
                    TypeService = table.Column<string>(type: "text", nullable: true),
                    ContactNumber = table.Column<string>(type: "text", nullable: true),
                    ProviderId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceEntities_UserEntities_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "UserEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentOptionEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true),
                    ServiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentOptionEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentOptionEntities_ServiceEntities_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "ServiceEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContractNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    Amount = table.Column<double>(type: "double precision", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uuid", nullable: false),
                    PaymentOptionId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillEntities_PaymentOptionEntities_PaymentOptionId",
                        column: x => x.PaymentOptionId,
                        principalTable: "PaymentOptionEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillEntities_ServiceEntities_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "ServiceEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillEntities_UserEntities_UserId",
                        column: x => x.UserId,
                        principalTable: "UserEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillEntities_PaymentOptionId",
                table: "BillEntities",
                column: "PaymentOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_BillEntities_ServiceId",
                table: "BillEntities",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_BillEntities_UserId",
                table: "BillEntities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentOptionEntities_ServiceId",
                table: "PaymentOptionEntities",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceEntities_ProviderId",
                table: "ServiceEntities",
                column: "ProviderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillEntities");

            migrationBuilder.DropTable(
                name: "PaymentOptionEntities");

            migrationBuilder.DropTable(
                name: "ServiceEntities");

            migrationBuilder.DropTable(
                name: "UserEntities");
        }
    }
}
