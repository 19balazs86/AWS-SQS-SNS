using Amazon.SQS;
using WorkerAWS.Workers;

namespace WorkerAWS;

public static class Program
{
    public static void Main(string[] args)
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
        IServiceCollection services    = builder.Services;

        // Add services to the container
        {
            services.AddHostedService<WorkerSenderSQS>();
            services.AddHostedService<WorkerReceiverSQS>();

            services.AddSingleton<IAmazonSQS>(new AmazonSQSClient());
        }

        // Run
        IHost host = builder.Build();

        host.Run();
    }
}