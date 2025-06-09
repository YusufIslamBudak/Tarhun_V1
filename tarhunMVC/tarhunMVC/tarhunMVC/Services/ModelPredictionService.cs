using Microsoft.ML;
using tarhunMVC.Models;

namespace tarhunMVC.Services
{
    public class ModelPredictionService
    {
        private readonly PredictionEngine<SensorData, HealthPrediction> _predictionEngine;

        public ModelPredictionService()
        {
            var mlContext = new MLContext();
            var modelPath = "model.zip"; // Modelin yolu
            var loadedModel = mlContext.Model.Load(modelPath, out _);
            _predictionEngine = mlContext.Model.CreatePredictionEngine<SensorData, HealthPrediction>(loadedModel);
        }

        public HealthPrediction Predict(SensorData sensorData)
        {
            return _predictionEngine.Predict(sensorData);
        }
    }
}