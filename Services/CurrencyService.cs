using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Xml;

namespace Sklep_MVC_Projekt.Services
{
    public class CurrencyService
    {

        public decimal ReindexPrices(string countryCode)
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestHeaders requestHeaders = httpClient.DefaultRequestHeaders;
            requestHeaders.Add("Accept", "application/xml");

            Task<HttpResponseMessage> httpResponse = httpClient.GetAsync("http://api.nbp.pl/api/exchangerates/tables/A");
            HttpResponseMessage httpResponseMessage = httpResponse.Result;
            HttpContent content = httpResponseMessage.Content;
            Task<string> responseData = content.ReadAsStringAsync();

            string data = responseData.Result;

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);

            XmlNode item = doc.SelectSingleNode("ArrayOfExchangeRatesTable").SelectSingleNode("ExchangeRatesTable").SelectSingleNode("Rates");


            Dictionary<string, string> list = new Dictionary<string, string>();

            foreach (XmlNode itemNode in item.ChildNodes)
            {
                string currency = itemNode.SelectSingleNode("Code").InnerText;
                string price = itemNode.SelectSingleNode("Mid").InnerText;
                list.Add(currency, price);
            }

            list.ToList().ForEach(itemNode => { Console.WriteLine($"{itemNode.Key} - {itemNode.Value}"); });

            if(list.TryGetValue(countryCode, out string priceMultiplayer))
            {
                return decimal.Parse(priceMultiplayer.Replace('.',','));
            }
            else { return decimal.One; }
        }

		public List<string> GetCurrency()
		{
			HttpClient httpClient = new HttpClient();
			HttpRequestHeaders requestHeaders = httpClient.DefaultRequestHeaders;
			requestHeaders.Add("Accept", "application/xml");

			Task<HttpResponseMessage> httpResponse = httpClient.GetAsync("http://api.nbp.pl/api/exchangerates/tables/A");
			HttpResponseMessage httpResponseMessage = httpResponse.Result;
			HttpContent content = httpResponseMessage.Content;
			Task<string> responseData = content.ReadAsStringAsync();

			string data = responseData.Result;

			XmlDocument doc = new XmlDocument();
			doc.LoadXml(data);

			XmlNode item = doc.SelectSingleNode("ArrayOfExchangeRatesTable").SelectSingleNode("ExchangeRatesTable").SelectSingleNode("Rates");


			List<string> list = new List<string>();

			foreach (XmlNode itemNode in item.ChildNodes)
			{
				string currency = itemNode.SelectSingleNode("Code").InnerText;
				list.Add(currency);
			}

            return list.ToList();
		}
	}
}
