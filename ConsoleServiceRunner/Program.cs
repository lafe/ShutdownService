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
        }
    }
}
