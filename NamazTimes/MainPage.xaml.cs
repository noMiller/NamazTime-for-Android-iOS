using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace NamazTimes
{

    public class Timings
    {
        public string Fajr { get; set; }
        public string Sunrise { get; set; }
        public string Dhuhr { get; set; }
        public string Asr { get; set; }
        public string Sunset { get; set; }
        public string Maghrib { get; set; }
        public string Isha { get; set; }
        public string Imsak { get; set; }
        public string Midnight { get; set; }
        public string Firstthird { get; set; }
        public string Lastthird { get; set; }

       
    }


    public partial class MainPage : ContentPage
    {
        string city, url, result, thisDay;// fajr, djuhr, asr, maghrib, isha;
        int day, month, year;
        DateTime date;
        //https://api.aladhan.com/v1/calendarByCity?city=Mecca&country=SA&method=4&month=05&year=2024
        

        
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            date = DateTime.Today;
            string formattedDate = date.ToString("dd.MM.yyyy");
            day = date.Day;
            month = date.Month;
            year = date.Year;
            city = CityEntry.Text.Trim();
            url = $"https://api.aladhan.com/v1/calendarByCity?city={city}&country=RU&method=4&month={month}&year={year}";
            result = await client.GetStringAsync(url);
           

            var JsonResult = JObject.Parse(result);
            thisDay = JsonResult["data"][day].ToString();
            var timetable = JsonConvert.DeserializeObject<PrayerTimetable>(thisDay);
            NamazTimesLabel.Text = "Время намазов на " + formattedDate;
            FajrLabel.Text = "Фаджр: " + timetable.timings.Fajr;
            ZyhrLabel.Text = "Зухр: " + timetable.timings.Dhuhr;
            AsrLabel.Text = "Аср: " + timetable.timings.Asr;
            MagribLabel.Text = "Магриб (Ифтар): " + timetable.timings.Maghrib;
            IshaLabel.Text = "Иша: " + timetable.timings.Isha;



        }

        public class PrayerTimetable
        {
            public Timings timings { get; set; }
        }
    }
}