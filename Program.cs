using System;
using System.Security.Cryptography.X509Certificates;
using PartyOn.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using PartyOn.Data;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json;
using Newtonsoft.Json.Serialization;

namespace PartyOn // Note: actual namespace depends on the project name.
{

    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            DataContextDapper dapper = new DataContextDapper(config);

            // //Comment out code line is the longer version of creating a new DataContextEF
            //DataContextEF entityFramework = new DataContextEF();
            DataContextEF entityFramework = new(config);

            // string sqlCommand = "SELECT GETDATE()";

            // DateTime time = dapper.LoadDataSingle<DateTime>(sqlCommand);

            // Console.WriteLine(time);

            /* ADDING WITH ENTITY FRAMEWORK*/
            // Computer myComputer = new Computer()
            // {
            //     MotherBoard = "L620",
            //     CPUCores = 16,
            //     HasWifi = true,
            //     HasLTE = false,
            //     ReleaseDate = DateTime.Now,
            //     Price = 1000.76m,
            //     VideoCard = "ASDFASFE12"
            // };

            // entityFramework.Add(myComputer);
            // entityFramework.SaveChanges();

            // string addToDB = @"INSERT INTO TutorialAppSchema.Computer (
            //     MotherBoard,
            //     CPUCores,
            //     HasWifi,
            //     HasLTE,
            //     ReleaseDate,
            //     Price,
            //     VideoCard
            // ) VALUES ( '" + myComputer.MotherBoard
            //         + "','" + myComputer.CPUCores
            //         + "','" + myComputer.HasWifi
            //         + "','" + myComputer.HasLTE
            //         + "','" + myComputer.ReleaseDate
            //         + "','" + myComputer.Price
            //         + "','" + myComputer.VideoCard
            // + "')";

            // //Console.WriteLine(addToDB);

            // //bool result = dapper.ExecuteSql(addToDB);

            // //Console.WriteLine(result);

            // string sqlSelect = @"
            // SELECT 
            //     Computer.MotherBoard,
            //     Computer.CPUCores,
            //     Computer.HasWifi,
            //     Computer.HasLTE,
            //     Computer.ReleaseDate,
            //     Computer.Price,
            //     Computer.VideoCard
            // FROM TutorialAppSchema.Computer";

            // //Query will return an IEnumberalbe!
            // IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);

            // foreach(Computer computer in computersEF)
            // {
            //     Console.WriteLine("'" + computer.MotherBoard
            //         + "','" + computer.CPUCores
            //         + "','" + computer.HasWifi
            //         + "','" + computer.HasLTE
            //         + "','" + computer.ReleaseDate
            //         + "','" + computer.Price
            //         + "','" + computer.VideoCard
            // + "'");
            // }

            // //USING ENTITY FRAMERWORK
            // IEnumerable<Computer> computersEF = entityFramework.Computer?.ToList() ?? new List<Computer>();

            // if(computersEF != null)
            // {
            //      foreach(Computer computer in computersEF)
            //     {
            //         Console.WriteLine("'" + computer.ComputerId
            //             + "','" + computer.MotherBoard
            //             + "','" + computer.CPUCores
            //             + "','" + computer.HasWifi
            //             + "','" + computer.HasLTE
            //             + "','" + computer.ReleaseDate
            //             + "','" + computer.Price
            //             + "','" + computer.VideoCard
            //     + "'");
            //     }
            // }

            string comparisonJson = File.ReadAllText("ComputersSnake.json");

            /* SERIELIAZE AND DESERIALIZE JSON FILES TO READ & WRITE */
            //Built in Json Serializer - Options required to serielize and deserielize camelcase - pass it in as options
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            //Built in Serializer
            //IEnumerable<Computer>? computers = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(comparisonJson, options);

            // Json Converter package (other option to deserialize objects)
            IEnumerable<Computer>? computers = JsonConvert.DeserializeObject<IEnumerable<Computer>>(comparisonJson);

            if (computers == null) return;

            foreach (Computer computer in computers)
            {
                if (computer == null)
                    continue;

                string addToDB = @"INSERT INTO TutorialAppSchema.Computer (
                    MotherBoard,
                    CPUCores,
                    HasWifi,
                    HasLTE,
                    ReleaseDate,
                    Price,
                    VideoCard
                ) VALUES ( '" + EscapeSingleQuote(computer.MotherBoard)
                        + "','" + computer.CPUCores
                        + "','" + computer.HasWifi
                        + "','" + computer.HasLTE
                        + "','" + computer.ReleaseDate
                        + "','" + computer.Price
                        + "','" + EscapeSingleQuote(computer.VideoCard)
                + "')";

                dapper.ExecuteSql(addToDB);
            }

            //Required to serielize as camelcase
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };


            string computersNewtonsoft = JsonConvert.SerializeObject(computers, settings);

            File.WriteAllText("computersNewtonsoft", computersNewtonsoft);

            string computersSystem = System.Text.Json.JsonSerializer.Serialize(computers, options);

            File.WriteAllText("computersSystem", computersSystem);


            // Console.WriteLine(myComputer.MotherBoard);
            // Console.WriteLine(myComputer.CPUCores);
            // Console.WriteLine(myComputer.HasWifi);
            // Console.WriteLine(myComputer.HasLTE);
            // Console.WriteLine(myComputer.ReleaseDate);
            // Console.WriteLine(myComputer.Price);
            // Console.WriteLine(myComputer.VideoCard);
        }

        static string EscapeSingleQuote(string input)
        {
            if(input == null)
            {
                return "";
            }
            string output = input.Replace("'", "''");
 
            return output;
        }
    }
}