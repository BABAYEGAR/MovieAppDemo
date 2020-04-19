using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MovieDemo.Models.Services;

namespace MovieDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //program startup
            BuildWebHost(args).Run();
        }
        /// <summary>
        /// Include use of kestrel server & IIS
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseUrls("http://localhost:5000/")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
        }
    }
}
