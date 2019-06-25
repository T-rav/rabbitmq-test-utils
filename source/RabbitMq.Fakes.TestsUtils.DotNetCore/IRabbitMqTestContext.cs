using RabbitMQ.Context;

namespace RabbitMq.TestContext
{
    public interface IRabbitMqTestContext : IRabbitMqContext
    {
        void Assert_Queue_Message_Count_Is(uint count);
    }
}
