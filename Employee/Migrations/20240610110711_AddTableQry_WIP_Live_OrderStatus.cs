using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employee.Migrations
{
    /// <inheritdoc />
    public partial class AddTableQry_WIP_Live_OrderStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Qry_WIP_Live_OrderStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cust_PO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ur_MO_no = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Partno = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanQty = table.Column<int>(type: "int", nullable: false),
                    StationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkOrderNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TF_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Wait_For_MoveInQty = table.Column<int>(type: "int", nullable: false),
                    MovedIn_Qty = table.Column<int>(type: "int", nullable: false),
                    ProdQty = table.Column<int>(type: "int", nullable: false),
                    InspQty = table.Column<int>(type: "int", nullable: false),
                    RejQty = table.Column<int>(type: "int", nullable: false),
                    MovedOut_Qty = table.Column<int>(type: "int", nullable: false),
                    Bal_Qty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qry_WIP_Live_OrderStatus", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Qry_WIP_Live_OrderStatus");
        }
    }
}
