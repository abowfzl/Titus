using System.Collections.Generic;
using Core.Domain;

namespace Service.Domain
{
    public interface IProductService
    {
        void InsertProducts(IList<Product> products);
    }
}