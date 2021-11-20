using System.IO;

namespace Core.ImportExport
{
    public interface IImportManager
    {
        void ImportProductsFromXlsx(Stream stream);
    }
}