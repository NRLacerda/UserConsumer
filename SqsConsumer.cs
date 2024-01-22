using Amazon.SQS;
using Amazon.SQS.Model;

namespace UserConsumer
{
    public class SqsConsumerService : BackgroundService
    {
        private readonly IAmazonSQS _sqs;
        private readonly CredentialsService _credentialsService;

        public SqsConsumerService(IAmazonSQS sqs, CredentialsService credentialsService)
        {
            _sqs = sqs;
            _credentialsService = credentialsService;
        }

        // por algum motivo eu nao consegui puxar usando o servico de credentials... vai manualmente mesmo
        private const string QueueName = "users.fifo";

        private readonly List<string> _messageAttributeNames = new() { "*" };
        protected override async Task ExecuteAsync(CancellationToken stopToken)
        {
            var queueUrl = await _sqs.GetQueueUrlAsync(QueueName, stopToken);
            Console.WriteLine(queueUrl.QueueUrl);
            var receiveRequest = new ReceiveMessageRequest
            {
                QueueUrl = queueUrl.QueueUrl,
                MessageAttributeNames = _messageAttributeNames,
                AttributeNames = _messageAttributeNames
            };
            while (!stopToken.IsCancellationRequested)
            {
                var messageResponse = await _sqs.ReceiveMessageAsync(receiveRequest, stopToken);
                if(messageResponse.HttpStatusCode != System.Net.HttpStatusCode.OK)
                {
                    // fazer o handling/logging
                    continue;
                }
                foreach (var message in messageResponse.Messages)
                {
                    Console.WriteLine(message.Body);
                }
            }
        }
    }
}
