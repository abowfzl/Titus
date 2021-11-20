using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain
{
    [Table("Product")]
    public class Product : BaseEntity
    {
        public string Name { get; set; }
    }
}