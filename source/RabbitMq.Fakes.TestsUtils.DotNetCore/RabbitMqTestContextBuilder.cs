using RabbitMQ.Client;
using RabbitMQ.Context;
using RabbitMQ.Fakes;

namespace RabbitMq.TestContext
{
    public class RabbitMqTestContextBuilder
    {
        private string _exchangeName;
        private string _queueName;

        public RabbitMqTestContextBuilder()
        {
            _exchangeName = string.Empty;
            _queueName = string.Empty;
        }

        public RabbitMqTestContextBuilder With_Exchange(string exchangeName)
        {
            _exchangeName = exchangeName;

            return this;
        }

        public RabbitMqTestContextBuilder With_Queue(string queueName)
        {
            _queueName = queueName;
            return this;
        }

        public IRabbitMqTestContext Build()
        {
            var rabbitServer = new RabbitServer();
            var connectionFactory = new FakeConnectionFactory(rabbitServer);
            var rabbitMqContext = new RabbitMqContext(connectionFactory);

            Configure_Queue_Binding(rabbitServer, _exchangeName, _queueName);

            return new RabbitMqTestContext(rabbitMqContext, connectionFactory, _queueName);
        }

        private void Configure_Queue_Binding(RabbitServer rabbitServer, string exchangeName, string queue)
        {
            var connectionFactory = new FakeConnectionFactory(rabbitServer);
            using (var connection = connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queue, durable: false, exclusive: false, autoDelete: false, arguments: null);
                channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Direct);

                channel.QueueBind(queue, exchangeName, queue);
            }
        }
    }
}
