using FluentAssertions;
using invoice_issuer.Contracts;
using invoice_issuer.Domain;
using invoice_issuer.Entities;
using invoice_issuer.Features.Invoices.Calculate;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace invoice_issuer.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async void InvoicesCalculateEndpoint_NotVATpayer_ReturnsSamePrice()
        {
            Request request = new Request
            {
                Customer = new Client
                {
                    Name = "Unit",
                    Country = "lt",
                },
                Merchant = new Provider
                {
                    Name = "Test",
                    Country = "lt",
                    VATpayer = false
                },
                Price = 100
            };

            var countryDataService = new Mock<ICountryDataService>();
            var vatDataService = new Mock<IVATDataService>();

            var response = await Features.Invoices.Calculate.Endpoint.HandleAsync(request, countryDataService.Object, vatDataService.Object);

            Assert.IsType<Ok<Response>>(response);

            var result = (Ok<Response>)response;

            result.Value.Should().NotBeNull();
            result.Value.VATratio.Should().Be(0);
            result.Value.TotalPrice.Should().Be(request.Price);
        }

        [Fact]
        public async void InvoicesCalculateEndpoint_CustomerCountryNotFound_ReturnsFailure()
        {
            Request request = new Request
            {
                Customer = new Client
                {
                    Name = "Unit",
                    Country = "test",
                },
                Merchant = new Provider
                {
                    Name = "Test",
                    Country = "test",
                    VATpayer = true
                },
                Price = 100
            };

            var countryDataService = new Mock<ICountryDataService>();
            var vatDataService = new Mock<IVATDataService>();

            countryDataService.Setup(x => x.GetCountryData(It.IsAny<string>())).ReturnsAsync((Country?)null);

            var response = await Features.Invoices.Calculate.Endpoint.HandleAsync(request, countryDataService.Object, vatDataService.Object);

            Assert.IsType<ProblemHttpResult>(response);
            var result = (ProblemHttpResult)response;
            result.ProblemDetails.Detail.Should().Be(DomainErrors.Invoices.CustomerCountryNotFound.Code);
        }

        [Fact]
        public async void InvoicesCalculateEndpoint_MerchantCountryNotFound_ReturnsFailure()
        {
            Request request = new Request
            {
                Customer = new Client
                {
                    Name = "Unit",
                    Country = "lt",
                },
                Merchant = new Provider
                {
                    Name = "Test",
                    Country = "test",
                    VATpayer = true
                },
                Price = 100
            };

            var countryDataService = new Mock<ICountryDataService>();
            var vatDataService = new Mock<IVATDataService>();

            var countryMock = new Mock<Country>();

            countryDataService.Setup(x => x.GetCountryData(request.Customer.Country)).ReturnsAsync(countryMock.Object);
            countryDataService.Setup(x => x.GetCountryData(request.Merchant.Country)).ReturnsAsync((Country?)null);

            var response = await Features.Invoices.Calculate.Endpoint.HandleAsync(request, countryDataService.Object, vatDataService.Object);

            Assert.IsType<ProblemHttpResult>(response);
            var result = (ProblemHttpResult)response;
            result.ProblemDetails.Detail.Should().Be(DomainErrors.Invoices.MerchantCountryNotFound.Code);
        }

        [Fact]
        public async void InvoicesCalculateEndpoint_CustomerOutsideEurope_ReturnsSamePrice()
        {
            Request request = new Request
            {
                Customer = new Client
                {
                    Name = "Unit",
                    Country = "usa",
                },
                Merchant = new Provider
                {
                    Name = "Test",
                    Country = "usa",
                    VATpayer = true
                },
                Price = 100
            };

            Country country = new Country
            {
                Name = "United States of America",
                Code = "usa",
                Region = "North America"
            };

            var countryDataService = new Mock<ICountryDataService>();
            var vatDataService = new Mock<IVATDataService>();

            countryDataService.Setup(x => x.GetCountryData(request.Customer.Country)).ReturnsAsync(country);
            countryDataService.Setup(x => x.GetCountryData(request.Merchant.Country)).ReturnsAsync(country);

            var response = await Features.Invoices.Calculate.Endpoint.HandleAsync(request, countryDataService.Object, vatDataService.Object);

            Assert.IsType<Ok<Response>>(response);

            var result = (Ok<Response>)response;

            result.Value.Should().NotBeNull();
            result.Value.VATratio.Should().Be(0);
            result.Value.TotalPrice.Should().Be(request.Price);
        }

        [Fact]
        public async void InvoicesCalculateEndpoint_VATCountryDataNotFound_ReturnsFailure()
        {
            Request request = new Request
            {
                Customer = new Client
                {
                    Name = "Unit",
                    Country = "lt",
                },
                Merchant = new Provider
                {
                    Name = "Test",
                    Country = "lt",
                    VATpayer = true
                },
                Price = 100
            };

            Country country = new Country
            {
                Name = "Lithuania",
                Code = "lt",
                Region = "Europe"
            };

            var countryDataService = new Mock<ICountryDataService>();
            var vatDataService = new Mock<IVATDataService>();

            countryDataService.Setup(x => x.GetCountryData(request.Customer.Country)).ReturnsAsync(country);
            countryDataService.Setup(x => x.GetCountryData(request.Merchant.Country)).ReturnsAsync(country);

            vatDataService.Setup(x => x.GetCountryVATData(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((Rate?)null);

            var response = await Features.Invoices.Calculate.Endpoint.HandleAsync(request, countryDataService.Object, vatDataService.Object);

            Assert.IsType<ProblemHttpResult>(response);
            var result = (ProblemHttpResult)response;
            result.ProblemDetails.Detail.Should().Be(DomainErrors.Invoices.VATdataNotFound.Code);
        }

        [Fact]
        public async void InvoicesCalculateEndpoint_ZeroVATrate_ReturnsSamePrice()
        {
            Request request = new Request
            {
                Customer = new Client
                {
                    Name = "Unit",
                    Country = "usa",
                },
                Merchant = new Provider
                {
                    Name = "Test",
                    Country = "usa",
                    VATpayer = true
                },
                Price = 100
            };

            Country country = new Country
            {
                Name = "Lithuania",
                Code = "lt",
                Region = "Europe"
            };

            Rate rate = new Rate
            {
                Name = "Standard",
                Rates = []
            };

            var countryDataService = new Mock<ICountryDataService>();
            var vatDataService = new Mock<IVATDataService>();

            countryDataService.Setup(x => x.GetCountryData(request.Customer.Country)).ReturnsAsync(country);
            countryDataService.Setup(x => x.GetCountryData(request.Merchant.Country)).ReturnsAsync(country);

            vatDataService.Setup(x => x.GetCountryVATData(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(rate);

            var response = await Features.Invoices.Calculate.Endpoint.HandleAsync(request, countryDataService.Object, vatDataService.Object);

            Assert.IsType<Ok<Response>>(response);

            var result = (Ok<Response>)response;

            result.Value.Should().NotBeNull();
            result.Value.VATratio.Should().Be(0);
            result.Value.TotalPrice.Should().Be(request.Price);
        }

        [Fact]
        public async void InvoicesCalculateEndpoint_SuccessfulCalculation_ReturnsCalculatedPrice()
        {
            Request request = new Request
            {
                Customer = new Client
                {
                    Name = "Unit",
                    Country = "usa",
                },
                Merchant = new Provider
                {
                    Name = "Test",
                    Country = "usa",
                    VATpayer = true
                },
                Price = 100
            };

            Country country = new Country
            {
                Name = "Lithuania",
                Code = "lt",
                Region = "Europe"
            };

            Rate rate = new Rate
            {
                Name = "Standard",
                Rates = [21]
            };

            double VATratio = 1.0 + (double)rate.Rates.First() / 100;

            var countryDataService = new Mock<ICountryDataService>();
            var vatDataService = new Mock<IVATDataService>();

            countryDataService.Setup(x => x.GetCountryData(request.Customer.Country)).ReturnsAsync(country);
            countryDataService.Setup(x => x.GetCountryData(request.Merchant.Country)).ReturnsAsync(country);

            vatDataService.Setup(x => x.GetCountryVATData(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(rate);

            var response = await Features.Invoices.Calculate.Endpoint.HandleAsync(request, countryDataService.Object, vatDataService.Object);

            Assert.IsType<Ok<Response>>(response);

            var result = (Ok<Response>)response;

            result.Value.Should().NotBeNull();
            result.Value.VATratio.Should().Be(rate.Rates.First());
            result.Value.TotalPrice.Should().Be(request.Price * (decimal)VATratio);
        }
    }
}