using Microsoft.EntityFrameworkCore.Migrations;

namespace WMS.Migrations
{
    public partial class workflowreq : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approvers",
                table: "RequestTypes");

            migrationBuilder.CreateTable(
                name: "WorkflowGrantAccesses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    LocationId = table.Column<int>(nullable: false),
                    FacilityId = table.Column<int>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: true),
                    SubDepartmentId = table.Column<int>(nullable: true),
                    AreaId = table.Column<int>(nullable: true),
                    RoomId = table.Column<int>(nullable: true),
                    EquipmentId = table.Column<int>(nullable: true),
                    ITAssetId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowGrantAccesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkflowGrantAccesses_DAreas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "DAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkflowGrantAccesses_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkflowGrantAccesses_Equipments_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkflowGrantAccesses_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkflowGrantAccesses_ITAssets_ITAssetId",
                        column: x => x.ITAssetId,
                        principalTable: "ITAssets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkflowGrantAccesses_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkflowGrantAccesses_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkflowGrantAccesses_SubDepartments_SubDepartmentId",
                        column: x => x.SubDepartmentId,
                        principalTable: "SubDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowPasswordChanges",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    LocationId = table.Column<int>(nullable: false),
                    FacilityId = table.Column<int>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: true),
                    SubDepartmentId = table.Column<int>(nullable: true),
                    AreaId = table.Column<int>(nullable: true),
                    RoomId = table.Column<int>(nullable: true),
                    EquipmentId = table.Column<int>(nullable: true),
                    ITAssetId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowPasswordChanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkflowPasswordChanges_DAreas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "DAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkflowPasswordChanges_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkflowPasswordChanges_Equipments_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkflowPasswordChanges_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkflowPasswordChanges_ITAssets_ITAssetId",
                        column: x => x.ITAssetId,
                        principalTable: "ITAssets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkflowPasswordChanges_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkflowPasswordChanges_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkflowPasswordChanges_SubDepartments_SubDepartmentId",
                        column: x => x.SubDepartmentId,
                        principalTable: "SubDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestWorkflows",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkflowPasswordChangeId = table.Column<int>(nullable: false),
                    WorkflowGrantAccessId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestWorkflows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestWorkflows_WorkflowGrantAccesses_WorkflowGrantAccessId",
                        column: x => x.WorkflowGrantAccessId,
                        principalTable: "WorkflowGrantAccesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestWorkflows_WorkflowPasswordChanges_WorkflowPasswordChangeId",
                        column: x => x.WorkflowPasswordChangeId,
                        principalTable: "WorkflowPasswordChanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestWorkflows_WorkflowGrantAccessId",
                table: "RequestWorkflows",
                column: "WorkflowGrantAccessId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestWorkflows_WorkflowPasswordChangeId",
                table: "RequestWorkflows",
                column: "WorkflowPasswordChangeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowGrantAccesses_AreaId",
                table: "WorkflowGrantAccesses",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowGrantAccesses_DepartmentId",
                table: "WorkflowGrantAccesses",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowGrantAccesses_EquipmentId",
                table: "WorkflowGrantAccesses",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowGrantAccesses_FacilityId",
                table: "WorkflowGrantAccesses",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowGrantAccesses_ITAssetId",
                table: "WorkflowGrantAccesses",
                column: "ITAssetId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowGrantAccesses_LocationId",
                table: "WorkflowGrantAccesses",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowGrantAccesses_RoomId",
                table: "WorkflowGrantAccesses",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowGrantAccesses_SubDepartmentId",
                table: "WorkflowGrantAccesses",
                column: "SubDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowPasswordChanges_AreaId",
                table: "WorkflowPasswordChanges",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowPasswordChanges_DepartmentId",
                table: "WorkflowPasswordChanges",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowPasswordChanges_EquipmentId",
                table: "WorkflowPasswordChanges",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowPasswordChanges_FacilityId",
                table: "WorkflowPasswordChanges",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowPasswordChanges_ITAssetId",
                table: "WorkflowPasswordChanges",
                column: "ITAssetId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowPasswordChanges_LocationId",
                table: "WorkflowPasswordChanges",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowPasswordChanges_RoomId",
                table: "WorkflowPasswordChanges",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowPasswordChanges_SubDepartmentId",
                table: "WorkflowPasswordChanges",
                column: "SubDepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestWorkflows");

            migrationBuilder.DropTable(
                name: "WorkflowGrantAccesses");

            migrationBuilder.DropTable(
                name: "WorkflowPasswordChanges");

            migrationBuilder.AddColumn<string>(
                name: "Approvers",
                table: "RequestTypes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
