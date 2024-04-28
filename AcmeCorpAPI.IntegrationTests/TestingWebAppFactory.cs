using AcmeCorpAPI.Models;
using AcmeCorpAPI.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace AcmeCorpAPI.IntegrationTests
{
    public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<AcmeCorpAPIContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<AcmeCorpAPIContext>(options =>
                {
                    //options.UseInMemoryDatabase("InMemoryAcmeCorpAPITest");
                });

                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                using (var appContext = scope.ServiceProvider.GetRequiredService<AcmeCorpAPIContext>())
                {
                    try
                    {
                        appContext.Database.EnsureCreated();
                        var testCustomer = appContext.Customer.FirstOrDefault(c => c.Name == "__test_customer_1__");
                        if (testCustomer == null)
                        {
                            testCustomer = appContext.Customer.Add(new Customer { Name = "__test_customer_1__", 
                                                                   ContactInfo= new(){Email="__test_customer_email_1__",
                                                                   PhoneNumber="__test_customer_phonenumber_1__"} }).Entity;
                        }

                        appContext.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        //Log errors or do anything you think it's needed
                        throw;
                    }
                }
            });
        }
    }
}