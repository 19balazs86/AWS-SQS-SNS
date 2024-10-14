using Amazon.SimpleNotificationService;
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

            services.AddHostedService<WorkerPublisherSNS>();
            services.AddHostedService<WorkerReceiverTopicSQS>();

            services.AddSingleton<IAmazonSQS>(new AmazonSQSClient());
            services.AddSingleton<IAmazonSimpleNotificationService>(new AmazonSimpleNotificationServiceClient());
        }

        // Run
        IHost host = builder.Build();

        host.Run();
    }
}