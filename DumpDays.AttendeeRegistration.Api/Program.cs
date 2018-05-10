using System;
using Microsoft.Owin.Hosting;

namespace DumpDays.AttendeeRegistration.Api
{
    public class Program
    {
        static void Main()
        {
            var baseAddress = "http://localhost:9000";
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine("Server is running on {0}", baseAddress);
                Console.Read();
            }
        }
    }
}