// Controllers/CaregiverController.cs

using Microsoft.AspNetCore.Mvc;

public class CaregiverController : Controller
{
    private readonly JsonFileService<Caregiver> _caregiverService;

    public CaregiverController()
    {
        _caregiverService = new JsonFileService<Caregiver>("caregivers.json");
    }

    public IActionResult Index()
    {
        var caregivers = _caregiverService.Read();
        return View(caregivers);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Caregiver caregiver)
    {
        var caregivers = _caregiverService.Read();
        caregiver.Id = caregivers.Count + 1;
        caregivers.Add(caregiver);
        _caregiverService.Write(caregivers);
        return RedirectToAction("Index");
    }
}
