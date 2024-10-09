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
        static async Task Main(string[] args)
        {
            Task firstTask = new Task(() => {
                Thread.Sleep(100);
                Console.WriteLine("Task 1");
            });
            firstTask.Start();

            Task secondTask = ConsoleAfterDelayAsync("Task 2", 150);
            ConsoleAfterDelay("Delay", 75);

            Task thirdTask = ConsoleAfterDelayAsync("Task 3", 50);

            await firstTask;
            await secondTask;
            await thirdTask;
            Console.WriteLine("After the Task was created");
        }

        static void ConsoleAfterDelay(string text, int delayTime)
        {
            Thread.Sleep(delayTime);
            Console.WriteLine(text);
        }
        static async Task ConsoleAfterDelayAsync (string text, int delayTime)
        {
            await Task.Delay(delayTime);
            Console.WriteLine(text);
        }
    }
}