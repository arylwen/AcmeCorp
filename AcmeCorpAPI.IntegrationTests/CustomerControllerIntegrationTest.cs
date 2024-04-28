using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AcmeCorpAPI.Models;
using Xunit;
using Xunit.Abstractions;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Text;

namespace AcmeCorpAPI.IntegrationTests
{
    public class CustomerControllerIntegrationTests : IClassFixture<TestingWebAppFactory<Program>>
	{
		private readonly HttpClient _client;
        private readonly ITestOutputHelper _testOutputHelper;

		public CustomerControllerIntegrationTests(TestingWebAppFactory<Program> factory,
                                                  ITestOutputHelper testOutputHelper) 
		{ 
            _client = factory.CreateClient();
            _testOutputHelper = testOutputHelper;
        }

           [Fact]
        public async Task AddCustomer(){
            _testOutputHelper.WriteLine("Add customer***************************");
            var createCustomer = new CustomerDTO
            {
                Name = "__test_create_customer_10__", 
                ContactInfo= new(){ Email="__test_customer_email_10__",
                                    PhoneNumber="__test_customer_phonenumber_10__"}
            };
            var serializedCreateCustomer = JsonSerializer.Serialize(createCustomer);
            var stringContent = new StringContent(serializedCreateCustomer, Encoding.UTF8, 
                    new MediaTypeWithQualityHeaderValue("application/json"));


            var response = await _client.PostAsync("/api/Customers", stringContent);
            response.EnsureSuccessStatusCode();
        }

		[Fact]
		public async Task AddOrder()
		{
            _testOutputHelper.WriteLine("*******************************************");
			var response = await _client.GetAsync("/api/Customers");

			
		}

        [Fact]
        public async Task GetCustomers(){
            var response = await _client.GetAsync("/api/Customers");

            response.EnsureSuccessStatusCode();

			var responseString = await response.Content.ReadAsStringAsync();

            _testOutputHelper.WriteLine(responseString);

			Assert.Contains("__test_customer_1__", responseString);
			Assert.Contains("__test_customer_email_1__", responseString);
        }
    }
}