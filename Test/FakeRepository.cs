using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Domain;
using Microsoft.EntityFrameworkCore;
using Moq;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;

namespace Test
{
    public class FakeRepository<TEntity> : Mock<IRepository<TEntity>> where TEntity : BaseEntity
    {
        private readonly DbContext _context;
        public DbSet<TEntity> DbSet;

        private DbContextOptions<BaseDbContext> ContextOptions { get; }

        public FakeRepository(DbContextOptions<BaseDbContext> contextOptions)
        {
            ContextOptions = contextOptions;
            _context = new BaseDbContext(ContextOptions);
            Seed();
        }


        private void Seed()
        {
            using (_context)
            {
                
            }
        }
    }
}