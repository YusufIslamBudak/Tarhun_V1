using Microsoft.ML.Data;

namespace tarhunMVC.Models
{
    public class SensorData
    {
        public float Temperature { get; set; }       // 1
        public float Humidity { get; set; }          // 2
        public float Pressure { get; set; }          // 3
        public float GasResistance { get; set; }     // 4
        public float Smoke { get; set; }               // 5 - MQ-2
        public float Methane { get; set; }             // 6 - MQ-4
        public float Ammonia { get; set; }             // 7 - MQ-137
        public float Co2 { get; set; }                 // 8 - MH-Z14
    }

}