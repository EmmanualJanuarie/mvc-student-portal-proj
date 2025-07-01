using Microsoft.AspNetCore.Mvc;

namespace StudentPortal.Controllers
{
    public class ChatController : Controller
    {
        // Display Student Chat Page
        public IActionResult StudentChat()
        {
            return View();
        }

        // Display Admin Chat Page
        public IActionResult AdminChat()
        {
            return View();
        }

        // Simulate Sending Message (Handled via SignalR in project)
        [HttpPost]
        public IActionResult SendMessage(string message)
        {
            TempData["Message"] = $"You sent: {message}";
            return RedirectToAction("StudentChat"); // Refresh chat page after message
        }
    }
}
