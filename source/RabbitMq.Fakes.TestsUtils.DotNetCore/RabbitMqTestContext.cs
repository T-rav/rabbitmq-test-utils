using System;
using System.Threading.Tasks;
using FluentAssertions;
using RabbitMQ.Context;
using RabbitMQ.Fakes;

namespace RabbitMq.TestContext
{
    public class RabbitMqTestContext : IRabbitMqTestContext
    {
        private readonly IRabbitMqContext _rabbitMqContext;
        private readonly FakeConnectionFactory _connectionFactory;
        private readonly string _queueName;

        public RabbitMqTestContext(IRabbitMqContext rabbitMqContext, 
                                   FakeConnectionFactory connectionFactory,
                                   string queueName)
        {
            _rabbitMqContext = rabbitMqContext;
            _connectionFactory = connectionFactory;
            _queueName = queueName;
        }

        public void DeclareQueue(string name)
        {
            _rabbitMqContext.DeclareQueue(name);
        }

        public void PublishMessage(string queueName, object message)
        {
            _rabbitMqContext.PublishMessage(queueName, message);
        }

        public void PublishMessage(string queueName, string exchange, object message)
        {
            _rabbitMqContext.PublishMessage(queueName,exchange, message);
        }

        public Task ConsumeMessage(string queueName, Func<byte[], Task<bool>> action)
        {
            return _rabbitMqContext.ConsumeMessage(queueName, action);
        }

        public void Assert_Queue_Message_Count_Is(uint count)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.MessageCount(_queueName).Should().Be(count);
                }
            }
        }
    }
}
