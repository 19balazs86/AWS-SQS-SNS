using Amazon.SQS;
using Amazon.SQS.Model;

namespace WorkerAWS.Workers;

// Receiving messages works the same way as in Azure Storage Queues
// The received messages has a Visibility Timeout
// You can have multiple receivers for the same queue
public sealed class WorkerReceiverSQS(IAmazonSQS _sqsClient, IConfiguration _configuration, ILogger<WorkerReceiverSQS> _logger)
    : WorkerBaseSQS(_sqsClient)
{
    private string? _queueUrl;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _queueUrl = await getQueueUrlByName(_configuration["SQS:QueueName"], stoppingToken);

        var receiveMessage = new ReceiveMessageRequest
        {
            QueueUrl            = _queueUrl,
            WaitTimeSeconds     = 15,
            MaxNumberOfMessages = 5
        };

        while (!stoppingToken.IsCancellationRequested)
        {
            ReceiveMessageResponse response = await _sqsClient.ReceiveMessageAsync(receiveMessage, stoppingToken);

            foreach (Message message in response.Messages)
            {
                // Process the message
                _logger.LogInformation("Message received: '{Body}'", message.Body);

                // Simulate an error
                if (Random.Shared.Next(0, 100) <= 15)
                {
                    _logger.LogError("Simulate an error for the message({Id})", message.MessageId);

                    await changeMessageToReprocess(message.ReceiptHandle, stoppingToken);

                    continue;
                }

                // Delete the message
                await _sqsClient.DeleteMessageAsync(_queueUrl, message.ReceiptHandle, stoppingToken);
            }
        }
    }

    private async Task changeMessageToReprocess(string receiptHandle, CancellationToken ct)
    {
        var visibilityRequest = new ChangeMessageVisibilityRequest
        {
            QueueUrl          = _queueUrl,
            ReceiptHandle     = receiptHandle,
            VisibilityTimeout = 0
        };

        // await _sqsClient.ChangeMessageVisibilityAsync(_queueUrl, receiptHandle, 0, ct);

        await _sqsClient.ChangeMessageVisibilityAsync(visibilityRequest, ct);
    }
}
