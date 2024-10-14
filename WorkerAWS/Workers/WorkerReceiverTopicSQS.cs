using Amazon.SQS;
using Amazon.SQS.Model;

namespace WorkerAWS.Workers;

// There is no specific client receiving messages from the Topic, because the Topic sends the messages to an SQS queue
public sealed class WorkerReceiverTopicSQS(IAmazonSQS _sqsClient, IConfiguration _configuration, ILogger<WorkerReceiverTopicSQS> _logger)
    : WorkerBaseSQS(_sqsClient)
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        string queue1Url = await getQueueUrlByName(_configuration["SNS:Queue1Name"], stoppingToken);
        string queue2Url = await getQueueUrlByName(_configuration["SNS:Queue2Name"], stoppingToken);

        Task task1 = receiveMessages(queue1Url, stoppingToken);
        Task task2 = receiveMessages(queue2Url, stoppingToken);

        await Task.WhenAll(task1, task2);
    }

    private async Task receiveMessages(string queueUrl, CancellationToken ct)
    {
        var receiveMessage = new ReceiveMessageRequest
        {
            QueueUrl            = queueUrl,
            WaitTimeSeconds     = 15,
            MaxNumberOfMessages = 5
        };

        while (!ct.IsCancellationRequested)
        {
            ReceiveMessageResponse response = await _sqsClient.ReceiveMessageAsync(receiveMessage, ct);

            foreach (Message message in response.Messages)
            {
                // Process the message
                _logger.LogInformation("Message received: '{Body}' | QueueUrl: {Url}", message.Body, queueUrl);

                // Delete the message
                await _sqsClient.DeleteMessageAsync(queueUrl, message.ReceiptHandle, ct);
            }
        }
    }
}
