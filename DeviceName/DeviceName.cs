using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace DeviceName
{
    class DeviceName
    {
        public string GetMarketName(string deviceModel)
        {
            var csvFile = File.ReadAllLines(Environment.CurrentDirectory + @"\supported_devices.csv");
            string realName = SearchForDevice(csvFile, deviceModel);
            return realName;
        }

        private string OnlineCSV(string deviceModel)
        {
            WebClient s = new WebClient();
            var data = s.DownloadString("http://storage.googleapis.com/play_public/supported_devices.csv");
            string[] stringSeparators = new string[] { "\r\n" };
            var lineData = data.Split(stringSeparators, StringSplitOptions.None);
            string realName = SearchForDevice(lineData, deviceModel);

            return realName;

        }

        private string SearchForDevice(string[] data, string deviceModel)
        {
            string realName = String.Empty;
            foreach (var line in data)
            {
                if (line.Contains(deviceModel))
                {
                    var allDevices = line.Split(',');
                    realName = String.Format("{0} {1}", allDevices[0], allDevices[1]);
                }
            }

            if (realName.Equals(String.Empty))
            {
                realName = "No device found";
            }

            return realName;
        }
    }
}
