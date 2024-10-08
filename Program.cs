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
            Computer myComputer = new Computer() 
            {
                Motherboard = "Z690",
                HasWifi = true,
                HasLTE = false,
                ReleaseDate = DateTime.Now,
                Price = 943.87m,
                VideoCard = "RTX 2060"
            };

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

            // File.WriteAllText("log.txt", "\n" + sql + "\n");
            using StreamWriter openFile = new("log.txt", append: true);
            
            openFile.WriteLine("\n" + sql + "\n");

            openFile.Close();
            string fileText = File.ReadAllText("log.txt");

            Console.WriteLine(fileText);
        }
    }
}