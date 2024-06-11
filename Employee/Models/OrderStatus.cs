using System.ComponentModel.DataAnnotations;

namespace Employee.Models
{
    public class OrderStatus
    {
        [Key]
        public int Id { get; set; }
        public string Cust_PO { get; set; }
        public string Ur_MO_no { get; set; }
        public string Partno { get; set; }
        public int PlanQty { get; set; }
        public string StationName { get; set; }
        public string WorkOrderNo { get; set; }
        public string TF_Name { get; set; }
        public int Wait_For_MoveInQty { get; set; }
        public int MovedIn_Qty { get; set; }
        public int ProdQty { get; set; }
        public int InspQty { get; set; }
        public int RejQty { get; set; }
        public int MovedOut_Qty { get; set; }
        public int Bal_Qty { get; set; }
    }

}
