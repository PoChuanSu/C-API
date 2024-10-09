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
using AutoMapper;
using System.Collections;

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

            string computersJson = File.ReadAllText("ComputersSnake.json");

            Mapper mapper = new Mapper(new MapperConfiguration((cfg) => {
                cfg.CreateMap<ComputerSnake, Computer>()
                .ForMember(destination => destination.ComputerId, options =>
                    options.MapFrom(source => source.computer_id))
                .ForMember(destination => destination.Motherboard, options =>
                    options.MapFrom(source => source.motherboard))
                .ForMember(destination => destination.CPUCores, options =>
                    options.MapFrom(source => source.cpu_cores))
                .ForMember(destination => destination.HasWifi, options =>
                    options.MapFrom(source => source.has_wifi))   
                .ForMember(destination => destination.HasLTE, options =>
                    options.MapFrom(source => source.has_lte))
                .ForMember(destination => destination.ReleaseDate, options =>
                    options.MapFrom(source => source.release_date))
                .ForMember(destination => destination.Price, options =>
                    options.MapFrom(source => source.price))
                .ForMember(destination => destination.VideoCard, options =>
                    options.MapFrom(source => source.video_card));
            }));

            IEnumerable<ComputerSnake>? computersystem = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<ComputerSnake>>(computersJson);

            if (computersystem != null) {
                IEnumerable<Computer> computerResult = mapper.Map<IEnumerable<Computer>>(computersystem);
                  Console.WriteLine("Automapper Count: " + computerResult.Count());
                // foreach (Computer computer in computerResult)
                // {
                //     Console.WriteLine(computer.Motherboard);
                // }
            }


            // Console.WriteLine(computersJson);
            // JsonSerializerOptions options = new JsonSerializerOptions()
            // {
            //     PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            // };

            // IEnumerable<Computer>? computersNewtonSoft = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson, options);
            IEnumerable<Computer>? computersSystemJsonPropertyMappiung = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computersJson);

            if (computersSystemJsonPropertyMappiung != null)
            {   
                Console.WriteLine("JSON Property Count: " + computersSystemJsonPropertyMappiung.Count());
                // foreach (Computer computer in computersSystemJsonPropertyMappiung)
                // {   
                //     Console.WriteLine(computer.Motherboard);
                // }
            }

            // JsonSerializerSettings settings = new JsonSerializerSettings()
            // {
            //     ContractResolver = new CamelCasePropertyNamesContractResolver()
            // };

            // string computersCopy = JsonConvert.SerializeObject(computersNewtonSoft, settings);
            // File.WriteAllText("computersCopyNewtonsoft.txt", computersCopy);

            // string computersCopySystem = System.Text.Json.JsonSerializer.Serialize(computersNewtonSoft, options);
            // File.WriteAllText(" computersCopySystem.txt", computersCopySystem);
        }
        
        static string EscapeSingleQuote (string input)
        {
            string output = input.Replace("'","''");
            return output;
        }
    }
}