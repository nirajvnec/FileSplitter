using CsvHelper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleApp5
{
    public class FileSplitter
    {
        private static readonly object syncRoot = new object();
        public  StreamWriter Writer { get; set; }

        public FileSplitter()
        {
            Writer = new StreamWriter("output.csv");
        }
        public void RunSplitter()
        {
            // Specify the path to the CSV file and the target chunk size
            string csvFilePath = @"C:\Users\Niraj\Downloads\Sample-Spreadsheet-5000-rows.csv";
            //int chunkSize = 50 * 1024; // 50 KB
            
            using (var reader = new StreamReader(csvFilePath))
            {
                int counter = 0;
                int fileCounter = 0;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    
                    //var writer = new StreamWriter();
                    if (counter == 0)
                    {
                        fileCounter++;
                        lock(syncRoot)
                         Writer = new StreamWriter("output" + fileCounter + ".csv");
                    }

                    if (counter == 400)
                    {
                        counter = 0;
                        Writer.Dispose();
                        continue;
                    }

                    Writer.WriteLine(line);
                    counter++;
                }
            }
        }
    }
}