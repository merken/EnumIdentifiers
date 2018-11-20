using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EnumIdentifiers.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EnumIdentifiers.Console
{
    class Program
    {
        private readonly IConfiguration configuration;
        private DotNetFlixDbContext context;

        static async Task Main(string[] args)
        {
            var program = new Program();

            await program.PurgeCustomers();

            await program.InsertNewCustomer(new Customer
            {
                FirstName = "Maarten",
                LastName = "Merken",
                Phone = "+32 486 77 55 88",
                Billing = Customer.BillingType.ISP,
                Email = "merken.maarten@gmail.com",
                SubscriptionLevel = SubscriptionLevel.Level.Standard
            });

            await program.InsertNewCustomer(new Customer
            {
                FirstName = "John",
                LastName = "Doe",
                Phone = "+32 000 000 001",
                Billing = Customer.BillingType.CreditCard,
                Email = "johndoe@gmail.com",
                SubscriptionLevel = SubscriptionLevel.Level.Basic
            });

            await program.InsertNewCustomer(new Customer
            {
                FirstName = "James",
                LastName = "Does",
                Phone = "+32 000 000 007",
                Billing = Customer.BillingType.Invoice,
                Email = "jamesdoes@gmail.com",
                SubscriptionLevel = SubscriptionLevel.Level.Premium
            });

            await program.PrintAllCustomersAndSubscriptions();
        }

        public Program()
        {
            this.configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var options = new DbContextOptionsBuilder<DotNetFlixDbContext>()
                .UseSqlServer(configuration.GetConnectionString("DotNetFlixDb"))
                .Options;

            this.context = new DotNetFlixDbContext(options);
        }

        public async Task PurgeCustomers()
        {
            this.context.Customers.RemoveRange(this.context.Customers);
            await this.context.SaveChangesAsync();
        }

        public async Task InsertNewCustomer(Customer customer)
        {
            await this.context.Customers.AddAsync(customer);
            await this.context.SaveChangesAsync();
        }

        public async Task PrintAllCustomersAndSubscriptions()
        {
            var customers = await this.context.Customers
                .Include(c => c.SubscriptionLevelRelation)
                .ToListAsync();

            System.Console.WriteLine($"ALL CUSTOMERS");
            foreach (var customer in customers)
            {
                PrintCustomerInfo(customer);
            }

            customers = await this.context.Customers
                .Where(c => c.SubscriptionLevel == SubscriptionLevel.Level.Premium)
                .Include(c => c.SubscriptionLevelRelation)
                .ToListAsync();

            //Premium
            System.Console.WriteLine($"PREMIUM CUSTOMERS");
            foreach (var customer in customers)
            {
                PrintCustomerInfo(customer);
            }

            customers = await this.context.Customers
                .Where(c => c.Billing == Customer.BillingType.CreditCard)
                .Include(c => c.SubscriptionLevelRelation)
                .ToListAsync();

            //CreditCard
            System.Console.WriteLine($"CREDITCARD CUSTOMERS");
            foreach (var customer in customers)
            {
                PrintCustomerInfo(customer);
            }

            customers = await this.context.Customers
                .Where(c => c.SubscriptionLevelRelation.Quality == SubscriptionLevel.VideoQuality.Standard)
                .Include(c => c.SubscriptionLevelRelation)
                .ToListAsync();

            //Standard Def
            System.Console.WriteLine($"SD CUSTOMERS");
            foreach (var customer in customers)
            {
                PrintCustomerInfo(customer);
            }
        }

        private void PrintCustomerInfo(Customer customer)
        {
            System.Console.WriteLine($"Customer {customer.FirstName} {customer.LastName}");
            System.Console.WriteLine($"Billing type {customer.Billing.ToString()}");
            System.Console.WriteLine($"Subscription {customer.SubscriptionLevel.ToString()}");
            System.Console.WriteLine($"Subscription Info");
            System.Console.WriteLine($"-----------------");
            System.Console.WriteLine($"Price per month {customer.SubscriptionLevelRelation.PricePerMonth}");
            System.Console.WriteLine($"Simultaneous devices {customer.SubscriptionLevelRelation.NumberOfSimultaneousDevices}");
            System.Console.WriteLine($"Download devices {customer.SubscriptionLevelRelation.NumberDevicesWithDownloadCapability}");
            System.Console.WriteLine($"Video Quality {customer.SubscriptionLevelRelation.Quality.ToString()}");
            System.Console.WriteLine($"");
        }
    }
}
