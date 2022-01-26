using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQData.Interfaces;
using RabbitMQExample.Connection;

namespace RabbitMQExample.Producer
{
    public class RabbitMQProducer : IRabbitMQPublisher
    {
        private static readonly Lazy<RabbitMQProducer> _instance = new Lazy<RabbitMQProducer>(() => new RabbitMQProducer());

        private RabbitMQProducer()
        {

        }

        public static RabbitMQProducer Instance => _instance.Value;

        private RabbitMQConnection rabbitMQConnection;
        private RabbitMQConnection RabbitmqConnection //rabbitmq bağlantısı almak için
        {
            get
            {
                if (rabbitMQConnection == null || !rabbitMQConnection.IsConnected)
                    rabbitMQConnection = new RabbitMQConnection();
                return rabbitMQConnection;
            }
            set => rabbitMQConnection = value;
        }
         
        public void Publish(string queueName, object message) //aldığı mesajı aldığı kuyruğa yazar
        {
            try
            {
                using (IModel channel = RabbitmqConnection.GetChannel(queueName))
                {
                    channel.BasicPublish(string.Empty, queueName, null, Encoding.UTF8.GetBytes(message.ToString()));
                }
                
            }
            catch (Exception ex)
            {

                Console.WriteLine($"{"RabbitMQPublisher" + ex.Message}");
            }
        }
    }
}
