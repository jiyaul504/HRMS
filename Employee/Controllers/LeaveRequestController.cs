using Employee.Data;
using Employee.Models;
using Employee.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee.Controllers
{
    public class LeaveRequestController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailService _emailService;

        public LeaveRequestController(ApplicationDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,StartDate,EndDate,Reason")] LeaveRequest leaveRequest)
        {
            if (ModelState.IsValid)
            {
                leaveRequest.Status = "Pending";
                _context.Add(leaveRequest);
                await _context.SaveChangesAsync();

                var approverEmail = "abc@example.com";
                var subject = "New Leave Request";
                var message = $"A new leave request has been created by {leaveRequest.EmployeeId}. Please review it.";
                var attachmentPaths = new List<string>();
                await _emailService.SendEmailAsync(approverEmail, subject, message, attachmentPaths);


                return RedirectToAction(nameof(Index));
            }
            return View(leaveRequest);
        }


        public async Task<IActionResult> Details(int id)
        {
            var leaveRequest = await _context.LeaveRequests.FindAsync(id);

            if (leaveRequest == null)
            {
                return NotFound();
            }

            return View(leaveRequest);
        }



        public async Task<IActionResult> Edit(int id)
        {
            var leaveRequest = await _context.LeaveRequests.FindAsync(id);

            if (leaveRequest == null)
            {
                return NotFound();
            }

            return View(leaveRequest);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,StartDate,EndDate,Reason,Status")] LeaveRequest leaveRequest)
        {
            if (id != leaveRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leaveRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveRequestExists(leaveRequest.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(leaveRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, [Bind("Id,EmployeeId,StartDate,EndDate,Reason,Status")] LeaveRequest leaveRequest)
        {
            if (id != leaveRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leaveRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveRequestExists(leaveRequest.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(leaveRequest);
        }

        private bool LeaveRequestExists(int id)
        {
            return _context.LeaveRequests.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var leaveRequest = await _context.LeaveRequests.FindAsync(id);
            if (leaveRequest == null)
            {
                return NotFound();
            }

            return View(leaveRequest);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leaveRequest = await _context.LeaveRequests.FindAsync(id);
            _context.LeaveRequests.Remove(leaveRequest);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Approve(int id)
        {
            var leaveRequest = await _context.LeaveRequests.FindAsync(id);
            if (leaveRequest == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(leaveRequest.EmployeeId);
            if (employee == null)
            {
                return NotFound();
            }

            leaveRequest.Status = "Approved";
            _context.LeaveRequests.Update(leaveRequest);
            await _context.SaveChangesAsync();

            var employeeEmail = employee.Email; // Fetch the employee's email address from your database
            var subject = "Leave Request Approved";
            var message = $"Dear {employee.FullName}, your {leaveRequest.Type} leave request from {leaveRequest.StartDate} to {leaveRequest.EndDate} for the reason '{leaveRequest.Reason}' has been approved by {employee.Manager}.";
            var attachmentPaths = new List<string>();
            await _emailService.SendEmailAsync(employeeEmail, subject, message, attachmentPaths);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Reject(int id, string rejectionReason)
        {
            var leaveRequest = await _context.LeaveRequests.FindAsync(id);
            if (leaveRequest == null)
            {
                return NotFound();
            }

            leaveRequest.Status = "Rejected";
            _context.LeaveRequests.Update(leaveRequest);
            await _context.SaveChangesAsync();

            try
            {
                var employee = await _context.Employees.FindAsync(leaveRequest.EmployeeId);
                if (employee == null)
                {
                    return NotFound();
                }

                var employeeEmail = employee.Email;
                var subject = "Leave Request Rejected";
                var message = $"Dear {employee.FullName}, your leave request from {leaveRequest.StartDate} to {leaveRequest.EndDate} has been rejected by {employee.Manager}. Reason: {rejectionReason}";
                var attachmentPaths = new List<string>();
                await _emailService.SendEmailAsync(employeeEmail, subject, message, attachmentPaths);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred when fetching the employee's email address: {ex.Message}");
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
