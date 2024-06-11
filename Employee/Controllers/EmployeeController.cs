
using Microsoft.AspNetCore.Mvc;
using Employee.Data;

namespace Employee.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employee
        public IActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Employee.Models.Employee());
            else

                return View(_context.Employees.Find(id));
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("Id,Description,FirstName,LastName,Title,Age,CreatedDate,Designation,EmpCode")] Employee.Models.Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (employee.Id == 0)
                {
                    _context.Add(employee);
                    TempData["SuccessMessage"] = "Employee Added successfully!";
                }
                else
                {
                    _context.Update(employee);
                    TempData["SuccessMessage"] = "Employee Updated successfully!";
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("AddOrEdit", new { id = 0 }); 
            }
            return View(employee);
        }


        // GET: Employees/Delete/5
        [HttpGet]
        [Route("Employee/Delete/{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _context.Employees
                .FirstOrDefault(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [Route("Employee/Delete/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var employee = _context.Employees.Find(id);
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Employee record deleted successfully!";
            return RedirectToAction(nameof(AddOrEdit));
        }
        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
