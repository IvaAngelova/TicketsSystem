using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketSystem.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    DepartmentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ModifDate17118162 = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.DepartmentID);
                });

            migrationBuilder.CreateTable(
                name: "TicketCategory",
                columns: table => new
                {
                    TicketCategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ModifDate17118162 = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketCategory", x => x.TicketCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "TicketPriority",
                columns: table => new
                {
                    TicketPriorityID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PriorityType = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ModifDate17118162 = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketPriority", x => x.TicketPriorityID);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DepartmentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TicketCategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifDate17118162 = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeID);                    
                    table.ForeignKey(
                        name: "FK_Employee_Department_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Department",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employee_TicketCategory_TicketCategoryID",
                        column: x => x.TicketCategoryID,
                        principalTable: "TicketCategory",
                        principalColumn: "TicketCategoryID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    TicketID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    АcceptedАТicketID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TicketCategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OpenDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TicketPriorityID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Photo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifDate17118162 = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.TicketID);
                    table.ForeignKey(
                        name: "FK_Ticket_Employee_CreatorID",
                        column: x => x.CreatorID,
                        principalTable: "Employee",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ticket_Employee_АcceptedАТicketID",
                        column: x => x.АcceptedАТicketID,
                        principalTable: "Employee",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ticket_TicketCategory_TicketCategoryID",
                        column: x => x.TicketCategoryID,
                        principalTable: "TicketCategory",
                        principalColumn: "TicketCategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ticket_TicketPriority_TicketPriorityID",
                        column: x => x.TicketPriorityID,
                        principalTable: "TicketPriority",
                        principalColumn: "TicketPriorityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketStatus",
                columns: table => new
                {
                    TicketStatusID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TicketID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModifDate17118162 = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketStatus", x => x.TicketStatusID);
                    table.ForeignKey(
                        name: "FK_TicketStatus_Employee_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employee",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketStatus_Ticket_TicketID",
                        column: x => x.TicketID,
                        principalTable: "Ticket",
                        principalColumn: "TicketID",
                        onDelete: ReferentialAction.Cascade);
                });

            

            migrationBuilder.CreateIndex(
                name: "IX_Department_DepartmentName",
                table: "Department",
                column: "DepartmentName",
                unique: true);

          

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DepartmentID",
                table: "Employee",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_TicketCategoryID",
                table: "Employee",
                column: "TicketCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_CreatorID",
                table: "Ticket",
                column: "CreatorID");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_TicketCategoryID",
                table: "Ticket",
                column: "TicketCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_TicketPriorityID",
                table: "Ticket",
                column: "TicketPriorityID");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_АcceptedАТicketID",
                table: "Ticket",
                column: "АcceptedАТicketID");

            migrationBuilder.CreateIndex(
                name: "IX_TicketCategory_CategoryName",
                table: "TicketCategory",
                column: "CategoryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketPriority_PriorityType",
                table: "TicketPriority",
                column: "PriorityType",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketStatus_EmployeeID",
                table: "TicketStatus",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_TicketStatus_TicketID",
                table: "TicketStatus",
                column: "TicketID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketStatus");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "TicketPriority");

           

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "TicketCategory");
        }
    }
}
