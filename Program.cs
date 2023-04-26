// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

using IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services => 
                {
                    services.AddSingleton<ISchedule, Schedule>();
                    services.AddSingleton<IOrder, Order>();
                    services.AddSingleton<ILoadOrder, LoadOrder>();
                })
                .Build();

StartProcess(host.Services);

await host.RunAsync();


static void StartProcess(IServiceProvider provider)
{
    ISchedule scheduleService = provider.GetRequiredService<ISchedule>();
    Console.WriteLine("-- Flight Schedule --");
    scheduleService.PrintSchedule();

    IOrder orderService = provider.GetRequiredService<IOrder>();
    Console.WriteLine("-- Orders Schedule --");
    orderService.PrintOrders();
}