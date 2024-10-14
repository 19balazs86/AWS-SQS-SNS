# Playing with AWS SQS and SNS

- This repository contains a simple worker service for sending and receiving messages using AWS SQS and SNS
- CloudFormation templates are provided for creating the necessary resources, including [Queues](aws-infra/SQS-template.yml) and [SNS Topic](aws-infra/SNS-SQS-template.yml), related policies, and subscriptions

## Resources

- [AWS CLI Command Reference](https://docs.aws.amazon.com/cli/latest)
- [CloudFormation templates](https://docs.aws.amazon.com/AWSCloudFormation/latest/UserGuide/template-guide.html): [SQS](https://docs.aws.amazon.com/AWSCloudFormation/latest/UserGuide/aws-resource-sqs-queue.html), [SNS](https://docs.aws.amazon.com/AWSCloudFormation/latest/UserGuide/aws-resource-sns-topic.html)