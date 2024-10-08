using System;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using HelloWorld.Models;
using HelloWorld.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
        
        IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        DataContextDapper dapper = new DataContextDapper(config);
            // Computer myComputer = new Computer() 
            // {
            //     Motherboard = "Z690",
            //     HasWifi = true,
            //     HasLTE = false,
            //     ReleaseDate = DateTime.Now,
            //     Price = 943.87m,
            //     VideoCard = "RTX 2060"
            // };

            // string sql = @"INSERT INTO TutorialAppSchema.Computer (
            //     Motherboard,
            //     HasWifi,
            //     HasLTE,
            //     ReleaseDate,
            //     Price,
            //     VideoCard
            // ) VALUES ('" + myComputer.Motherboard 
            //         + "','" + myComputer.HasWifi
            //         + "','" + myComputer.HasLTE
            //         + "','" + myComputer.ReleaseDate
            //         + "','" + myComputer.Price
            //         + "','" + myComputer.VideoCard + "')";

            // // File.WriteAllText("log.txt", "\n" + sql + "\n");
            // using StreamWriter openFile = new("log.txt", append: true);
            
            // openFile.WriteLine("\n" + sql + "\n");

            // openFile.Close();
            // string fileText = File.ReadAllText("log.txt");

            // Console.WriteLine(fileText);

            string computersJson = File.ReadAllText("Computers.json");

            // Console.WriteLine(computersJson);
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            IEnumerable<Computer>? computersNewtonSoft = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson, options);
            IEnumerable<Computer>? computersSystem = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computersJson);

            if (computersNewtonSoft != null)
            {
                foreach (Computer computer in computersNewtonSoft)
                {   
                    string sql = @"INSERT INTO TutorialAppSchema.Computer (
                        Motherboard,
                        HasWifi,
                        HasLTE,
                        ReleaseDate,
                        Price,
                        VideoCard
                    ) VALUES ('" + EscapeSingleQuote(computer.Motherboard)
                        + "','" + computer.HasWifi
                        + "','" + computer.HasLTE
                        + "','" + computer.ReleaseDate?.ToString("yyyy-MM-dd")
                        + "','" + computer.Price
                        + "','" + EscapeSingleQuote(computer.VideoCard) + "')";

                    dapper.ExecuteSql(sql);
                }
            }

            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            string computersCopy = JsonConvert.SerializeObject(computersNewtonSoft, settings);
            File.WriteAllText("computersCopyNewtonsoft.txt", computersCopy);

            string computersCopySystem = System.Text.Json.JsonSerializer.Serialize(computersNewtonSoft, options);
            File.WriteAllText(" computersCopySystem.txt", computersCopySystem);
        }
        
        static string EscapeSingleQuote (string input)
        {
            string output = input.Replace("'","''");
            return output;
        }
    }
}