using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;

namespace WorkerAWS.Workers;

public sealed class WorkerPublisherSNS(IAmazonSimpleNotificationService _snsClient, IConfiguration _configuration) : BackgroundService
{
    private int _messageId = 0;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Topic topic = await _snsClient.FindTopicAsync(_configuration["SNS:TopicName"]);

        while (!stoppingToken.IsCancellationRequested)
        {
            string messageBody = $"Hello from SQS | MessageId: {++_messageId} At: {DateTime.Now:HH:mm:ss}";

            var publishRequest = new PublishRequest()//(topicArn, messageBody);
            {
                TopicArn = topic.TopicArn,
                Message  = messageBody // Normally you would serialize a JSON to send
                // MessageAttributes =
                // MessageGroupId = "MyGroup" // It works like the SessionId in an Azure Service Bus message
            };

            // PublishResponse response = await _snsClient.PublishAsync(topicArn, messageBody, stoppingToken);

            PublishResponse response = await _snsClient.PublishAsync(publishRequest, stoppingToken);

            await Task.Delay(1_000, stoppingToken);
        }
    }
}
