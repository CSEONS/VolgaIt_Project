using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VolgaIt.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "885c39db-1e52-45eb-8d07-d185bd722ac3", "aadde235-b1ee-4f35-a797-f67a908e405a" });

            migrationBuilder.DeleteData(
                table: "TransportTypes",
                keyColumn: "Id",
                keyValue: "6ce84a04-6ea5-42fa-af6d-528e7c0edda7");

            migrationBuilder.DeleteData(
                table: "TransportTypes",
                keyColumn: "Id",
                keyValue: "74857f35-fcf9-4773-9dbd-c114a843e2a3");

            migrationBuilder.DeleteData(
                table: "TransportTypes",
                keyColumn: "Id",
                keyValue: "c61d2cae-edfe-4a34-be18-49010a94e058");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "885c39db-1e52-45eb-8d07-d185bd722ac3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aadde235-b1ee-4f35-a797-f67a908e405a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cd9c6ce6-0cdd-4d49-b094-aed58fc40e5c", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccesToken", "AccessFailedCount", "Balance", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c5047230-9b04-4854-8d43-ad03db61aa6c", null, 0, 0.0, "091afd5e-56ca-428b-8886-ad0737197b58", null, false, false, null, null, "ADMIN", "AQAAAAIAAYagAAAAEEsI4ep5w7+J3IAvas6G9oTqlFIetdKlJ+VagAXfrfCnOUC7aeWcByT4fPNVARN/4A==", null, false, null, "7c599311-4aa3-49ad-9940-e246fff8f119", false, "Admin" });

            migrationBuilder.InsertData(
                table: "TransportTypes",
                columns: new[] { "Id", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3c9130bd-00b7-41d9-a750-2c02d3549c18", "Car", "CAR" },
                    { "459f1394-e2ba-4a98-8d7f-d6b14af69d19", "Scooter", "SCOOTER" },
                    { "a864f586-8cd3-4085-8927-1cf1e7bfdb45", "Bike", "BIKE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "cd9c6ce6-0cdd-4d49-b094-aed58fc40e5c", "c5047230-9b04-4854-8d43-ad03db61aa6c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "cd9c6ce6-0cdd-4d49-b094-aed58fc40e5c", "c5047230-9b04-4854-8d43-ad03db61aa6c" });

            migrationBuilder.DeleteData(
                table: "TransportTypes",
                keyColumn: "Id",
                keyValue: "3c9130bd-00b7-41d9-a750-2c02d3549c18");

            migrationBuilder.DeleteData(
                table: "TransportTypes",
                keyColumn: "Id",
                keyValue: "459f1394-e2ba-4a98-8d7f-d6b14af69d19");

            migrationBuilder.DeleteData(
                table: "TransportTypes",
                keyColumn: "Id",
                keyValue: "a864f586-8cd3-4085-8927-1cf1e7bfdb45");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cd9c6ce6-0cdd-4d49-b094-aed58fc40e5c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c5047230-9b04-4854-8d43-ad03db61aa6c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "885c39db-1e52-45eb-8d07-d185bd722ac3", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccesToken", "AccessFailedCount", "Balance", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "aadde235-b1ee-4f35-a797-f67a908e405a", null, 0, 0.0, "beb0f2e3-127c-41e7-962b-c93876eed204", null, false, false, null, null, "ADMIN", "AQAAAAIAAYagAAAAEA12szU8S/bTKcyoJ/5cQUYaSj6zQVSEdThQsfL/6DqhbRBSN9WUNKMBTLF3f/OjNw==", null, false, null, "6ea79279-34be-41c5-be50-a7e6e01ac488", false, "Admin" });

            migrationBuilder.InsertData(
                table: "TransportTypes",
                columns: new[] { "Id", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6ce84a04-6ea5-42fa-af6d-528e7c0edda7", "Scooter", "SCOOTER" },
                    { "74857f35-fcf9-4773-9dbd-c114a843e2a3", "Car", "CAR" },
                    { "c61d2cae-edfe-4a34-be18-49010a94e058", "Bike", "BIKE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "885c39db-1e52-45eb-8d07-d185bd722ac3", "aadde235-b1ee-4f35-a797-f67a908e405a" });
        }
    }
}
