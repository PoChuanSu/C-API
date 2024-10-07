using System;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using HelloWorld.Models;
using HelloWorld.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {   
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            DataContextDapper dapper = new DataContextDapper(config);
            DataContextEF entityFramework = new DataContextEF(config);

            // string connectionString = "Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=false;User Id=sa;Password=SQLConnect1@@";
            // IDbConnection dbConnection = new SqlConnection (connectionString);

            DateTime rightnow =  dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");

            Computer myComputer = new Computer() 
            {
                Motherboard = "Z690",
                HasWifi = true,
                HasLTE = false,
                ReleaseDate = DateTime.Now,
                Price = 943.87m,
                VideoCard = "RTX 2060"
            };

            entityFramework.Add(myComputer);
            entityFramework.SaveChanges();

            string sql = @"INSERT INTO TutorialAppSchema.Computer (
                Motherboard,
                HasWifi,
                HasLTE,
                ReleaseDate,
                Price,
                VideoCard
            ) VALUES ('" + myComputer.Motherboard 
                    + "','" + myComputer.HasWifi
                    + "','" + myComputer.HasLTE
                    + "','" + myComputer.ReleaseDate
                    + "','" + myComputer.Price
                    + "','" + myComputer.VideoCard + "')";

            Console.WriteLine(sql);

            bool result = dapper.ExecuteSql(sql);

            Console.WriteLine(result);

            string sqlSelect = @"SELECT
                Computer.ComputerId,
                Computer.Motherboard,
                Computer.HasWifi,
                Computer.HasLTE,
                Computer.ReleaseDate,
                Computer.Price,
                Computer.VideoCard
             FROM TutorialAppSchema.Computer";

            IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);
            IEnumerable<Computer>? computersEf = entityFramework.Computer?.ToList<Computer>();

            if (computersEf != null)
            {
                foreach(Computer singleComputer in computers) 
                {
                    Console.WriteLine(
                        "'" + singleComputer.ComputerId
                        + "','" + singleComputer.Motherboard 
                        + "','" + singleComputer.HasWifi
                        + "','" + singleComputer.HasLTE
                        + "','" + singleComputer.ReleaseDate
                        + "','" + singleComputer.Price
                        + "','" + singleComputer.VideoCard + "'"
                    );
                }
            }
            
            foreach(Computer singleComputer in computers) 
            {
                Console.WriteLine(
                    "'" + singleComputer.ComputerId
                    + "','" + singleComputer.Motherboard 
                    + "','" + singleComputer.HasWifi
                    + "','" + singleComputer.HasLTE
                    + "','" + singleComputer.ReleaseDate
                    + "','" + singleComputer.Price
                    + "','" + singleComputer.VideoCard + "'"
                );
            }
        }
    }
}