using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace designhub.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileGroup",
                columns: table => new
                {
                    FileGroupID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "strftime('%Y-%m-%d')"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileGroup", x => x.FileGroupID);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectsID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "strftime('%Y-%m-%d')"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectsID);
                });

            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    FileID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "strftime('%Y-%m-%d')"),
                    FileGroupID = table.Column<int>(nullable: false),
                    FilePath = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.FileID);
                    table.ForeignKey(
                        name: "FK_File_FileGroup_FileGroupID",
                        column: x => x.FileGroupID,
                        principalTable: "FileGroup",
                        principalColumn: "FileGroupID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectFileGroup",
                columns: table => new
                {
                    ProjectFileGroupID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    FileGroupID = table.Column<int>(nullable: false),
                    ProjectsID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectFileGroup", x => x.ProjectFileGroupID);
                    table.ForeignKey(
                        name: "FK_ProjectFileGroup_FileGroup_FileGroupID",
                        column: x => x.FileGroupID,
                        principalTable: "FileGroup",
                        principalColumn: "FileGroupID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectFileGroup_Projects_ProjectsID",
                        column: x => x.ProjectsID,
                        principalTable: "Projects",
                        principalColumn: "ProjectsID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    CommentID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateCreated = table.Column<DateTime>(nullable: false, defaultValueSql: "strftime('%Y-%m-%d')"),
                    FileID = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK_Comment_File_FileID",
                        column: x => x.FileID,
                        principalTable: "File",
                        principalColumn: "FileID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_FileID",
                table: "Comment",
                column: "FileID");

            migrationBuilder.CreateIndex(
                name: "IX_File_FileGroupID",
                table: "File",
                column: "FileGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFileGroup_FileGroupID",
                table: "ProjectFileGroup",
                column: "FileGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFileGroup_ProjectsID",
                table: "ProjectFileGroup",
                column: "ProjectsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "ProjectFileGroup");

            migrationBuilder.DropTable(
                name: "File");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "FileGroup");
        }
    }
}
