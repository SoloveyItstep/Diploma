using Currency.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Currency
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ICurrencyUSD response = new CurrencyUSD();
            Data data = response.GetLastCurrency(DateTime.Now);
            Console.WriteLine("date - {0}, currency - {1}",
                data.date.ToShortDateString(),data.curency);
            Console.ReadKey();
        }
    }
}
