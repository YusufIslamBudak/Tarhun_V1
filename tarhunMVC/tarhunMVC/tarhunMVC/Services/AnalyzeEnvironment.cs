using tarhunMVC.Models;
using System.Collections.Generic;

namespace tarhunMVC.Services
{
    public class AnalyzeEnvironment
    {
        public static (string, List<string>) Analyze(SensorData sensorData)
        {
            List<string> recommendations = new List<string>();
            string healthStatus = "Sağlıklı";

            // Analizler (Örnek: Nem, gaz seviyesi vs.)
            if (sensorData.Humidity > 70)
            {
                healthStatus = "Sağlıksız";
                recommendations.Add("Nem oranı yüksek. Havadar yapın.");
            }

            if (sensorData.Co2 > 1000)
            {
                healthStatus = "Sağlıksız";
                recommendations.Add("CO2 seviyesi yüksek. Havalandırmayı açın.");
            }

            if (sensorData.GasResistance < 500)
            {
                healthStatus = "Sağlıksız";
                recommendations.Add("Gaz seviyesi anormal. Gaz dedektörünü kontrol edin.");
            }

            if (sensorData.Pressure < 980 || sensorData.Pressure > 1020)
            {
                healthStatus = "Sağlıksız";
                recommendations.Add("Basınç seviyesi anormal. Çevreyi gözden geçirin.");
            }

            return (healthStatus, recommendations);
        }
    }
}