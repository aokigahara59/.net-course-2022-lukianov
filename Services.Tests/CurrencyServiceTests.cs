using Models;
using Xunit;

namespace Services.Tests
{
    public class CurrencyServiceTests
    {

        [Fact]
        public async void CurrencyExchangePositivTest()
        {
            // Arrange
            CurrencyService currencyService = new CurrencyService();

            Currency fromCurrency = new Currency { Name = "USD" };
            Currency toCurrency = new Currency { Name = "RUB" };

            // Act
            currencyService.Authorize("fn4BrxqVaPu2YkrswXvad2Cug5Pdan");

            var response = await currencyService.ExchangeMoneyAsync(fromCurrency, toCurrency, 500);

            // Assert
            Assert.NotEqual(0, response.Amount);
        }

    }
}
