using Microsoft.ML.Data;

namespace tarhunMVC.Models
{
    public class HealthPrediction
    {
        [ColumnName("PredictedLabel")]
        public bool PredictedHealth { get; set; }

        public float Probability { get; set; }
        public float Score { get; set; }
    }
}