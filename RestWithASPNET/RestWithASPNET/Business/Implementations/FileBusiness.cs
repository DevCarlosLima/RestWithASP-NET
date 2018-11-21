using System.IO;

namespace RestWithASPNET.Business.Implementations
{
    public class FileBusiness : IFileBusiness
    {
        public byte[] GetPDFFile()
        {
            string path = Directory.GetCurrentDirectory();
            var fullPath = path + "\\Other\\exemplo.pdf";

            return File.ReadAllBytes(fullPath);
        }
    }
}
