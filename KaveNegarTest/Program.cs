using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core;
using Core.Domain;
using Core.ImportExport;
using Core.Redis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Cach;
using Service.Domain;
using Service.ImportExport;

namespace KaveNegarTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Application Started");

            var config = ConfigurationBiulder();

            var serviceProdvider = BuildDependencyInjection(config);

            Console.WriteLine("App is Ready");

            var stream = File.Open("Files/TempExcel.xlsx", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            Console.WriteLine("Excel File Streamed");

            var importManagerService = serviceProdvider.GetService<IImportManager>();

            var setRedisKeys = importManagerService?.ImportProductsToRedisFromXlsx(stream);

            if (setRedisKeys == null || !setRedisKeys.Any())
            {
                Console.WriteLine("No Data Inserted To Redis");
                Console.ReadLine();
            }

            Console.WriteLine("Excel Imported to Redis");

            var distributedCachManager = serviceProdvider.GetService<IDistributedCachManager>();
            
            var products = new List<Product>();
            foreach (var redisKey in setRedisKeys)
            {
                var product = distributedCachManager?.GetKey<Product>(redisKey);
                products.Add(product);
            }

            Console.WriteLine("All products Loaded From Redis");

            var productService = serviceProdvider.GetService<IProductService>();
            productService?.InsertProducts(products);

            Console.WriteLine("All Products Inserted To DataBase");

            Console.WriteLine("Work Done");

            Console.ReadKey();
        }

        private static IConfiguration ConfigurationBiulder()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            var config = builder.Build();

            return config;
        }

        private static IServiceProvider BuildDependencyInjection(IConfiguration config)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IConfiguration>(config)
                .AddTransient<IImportManager, ImportManager>()
                .AddTransient<IProductService, ProductService>()
                .AddTransient<IRedisConnectionWrapper, RedisConnectionWrapper>()
                .AddTransient<IDistributedCachManager, RedisCachManager>()
                .AddTransient(typeof(IRepository<>), typeof(EntityRepository<>))
                .AddDbContext<BaseDbContext>(options =>
                    options.UseSqlServer(config.GetConnectionString("ConnectionStrings")))
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}