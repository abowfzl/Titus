using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Domain;
using Core.ImportExport;
using OfficeOpenXml;
using Service.Cach;
using Service.ImportExport.Helper;

namespace Service.ImportExport
{
    public class ImportManager : IImportManager
    {
        private readonly IDistributedCachManager _distributedCachManager;

        public ImportManager(IDistributedCachManager distributedCachManager)
        {
            _distributedCachManager = distributedCachManager;
        }

        private static IList<PropertyByName<T>> GetPropertiesByExcelCells<T>(ExcelWorksheet worksheet)
        {
            var properties = new List<PropertyByName<T>>();
            var poz = 1;
            while (true)
            {
                try
                {
                    var cell = worksheet.Cells[1, poz];

                    if (string.IsNullOrEmpty(cell?.Value?.ToString()))
                        break;

                    poz += 1;
                    properties.Add(new PropertyByName<T>(cell.Value.ToString()));
                }
                catch
                {
                    break;
                }
            }

            return properties;
        }

        public List<string> ImportProductsToRedisFromXlsx(Stream stream)
        {
            using var xlPackage = new ExcelPackage(stream);

            // get the first worksheet in the workbook
            var worksheet = xlPackage.Workbook.Worksheets.FirstOrDefault();

            if (worksheet == null)
                throw new Exception("No worksheet found");

            var properties = GetPropertiesByExcelCells<Product>(worksheet);

            var manager = new PropertyManager<Product>(properties);

            var iRow = 2;

            var products = new List<Product>();

            while (true)
            {
                var allColumnsAreEmpty = manager.GetProperties
                    .Select(property => worksheet.Cells[iRow, property.PropertyOrderPosition])
                    .All(cell => cell?.Value == null || string.IsNullOrEmpty(cell.Value.ToString()));

                if (allColumnsAreEmpty)
                    break;

                manager.ReadFromXlsx(worksheet, iRow);

                foreach (var property in manager.GetProperties)
                {
                    var product = new Product();

                    switch (property.PropertyName)
                    {
                        case "Name":
                            product.Name = property.StringValue;
                            break;
                    }

                    if (products.All(p => p.Name != product.Name))
                        products.Add(product);
                }

                iRow++;
            }

            var setRedisKeys = new List<string>();

            if (!products.Any()) return setRedisKeys;
            {
                foreach (var product in products)
                {
                    var redisKey = $"product-{product.Name}";

                    _distributedCachManager.SetKey(redisKey, product, 30);

                    setRedisKeys.Add(redisKey);
                }
            }

            return setRedisKeys;
        }
    }
}