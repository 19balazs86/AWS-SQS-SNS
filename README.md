# Playing with AWS SQS and SNS

- This repository contains a simple worker service for sending and receiving messages using AWS SQS and SNS
- CloudFormation templates are provided for creating the necessary resources, including [Queues](aws-infra/SQS-template.yml) and [SNS Topic](aws-infra/SNS-SQS-template.yml), related policies, and subscriptions

## Resources

#### 🧰 `AWS .NET SDK`

- [Documentation](https://docs.aws.amazon.com/sdk-for-net)
- [Developer Guide](https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/welcome.html)
- [API Reference](https://docs.aws.amazon.com/sdkfornet/v3/apidocs)

#### ✉️ `SQS and SNS`

- [AWS CLI Command Reference](https://docs.aws.amazon.com/cli/latest)
- [CloudFormation templates](https://docs.aws.amazon.com/AWSCloudFormation/latest/UserGuide/template-guide.html): [SQS](https://docs.aws.amazon.com/AWSCloudFormation/latest/UserGuide/aws-resource-sqs-queue.html), [SNS](https://docs.aws.amazon.com/AWSCloudFormation/latest/UserGuide/aws-resource-sns-topic.html)
- [Message Processing Framework](https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/msg-proc-fw.html)
  - [AWS message processing with AWS.Messaging](https://youtu.be/jCrk5M1dcU0?list=PL59L9XrzUa-kl89ThijziX03fgTrbZCd7) 📽️*20 min - Rahul Nath*
- [Getting started with SQS](https://youtu.be/U7PvdYlvA-8) 📽️*37 min - Rahul Nath*