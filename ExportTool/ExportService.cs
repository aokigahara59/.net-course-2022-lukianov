using System.Globalization;
using CsvHelper;
using Models;

namespace ExportTool
{
    public class ExportService
    {

        public void ExportClientData(List<Client> clients, string directory, string fileName)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(directory);

            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }

            string fullPath = Path.Combine(directoryInfo.FullName, fileName);

            using (FileStream fileStream = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    using (CsvWriter writer = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                    {
                        writer.WriteRecords(clients.AsEnumerable());
                       
                        writer.Flush();
                    }
                }
            }
        }


        public List<Client> ImportClients(string directory, string fileName)
        {
            string fullPath = Path.Combine(directory, fileName);

            using (FileStream fileStream = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    using (CsvReader reader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                    {
                        return reader.GetRecords<Client>().ToList();
                    }
                }
            }
        }

    }
}