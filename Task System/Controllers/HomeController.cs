using Microsoft.AspNetCore.Mvc;
using Project_Task.Models;
using System.Diagnostics;
using Task_System.Data_Acsess;
using Task_System.Models;

namespace Task_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ApplicationDBContext _db = new();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult BookAppointment(string search)
        {
            var doctorsQuery = _db.Doctors.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                doctorsQuery = doctorsQuery.Where(d => d.Name.Contains(search));
            }

            var doctors = doctorsQuery.OrderBy(d => d.Name).ToList();
            return View(doctors);
        }


        [HttpGet]
        public ViewResult CompleteAppointment()
        {
            var booked = _db.Patients.OrderBy(p => p.AppointmentDate).ThenBy(p => p.AppointmentTime).ToList();
            return View(booked);
        }

        [HttpPost]

        public IActionResult CompleteAppointment(Patient patient)
        {
            // 1  تأكدان المدخلات الأساسية صحيحة
            if (patient == null || patient.AppointmentDate == default || patient.AppointmentTime == null)
            {
                ViewBag.ErrorMessage = "Please Enter the name and date and time correctly";
                return View(_db.Patients.OrderBy(p => p.AppointmentDate).ToList());
            }


            // 2   التاريخ والوقت 
            var appointmentDateTime = patient.AppointmentDate.Date + patient.AppointmentTime.Value;


            // 3  منع الجمعة والسبت
            if (appointmentDateTime.DayOfWeek == DayOfWeek.Friday || appointmentDateTime.DayOfWeek == DayOfWeek.Saturday)
            {
                ViewBag.ErrorMessage = "Reservations are not available on Friday or Saturday.";
                return View(_db.Patients.OrderBy(p => p.AppointmentDate).ToList());
            }


            // 4)  الوقت: من 8:00 إلى 5:00

            var start = new TimeSpan(8, 0, 0);
            var end = new TimeSpan(17, 0, 0);
            if (patient.AppointmentTime.Value < start || patient.AppointmentTime.Value >= end)
            {
                ViewBag.ErrorMessage = "Reservations can only be made between 8am and 5pm.";
                return View(_db.Patients.OrderBy(p => p.AppointmentDate).ToList());
            }

            // 5  نحجز كل نصف ساعة 
            if (patient.AppointmentTime.Value.Minutes % 30 != 0)
            {
                ViewBag.ErrorMessage = "Booking every half hour only.";
                return View(_db.Patients.OrderBy(p => p.AppointmentDate).ToList());
            }


            bool alreadyBooked = _db.Patients.Any(p =>
                p.AppointmentDate.Date == patient.AppointmentDate.Date &&
                p.AppointmentTime == patient.AppointmentTime);


            if (alreadyBooked)
            {
                ViewBag.ErrorMessage = "This appointment is already booked, please choose another time.";
                return View(_db.Patients.OrderBy(p => p.AppointmentDate).ToList());
            }


            _db.Patients.Add(patient);
            _db.SaveChanges();

            ViewBag.SuccessMessage = $"The appointment was booked on {patient.AppointmentDate:yyyy-MM-dd} at {patient.AppointmentTime.Value:hh\\:mm}.";

            return RedirectToAction(nameof(BookAppointment));
              //return View(_db.Patients.OrderBy(p => p.AppointmentDate).ToList());
            

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}








