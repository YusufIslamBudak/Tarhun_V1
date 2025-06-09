using System.IO.Ports;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using tarhunMVC.Models;

namespace tarhunMVC.Services
{
    public class ArduinoDataService : BackgroundService
    {
        private readonly SerialPort _serialPort;
        private SensorData _latestSensorData;

        public ArduinoDataService()
        {
            _serialPort = new SerialPort("/dev/cu.usbserial-120", 115200); // Mac için doğru portu kullandığına emin ol
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _serialPort.Open();
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var data = _serialPort.ReadLine();
                    Console.WriteLine("📥 Arduino'dan gelen veri: " + data);

                    var values = data.Split(',');

                    if (values.Length == 8)
                    {
                        var sensorData = new SensorData
                        {
                            Temperature = float.Parse(values[0], CultureInfo.InvariantCulture),
                            Humidity = float.Parse(values[1], CultureInfo.InvariantCulture),
                            Pressure = float.Parse(values[2], CultureInfo.InvariantCulture),
                            GasResistance = float.Parse(values[3], CultureInfo.InvariantCulture),
                            Smoke = float.Parse(values[4], CultureInfo.InvariantCulture),
                            Methane = float.Parse(values[5], CultureInfo.InvariantCulture),
                            Ammonia = float.Parse(values[6], CultureInfo.InvariantCulture),
                            Co2 = float.Parse(values[7], CultureInfo.InvariantCulture)
                        };

                        _latestSensorData = sensorData;
                        Console.WriteLine("✅ SensorData güncellendi.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("❌ Hata: " + ex.Message);
                }

                await Task.Delay(1000);
            }
        }

        public SensorData GetLatestSensorData()
        {
            return _latestSensorData;
        }
    }
}
