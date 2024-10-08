using Microsoft.AspNetCore.Mvc;
using firstProjectMVC.Data;
using ContactDoctor.Models;
using Hospital.Models;
using Microsoft.EntityFrameworkCore;
namespace Hospital.Controllers
{
    public class AppointmentController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public IActionResult Index()
        {
            var doctors = context.Doctors.ToList();
            return View(model: doctors);
        }

        public IActionResult TakeAppointment(string drName, int drId)
        {
            var doctor = new { drName, drId };
            return View(model: doctor);
        }
        [HttpPost]
        public IActionResult Insert(Appointment appoint)
        {
            context.Appointments.Add(appoint);
            context.SaveChanges();
            return RedirectToAction("ShowAll");
        }
        [HttpGet]
        public IActionResult ShowAll()
        {
            var apointments = context.Appointments.Include(e => e.doctor).Select(e => new { e.Id, e.PatientName, e.Date, e.Time, DrName = e.doctor.Name }).ToList();
            return View(model: apointments);
        }
        [HttpGet]
        public IActionResult Edit(int appointmentId)
        {
            var appointment = context.Appointments.Where(e => e.Id == appointmentId)
                .Select(e=>new {e.Id , e.DoctorId, e.Date , e.Time , e.PatientName}).FirstOrDefault();
            return View(model: appointment);
        }
        [HttpPost]
        public IActionResult Edit(Appointment appointment)
        {
            context.Update(appointment);
            context.SaveChanges();
            return RedirectToAction("ShowAll");
        }
        
        public IActionResult Delete(int appointmentId) { 
        Appointment appointment = new Appointment() { Id = appointmentId };
            context.Appointments.Remove(appointment);
            context.SaveChanges();
            return RedirectToAction("ShowAll");
        }
    }
}
