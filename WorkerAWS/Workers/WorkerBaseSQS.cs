using Amazon.SQS;
using Amazon.SQS.Model;

namespace WorkerAWS.Workers;

public abstract class WorkerBaseSQS : BackgroundService
{
    protected readonly IAmazonSQS _sqsClient;

    public WorkerBaseSQS(IAmazonSQS sqsClient)
    {
        _sqsClient = sqsClient;
    }

    protected async Task<string> getQueueUrlByName(string? queueName, CancellationToken ct = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(queueName);

        try
        {
            // You can simply use the URL from AWS, but retrieving the URL by name is more elegant
            // You can have the same configuration for different regions where the queue exists with the same name

            GetQueueUrlResponse urlResponse = await _sqsClient.GetQueueUrlAsync(queueName, ct);

            return urlResponse.QueueUrl;
        }
        catch (QueueDoesNotExistException)
        {
            throw new InvalidOperationException($"The specified queue('{queueName}') does not exist");
        }
    }
}
