using System;
using System.Collections.Generic;
using Core;
using Core.Domain;

namespace Service.Domain
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public void InsertProducts(IList<Product> products)
        {
            if (products == null)
                throw new ArgumentNullException(nameof(products));

            _productRepository.Insert(products);
        }
    }
}