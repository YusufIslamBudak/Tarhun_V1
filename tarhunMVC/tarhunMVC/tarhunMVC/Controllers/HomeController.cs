using Microsoft.AspNetCore.Mvc;
using tarhunMVC.Models;
using tarhunMVC.Services;

namespace tarhunMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ArduinoDataService _arduinoDataService;
        private readonly ModelPredictionService _modelPredictionService;

        public HomeController(ArduinoDataService arduinoDataService, ModelPredictionService modelPredictionService)
        {
            _arduinoDataService = arduinoDataService;
            _modelPredictionService = modelPredictionService;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Graph()
        {
            return View();
        }


        [HttpGet]
        public JsonResult GetSensorData()
        {
            var sensorData = _arduinoDataService.GetLatestSensorData();

            if (sensorData != null)
            {
                var prediction = _modelPredictionService.Predict(sensorData);
                var (healthStatus, recommendations) = AnalyzeEnvironment.Analyze(sensorData);

                return Json(new
                {
                    success = true,
                    prediction = prediction.PredictedHealth ? "Sağlıklı" : "Sağlıksız",
                    probability = prediction.Probability,
                    analysis = healthStatus,
                    reasons = recommendations,
                    temperature = sensorData.Temperature,
                    humidity = sensorData.Humidity,
                    pressure = sensorData.Pressure,
                    gasResistance = sensorData.GasResistance,
                    smoke = sensorData.Smoke,
                    methane = sensorData.Methane,
                    ammonia = sensorData.Ammonia,
                    co2 = sensorData.Co2
                });
            }

            return Json(new { success = false });
        }
    }
}