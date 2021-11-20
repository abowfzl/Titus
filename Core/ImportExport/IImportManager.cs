using System.Collections.Generic;
using System.IO;

namespace Core.ImportExport
{
    public interface IImportManager
    {
        List<string> ImportProductsToRedisFromXlsx(Stream stream);
    }
}