using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace RabbitMQExample.Connection
{
    public class RabbitMQConnection
    {
        public RabbitMQConnection()
        {
            GetConnection();
        }
        private static readonly object lockobj = new object();
        public IConnection Connection { get; set; }
        public bool IsConnected { get; set; } 

        public IConnection GetConnection() //bağlantı oluşturma
        {
            if (IsConnected) 
                return Connection;
            try
            {
                lock (lockobj)
                {
                    if (IsConnected)  
                        return Connection;
                    {
                        ConnectionFactory connectionFactory = new ConnectionFactory
                        {
                            Uri = new Uri(Environment.GetEnvironmentVariable("RABBITMQ_URI"))
                        };
                        Connection = connectionFactory.CreateConnection();
                        IsConnected = true;
                        return Connection;
                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{"RabbitMQConnection" + ex.Message}");
                return null;

            }

        }

        public IModel GetChannel(string queueName)
        {
            IModel channel = Connection.CreateModel();
            channel.QueueDeclare(queueName, false, false, true, null);
            return channel;
        }
    }
}
