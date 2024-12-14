using DynamicChartApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Dynamic;

namespace DynamicChartApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(DBConnectionInfo connectionInfo)
        {
            try
            {
                var connectionString = $"Server={connectionInfo.Server};Database={connectionInfo.Database};User Id={connectionInfo.Username};Password={connectionInfo.Password};TrustServerCertificate = True;";

                using var connection = new SqlConnection(connectionString);
                connection.Open();

                var command = new SqlCommand($"SELECT * FROM {connectionInfo.Table}", connection);
                var reader = command.ExecuteReader();

                var data = new List<dynamic>();
                while (reader.Read())
                {
                    var row = new ExpandoObject() as IDictionary<string, Object>;
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row.Add(reader.GetName(i), reader[i]);
                    }
                    data.Add(row);
                }

                ViewBag.TableData = data;
                return View("Generate");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Baðlantý hatasý: {ex.Message}";
                return View("Index");
            }
        }

        [HttpPost]
        public IActionResult Generate(List<string> SelectedRows, string ChartType)
        {
            if (SelectedRows == null || SelectedRows.Count == 0)
            {
                ViewBag.ErrorMessage = "Lütfen en az bir veri seçiniz.";
                return RedirectToAction("Index");
            }

            var selectedData = new List<Dictionary<string, object>>();
            foreach (var row in SelectedRows)
            {
                var deserializedRow = JsonConvert.DeserializeObject<Dictionary<string, object>>(row);
                selectedData.Add(deserializedRow);
            }

            TempData["ChartData"] = JsonConvert.SerializeObject(selectedData);
            TempData["ChartType"] = ChartType;

            return RedirectToAction("Chart");
        }

        [HttpPost]
        public IActionResult Chart(string chartType, List<string> selectedRows)
        {
            var selectedData = selectedRows.Select(row => Newtonsoft.Json.JsonConvert.DeserializeObject<IDictionary<string, object>>(row)).ToList();

            var data = selectedData.Select(r => Convert.ToInt32(r["Value"])).ToList();  
            var labels = selectedData.Select(r => r["Key"].ToString()).ToList();  

            ViewBag.ChartType = chartType;
            ViewBag.Data = data;
            ViewBag.Labels = labels;

            return View("Chart");  

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
