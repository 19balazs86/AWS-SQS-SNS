using Amazon.SQS;
using Amazon.SQS.Model;

namespace WorkerAWS.Workers;

public sealed class WorkerSenderSQS(IAmazonSQS _sqsClient, IConfiguration _configuration) : WorkerBaseSQS(_sqsClient)
{
    private int _messageId = 0;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        string queueUrl = await getQueueUrlByName(_configuration["SQS:QueueName"], stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            string messageBody = $"Hello from SQS | MessageId: {++_messageId} At: {DateTime.Now:HH:mm:ss}";

            var sendMessage = new SendMessageRequest//(queueUrl, messageBody);
            {
                QueueUrl    = queueUrl,
                MessageBody = messageBody // Normally you would serialize a JSON to send
                // MessageAttributes =
                // MessageGroupId = "MyGroup" // It works like the SessionId in an Azure Service Bus message
            };

            // SendMessageResponse response = await _sqsClient.SendMessageAsync(queueUrl, messageBody);

            SendMessageResponse response = await _sqsClient.SendMessageAsync(sendMessage, stoppingToken);

            await Task.Delay(1_000, stoppingToken);
        }
    }
}
