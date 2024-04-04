using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuicklyGo.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conversations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlImg = table.Column<string>(type: "text", nullable: false, defaultValue: "Img/avatar/User-Default.jpg"),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Group = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VerifyCode = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    UrlImgAvatar = table.Column<string>(type: "text", nullable: true, defaultValue: "Img/avatar/User-Default.jpg"),
                    UrlBackground = table.Column<string>(type: "text", nullable: true, defaultValue: "Img/avatar_background/default.jpg"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 2),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatorId = table.Column<string>(type: "nvarchar(21)", nullable: false),
                    ConversationId = table.Column<string>(type: "nvarchar(21)", nullable: false),
                    BodyContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlImg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(21)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notices_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Relationships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(21)", nullable: false),
                    FriendId = table.Column<string>(type: "nvarchar(21)", nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relationships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Relationships_Users_FriendId",
                        column: x => x.FriendId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Relationships_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MessageReceipients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceipientId = table.Column<string>(type: "nvarchar(21)", nullable: false),
                    MessageId = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageReceipients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageReceipients_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MessageReceipients_Users_ReceipientId",
                        column: x => x.ReceipientId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserConversations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(21)", nullable: false),
                    ConversationId = table.Column<string>(type: "nvarchar(21)", nullable: false),
                    LastSeenMessage = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    MessageId = table.Column<int>(type: "int", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserConversations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserConversations_Conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserConversations_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserConversations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Conversations",
                columns: new[] { "Id", "CreateAt", "Description", "Name", "Status", "UpdateAt" },
                values: new object[] { "KwofCoKs3ThJVE5gOkbn8", new DateTime(2024, 4, 4, 14, 27, 25, 885, DateTimeKind.Local).AddTicks(8378), "Admin conversation", "Admin", 1, new DateTime(2024, 4, 4, 14, 27, 25, 885, DateTimeKind.Local).AddTicks(8379) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateAt", "Description", "Email", "FirstName", "LastName", "Location", "Password", "PhoneNumber", "Status", "UpdateAt", "UserName", "VerifyCode" },
                values: new object[,]
                {
                    { "0KhfroI2WdGEHFQf7LU6B", new DateTime(2024, 4, 4, 14, 27, 25, 210, DateTimeKind.Local).AddTicks(1633), null, "admin@gmail.com", "Admin", "Admin", null, "$2a$11$UB0pLgL3bQzFJ8FGOMX5QetiWfyjO00GeBpZtfR8WaKEolcTZ./sC", "0123456789", 1, new DateTime(2024, 4, 4, 14, 27, 25, 210, DateTimeKind.Local).AddTicks(1742), "admin", "123456" },
                    { "85GBCdMebC8qRHCXdDUqm", new DateTime(2024, 4, 4, 14, 27, 25, 355, DateTimeKind.Local).AddTicks(1855), null, "lamtran@gmail.com", "Lam", "Tran", null, "$2a$11$QTOFIv.b8ElWKgVrSuQtw.teRsCR7CmNQuJ0UaxB8cfAWZ.65eAEi", "0123456781", 1, new DateTime(2024, 4, 4, 14, 27, 25, 355, DateTimeKind.Local).AddTicks(1975), "lamtran", "123456" },
                    { "FVHgafCy1NhVkKfJ0gnha", new DateTime(2024, 4, 4, 14, 27, 25, 632, DateTimeKind.Local).AddTicks(5048), null, "vinhnguyen@gmail.com", "Vinh", "Nguyen", null, "$2a$11$TULxxAKLbu3xNHx.OeV9ueGXi3dshSMe.OCJ5SR1zGfFBPeaHMg2y", "0123456783", 1, new DateTime(2024, 4, 4, 14, 27, 25, 632, DateTimeKind.Local).AddTicks(5060), "vinhnguyen", "123456" },
                    { "qenbeVD2Y19Kyx4DGfYfj", new DateTime(2024, 4, 4, 14, 27, 25, 758, DateTimeKind.Local).AddTicks(9915), null, "bradpitt@gmail.com", "Brad", "Pitt", null, "$2a$11$F7KF42owyBkunUAG11tyBe0Z5pXlMtmyoFBl1CSag5uYiQIZe.CZS", "0123456784", 1, new DateTime(2024, 4, 4, 14, 27, 25, 758, DateTimeKind.Local).AddTicks(9920), "bradpitt", "123456" },
                    { "t30fnEOCCcuja8zcBrVdH", new DateTime(2024, 4, 4, 14, 27, 25, 496, DateTimeKind.Local).AddTicks(9805), null, "dungnguyen@gmail.com", "Dung", "Nguyen", null, "$2a$11$fak6x0UyZ6yc9fjQbEwtEuJAfsgmOM73kJv8q18rjUMnAU.Z9LKi.", "0123456782", 1, new DateTime(2024, 4, 4, 14, 27, 25, 496, DateTimeKind.Local).AddTicks(9838), "dungnguyen", "123456" },
                    { "YizSnc1Hj7JlhbdpGhsxw", new DateTime(2024, 4, 4, 14, 27, 25, 885, DateTimeKind.Local).AddTicks(8144), null, "angelinajolie@gmail.com", "Angelina", "Jolie", null, "$2a$11$N9RWDyBeQRux.sUzm9k3w.qE5hP6M7XXC33xTgWe.b2OloTYl5XYG", "0123456785", 1, new DateTime(2024, 4, 4, 14, 27, 25, 885, DateTimeKind.Local).AddTicks(8147), "angelinajolie", "123456" }
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "BodyContent", "ConversationId", "CreateAt", "CreatorId", "UpdateAt" },
                values: new object[,]
                {
                    { 1, "Hello", "KwofCoKs3ThJVE5gOkbn8", new DateTime(2024, 4, 4, 14, 27, 25, 885, DateTimeKind.Local).AddTicks(8415), "0KhfroI2WdGEHFQf7LU6B", new DateTime(2024, 4, 4, 14, 27, 25, 885, DateTimeKind.Local).AddTicks(8415) },
                    { 2, "Hi", "KwofCoKs3ThJVE5gOkbn8", new DateTime(2024, 4, 4, 14, 27, 25, 885, DateTimeKind.Local).AddTicks(8419), "0KhfroI2WdGEHFQf7LU6B", new DateTime(2024, 4, 4, 14, 27, 25, 885, DateTimeKind.Local).AddTicks(8419) },
                    { 3, "what are you doing ?", "KwofCoKs3ThJVE5gOkbn8", new DateTime(2024, 4, 4, 14, 27, 25, 885, DateTimeKind.Local).AddTicks(8422), "85GBCdMebC8qRHCXdDUqm", new DateTime(2024, 4, 4, 14, 27, 25, 885, DateTimeKind.Local).AddTicks(8423) }
                });

            migrationBuilder.InsertData(
                table: "Notices",
                columns: new[] { "Id", "CreateAt", "Description", "Status", "Title", "UpdateAt", "UrlImg", "UserId" },
                values: new object[] { 1, new DateTime(2024, 4, 4, 14, 27, 25, 885, DateTimeKind.Local).AddTicks(8461), "Notice 1", 1, "Notice 1", new DateTime(2024, 4, 4, 14, 27, 25, 885, DateTimeKind.Local).AddTicks(8462), null, "0KhfroI2WdGEHFQf7LU6B" });

            migrationBuilder.InsertData(
                table: "UserConversations",
                columns: new[] { "Id", "ConversationId", "CreateAt", "LastSeenMessage", "MessageId", "Status", "UpdateAt", "UserId" },
                values: new object[,]
                {
                    { 1, "KwofCoKs3ThJVE5gOkbn8", new DateTime(2024, 4, 4, 14, 27, 25, 885, DateTimeKind.Local).AddTicks(8442), new DateTime(2024, 4, 4, 14, 27, 25, 885, DateTimeKind.Local).AddTicks(8440), null, 1, new DateTime(2024, 4, 4, 14, 27, 25, 885, DateTimeKind.Local).AddTicks(8443), "0KhfroI2WdGEHFQf7LU6B" },
                    { 2, "KwofCoKs3ThJVE5gOkbn8", new DateTime(2024, 4, 4, 14, 27, 25, 885, DateTimeKind.Local).AddTicks(8448), new DateTime(2024, 4, 4, 14, 27, 25, 885, DateTimeKind.Local).AddTicks(8447), null, 1, new DateTime(2024, 4, 4, 14, 27, 25, 885, DateTimeKind.Local).AddTicks(8448), "85GBCdMebC8qRHCXdDUqm" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessageReceipients_MessageId",
                table: "MessageReceipients",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageReceipients_ReceipientId",
                table: "MessageReceipients",
                column: "ReceipientId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ConversationId",
                table: "Messages",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_CreatorId",
                table: "Messages",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Notices_UserId",
                table: "Notices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Relationships_FriendId",
                table: "Relationships",
                column: "FriendId");

            migrationBuilder.CreateIndex(
                name: "IX_Relationships_UserId",
                table: "Relationships",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserConversations_ConversationId",
                table: "UserConversations",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserConversations_MessageId",
                table: "UserConversations",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserConversations_UserId",
                table: "UserConversations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PhoneNumber",
                table: "Users",
                column: "PhoneNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageReceipients");

            migrationBuilder.DropTable(
                name: "Notices");

            migrationBuilder.DropTable(
                name: "Relationships");

            migrationBuilder.DropTable(
                name: "UserConversations");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Conversations");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
