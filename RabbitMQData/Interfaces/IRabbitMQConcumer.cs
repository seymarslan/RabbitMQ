using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQData.Interfaces
{
    public interface IRabbitMQConsumer
    {
        void Consumer(string queue);
    }
}
