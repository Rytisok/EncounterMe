using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Encounter_Me.Api.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CapturePoints",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lat = table.Column<double>(type: "float", nullable: false),
                    Lon = table.Column<double>(type: "float", nullable: false),
                    faction = table.Column<int>(type: "int", nullable: false),
                    DefenseLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CapturePoints", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "Trails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lat = table.Column<double>(type: "float", nullable: false),
                    Lng = table.Column<double>(type: "float", nullable: false),
                    Length = table.Column<double>(type: "float", nullable: false),
                    Diff = table.Column<int>(type: "int", nullable: false),
                    GeoJsonData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    trailType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Faction = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoredSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    UserPhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: false),
                    ExperiencePoints = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CapturePoints",
                columns: new[] { "guid", "DefenseLevel", "Lat", "Lon", "Name", "faction" },
                values: new object[,]
                {
                    { new Guid("68c1b83d-47fd-4e1d-b4d2-873b01951dbd"), 0, 54.748611099999998, 25.3758333, null, 0 },
                    { new Guid("d0ebeea1-d6a7-4fbf-ae6a-67991de051cb"), 0, 54.684124199999999, 25.3581462, null, 0 },
                    { new Guid("98b6afad-aec8-4d30-9ac9-51d082269e35"), 0, 54.690475399999997, 25.346211700000001, null, 0 },
                    { new Guid("8735c6ea-0b06-45fb-848d-66d941913611"), 0, 54.691921999999998, 25.3470567, null, 0 },
                    { new Guid("2b49dc72-ecac-4fda-9cd3-3854e437e837"), 0, 54.686053800000003, 25.2898377, null, 0 },
                    { new Guid("ebe9e01b-6e18-42fb-84c1-2a7b6fd22ced"), 0, 54.692265800000001, 25.2999902, null, 0 },
                    { new Guid("8d223e82-c437-40e2-802f-e5bd9191e56b"), 0, 54.676466699999999, 25.288431200000002, null, 0 },
                    { new Guid("821cd929-fa65-413c-aed8-fd549a273fd3"), 0, 54.680654799999999, 25.298798900000001, null, 0 },
                    { new Guid("8026ebf9-7a9b-445e-8664-4a885da6cb67"), 0, 54.689375200000001, 25.289997, null, 0 },
                    { new Guid("298de21a-ee87-4925-8cef-2a163da55392"), 0, 54.684777799999999, 25.303000000000001, null, 0 },
                    { new Guid("b8628ea8-e577-4f99-9ed9-289cc6f18e0c"), 0, 54.688045899999999, 25.307731, null, 0 },
                    { new Guid("28ee8a5a-3a79-404c-a9ab-8bee7083d6b5"), 0, 54.679221599999998, 25.302601200000002, null, 0 },
                    { new Guid("e7dd6933-6d1e-440b-86bd-df6ca05d8af8"), 0, 54.678319500000001, 25.294185200000001, null, 0 },
                    { new Guid("b5cd571e-5d29-4875-b913-66662ead0d51"), 0, 54.688021399999997, 25.2704679, null, 0 },
                    { new Guid("09cbc6af-14b1-4da9-b1e7-8c8b95062c49"), 0, 54.685118699999997, 25.274373700000002, null, 0 },
                    { new Guid("cc9a4a99-0941-4bc9-b364-da58ebefd892"), 0, 54.691699900000003, 25.270291799999999, null, 0 },
                    { new Guid("37b9cfac-edf7-46bb-a14e-24580769d794"), 0, 54.681162299999997, 25.287802500000002, null, 0 },
                    { new Guid("87a5c9a7-2261-446d-ba5f-84eb1db9df20"), 0, 54.679528300000001, 25.258906499999998, null, 0 },
                    { new Guid("b89897d0-3b03-48c4-832e-09d194783b64"), 0, 54.689592099999999, 25.2566545, null, 0 },
                    { new Guid("a7e1c688-56b9-4b90-8706-107fcda74be9"), 0, 54.6913281, 25.264944, null, 0 },
                    { new Guid("5869f87d-e7f4-48c6-a6eb-37f28d8d537a"), 0, 54.681010700000002, 25.267456899999999, null, 0 },
                    { new Guid("6b6693b4-1a04-48de-959f-4cb50936eec5"), 0, 54.688381100000001, 25.283764399999999, null, 0 },
                    { new Guid("2c160c28-aaee-4496-b80d-c9e179c16f84"), 0, 54.689981099999997, 25.251933300000001, null, 0 },
                    { new Guid("5c5b20b5-6ab5-4270-b8a6-b94d7c9367d6"), 0, 54.684516299999999, 25.2483027, null, 0 },
                    { new Guid("b1c0d386-61f7-4784-9fdb-708c1d26e241"), 0, 54.682180299999999, 25.245385800000001, null, 0 },
                    { new Guid("f474ce78-f6d3-4cd4-b989-77b561395a09"), 0, 54.687883100000001, 25.250481099999998, null, 0 },
                    { new Guid("1a3d668f-9e2f-4cf1-9215-a11ce019e4de"), 0, 54.688525800000001, 25.359409100000001, null, 0 },
                    { new Guid("b0ff6f5e-58d5-4160-b411-77b2eac83597"), 0, 54.689592099999999, 25.2566545, null, 0 },
                    { new Guid("251c6b58-f0d2-4ac2-9946-47ee119543ef"), 0, 54.6900282, 25.355146099999999, null, 0 },
                    { new Guid("a7faa731-6a09-4842-96b4-93780e91af7c"), 0, 54.688403700000002, 25.367331799999999, null, 0 },
                    { new Guid("5693c0d9-8d07-472a-ac9f-e1f9083bc5ab"), 0, 54.697761300000003, 25.252522899999999, null, 0 },
                    { new Guid("2c33ed80-bfd8-4b2d-9629-31ebfd145ec5"), 0, 54.695520299999998, 25.256096800000002, null, 0 },
                    { new Guid("5f6b2504-f9ef-4af3-95d8-4129ad973439"), 0, 54.695232599999997, 25.254861200000001, null, 0 },
                    { new Guid("1cb80fff-00f2-4f59-b3e9-a4cc79412fda"), 0, 54.705943499999997, 25.216359199999999, null, 0 },
                    { new Guid("1b7ebadc-219e-4796-89c5-df46889070e5"), 0, 54.699750299999998, 25.2160327, null, 0 },
                    { new Guid("e0ab7111-3aef-4b91-b93c-d8ea572d051d"), 0, 54.709637100000002, 25.2039349, null, 0 },
                    { new Guid("fb5b0add-d2c4-42e6-955c-3e8824f096cd"), 0, 54.708649999999999, 25.1954779, null, 0 },
                    { new Guid("b0b27dbf-df01-4eee-b8c9-07f5a5e620b3"), 0, 54.703384300000003, 25.222996200000001, null, 0 },
                    { new Guid("bad0b35e-5ef6-41c4-a866-e4d09568f70e"), 0, 54.702353500000001, 25.2103021, null, 0 },
                    { new Guid("4643acba-7946-46da-ab01-4d9be7f8f33a"), 0, 54.695739799999998, 25.170678200000001, null, 0 },
                    { new Guid("d1fc559d-af40-4694-9a0f-1ab11f6cb68f"), 0, 54.708649999999999, 25.1954779, null, 0 },
                    { new Guid("cbf36b28-fb5b-42ee-8937-42ab301bde54"), 0, 54.704560100000002, 25.191844499999998, null, 0 }
                });

            migrationBuilder.InsertData(
                table: "CapturePoints",
                columns: new[] { "guid", "DefenseLevel", "Lat", "Lon", "Name", "faction" },
                values: new object[,]
                {
                    { new Guid("440dda2c-d810-4e29-a25f-a8a835b1c313"), 0, 54.7011647, 25.188666600000001, null, 0 },
                    { new Guid("3eccc9ac-34f8-44e1-9124-cce9de4fca08"), 0, 54.7038078, 25.193934299999999, null, 0 },
                    { new Guid("e0869bf0-8975-4ad1-9a30-174259b7159e"), 0, 54.703209800000003, 25.1820749, null, 0 },
                    { new Guid("87754eda-a21f-45bc-8a0d-dea142786f50"), 0, 54.702336699999996, 25.1936398, null, 0 },
                    { new Guid("5cddb1d4-b172-4149-aba9-70672fd0952a"), 0, 54.7070583, 25.173080800000001, null, 0 },
                    { new Guid("56937cce-40a5-4640-94ec-930c77f63bf0"), 0, 54.705591800000001, 25.159472699999998, null, 0 },
                    { new Guid("c8ed44b8-9f67-40ea-bae3-a6ccc006aecd"), 0, 54.700563199999998, 25.158257500000001, null, 0 },
                    { new Guid("1989af60-cea7-4bc7-8541-92a8e2996708"), 0, 54.701200399999998, 25.156805800000001, null, 0 },
                    { new Guid("cb067098-7da6-49be-84e9-5b799d204e15"), 0, 54.709584700000001, 25.141535399999999, null, 0 },
                    { new Guid("f45ff137-4efd-485f-bc3b-7208ababea66"), 0, 54.687979200000001, 25.369727600000001, null, 0 },
                    { new Guid("0f888741-1fb4-4f87-b469-09dd7c241bcd"), 0, 54.691749700000003, 25.353168400000001, null, 0 },
                    { new Guid("2566731f-e76f-40a5-8137-9f12692eb09f"), 0, 54.688609900000003, 25.367363999999998, null, 0 },
                    { new Guid("42668260-84ae-490b-ad51-62b0960ce981"), 0, 54.689206800000001, 25.354091, null, 0 },
                    { new Guid("ab839ca1-9e91-4b21-ad40-618e85080eaf"), 0, 54.689222200000003, 25.369472200000001, null, 0 },
                    { new Guid("8ddfff52-fcf3-4364-aabc-678b97bcedbc"), 0, 54.690843299999997, 25.244598400000001, null, 0 },
                    { new Guid("159ee56d-14ab-419a-9b75-33685d779a21"), 0, 54.678197099999998, 25.2510288, null, 0 },
                    { new Guid("80320f0f-ee84-4ab5-a6dc-6dcc570fcd8e"), 0, 54.689597800000001, 25.232554100000002, null, 0 },
                    { new Guid("8862a505-50dd-4f0b-9c29-bbe48717cbb6"), 0, 54.6712852, 25.273366299999999, null, 0 },
                    { new Guid("4b1d346a-1c3f-4dd2-9a2c-ed21a75a3384"), 0, 54.674576999999999, 25.2295795, null, 0 },
                    { new Guid("397a323c-b538-4894-a220-37164b80f5c2"), 0, 54.660449900000003, 25.171490899999998, null, 0 },
                    { new Guid("8f7e6fb0-d74f-427a-b40f-bdef7dc3bcd2"), 0, 54.657991299999999, 25.1332238, null, 0 },
                    { new Guid("05ac125a-91c9-4d1f-83d0-c35ccdc7132a"), 0, 54.640571299999998, 25.364100199999999, null, 0 },
                    { new Guid("02fbe2a1-2beb-4ce5-8c78-1cd1177cd0df"), 0, 54.656621700000002, 25.3390545, null, 0 },
                    { new Guid("f452bf56-3c6a-4e9b-9d37-6f2b1b022a00"), 0, 54.655765000000002, 25.306873700000001, null, 0 },
                    { new Guid("03ca1797-d512-4c68-aeb4-90541b1934fe"), 0, 54.653848600000003, 25.2425572, null, 0 },
                    { new Guid("3e67d3e7-5e15-453d-8413-22b010576eba"), 0, 54.643932700000001, 25.247612700000001, null, 0 },
                    { new Guid("7581f60f-84cb-4a20-beaf-0e3112474984"), 0, 54.640471400000003, 25.232637199999999, null, 0 },
                    { new Guid("563eb36f-fd1c-4d90-8bac-bc5cd441c164"), 0, 54.640455099999997, 25.241118400000001, null, 0 },
                    { new Guid("37568228-5f06-4b7b-badb-1e3230311bee"), 0, 54.639566000000002, 25.234148000000001, null, 0 },
                    { new Guid("1d7d548a-17ed-4b2d-8112-5cb0dbcfe0f1"), 0, 54.640622999999998, 25.232517999999999, null, 0 },
                    { new Guid("d5b38516-aa1f-4f7f-be92-5fd3e36565cf"), 0, 54.643633299999998, 25.243744899999999, null, 0 },
                    { new Guid("0d77f366-2283-4c37-be56-13288ed7c42d"), 0, 54.655548699999997, 25.201234299999999, null, 0 },
                    { new Guid("0aebea87-e5b7-42c9-ab04-e3ec1844df6d"), 0, 54.641558699999997, 25.217359399999999, null, 0 },
                    { new Guid("065a2a57-7eeb-4fa2-b053-d9595fc016a9"), 0, 54.654876700000003, 25.220457799999998, null, 0 },
                    { new Guid("c6dd220e-fe3c-4451-bacf-fde642a356ab"), 0, 54.643559400000001, 25.1898506, null, 0 },
                    { new Guid("e7045ab6-26af-4b94-9a27-8f5fa61dc1f3"), 0, 54.641740300000002, 25.188721399999999, null, 0 },
                    { new Guid("56eb4a5f-1d57-4fc5-809c-850304530a97"), 0, 54.641193999999999, 25.1718358, null, 0 },
                    { new Guid("b11c95fc-d02f-4648-a231-1764a61b2e5a"), 0, 54.6522766, 25.172440399999999, null, 0 },
                    { new Guid("5dfc05bc-d428-4a2b-b16b-073a52ab04f3"), 0, 54.640542000000003, 25.185730299999999, null, 0 },
                    { new Guid("74d7b011-b488-4cb1-ba5d-43c8f58b5d64"), 0, 54.656309499999999, 25.175768699999999, null, 0 },
                    { new Guid("13ff002c-a836-4e3d-8a96-0e908b80ff1d"), 0, 54.654045799999999, 25.175360000000001, null, 0 },
                    { new Guid("6b9096e6-3756-4715-ac3c-86c34cbf7c7f"), 0, 54.654509500000003, 25.152398900000001, null, 0 }
                });

            migrationBuilder.InsertData(
                table: "CapturePoints",
                columns: new[] { "guid", "DefenseLevel", "Lat", "Lon", "Name", "faction" },
                values: new object[,]
                {
                    { new Guid("887a4629-01b2-43a7-bdea-f478f56de969"), 0, 54.673854900000002, 25.277013199999999, null, 0 },
                    { new Guid("eefb7ba5-5260-4427-84a1-14828f01be8f"), 0, 54.674898900000002, 25.285538299999999, null, 0 },
                    { new Guid("8a06f6d0-a1e9-4ffe-9e9c-d4c6e7027595"), 0, 54.671235099999997, 25.284942099999999, null, 0 },
                    { new Guid("23cbfa11-b80e-4417-abc4-79abdaa16173"), 0, 54.669180099999998, 25.277198200000001, null, 0 },
                    { new Guid("fd033e0b-a3ba-4252-8ec2-4b9cd824db0a"), 0, 54.674576999999999, 25.2295795, null, 0 },
                    { new Guid("f10b7bf1-8bdb-4b9a-81de-ad21edabe6f8"), 0, 54.6882187, 25.214095499999999, null, 0 },
                    { new Guid("0dc6893d-84f2-4077-bb3e-30c8b05d5750"), 0, 54.688788500000001, 25.215935099999999, null, 0 },
                    { new Guid("4326c371-2d17-415f-aa2a-bf09fefae694"), 0, 54.687583699999998, 25.212164999999999, null, 0 },
                    { new Guid("1b84142b-d044-4c92-a200-2eb99d78b925"), 0, 54.678457899999998, 25.213813500000001, null, 0 },
                    { new Guid("cfb57427-d1a1-490f-8364-885558dbb045"), 0, 54.679287600000002, 25.211836699999999, null, 0 },
                    { new Guid("365f16b5-1cdc-4abb-a4ff-3195473d14a1"), 0, 54.690390000000001, 25.2171676, null, 0 },
                    { new Guid("8f39e3ac-7965-4b42-8878-5b4837f9d841"), 0, 54.687936899999997, 25.208968599999999, null, 0 },
                    { new Guid("85dd5bc2-81cc-4ce7-912f-95f51e6da221"), 0, 54.686971499999999, 25.219904799999998, null, 0 },
                    { new Guid("bb1f8972-004b-490f-abbb-f11c89ba3eb2"), 0, 54.684601600000001, 25.218092899999998, null, 0 },
                    { new Guid("77b5d24d-a024-42a5-9c5c-c30d8d29d8ac"), 0, 54.689333300000001, 25.188888899999998, null, 0 },
                    { new Guid("9d34e37e-29f2-4d94-83ca-135ddbd9956d"), 0, 54.6627662, 25.3801448, null, 0 },
                    { new Guid("b6cebabb-fa5f-415a-bbd4-103d7a03b0f2"), 0, 54.6954876, 25.2555874, null, 0 },
                    { new Guid("253b763f-20bc-4ae1-8a8a-5411378fa740"), 0, 54.6650153, 25.324815399999999, null, 0 },
                    { new Guid("fcb1caa1-f90e-4ac7-9cda-f8e2932be0d2"), 0, 54.673728599999997, 25.326506500000001, null, 0 },
                    { new Guid("fff86cf1-b0ed-4c20-a3ff-0a52926a64b5"), 0, 54.656621700000002, 25.3390545, null, 0 },
                    { new Guid("61ebc6ea-5ca3-4313-b492-9a4b7bd1349a"), 0, 54.668958600000003, 25.303552400000001, null, 0 },
                    { new Guid("965dfc02-70d2-4a74-9c2f-87680747aab1"), 0, 54.669732799999998, 25.303254899999999, null, 0 },
                    { new Guid("d42564f2-4009-4db6-8929-b9b11a747707"), 0, 54.670485499999998, 25.302135, null, 0 },
                    { new Guid("56204776-931a-46f8-a974-f714a264d5e9"), 0, 54.669165300000003, 25.304578500000002, null, 0 },
                    { new Guid("02870cac-b841-4325-9765-79d8f8110770"), 0, 54.667988800000003, 25.317709499999999, null, 0 },
                    { new Guid("4aadbdb8-4790-45bb-983d-6a9de9cbb9a3"), 0, 54.668375400000002, 25.2967662, null, 0 },
                    { new Guid("b3a1f9c2-6ef5-4c9f-a009-4d7521ea7a5d"), 0, 54.674232199999999, 25.289451799999998, null, 0 },
                    { new Guid("63c336c2-9c0a-477f-afa1-e8fbd48aab5e"), 0, 54.673962799999998, 25.288797299999999, null, 0 },
                    { new Guid("bf099530-c58c-4e66-b3d3-6614a3d2e5e0"), 0, 54.659052500000001, 25.2571379, null, 0 },
                    { new Guid("862d26a0-e645-4c22-a6e6-725ef6bc7db7"), 0, 54.663380799999999, 25.274669599999999, null, 0 },
                    { new Guid("4b8d040a-5ba8-4dab-8aff-4740796ef760"), 0, 54.670119800000002, 25.324216100000001, null, 0 },
                    { new Guid("716e426e-2b62-461e-8cfd-eb42b949cf7a"), 0, 54.707894099999997, 25.247286599999999, null, 0 },
                    { new Guid("fcc21782-0e16-4903-a65d-b0a31f4caf78"), 0, 54.7013453, 25.242472899999999, null, 0 },
                    { new Guid("a1970c6d-ed96-4aa6-b25c-361ddc15ce06"), 0, 54.710954999999998, 25.278835399999998, null, 0 },
                    { new Guid("4d3fb230-1039-4d3d-a72c-6c70282d86cd"), 0, 54.739085000000003, 25.287427699999999, null, 0 },
                    { new Guid("d0d2717e-40e9-422d-8a16-99c33309d41b"), 0, 54.736769199999998, 25.285568300000001, null, 0 },
                    { new Guid("92dfd437-80bb-4127-9b64-88f8fd9810da"), 0, 54.736516000000002, 25.285228199999999, null, 0 },
                    { new Guid("d61a2fc3-793a-4d1b-92e9-5cd058e58945"), 0, 54.733914400000003, 25.286739399999998, null, 0 },
                    { new Guid("1f433afb-ae7c-41d1-ad76-be43fe065cf9"), 0, 54.738559299999999, 25.277605399999999, null, 0 },
                    { new Guid("00b32b8a-6e84-4e7a-9ed9-d477fef97d37"), 0, 54.739717800000001, 25.243719800000001, null, 0 },
                    { new Guid("f36374c2-cdf0-4389-a30c-6b1b627e7d76"), 0, 54.730388900000001, 25.226891899999998, null, 0 },
                    { new Guid("0be8733c-6b0e-4b3e-9bb6-e1bb553ccf92"), 0, 54.731210900000001, 25.222606500000001, null, 0 }
                });

            migrationBuilder.InsertData(
                table: "CapturePoints",
                columns: new[] { "guid", "DefenseLevel", "Lat", "Lon", "Name", "faction" },
                values: new object[,]
                {
                    { new Guid("7deda527-ec0b-4443-993d-e3b60afad70b"), 0, 54.731210900000001, 25.222606500000001, null, 0 },
                    { new Guid("3bbbe716-b0c4-4d43-bb9e-96bf791ff7f0"), 0, 54.730069, 25.215012300000001, null, 0 },
                    { new Guid("92a882af-66cb-494f-94d5-d190842f1682"), 0, 54.731852000000003, 25.173992200000001, null, 0 },
                    { new Guid("8a4a2e05-bd4b-483d-9fc7-0126afce034a"), 0, 54.738847900000003, 25.186406600000002, null, 0 },
                    { new Guid("571bcab5-669e-4617-8c62-4a018a33410d"), 0, 54.738288300000001, 25.1873662, null, 0 },
                    { new Guid("74ff9083-5a2e-4ead-9cc2-d712ebe74f94"), 0, 54.736105999999999, 25.189384499999999, null, 0 },
                    { new Guid("308a0f99-ed46-43af-b1ed-8a20c68d4f71"), 0, 54.721665600000001, 25.3724749, null, 0 },
                    { new Guid("a284d90e-f17f-4f24-a1cf-2460305cc44d"), 0, 54.720356000000002, 25.338956400000001, null, 0 },
                    { new Guid("e5edc1d7-b7a8-4e83-8d70-645a381b4f80"), 0, 54.728240200000002, 25.2951011, null, 0 },
                    { new Guid("d66c2df7-3a55-44fa-830e-a83c6d7706fb"), 0, 54.723356899999999, 25.3150972, null, 0 },
                    { new Guid("de9a9203-304e-42fd-8671-85664b146fd2"), 0, 54.726454799999999, 25.289966499999998, null, 0 },
                    { new Guid("cce4cd53-ece2-4305-906d-ee35bd035c70"), 0, 54.728188199999998, 25.294866200000001, null, 0 },
                    { new Guid("4f9170b3-2453-47cf-8327-08d7a11535cc"), 0, 54.728143600000003, 25.294325700000002, null, 0 },
                    { new Guid("b18e4dee-19c2-4765-93ce-aa192bf95761"), 0, 54.7135502, 25.295130199999999, null, 0 },
                    { new Guid("827ff536-d2f0-4a28-86da-e3bb9afc2d49"), 0, 54.7138487, 25.2956465, null, 0 },
                    { new Guid("621a84a4-6e02-4321-b171-7aec5daa12c2"), 0, 54.715147000000002, 25.2815251, null, 0 },
                    { new Guid("02518a85-b081-4fb8-9ebd-f6017e830265"), 0, 54.714069000000002, 25.281541499999999, null, 0 },
                    { new Guid("df82e402-29a1-4cd4-b978-29159fe84353"), 0, 54.737077900000003, 25.285828899999998, null, 0 },
                    { new Guid("470105b8-f12a-4f5d-a30b-082ef7b9ab74"), 0, 54.693342100000002, 25.240887799999999, null, 0 },
                    { new Guid("9ba46a70-73b4-46a5-9fac-7a278ec08f8f"), 0, 54.7284808, 25.287548399999999, null, 0 },
                    { new Guid("95678682-fdd6-420f-b548-3b8d9b2b2647"), 0, 54.728352200000003, 25.294312600000001, null, 0 },
                    { new Guid("380678a1-4b1b-4837-8342-6ecced87011a"), 0, 54.762991700000001, 25.3337915, null, 0 },
                    { new Guid("ed58e628-1077-436c-9f3b-d7d9643ac405"), 0, 54.754768800000001, 25.323355299999999, null, 0 },
                    { new Guid("c4d61d21-58a8-4079-a041-5a665efd80d2"), 0, 54.758522800000001, 25.340531800000001, null, 0 },
                    { new Guid("758fb568-f99f-472e-b20a-6afdc8287fa6"), 0, 54.749257200000002, 25.330741700000001, null, 0 },
                    { new Guid("aa6797f7-4274-4fba-963d-e44aeffa07d4"), 0, 54.7614831, 25.3467284, null, 0 },
                    { new Guid("68ed3715-df6a-41f8-b095-86a48ee83b99"), 0, 54.756707499999997, 25.326833600000001, null, 0 },
                    { new Guid("de866ab0-af80-428c-a8f8-7a45a1649fde"), 0, 54.760529699999999, 25.346807500000001, null, 0 },
                    { new Guid("bac82716-a57f-4241-8dfc-c38318ab5168"), 0, 54.7619817, 25.310222799999998, null, 0 },
                    { new Guid("7b0f72f3-53fd-4743-b2bd-5021d84322c2"), 0, 54.750295899999998, 25.310930899999999, null, 0 },
                    { new Guid("91f7fffe-f625-4429-8abf-c182980f1a7c"), 0, 54.749260300000003, 25.3082192, null, 0 },
                    { new Guid("cee17284-7b9e-4a4f-87cf-5ce0afe318a5"), 0, 54.750899699999998, 25.314095900000002, null, 0 },
                    { new Guid("a17f5074-216a-486a-a5ed-eb716ce7bfb2"), 0, 54.748256699999999, 25.304576300000001, null, 0 },
                    { new Guid("99b759c9-e6e0-4a87-9d38-1ad857a62e7c"), 0, 54.753185999999999, 25.306946499999999, null, 0 },
                    { new Guid("d1c352a8-f620-4fe4-a6f2-686e2d41282f"), 0, 54.758053799999999, 25.308954100000001, null, 0 },
                    { new Guid("fef6db84-01c0-4cdf-ad65-ad511b7f3d8d"), 0, 54.747977599999999, 25.2927274, null, 0 },
                    { new Guid("8c6d4ee8-33fa-478f-8d18-32461b7472e9"), 0, 54.752060399999998, 25.274545100000001, null, 0 },
                    { new Guid("97e2c65c-bb09-4675-ae82-560d65cb3900"), 0, 54.746316899999997, 25.238751000000001, null, 0 },
                    { new Guid("33eb93ac-6c79-4614-a1ad-03cb6faa58e8"), 0, 54.764705399999997, 25.2067111, null, 0 },
                    { new Guid("eeebab30-a1db-4459-8cdf-1cbaf642546d"), 0, 54.761861099999997, 25.172555599999999, null, 0 },
                    { new Guid("aaeb1b4e-1804-4b13-9d59-483400b2a064"), 0, 54.763564799999997, 25.158733999999999, null, 0 },
                    { new Guid("9152136a-f2a2-4dd2-9648-c8dc99902513"), 0, 54.7593988, 25.145688100000001, null, 0 }
                });

            migrationBuilder.InsertData(
                table: "CapturePoints",
                columns: new[] { "guid", "DefenseLevel", "Lat", "Lon", "Name", "faction" },
                values: new object[,]
                {
                    { new Guid("a66cbc16-9255-4fb4-aeab-04284d6d178d"), 0, 54.731521899999997, 25.358135099999998, null, 0 },
                    { new Guid("fc531052-ef8f-449e-b0ed-023174575bd7"), 0, 54.733837200000004, 25.375958399999998, null, 0 },
                    { new Guid("214c0d2f-dba6-4c2c-bf1c-08770dc2973f"), 0, 54.745708999999998, 25.294931500000001, null, 0 },
                    { new Guid("1ddfee2c-8850-4a56-85a9-8144a18d7975"), 0, 54.741878499999999, 25.318314999999998, null, 0 },
                    { new Guid("f36b5e05-133b-445b-be23-4d1e88b8ebe5"), 0, 54.7363456, 25.2875534, null, 0 },
                    { new Guid("1fbf4792-c4c1-4fb9-bc9a-28df169ca61c"), 0, 54.723873599999997, 25.264458999999999, null, 0 },
                    { new Guid("04055ab4-bb46-44ea-a7f1-7cc8adb50a6b"), 0, 54.725293499999999, 25.2605228, null, 0 },
                    { new Guid("11f560e0-fc88-4de9-93d8-fd95b52d9af6"), 0, 54.718815200000002, 25.284901000000001, null, 0 },
                    { new Guid("fb337bca-51ad-433e-9bba-2e82cb8f9eaa"), 0, 54.701881299999997, 25.321216700000001, null, 0 },
                    { new Guid("a50e1061-0c17-4efc-97af-6917a906860b"), 0, 54.7031919, 25.342564899999999, null, 0 },
                    { new Guid("3a95a68c-e8ba-464d-98ba-7dcffe025d0a"), 0, 54.705209799999999, 25.335022500000001, null, 0 },
                    { new Guid("192b8e8d-c1ea-428c-bfe5-74a9a2abf7f8"), 0, 54.6981775, 25.321203000000001, null, 0 },
                    { new Guid("e9d5942e-903b-4102-9ac4-91506346998a"), 0, 54.700818699999999, 25.332185299999999, null, 0 },
                    { new Guid("b1847510-a867-4af6-8bd9-3f83451f0460"), 0, 54.698651400000003, 25.3234031, null, 0 },
                    { new Guid("710d773f-3a11-4b2f-9dd2-e13a2dc7de70"), 0, 54.6987077, 25.321535900000001, null, 0 },
                    { new Guid("22b3b27a-09b7-4a60-840a-ad13bdf923d0"), 0, 54.7081132, 25.292515999999999, null, 0 },
                    { new Guid("33c8d802-7b42-4976-88a7-600497a9ea13"), 0, 54.697960799999997, 25.301251300000001, null, 0 },
                    { new Guid("dc2b99e7-4425-492e-955e-9dec91f76b87"), 0, 54.695128599999997, 25.305547600000001, null, 0 },
                    { new Guid("f20511d2-4842-437e-9dd7-add78322a7b3"), 0, 54.6962306, 25.302460400000001, null, 0 },
                    { new Guid("f7181728-6989-4327-aea4-4dc0b7898b98"), 0, 54.693857299999998, 25.305722400000001, null, 0 },
                    { new Guid("1de75098-50a7-49b8-84a9-958ed4a74724"), 0, 54.701055699999998, 25.3052967, null, 0 },
                    { new Guid("42d13089-472d-4e12-937a-4999c69e4637"), 0, 54.698627700000003, 25.314313299999998, null, 0 },
                    { new Guid("63fc6d56-e24d-41ee-ad0c-e4ee066c4808"), 0, 54.695177100000002, 25.3175211, null, 0 },
                    { new Guid("3abd7957-a3bd-4193-9f83-e967658d7da2"), 0, 54.710001300000002, 25.310697600000001, null, 0 },
                    { new Guid("a79f7311-d239-4b5e-8eaa-138b6ade5e45"), 0, 54.696119199999998, 25.271928899999999, null, 0 },
                    { new Guid("13566b26-c7c5-4e81-b563-61ac8598bcb2"), 0, 54.694758499999999, 25.275410399999998, null, 0 },
                    { new Guid("93fac14e-a863-46db-83d4-7f065d0655f0"), 0, 54.6924943, 25.2788322, null, 0 },
                    { new Guid("96d966da-1c3a-4ac0-b0b4-e1264ad4f7e1"), 0, 54.693942300000003, 25.262025099999999, null, 0 },
                    { new Guid("9cc4c93c-d200-48ae-871e-00ad49daf73c"), 0, 54.695520299999998, 25.256096800000002, null, 0 },
                    { new Guid("91f3db01-e11a-48fe-bfd8-fff5f0089046"), 0, 54.699551300000003, 25.268629799999999, null, 0 },
                    { new Guid("a3a7e1bb-297b-49ce-bb8e-e89e1794e4bf"), 0, 54.691437700000002, 25.270279800000001, null, 0 },
                    { new Guid("506b6d50-3744-47b0-994d-2e5c9fcd6d7b"), 0, 54.704080599999998, 25.2842685, null, 0 },
                    { new Guid("5469c919-2081-48fe-83de-422cc680f00d"), 0, 54.708357700000001, 25.2849854, null, 0 },
                    { new Guid("4e576156-c333-4c8e-aba2-fa82fbfeeb1e"), 0, 54.699518699999999, 25.3222548, null, 0 },
                    { new Guid("d9d52356-6be4-4f8e-8512-bd6f55ff104c"), 0, 54.703688499999998, 25.336889899999999, null, 0 },
                    { new Guid("454b2dc4-d8a6-45e2-8cab-a25f407ef869"), 0, 54.698457599999998, 25.3802746, null, 0 },
                    { new Guid("55ffb582-cdda-49f7-8a2a-192dca75cfca"), 0, 54.6981009, 25.367788699999998, null, 0 },
                    { new Guid("d5a42fde-c47a-4670-930c-8b913e5193fc"), 0, 54.719557799999997, 25.263377999999999, null, 0 },
                    { new Guid("1c2d2c78-0610-4845-bf3f-a7f7a162bebb"), 0, 54.727764200000003, 25.2492135, null, 0 },
                    { new Guid("e3734d5f-ab86-452a-ab89-8e4b5ef656d8"), 0, 54.710810500000001, 25.2272681, null, 0 },
                    { new Guid("53100cbd-acf5-4950-9898-81efee0f5d4f"), 0, 54.715411899999999, 25.235426700000001, null, 0 },
                    { new Guid("157749cd-321d-4f8e-bf6b-74ff2e73e0c8"), 0, 54.725644099999997, 25.236901899999999, null, 0 }
                });

            migrationBuilder.InsertData(
                table: "CapturePoints",
                columns: new[] { "guid", "DefenseLevel", "Lat", "Lon", "Name", "faction" },
                values: new object[,]
                {
                    { new Guid("2fdd59e9-06c6-4826-ba88-f9778b7ecde0"), 0, 54.723899099999997, 25.196537299999999, null, 0 },
                    { new Guid("8442c669-522d-42da-bf0c-f86b9a535528"), 0, 54.724440600000001, 25.2007692, null, 0 },
                    { new Guid("7e8292be-62ae-49a4-baf7-05e4852defdf"), 0, 54.7260347, 25.200862600000001, null, 0 },
                    { new Guid("756999e8-aaa2-4428-90b7-3f2786b88774"), 0, 54.726151000000002, 25.2006351, null, 0 },
                    { new Guid("d92aa4a5-d508-44b1-9e97-63efc8b168cd"), 0, 54.723404899999998, 25.198326600000001, null, 0 },
                    { new Guid("58c590cc-18d0-4662-b7f8-b1b1eca453b4"), 0, 54.720406699999998, 25.196854600000002, null, 0 },
                    { new Guid("00fefb15-9402-4e8c-bbd8-f75674b4f671"), 0, 54.709637100000002, 25.2039349, null, 0 },
                    { new Guid("17a93a1b-7c35-4389-b2ec-13503694f707"), 0, 54.749247400000002, 25.326811899999999, null, 0 },
                    { new Guid("97010be5-1845-4b18-bc09-b40719e5009d"), 0, 54.712980100000003, 25.201978199999999, null, 0 },
                    { new Guid("6ef74e36-d64a-4d59-bc72-61e2380a4f81"), 0, 54.724850199999999, 25.192924099999999, null, 0 },
                    { new Guid("70ae5f78-dcf8-428c-b9b0-d66fa6523666"), 0, 54.7124381, 25.1674772, null, 0 },
                    { new Guid("e2d5def8-e120-4669-903c-7bd4d631fb71"), 0, 54.726483700000003, 25.190851299999999, null, 0 },
                    { new Guid("f405efd0-5a66-498b-af0c-93b24e27bc89"), 0, 54.727338400000001, 25.1887525, null, 0 },
                    { new Guid("66b73710-9d5e-4ad8-a239-94e6d1be1cbb"), 0, 54.725736400000002, 25.192423099999999, null, 0 },
                    { new Guid("0d69c3ee-ab7b-43c4-b410-974a7e3e9fa9"), 0, 54.702167000000003, 25.367635, null, 0 },
                    { new Guid("f499c4e1-7ac3-42a6-9727-6bdebc524ee5"), 0, 54.699413100000001, 25.367197999999998, null, 0 },
                    { new Guid("383786a7-eb7d-4475-9506-7ad70b68edd6"), 0, 54.699235600000002, 25.366421200000001, null, 0 },
                    { new Guid("35e7da2d-7e37-43f2-bbf5-cf79ccf09f41"), 0, 54.701504200000002, 25.366756200000001, null, 0 },
                    { new Guid("04d3bc89-4ca5-44f2-bee0-d1be31492bd9"), 0, 54.6951933, 25.380193599999998, null, 0 },
                    { new Guid("fc1984f2-723a-4089-bdba-0924f69f7d59"), 0, 54.695056899999997, 25.379333899999999, null, 0 },
                    { new Guid("4bcc84fd-58eb-4a83-8390-33d51a96282a"), 0, 54.694825199999997, 25.378878, null, 0 },
                    { new Guid("a928a0ab-1439-46de-878b-e8472c2e9f99"), 0, 54.722094400000003, 25.2168539, null, 0 },
                    { new Guid("9e045c6e-3636-49c9-bb28-7f28ea320dd5"), 0, 54.765109500000001, 25.3334142, null, 0 }
                });

            migrationBuilder.InsertData(
                table: "Trails",
                columns: new[] { "Id", "Diff", "GeoJsonData", "Lat", "Length", "Lng", "trailType" },
                values: new object[,]
                {
                    { 2, 3, "sample-data/test1.geojson", 54.685002361652998, 3.0, 25.240305662154999, 1 },
                    { 1, 2, "sample-data/test.geojson", 54.721400000000003, 2.3999999999999999, 25.255500000000001, 0 },
                    { 3, 1, "sample-data/test2.geojson", 54.686813299999997, 1.3999999999999999, 25.290559500000001, 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CapturePoints");

            migrationBuilder.DropTable(
                name: "Trails");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
