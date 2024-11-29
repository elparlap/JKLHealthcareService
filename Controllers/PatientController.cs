
using Microsoft.AspNetCore.Mvc;
public class PatientController : Controller
{
    private readonly JsonFileService<Patient> _patientService;

    public PatientController()
    {
        _patientService = new JsonFileService<Patient>("patients.json");
    }

    public IActionResult Index()
    {
        var patients = _patientService.Read();
        return View(patients);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Patient patient)
    {
        var patients = _patientService.Read();
        patient.Id = patients.Count + 1;
        patients.Add(patient);
        _patientService.Write(patients);
        return RedirectToAction("Index");
    }
}
