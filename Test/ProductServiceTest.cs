using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Domain;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Service.Domain;

namespace Test
{
    public class ProductServiceTest
    {
        private readonly IProductService _productService;
        private FakeRepository<Product> _productRepository;


        public ProductServiceTest()
        {
            _productRepository =
                new FakeRepository<Product>(new DbContextOptionsBuilder<BaseDbContext>().UseSqlServer().Options);
            _productService = new FakeProductService(_productRepository.Object);
        }

        [SetUp]
        public void Setup()
        {
            var dbSet = new Mock<DbSet<Product>>();
            var products = new List<Product>()
            {
                new Product()
                {
                    Id = 1,
                    Name = "Abolfazl"
                },
                new Product()
                {
                    Id = 2,
                    Name = "Moslemian"
                }
            };
            var queryable = products.AsQueryable();
            dbSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            _productRepository.DbSet = dbSet.Object;
            _productRepository.DbSet.AddRange(products);
        }

        [Test]
        public void Can_insert_to_database()
        {
            var proucts = new List<Product>()
            {
                new Product() {Name = "Moslemian"},
                new Product() {Name = "Abolfazl"}
            };

            _productService.InsertProducts(proucts);

            var mockProductfromDb = _productRepository.DbSet.First(r => r.Id == 1);

            Assert.IsTrue(proucts.Any(p => mockProductfromDb.Name == p.Name));
        }
    }
}