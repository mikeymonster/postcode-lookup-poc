using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using postcode_lookup_poc;

Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();
    })
    .Build()
    .Run();

