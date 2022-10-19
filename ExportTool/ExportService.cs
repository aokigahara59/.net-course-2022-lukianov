using System.Globalization;
using CsvHelper;
using Models;
using Newtonsoft.Json;

namespace ExportTool
{
    public class ExportService
    {

        public async Task ExportClientData(List<Client> clients, string directory, string fileName)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(directory);

            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }

            string fullPath = Path.Combine(directoryInfo.FullName, fileName);

            await using (FileStream fileStream = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                await using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    await using (CsvWriter writer = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                    {
                        await Task.Run(() =>
                        {
                            writer.WriteRecords(clients.AsEnumerable());

                            writer.Flush();
                        });
                        
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


        public async Task ExportPersonToJson<T>(T person, string directory, string fileName) where T : Person
        {
            await Task.Run(() =>
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(directory);

                if (!directoryInfo.Exists)
                {
                    directoryInfo.Create();
                }

                string fullPath = Path.Combine(directory, fileName);

                string serializedPerson = JsonConvert.SerializeObject(person);

                using (FileStream fileStream = new FileStream(fullPath, FileMode.OpenOrCreate))
                {
                    using (StreamWriter streamWriter = new StreamWriter(fileStream))
                    {
                        streamWriter.Write(serializedPerson);
                    }
                }
            });
        }

        public async Task<T> ImportPersonFromJson<T>(string directory, string fileName) where T : Person
        {
            Task<T> result = Task.Run(() =>
            {
                string fullPath = Path.Combine(directory, fileName);

                using (FileStream fileStream = new FileStream(fullPath, FileMode.OpenOrCreate))
                {
                    using (StreamReader streamReader = new StreamReader(fileStream))
                    {
                        string serializedPerson = streamReader.ReadToEnd();

                        return JsonConvert.DeserializeObject<T>(serializedPerson);
                    }
                }
            });

            await result;
            return result.Result;

        }

    }
}