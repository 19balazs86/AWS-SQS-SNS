#!/bin/bash

# Default values
STACK_NAME="Test-SNS-Topic"
TEMPLATE_FILE="SNS-SQS-template.yml"

./AWS-Run-template.sh "$STACK_NAME" "$TEMPLATE_FILE"
