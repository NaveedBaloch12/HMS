using DinkToPdf;
using DinkToPdf.Contracts;
using HMS.Data;
using HMS.Entites;
using HMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Controllers
{
    public class BillingController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IConverter _converter;

        public BillingController(AppDbContext context, IConverter converter)
        {
            _context = context;
            _converter = converter;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GenerateBill(int patientId)
        {
            var patient = _context.Patients.FirstOrDefault(p => p.Id == patientId);
            if (patient == null) return NotFound("Patient Not Found");

            // Calculate MedicineCost and TestCost
            var medicines = _context.DispensedMedicines.Where(m => m.PatientId == patientId);
            decimal medicineCost = medicines.Sum(m => m.TotalPrice);
            var testCost = _context.LabResults.Where(t => t.PatientId == patientId).Sum(t => t.Cost);

            var model = new BillingViewModel
            {
                PatientId = patient.Id,
                PatientName = patient.Name,
                DoctorFee = Globals.DoctorFee,
                MedicineCost = medicineCost,
                TestCost = testCost
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult SaveBill(BillingViewModel model)
        {
            var bill = new Bill
            {
                PatientId = model.PatientId,
                DoctorFee = model.DoctorFee,
                MedicineCost = model.MedicineCost,
                TestCost = model.TestCost,
                Date = DateTime.Now,
                TotalAmount = model.TotalAmount,
            };

            string htmlContent = $@"
<html>
    <head>
        <style>
            body {{ font-family: Arial, sans-serif; line-height: 1.6; }}
            .header {{ text-align: center; margin-bottom: 20px; }}
            .details {{ width: 100%; margin: 20px auto; border-collapse: collapse; }}
            .details td {{ padding: 10px 5px; }}
            .details .label {{ text-align: left; font-weight: bold; }}
            .details .value {{ text-align: right; }}
            .total {{ font-weight: bold; font-size: 1.2em; }}
            hr {{ border: 0; border-top: 1px solid #ccc; margin: 10px 0; }}
        </style>
    </head>
    <body>
        <div class='header'>
            <h1>Hospital Bill</h1>
            <p>HMS System</p>
        </div>
        <hr/>
        <table class='details'>
            <tr>
                <td class='label'>Patient Name:</td>
                <td class='value'>{model.PatientName}</td>
            </tr>
            <tr>
                <td class='label'>Doctor Fee:</td>
                <td class='value'>{model.DoctorFee}/-</td>
            </tr>
            <tr>
                <td class='label'>Medicine Cost:</td>
                <td class='value'>{model.MedicineCost}/-</td>
            </tr>
            <tr>
                <td class='label'>Test Cost:</td>
                <td class='value'>{model.TestCost}/-</td>
            </tr>
            <tr>
                <td colspan='2'><hr/></td>
            </tr>
            <tr class='total'>
                <td class='label'>Total Amount:</td>
                <td class='value'>{model.TotalAmount}/-</td>
            </tr>
        </table>
            <hr/>
        <div class='footer'>
            <p style=""text-align: center; font-size: 0.9em; color: #555;"">
                Thank you for choosing our hospital. For any queries, please contact us at +92 456 7890012 or email us at hms_system@gmail.com.
                <br/>
                Visit us again! Stay healthy.
            </p>
        </div>
    </body>
</html>";





            var pdf = new HtmlToPdfDocument
            {
                GlobalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4
                },
                Objects = {
                new ObjectSettings
                    {
                        HtmlContent = htmlContent,
                        WebSettings = { DefaultEncoding = "utf-8" }
                    }
                }
            };

            // Generate the PDF file
            var pdfFile = _converter.Convert(pdf);

            // Save the PDF to the user's desktop
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktopPath, $"{model.PatientName}_Bill.pdf");
            System.IO.File.WriteAllBytes(filePath, pdfFile);

            var Patient = _context.Patients.Find(model.PatientId);
            Patient.ClearedBills = true;

            _context.Patients.Update(Patient);
            _context.Bills.Add(bill);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");

        }

    }
}
