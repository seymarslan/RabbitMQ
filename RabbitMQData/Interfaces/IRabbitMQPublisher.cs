using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQData.Interfaces
{
    public interface IRabbitMQPublisher
    {
        void Publish(string queueName, object message);
    }
}
