#!/bin/bash

# Default values
STACK_NAME="Test-SQS-Queue"
TEMPLATE_FILE="SQS-template.yml"
PARAMETERS="ParameterKey=QueueName,ParameterValue=TestQueue"

./AWS-Run-template.sh "$STACK_NAME" "$TEMPLATE_FILE" "$PARAMETERS"
