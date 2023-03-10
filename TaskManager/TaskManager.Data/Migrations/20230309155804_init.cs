using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskManager.Data.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    CancelDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommentType = table.Column<byte>(type: "tinyint", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskComments_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "CreateDate", "ProjectName", "UpdateTime" },
                values: new object[,]
                {
                    { new Guid("3bd48ebf-e30c-4888-8d59-26d0bb4650e5"), new DateTime(2023, 3, 9, 18, 58, 4, 301, DateTimeKind.Local).AddTicks(5890), "TestName2", new DateTime(2023, 3, 9, 18, 58, 4, 301, DateTimeKind.Local).AddTicks(5890) },
                    { new Guid("4ea36732-f7aa-483f-ba7f-87027b5ebf50"), new DateTime(2023, 3, 9, 18, 58, 4, 301, DateTimeKind.Local).AddTicks(5900), "TestName4", new DateTime(2023, 3, 9, 18, 58, 4, 301, DateTimeKind.Local).AddTicks(5910) },
                    { new Guid("dbb2dd56-6092-42f8-b468-8dde8fe35412"), new DateTime(2023, 3, 9, 18, 58, 4, 301, DateTimeKind.Local).AddTicks(5850), "TestName1", new DateTime(2023, 3, 9, 18, 58, 4, 301, DateTimeKind.Local).AddTicks(5880) },
                    { new Guid("fb4517a5-70bc-4f97-8e49-281e9d9da2c6"), new DateTime(2023, 3, 9, 18, 58, 4, 301, DateTimeKind.Local).AddTicks(5900), "TestName3", new DateTime(2023, 3, 9, 18, 58, 4, 301, DateTimeKind.Local).AddTicks(5900) }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "CancelDate", "CreateDate", "ProjectId", "StartDate", "TaskName", "UpdateTime" },
                values: new object[,]
                {
                    { new Guid("11034b68-b462-49a3-8dbf-123de26f9af6"), new DateTime(2023, 3, 9, 18, 58, 4, 301, DateTimeKind.Local).AddTicks(6020), new DateTime(2023, 3, 9, 18, 58, 4, 301, DateTimeKind.Local).AddTicks(6020), new Guid("4ea36732-f7aa-483f-ba7f-87027b5ebf50"), new DateTime(2023, 3, 9, 18, 58, 4, 301, DateTimeKind.Local).AddTicks(6020), "TaskName8", new DateTime(2023, 3, 9, 18, 58, 4, 301, DateTimeKind.Local).AddTicks(6020) },
                    { new Guid("2833c704-ea71-4f2c-990f-11f1d09dbcc0"), new DateTime(2023, 3, 9, 18, 58, 4, 301, DateTimeKind.Local).AddTicks(5990), new DateTime(2023, 3, 9, 18, 58, 4, 301, DateTimeKind.Local).AddTicks(5990), new Guid("fb4517a5-70bc-4f97-8e49-281e9d9da2c6"), new DateTime(2023, 3, 9, 18, 58, 4, 301, DateTimeKind.Local).AddTicks(5990), "TaskName5", new DateTime(2023, 3, 9, 18, 58, 4, 301, DateTimeKind.Local).AddTicks(5990) },
                    { new Guid("a602b4dd-0861-4027-b02b-0192199c6109"), new DateTime(2023, 3, 9, 18, 58, 4, 301, DateTimeKind.Local).AddTicks(5950), new DateTime(2023, 3, 9, 18, 58, 4, 301, DateTimeKind.Local).AddTicks(5950), new Guid("dbb2dd56-6092-42f8-b468-8dde8fe35412"), new DateTime(2023, 3, 9, 18, 58, 4, 301, DateTimeKind.Local).AddTicks(5940), "TaskName1", new DateTime(2023, 3, 9, 18, 58, 4, 301, DateTimeKind.Local).AddTicks(5950) },
                    { new Guid("b4e9b2a5-1fa7-4e01-ac81-0d2f5e460d88"), new DateTime(2023, 3, 9, 18, 58, 4, 301, DateTimeKind.Local).AddTicks(6000), new DateTime(2023, 3, 9, 18, 58, 4, 301, DateTimeKind.Local).AddTicks(6000), new Guid("fb4517a5-70bc-4f97-8e49-281e9d9da2c6"), new DateTime(2023, 3, 9, 18, 58, 4, 301, DateTimeKind.Local).AddTicks(6000), "TaskName6", new DateTime(2023, 3, 9, 18, 58, 4, 301, DateTimeKind.Local).AddTicks(6000) },
                    { new Guid("bddb819c-e7f2-4aa8-9363-b378daa5c439"), new DateTime(2023, 3, 9, 18, 58, 4, 301, DateTimeKind.Local).AddTicks(6010), new DateTime(2023, 3, 9, 18, 58, 4, 301, DateTimeKind.Local).AddTicks(6010), new Guid("4ea36732-f7aa-483f-ba7f-87027b5ebf50"), new DateTime(2023, 3, 9, 18, 58, 4, 301, DateTimeKind.Local).AddTicks(6010), "TaskName7", new DateTime(2023, 3, 9, 18, 58, 4, 301, DateTimeKind.Local).AddTicks(6010) },
                    { new Guid("bf7cff51-3b51-405c-9f84-761087b8e160"), new DateTime(2023, 3, 9, 18, 58, 4, 301, DateTimeKind.Local).AddTicks(5980), new DateTime(2023, 3, 9, 18, 58, 4, 301, DateTimeKind.Local).AddTicks(5980), new Guid("3bd48ebf-e30c-4888-8d59-26d0bb4650e5"), new DateTime(2023, 3, 9, 18, 58, 4, 301, DateTimeKind.Local).AddTicks(5980), "TaskName4", new DateTime(2023, 3, 9, 18, 58, 4, 301, DateTimeKind.Local).AddTicks(5980) },
                    { new Guid("cdf9d409-7e1e-4a24-ac39-37221af7d81f"), new DateTime(2023, 3, 9, 18, 58, 4, 301, DateTimeKind.Local).AddTicks(5970), new DateTime(2023, 3, 9, 18, 58, 4, 301, DateTimeKind.Local).AddTicks(5970), new Guid("3bd48ebf-e30c-4888-8d59-26d0bb4650e5"), new DateTime(2023, 3, 9, 18, 58, 4, 301, DateTimeKind.Local).AddTicks(5970), "TaskName3", new DateTime(2023, 3, 9, 18, 58, 4, 301, DateTimeKind.Local).AddTicks(5970) },
                    { new Guid("dac10e63-c5c7-4800-83d8-796aa66712d5"), new DateTime(2023, 3, 9, 18, 58, 4, 301, DateTimeKind.Local).AddTicks(5960), new DateTime(2023, 3, 9, 18, 58, 4, 301, DateTimeKind.Local).AddTicks(5960), new Guid("dbb2dd56-6092-42f8-b468-8dde8fe35412"), new DateTime(2023, 3, 9, 18, 58, 4, 301, DateTimeKind.Local).AddTicks(5960), "TaskName2", new DateTime(2023, 3, 9, 18, 58, 4, 301, DateTimeKind.Local).AddTicks(5960) }
                });

            migrationBuilder.InsertData(
                table: "TaskComments",
                columns: new[] { "Id", "CommentType", "Content", "FileName", "TaskId" },
                values: new object[,]
                {
                    { new Guid("0669acd7-e12f-4993-a234-ca3faf4d3e7d"), (byte)1, new byte[] { 1 }, null, new Guid("b4e9b2a5-1fa7-4e01-ac81-0d2f5e460d88") },
                    { new Guid("11229f2d-a041-44b6-b6d0-2aab38e23f21"), (byte)1, new byte[] { 1 }, null, new Guid("bddb819c-e7f2-4aa8-9363-b378daa5c439") },
                    { new Guid("144508c7-0e75-4f1e-bc30-90be8063ec9c"), (byte)1, new byte[] { 1 }, null, new Guid("b4e9b2a5-1fa7-4e01-ac81-0d2f5e460d88") },
                    { new Guid("179d0711-89e7-422a-8535-7ceefecbc900"), (byte)1, new byte[] { 1 }, null, new Guid("bf7cff51-3b51-405c-9f84-761087b8e160") },
                    { new Guid("1e9fee81-1cf3-4f93-9180-761220c987a5"), (byte)1, new byte[] { 1 }, null, new Guid("bf7cff51-3b51-405c-9f84-761087b8e160") },
                    { new Guid("4eba2480-bfa5-4dca-9fc3-f2052ff6de24"), (byte)1, new byte[] { 1 }, null, new Guid("2833c704-ea71-4f2c-990f-11f1d09dbcc0") },
                    { new Guid("6b715346-2b45-40d1-890c-62b93e2b6ce5"), (byte)1, new byte[] { 1 }, null, new Guid("dac10e63-c5c7-4800-83d8-796aa66712d5") },
                    { new Guid("8abbf689-8dcf-4a0e-8215-97b90a220e59"), (byte)1, new byte[] { 1 }, null, new Guid("dac10e63-c5c7-4800-83d8-796aa66712d5") },
                    { new Guid("a5f655f2-7605-445c-a151-312ec8977981"), (byte)1, new byte[] { 1 }, null, new Guid("2833c704-ea71-4f2c-990f-11f1d09dbcc0") },
                    { new Guid("a8b09fcf-7618-4345-8490-55d21b432f04"), (byte)1, new byte[] { 1 }, null, new Guid("cdf9d409-7e1e-4a24-ac39-37221af7d81f") },
                    { new Guid("c81bd951-b19b-4123-948a-619d5d634152"), (byte)1, new byte[] { 1 }, null, new Guid("a602b4dd-0861-4027-b02b-0192199c6109") },
                    { new Guid("d38e258e-31b9-430d-bd22-d48fc3d28d99"), (byte)1, new byte[] { 1 }, null, new Guid("cdf9d409-7e1e-4a24-ac39-37221af7d81f") },
                    { new Guid("dec822f7-d469-4746-ab44-a33211feaf7f"), (byte)1, new byte[] { 1 }, null, new Guid("a602b4dd-0861-4027-b02b-0192199c6109") },
                    { new Guid("e2e969d9-5309-410c-8c4d-bef13f724a89"), (byte)1, new byte[] { 1 }, null, new Guid("11034b68-b462-49a3-8dbf-123de26f9af6") },
                    { new Guid("ec8018ac-fe83-48cd-9f18-a5560a010d5b"), (byte)1, new byte[] { 1 }, null, new Guid("bddb819c-e7f2-4aa8-9363-b378daa5c439") },
                    { new Guid("f1cd4363-0e47-47ea-b66f-86f108ca7db0"), (byte)1, new byte[] { 1 }, null, new Guid("11034b68-b462-49a3-8dbf-123de26f9af6") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskComments_TaskId",
                table: "TaskComments",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ProjectId",
                table: "Tasks",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskComments");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
