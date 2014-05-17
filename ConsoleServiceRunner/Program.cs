using System;
using System.Linq;
using lafe.ShutdownService.ServiceLibrary;

namespace lafe.ShutdownService.ConsoleServiceRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new Service();
            service.OnStart(args);

            Generate();
        }

        private static void Generate()
        {
            var file = @"D:\Users\Lars Fernhomberg\Documents\Visual Studio 2013\Projects\ShutdownService\ShutdownService\Logging\LogNumbers.txt";

            var lines = System.IO.File.ReadAllLines(file);
            if (lines.Length == 0)
            {
                Console.WriteLine("No lines found");
                return;
            }

            var baseNumber = 0;
            var startIndex = 0;
            var success = int.TryParse(lines[0], out baseNumber);
            if (success)
            {
                //If first line was number, content starts in line 2
                startIndex++;
            }

            var counter = 0;
            foreach (var line in lines.Skip(startIndex))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                counter++;
                var id = baseNumber + counter;
                Console.WriteLine("{0} {1}", line, id);
            }
        }
    }
}
