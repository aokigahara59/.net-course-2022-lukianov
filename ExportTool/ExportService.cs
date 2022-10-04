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
                        writer.WriteField(nameof(Client.Name));
                        writer.WriteField(nameof(Client.LastName));
                        writer.WriteField(nameof(Client.PhoneNumber));
                        writer.WriteField(nameof(Client.Bonus));
                        writer.WriteField(nameof(Client.Birthday));
                        writer.WriteField(nameof(Client.PassportId));
                        
                        writer.NextRecord();

                        foreach (var client in clients)
                        {
                            writer.WriteField(client.Name);
                            writer.WriteField(client.LastName);
                            writer.WriteField(client.PhoneNumber);
                            writer.WriteField(client.Bonus);
                            writer.WriteField(client.Birthday);
                            writer.WriteField(client.PassportId);

                            writer.NextRecord();
                        }
                       
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