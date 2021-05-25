using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SevenDeadlySins
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    #region --設置讀取設定檔(Json)--
                    .ConfigureAppConfiguration((hostContext, config) =>
                             {
                                 var env = hostContext.HostingEnvironment;
                                 config.SetBasePath(Path.Combine(env.ContentRootPath, "Data"))
                                       .AddJsonFile(path: "setting.json", optional: false, reloadOnChange: true)
                                       .AddJsonFile(path: $"settings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                             })
                    #endregion
                    .UseStartup<Startup>();
                });
    }
}
