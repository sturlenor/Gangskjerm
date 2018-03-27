using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Xml;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GangSkjerm
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class YrPage : Page
    {
        WeatherData data;
        public YrPage()
        {
            this.InitializeComponent();
            data = GetWeatherData();
            HeaderText.Text = "Været i Bergen" + " " + FindDate(data.WeatherItems[0].TimeFrom);

            Period1Time.Text = FindHour(data.WeatherItems[0].TimeFrom) + " - " + FindHour(data.WeatherItems[0].TimeTo);
            Period1Symbol.Source = new BitmapImage(new Uri(this.BaseUri, "/Assets/" + data.WeatherItems[0].SymbolVar + ".png"));
            Period1Temp.Text = data.WeatherItems[0].Temp;
            if (data.WeatherItems[0].Temp.Substring(0, 1).Equals("-"))
                Period1Temp.Foreground = new SolidColorBrush(Colors.LightBlue);
            else
                Period1Temp.Foreground = new SolidColorBrush(Colors.Red);
            Period1Wind.Text = data.WeatherItems[0].WindSpeedName;
            Period1Rain.Text = data.WeatherItems[0].PrecipitationValue + "mm";

            Period2Time.Text = FindHour(data.WeatherItems[1].TimeFrom) + " - " + FindHour(data.WeatherItems[1].TimeTo);
            Period2Symbol.Source = new BitmapImage(new Uri(this.BaseUri, "/Assets/" + data.WeatherItems[1].SymbolVar + ".png"));
            Period2Temp.Text = data.WeatherItems[1].Temp;
            if (data.WeatherItems[1].Temp.Substring(0, 1).Equals("-"))
                Period2Temp.Foreground = new SolidColorBrush(Colors.LightBlue);
            else
                Period2Temp.Foreground = new SolidColorBrush(Colors.Red);
            Period2Wind.Text = data.WeatherItems[1].WindSpeedName;
            Period2Rain.Text = data.WeatherItems[1].PrecipitationValue + "mm";

            Period3Time.Text = FindHour(data.WeatherItems[2].TimeFrom) + " - " + FindHour(data.WeatherItems[2].TimeTo);
            Period3Symbol.Source = new BitmapImage(new Uri(this.BaseUri, "/Assets/" + data.WeatherItems[2].SymbolVar + ".png"));
            Period3Temp.Text = data.WeatherItems[2].Temp;
            if (data.WeatherItems[2].Temp.Substring(0, 1).Equals("-"))
                Period3Temp.Foreground = new SolidColorBrush(Colors.LightBlue);
            else
                Period3Temp.Foreground = new SolidColorBrush(Colors.Red);
            Period3Wind.Text = data.WeatherItems[2].WindSpeedName;
            Period3Rain.Text = data.WeatherItems[2].PrecipitationValue + "mm";

            Period4Time.Text = FindHour(data.WeatherItems[3].TimeFrom) + " - " + FindHour(data.WeatherItems[3].TimeTo);
            Period4Symbol.Source = new BitmapImage(new Uri(this.BaseUri, "/Assets/" + data.WeatherItems[3].SymbolVar + ".png"));
            Period4Temp.Text = data.WeatherItems[3].Temp;
            if (data.WeatherItems[3].Temp.Substring(0, 1).Equals("-"))
                Period4Temp.Foreground = new SolidColorBrush(Colors.LightBlue);
            else
                Period4Temp.Foreground = new SolidColorBrush(Colors.Red);
            Period4Wind.Text = data.WeatherItems[3].WindSpeedName;
            Period4Rain.Text = data.WeatherItems[3].PrecipitationValue + "mm";


        }

        private String FindDate(String s)
        {
            String year = s.Substring(0, 4);
            String month = s.Substring(5, 2);
            String day = s.Substring(8, 2);
            return day + "." + month + "." + year;
        }
        private String FindHour(String s)
        {
            return s.Substring(11, 2);
        }

        string url = "https://www.yr.no/sted/Norge/Hordaland/Bergen/Bergen/forecast.xml";

        public string GetWeatherXml()
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            return client.Execute(request).Content;
        }

        public WeatherData GetWeatherData()
        {
            WeatherData Data = new WeatherData() { WeatherItems = new List<WeatherItem>() };

            StringBuilder sb = new StringBuilder("");

            System.Xml.XmlDocument doc = new XmlDocument();

            doc.LoadXml(GetWeatherXml());

            System.Xml.XmlNode sun = doc.SelectSingleNode("/weatherdata/sun");
            System.Xml.XmlNode location = doc.SelectSingleNode("/weatherdata/location");
            System.Xml.XmlNodeList nodelist = doc.SelectNodes("/weatherdata/forecast/tabular/time");

            Data.SunRise = sun.Attributes["rise"].Value;
            Data.SunSet = sun.Attributes["set"].Value;
            Data.IsFilled = true;

            Data.LocationName = location["name"].Value;
            Data.LocationType = location["type"].Value;
            Data.LocationCountry = location["country"].Value;

            Data.LocationLatitude = location["location"].Attributes["latitude"].Value;
            Data.LocationLongitude = location["location"].Attributes["longitude"].Value;

            for (int i = 0; i < 7; i++)
            {
                WeatherItem item = new WeatherItem();

                item.Period = nodelist[i].Attributes["period"].Value;
                item.TimeFrom = DateTime.Parse(nodelist[i].Attributes["from"].Value).ToString("yyyy-MM-dd HH:mm");
                item.TimeTo = DateTime.Parse(nodelist[i].Attributes["to"].Value).ToString("yyyy-MM-dd HH:mm");
                item.SymbolNumber = nodelist[i]["symbol"].Attributes["number"].Value.PadLeft(2, '0');
                item.SymbolName = nodelist[i]["symbol"].Attributes["name"].Value;
                item.SymbolVar = nodelist[i]["symbol"].Attributes["var"].Value;
                item.Temp = nodelist[i]["temperature"].Attributes["value"].Value + "°C";
                item.PrecipitationValue = nodelist[i]["precipitation"].Attributes["value"].Value;
                item.WindSpeedMPS = nodelist[i]["windSpeed"].Attributes["mps"].Value;
                item.WindSpeedName = nodelist[i]["windSpeed"].Attributes["name"].Value;
                item.WindDirectionCode = nodelist[i]["windDirection"].Attributes["code"].Value;
                item.WindDirectionDeg = nodelist[i]["windDirection"].Attributes["deg"].Value;
                item.WindDirectionName = nodelist[i]["windDirection"].Attributes["name"].Value;

                item.IsFilled = true;

                Data.WeatherItems.Add(item);
            }

            return Data;
        }


        public class WeatherItem
        {
            public string TimeFrom { get; set; }
            public string TimeTo { get; set; }

            public string Period { get; set; }

            public string SymbolNumber { get; set; }
            public string SymbolName { get; set; }
            public string SymbolVar { get; set; }

            public string PrecipitationValue { get; set; }

            public string WindDirectionDeg { get; set; }
            public string WindDirectionCode { get; set; }
            public string WindDirectionName { get; set; }
            public string WindSpeedMPS { get; set; }
            public string WindSpeedName { get; set; }

            public string Temp { get; set; }
            public string Pressure { get; set; }

            public bool IsFilled { get; set; }

        }

        public class WeatherData
        {
            public string LocationName { get; set; }
            public string LocationType { get; set; }
            public string LocationCountry { get; set; }
            public string LocationLatitude { get; set; }
            public string LocationLongitude { get; set; }

            public string SunRise { get; set; }
            public string SunSet { get; set; }

            public List<WeatherItem> WeatherItems { get; set; }

            public bool IsFilled { get; set; }

            public WeatherData()
            {
                WeatherItems = new List<WeatherItem>();
            }
        }
    }
}
