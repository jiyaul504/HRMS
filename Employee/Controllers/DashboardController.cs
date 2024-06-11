using Employee.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Employee.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
         
            var orderStatuses = _context.Qry_WIP_Live_OrderStatus.ToList();
            
            ViewBag.Qry_WIP_Live_OrderStatus =orderStatuses;
           
            return View();
        }
     


    }
}
