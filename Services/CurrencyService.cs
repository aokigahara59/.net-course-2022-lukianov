using Models;
using Newtonsoft.Json;

namespace Services
{
    public class CurrencyService
    {

        private string _token;

        public void Authorize(string token)
        {
            _token = token;
        }

        public async Task<CurrencyResponse> ExchangeMoneyAsync(Currency fromCurrency, Currency toCurrency, float amount)
        {
            CurrencyResponse exchangedResult;

            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage responseMessage = await httpClient.GetAsync($"https://www.amdoren.com/api/currency.php?" +
                                                                        $"api_key={_token}" +
                                                                        $"&from={fromCurrency.Name}" +
                                                                        $"&to={toCurrency.Name}" +
                                                                        $"&amount={amount}");

                responseMessage.EnsureSuccessStatusCode();

                string message = await responseMessage.Content.ReadAsStringAsync();

                exchangedResult = JsonConvert.DeserializeObject<CurrencyResponse>(message);
            }

            return exchangedResult;
        }
    }
}
