using Currency.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Currency
{
    public interface ICurrencyUSD
    {
        CultureInfo culture { get; }
        String currentDate { get; }
        String CurrentDateUrl { get; }
        String Url { get; }
        String GetCurrentDateCurrency();
        Data GetLastCurrency(DateTime date);
        String GetRequest(String url);
        Data data { get; set; }
    }
    public class CurrencyUSD : ICurrencyUSD
    {
        public CultureInfo culture {
            get {
                return CultureInfo.CurrentCulture;
            } }
        public String currentDate
        {
            get
            {
                return DateTime.Now.ToShortDateString();
            }
        }
        public String CurrentDateUrl
        {
            get
            {
                return String.Format("https://api.privatbank.ua/p24api/exchange_rates?json&date=" + currentDate);
            }
        }
        public String Url
        {
            get
            {
                return String.Format("https://api.privatbank.ua/p24api/exchange_rates?json&date=");
            }
        }
        public Data data { get; set; }


        public String GetCurrentDateCurrency()
        {
            String request = GetRequest(CurrentDateUrl);
            UserServices services = JsonConvert.DeserializeObject<UserServices>(
                request, new IsoDateTimeConverter { DateTimeFormat = "dd.MM.yyyy" });
            if (services.exchangeRate.Count() == 0)
            {
                return null;
            }
            else {
                String uah = services.exchangeRate.Where(ex => ex.currency == "USD").FirstOrDefault().saleRate.ToString("C");
                return uah;
            }
        }

        public Data GetLastCurrency(DateTime date)
        {
            Data data = new Data();
            String request = GetRequest(Url+date.ToShortDateString());
            UserServices services = JsonConvert.DeserializeObject<UserServices>(
                request, new IsoDateTimeConverter { DateTimeFormat = "dd.MM.yyyy" });
            if (services.exchangeRate.Count() == 0)
            {
                data = GetLastCurrency(date.AddDays(-1));
            }
            else {
                Decimal uah = services.exchangeRate.Where(ex => ex.currency == "USD").FirstOrDefault().saleRate;
                if (uah < 1)
                {
                    data = GetLastCurrency(date.AddDays(-1));
                }
                else {
                    data.curency = uah.ToString();
                    data.date = date;
                }
            }
            return data;
        } 

        public String GetRequest(String url)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("ru-RU");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            String answer = String.Empty;
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    answer = reader.ReadToEnd();
                }
            }
            return answer;
        }
    }
}
