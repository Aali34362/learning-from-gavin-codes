using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TCPData;
using ThePretendCompanyApplication;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddTransient<Data>();
    })
    .Build();

LinqProgram.LinqProgramMain();
