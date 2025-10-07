using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exam_System.Migrations
{
    /// <inheritdoc />
    public partial class answerFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerDetail_Answers_AnswerId",
                table: "AnswerDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_AnswerDetail_Questions_QuestionId",
                table: "AnswerDetail");

            migrationBuilder.DropTable(
                name: "SelectedChoices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnswerDetail",
                table: "AnswerDetail");

            migrationBuilder.RenameTable(
                name: "AnswerDetail",
                newName: "AnswerDetails");

            migrationBuilder.RenameIndex(
                name: "IX_AnswerDetail_QuestionId",
                table: "AnswerDetails",
                newName: "IX_AnswerDetails_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_AnswerDetail_AnswerId",
                table: "AnswerDetails",
                newName: "IX_AnswerDetails_AnswerId");

            migrationBuilder.AddColumn<string>(
                name: "SelectedChoiceIds",
                table: "AnswerDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnswerDetails",
                table: "AnswerDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerDetails_Answers_AnswerId",
                table: "AnswerDetails",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerDetails_Questions_QuestionId",
                table: "AnswerDetails",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerDetails_Answers_AnswerId",
                table: "AnswerDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_AnswerDetails_Questions_QuestionId",
                table: "AnswerDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnswerDetails",
                table: "AnswerDetails");

            migrationBuilder.DropColumn(
                name: "SelectedChoiceIds",
                table: "AnswerDetails");

            migrationBuilder.RenameTable(
                name: "AnswerDetails",
                newName: "AnswerDetail");

            migrationBuilder.RenameIndex(
                name: "IX_AnswerDetails_QuestionId",
                table: "AnswerDetail",
                newName: "IX_AnswerDetail_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_AnswerDetails_AnswerId",
                table: "AnswerDetail",
                newName: "IX_AnswerDetail_AnswerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnswerDetail",
                table: "AnswerDetail",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "SelectedChoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnswerDetailsId = table.Column<int>(type: "int", nullable: false),
                    choiceId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectedChoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SelectedChoices_AnswerDetail_AnswerDetailsId",
                        column: x => x.AnswerDetailsId,
                        principalTable: "AnswerDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SelectedChoices_Choices_choiceId",
                        column: x => x.choiceId,
                        principalTable: "Choices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SelectedChoices_AnswerDetailsId_choiceId",
                table: "SelectedChoices",
                columns: new[] { "AnswerDetailsId", "choiceId" });

            migrationBuilder.CreateIndex(
                name: "IX_SelectedChoices_choiceId",
                table: "SelectedChoices",
                column: "choiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerDetail_Answers_AnswerId",
                table: "AnswerDetail",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerDetail_Questions_QuestionId",
                table: "AnswerDetail",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
