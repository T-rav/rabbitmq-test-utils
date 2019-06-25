using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Context;

namespace RabbitMq.Fakes.TestsUtils.DotNetCore
{
    public interface IRabbitMqTestContext : IRabbitMqContext
    {
        void Assert_Queue_Message_Count_Is(uint count);
    }
}
