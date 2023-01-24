using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    internal class ParallelCall
    {
        public static AsyncLocal<DataRow?> asyncLocal = new AsyncLocal<DataRow?>();
        public static object syncRoot = new object();
        public static void Run()
        {
            var timer = new Stopwatch();
            timer.Start();
            DataTable dt = new DataTable();
            DataColumn empName = new DataColumn("Name", typeof(String));
            DataColumn empSalary = new DataColumn("Salary", typeof(Int32));

            dt.Columns.Add(empName);
            dt.Columns.Add(empSalary);

            Random rand = new Random(DateTime.Now.Second);

            


            for (int i = 0; i <= 10000000; i++)
            {

                DataRow dr = dt.NewRow();
                dr["Name"] = GetRanName(rand);
                dr["Salary"] = i + 1000;
                dt.Rows.Add(dr);
            }

            //dt.Clear();
            timer.Stop();

            double timeTaken = timer.Elapsed.Seconds;
            string foo1 = "Time taken: " + timeTaken.ToString(@"m\:ss\.fff");

            Console.WriteLine(foo1);

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

