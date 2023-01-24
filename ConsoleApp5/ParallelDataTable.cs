using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    public class ParallelDataTable
    {

        public static void Run()
        {
            var timer = new Stopwatch();
            timer.Start();
            var dataTable = new DataTable();
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Salary", typeof(int));
            //dataTable.Columns.Add("Column3", typeof(DateTime));

            var blockingCollection = new BlockingCollection<DataRow>(boundedCapacity: 10000000);

            Random rand = new Random(DateTime.Now.Second);
            Task.Run(() =>
            {
                for (int i = 0; i < 10000000; i++)
                {
                    var dataRow = dataTable.NewRow();
                    dataRow["Name"] = GetRanName(rand);
                    dataRow["Salary"] = i + 1000;
                    blockingCollection.Add(dataRow);
                }
                blockingCollection.CompleteAdding();
            }).Wait();
            DataRow singleRow = blockingCollection.Take();
            DataRow specificRow = blockingCollection.ElementAt(1000);
            DataRow specificRow1 = blockingCollection.ElementAt(2000);
            var x = blockingCollection.Select(i => i.Field<string>("Name")).Distinct().ToList();

            timer.Stop();

            double timeTaken = timer.Elapsed.Seconds;
            string foo1 = "Time taken: " + timeTaken.ToString(@"m\:ss\.fff");

            Console.WriteLine(foo1);
            Parallel.ForEach(blockingCollection.GetConsumingEnumerable(), dataRow =>
            {
                // Perform thread-safe operations on dataRow
            });
        }

        static string GetRanName(Random rand)
        {
            string[] maleNames = new string[] { "aaron", "abdul", "abe", "abel", "abraham", "adam", "adan", "adolfo", "adolph", "adrian" };
            string[] femaleNames = new string[] { "abby", "abigail", "adele", "adrian" };
            String Name = string.Empty;

            if (rand.Next(1, 2) == 1)
            {
                Name = maleNames[rand.Next(0, maleNames.Length - 1)];
            }
            else
            {
                Name = femaleNames[rand.Next(0, femaleNames.Length - 1)];
            }

            return Name;

        }
    }
}

