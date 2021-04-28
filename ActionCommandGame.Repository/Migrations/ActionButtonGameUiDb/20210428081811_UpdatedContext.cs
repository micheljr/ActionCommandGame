using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ActionCommandGame.Repository.Migrations.ActionButtonGameUiDb
{
    public partial class UpdatedContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Fuel = table.Column<int>(type: "int", nullable: false),
                    Attack = table.Column<int>(type: "int", nullable: false),
                    Defense = table.Column<int>(type: "int", nullable: false),
                    ActionCooldownSeconds = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NegativeGameEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefenseWithGearDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefenseWithoutGearDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefenseLoss = table.Column<int>(type: "int", nullable: false),
                    Probability = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NegativeGameEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PositiveGameEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Money = table.Column<int>(type: "int", nullable: false),
                    Experience = table.Column<int>(type: "int", nullable: false),
                    Probability = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositiveGameEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RemainingFuel = table.Column<int>(type: "int", nullable: false),
                    RemainingAttack = table.Column<int>(type: "int", nullable: false),
                    RemainingDefense = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Money = table.Column<int>(type: "int", nullable: false),
                    Experience = table.Column<int>(type: "int", nullable: false),
                    LastActionExecutedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentFuelPlayerItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentAttackPlayerItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentDefensePlayerItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_PlayerItems_CurrentAttackPlayerItemId",
                        column: x => x.CurrentAttackPlayerItemId,
                        principalTable: "PlayerItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Players_PlayerItems_CurrentDefensePlayerItemId",
                        column: x => x.CurrentDefensePlayerItemId,
                        principalTable: "PlayerItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Players_PlayerItems_CurrentFuelPlayerItemId",
                        column: x => x.CurrentFuelPlayerItemId,
                        principalTable: "PlayerItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "ActionCooldownSeconds", "Attack", "Defense", "Description", "Fuel", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("4846bbd1-7f20-4f2a-8591-fe0af5fafe62"), 0, 50, 0, null, 0, "Basic Pickaxe", 50 },
                    { new Guid("989ee5c7-4314-4d5a-a98a-08fbb43b0f52"), 0, 0, 0, "Yes, show everyone how much money you are willing to spend on something useless!", 0, "Crown of Flexing", 500000 },
                    { new Guid("4678281d-51fe-4ff9-a607-020e427b3df6"), 0, 0, 0, "For those who cannot afford the Crown of Flexing.", 0, "Blue Medal", 100000 },
                    { new Guid("57154828-fbc2-4e7c-be69-f6837e370af4"), 0, 0, 0, "Does nothing. Do you feel special now?", 0, "Balloon", 10 },
                    { new Guid("71857d8e-c0a2-4a9a-95df-772c5ee41fb2"), 1, 0, 0, null, 1000, "Developer Food", 1 },
                    { new Guid("738ab24b-2984-4d0a-bfc7-8bf30a881f75"), 15, 0, 0, null, 500, "Celestial Burrito", 10000 },
                    { new Guid("6abd5d22-aaad-465e-9b6b-7e2fc49c59dd"), 25, 0, 0, null, 100, "Abbye Beer", 500 },
                    { new Guid("181bdb55-a2c0-4416-a222-94101e97a78d"), 25, 0, 0, null, 100, "Abbye cheese", 500 },
                    { new Guid("4c72f8d1-ff92-4e47-8498-686fbf53e476"), 30, 0, 0, null, 30, "Field Rations", 300 },
                    { new Guid("ef18d138-4396-415a-a164-3052cbc89f90"), 45, 0, 0, null, 5, "Energy Bar", 10 },
                    { new Guid("e5d5525b-9394-4687-9f38-32e2c6840010"), 50, 0, 0, null, 4, "Apple", 8 },
                    { new Guid("ce94dd35-c7bd-4c45-9bc1-21df4e647136"), 0, 0, 2000, null, 0, "Emerald Shield", 10000 },
                    { new Guid("e44b349f-aeea-4b92-b5cc-9c697aa4598a"), 0, 0, 2000, null, 0, "Rock Shield", 10000 },
                    { new Guid("da92e57a-ce50-44d8-9805-c384dd0de52d"), 0, 0, 500, null, 0, "Iron plated Armor", 1000 },
                    { new Guid("bf6c7638-cdbd-4dca-8cff-346207773769"), 0, 0, 150, null, 0, "Hardened Leather Gear", 200 },
                    { new Guid("44e08e68-2257-4fe2-80f7-eff9e726b704"), 0, 0, 20, null, 0, "Torn Clothes", 20 },
                    { new Guid("b57b8368-ed3c-4990-947c-798b858f23ff"), 0, 50, 0, null, 0, "Thor's Hammer", 1000000 },
                    { new Guid("21abee98-561e-41a9-a92f-b158653a0593"), 0, 5000, 0, null, 0, "Mithril Warpick", 15000 },
                    { new Guid("5d4e1f82-6659-426b-aef8-32e44e4c9880"), 0, 500, 0, null, 0, "Turbo Pick", 500 },
                    { new Guid("f2d28aaf-0db4-4152-8f4d-5e68626aebed"), 0, 300, 0, null, 0, "Enhanced Pick", 300 },
                    { new Guid("52bd5012-5c88-41a5-b789-f630f0134cf1"), 0, 0, 20000, null, 0, "Diamond Shield", 10000 }
                });

            migrationBuilder.InsertData(
                table: "NegativeGameEvents",
                columns: new[] { "Id", "DefenseLoss", "DefenseWithGearDescription", "DefenseWithoutGearDescription", "Description", "Name", "Probability" },
                values: new object[,]
                {
                    { new Guid("fccdc4e6-b17b-43c6-b80d-70b8fa1919f4"), 3, "Your gear barely covers you from the noxious goop. You are safe.", "The slime covers your hands and arms and starts biting through your flesh. This hurts!", "As you are mining, you uncover a green slime oozing from the cracks!", "Ancient Bacteria", 50 },
                    { new Guid("d238ca22-caf7-496a-81a6-a6294132e0b6"), 2, "Your gear grants a safe landing, protecting you and your pickaxe.", "You tumble down the dark hole and take a really bad landing. That hurt!", "As you are mining, the ground suddenly gives way and you fall down into a chasm!", "Sinkhole", 100 },
                    { new Guid("058c9d61-9ece-4bdf-b72a-b77bbf944243"), 3, "It tries to bite you, but your mining gear keeps the rat's teeth from sinking in.", "It tries to bite you and nicks you in the ankles. It already starts to glow dangerously.", "As you are mining, you feel something scurry between your feet!", "Cave Rat", 50 },
                    { new Guid("c34d4618-280b-4367-8ed4-e8b473685047"), 2, "Your mining gear allows you and your tools to escape unscathed", "You try to cover your face but the rocks are too heavy. That hurt!", "As you are mining, the cave walls rumble and rocks tumble down on you", "Rockfall", 100 }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CurrentAttackPlayerItemId", "CurrentDefensePlayerItemId", "CurrentFuelPlayerItemId", "Experience", "LastActionExecutedDateTime", "Money", "Name" },
                values: new object[,]
                {
                    { new Guid("ffcecb8b-6560-4621-987d-ab92c4b51f78"), null, null, null, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "NewPlayer" },
                    { new Guid("7b3d6d4d-f5b8-481e-b96a-54c282b0cbb6"), null, null, null, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, "John Doe" },
                    { new Guid("a6d0165e-6750-41bb-ad92-1896b03a78db"), null, null, null, 2000, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100000, "John Francks" },
                    { new Guid("7b876ee4-eb32-4ee3-bcca-fd08bc396bdc"), null, null, null, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 500, "Luc Doleman" },
                    { new Guid("232a9fa8-4b27-4c77-91b7-81a13a95b198"), null, null, null, 200, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 12345, "Emilio Fratilleci" }
                });

            migrationBuilder.InsertData(
                table: "PositiveGameEvents",
                columns: new[] { "Id", "Description", "Experience", "Money", "Name", "Probability" },
                values: new object[,]
                {
                    { new Guid("c600bd27-4505-41c2-a158-98a480f06084"), null, 40, 100, "Peculiar Mask", 350 },
                    { new Guid("d7636151-e58b-483f-ae46-034ded03516c"), null, 50, 140, "Quartz Geode", 300 },
                    { new Guid("061ff00b-e620-4b9a-bb3a-c3a6dc5834bc"), null, 80, 160, "Ancient Weapon", 300 },
                    { new Guid("c15a463c-9f09-4732-bf66-ca8f81d656a4"), null, 80, 160, "Ancient Instrument", 300 },
                    { new Guid("a947a9c5-9e4a-4e7b-b0f9-df7b55d1bad8"), null, 80, 180, "Ancient Texts", 300 },
                    { new Guid("7d0ef02c-7c1d-46fd-b41a-653836f404a1"), null, 100, 300, "Gemstone", 110 },
                    { new Guid("b343b62a-19c4-4919-9fdc-5a7ee02940e3"), null, 150, 500, "Ancient Bust", 150 },
                    { new Guid("21457956-8ed2-4c13-9df1-35c2c6446ed1"), null, 150, 400, "Meteorite", 200 },
                    { new Guid("d244bb71-34fc-4f39-8928-8b3a5dc2932c"), null, 15, 60, "Jewelry", 400 },
                    { new Guid("332b38a4-59bb-4c24-89cb-fba6c04f6563"), null, 200, 1000, "Buried Treasure", 100 },
                    { new Guid("13e446b9-bbeb-4afe-b0f3-519561a0e4d9"), null, 1500, 60000, "Alien DNA", 5 },
                    { new Guid("45f60fc7-0ee8-45e8-be57-fbf700c638e8"), null, 400, 3000, "Rare Collector's Item", 30 }
                });

            migrationBuilder.InsertData(
                table: "PositiveGameEvents",
                columns: new[] { "Id", "Description", "Experience", "Money", "Name", "Probability" },
                values: new object[,]
                {
                    { new Guid("779feab2-6b41-42a4-a1cc-63de6e684825"), null, 350, 2000, "Pure Gold", 30 },
                    { new Guid("3f083ce5-34a5-411d-8898-da1736510a13"), null, 100, 300, "Mysterious Potion", 80 },
                    { new Guid("bf4f169a-3b3d-425d-8c07-3c88b562a462"), null, 13, 50, "Scrap Metal", 400 },
                    { new Guid("28d4a946-6b37-4524-9555-4f7a0bcc6650"), "It slips out of your hands and rolls inside a crack in the floor. It is out of reach.", 0, 0, "The biggest Opal you ever saw.", 500 },
                    { new Guid("7e475bf3-7e1d-43cb-b476-226d5c9d6005"), null, 8, 20, "Cave Shroom", 650 },
                    { new Guid("2eb8662f-8613-4a98-ad1d-fa09754b4bd4"), null, 0, 0, "Nothing but boring rocks", 1000 },
                    { new Guid("d172e804-4856-4516-bd68-c631d802e96d"), null, 1000, 20000, "Safe Deposit Box Key", 10 },
                    { new Guid("5e8c8e26-f897-4ce1-85cc-bc7da758d2f3"), null, 0, 0, "Sand, dirt and dust", 1000 },
                    { new Guid("b57a8ab3-f8ed-460d-bd86-41303a9b38f0"), "You hold it to the light and warm it up to reveal secret texts, but it remains empty.", 0, 0, "A piece of empty paper", 1000 },
                    { new Guid("aa0fa264-97b2-4856-b504-553d329ae061"), "The water flows around your feet and creates a dirty puddle.", 0, 0, "A small water stream", 1000 },
                    { new Guid("2a903ab2-3917-4c86-87e6-8bf1118a30a6"), null, 1, 1, "Junk", 2000 },
                    { new Guid("a3c261be-9f70-4010-9fa5-32ead1b13629"), null, 10, 30, "Artifact", 500 },
                    { new Guid("51b720d8-78a9-449e-86ce-2c16418dc070"), null, 1, 1, "Murphy's idea bin", 300 },
                    { new Guid("4c24e24c-8331-4e5f-b6ad-b0ba658340fc"), null, 1, 1, "Children's Treasure Map", 300 },
                    { new Guid("8420d68c-ce58-4723-8898-4db58d5dda38"), null, 3, 5, "Trinket", 1000 },
                    { new Guid("20570d39-52f3-457f-98df-26ba4f94196e"), null, 5, 10, "Old Tool", 800 },
                    { new Guid("32fea2ac-6632-4d5b-bdd8-dd6444957dd7"), null, 5, 10, "Old Equipment", 800 },
                    { new Guid("951e36e9-a48f-47e0-9304-222942bde17a"), null, 5, 10, "Ornate Shell", 800 },
                    { new Guid("f52b2a1d-36b1-43a7-91ea-117cef3b6799"), null, 6, 12, "Fossil", 700 },
                    { new Guid("3b931d7d-a739-4070-bf20-c6e67ded0d49"), null, 1, 1, "Donald's book of excuses", 300 },
                    { new Guid("6b388682-1128-4d8f-83f8-cd6a2e97e578"), null, 1500, 30000, "Advanced Bio Tech", 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerItems_ItemId",
                table: "PlayerItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerItems_PlayerId",
                table: "PlayerItems",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_CurrentAttackPlayerItemId",
                table: "Players",
                column: "CurrentAttackPlayerItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_CurrentDefensePlayerItemId",
                table: "Players",
                column: "CurrentDefensePlayerItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_CurrentFuelPlayerItemId",
                table: "Players",
                column: "CurrentFuelPlayerItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerItems_Players_PlayerId",
                table: "PlayerItems",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerItems_Items_ItemId",
                table: "PlayerItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerItems_Players_PlayerId",
                table: "PlayerItems");

            migrationBuilder.DropTable(
                name: "NegativeGameEvents");

            migrationBuilder.DropTable(
                name: "PositiveGameEvents");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "PlayerItems");
        }
    }
}
