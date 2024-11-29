using Microsoft.AspNetCore.Mvc;

public class AppointmentController : Controller
{
    private readonly JsonFileService<Appointment> _appointmentService;

    public AppointmentController()
    {
        _appointmentService = new JsonFileService<Appointment>("appointments.json");
    }

    public IActionResult Index()
    {
        var appointments = _appointmentService.Read();
        return View(appointments);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Appointment appointment)
    {
        var appointments = _appointmentService.Read();
        appointment.Id = appointments.Count + 1;
        appointments.Add(appointment);
        _appointmentService.Write(appointments);
        return RedirectToAction("Index");
    }
}
