using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using WILWebApp.Entities;
using WILWebApp.Hubs;
using WILWebApp.Models;


namespace WILWebApp.Controllers
{
   
    public class SOSController : Controller
    {
        private readonly DB_Context _context;
        private readonly HttpClient _client;

        public SOSController(DB_Context context, HttpClient client)
        {
            _context = context;
            _client = client;

            if (_client.BaseAddress == null)
            {
                _client.BaseAddress = new Uri("https://localhost:7028/"); // replace with your actual base URL
            }
        }


        [HttpGet]

        public IActionResult GetReportData()
        {
            try
            {
                var reports = _context.StudentReports.ToList();
                if (reports.Count == 0)
                {
                    return NotFound("No Reports were Issued!");
                }

                return Ok(reports);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Id}")]

        public IActionResult GetDetails(int Id)
        {
            try
            {
                var report = _context.StudentReports.Find(Id);
                if (report == null)
                {
                    return NotFound($"No Reports were issued with id {Id}");
                }

                return Ok(report);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult PostData(StudentReport model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState); // Return validation errors
                }

                // Check for uniqueness constraints
                if (_context.StudentReports.Any(r => r.incidentType == model.incidentType))
                {
                    ModelState.AddModelError("incidentType", "Incident Type must be unique.");
                    return BadRequest(ModelState);
                }

                if (_context.StudentReports.Any(r => r.description == model.description))
                {
                    ModelState.AddModelError("description", "Description must be unique.");
                    return BadRequest(ModelState);
                }

                if (_context.StudentReports.Any(r => r.location == model.location))
                {
                    ModelState.AddModelError("location", "Location must be unique.");
                    return BadRequest(ModelState);
                }

                if (_context.StudentReports.Any(r => r.reportedAt == model.reportedAt))
                {
                    ModelState.AddModelError("reportedAt", "Reported At must be unique.");
                    return BadRequest(ModelState);
                }

                if (_context.StudentReports.Any(r => r.status == model.status))
                {
                    ModelState.AddModelError("status", "Status must be unique.");
                    return BadRequest(ModelState);
                }

                // If all checks pass, add the model to the context
                _context.Add(model);
                _context.SaveChanges();
                return Ok("Report has been issued.");
            }
            catch (DbUpdateException ex)
            {
                // Log the exception
                var innerException = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                ModelState.AddModelError("", $"An error occurred while saving changes: {innerException}");
                return View("ReportForm", model); // Return to the ReportForm view with the model
            }
            catch (Exception ex)
            {
                // Log the exception
                ModelState.AddModelError("", "An unexpected error occurred. Please try again.");
                return View("ReportForm", model); // Return to the ReportForm view with the model
            }
        }

        [HttpPut]
        public IActionResult UpdateFormData(StudentReport model)
        {
            try
            {
                if (model == null || model.Id == 0)
                {
                    return BadRequest("Invalid Model.");
                }
                else if (model.Id == 0)
                {
                    return BadRequest($"Invalid Report Model Id {model.Id}.");
                }

                var report = _context.StudentReports.Find(model.Id);
                if (report == null)
                {
                    return NotFound($"No Reports were issued with id {model.Id}");
                }

                report.incidentType = model.incidentType;
                report.description = model.description;
                report.location = model.location;
                report.reportedAt = model.reportedAt;
                report.status = model.status;

                _context.SaveChanges();
                return Ok("An Update has been done to the report.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        public IActionResult ReportForm()
        {
            return View(new StudentReport());
        }

        [HttpPost]
        public async Task<IActionResult> ReportForm(StudentReport model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                // Ensure the endpoint matches the method you want to call
                HttpResponseMessage res = await _client.PostAsync(_client.BaseAddress + "/SOS/PostData", content);

                if (res.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Report successfully Posted.";
                    return RedirectToAction("");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                // Optionally return the model to the view for user correction
                return View(model);
            }

            return View(model); // Return the model in case of failure
        }


        [HttpGet]
        public IActionResult EditReportData(int id)
        {
            try
            {
                StudentReport report = new StudentReport();
                HttpResponseMessage res = _client.GetAsync(_client.BaseAddress + "/SOS/GetDetails" + id).Result;

                if (res.IsSuccessStatusCode)
                {
                    string data = res.Content.ReadAsStringAsync().Result;
                    report = JsonConvert.DeserializeObject<StudentReport>(data);
                }

                return View(report);
            }
            catch (Exception ex)
            {
                return View();

            }
        }

        [HttpPost]
        public IActionResult PostEditData(int Id, string Status)
        {
            // Logic to update the status in the database
            var report = _context.StudentReports.Find(Id);
            if (report != null)
            {
                report.status = Status;
                _context.SaveChanges(); // Save changes to the database
            }

            // Redirect back to the AdminView after updating
            return RedirectToAction("AdminView");
        }

        // Display Admin View of All Reports
        public IActionResult AdminView()
        {
            var reports = _context.StudentReports.ToList(); // Fetch all reports from the database
            return View(reports); // Pass the reports to the view
        }



    }

}
