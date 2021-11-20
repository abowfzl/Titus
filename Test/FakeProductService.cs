using Core;
using Core.Domain;
using Moq;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using Service.Domain;

namespace Test
{
    public class FakeProductService : ProductService
    {
        public FakeProductService(IRepository<Product> productRepository = null) : base(productRepository ?? new Mock<IRepository<Product>>()
            .Object)
        {
        }
    }
}