using System;
using System.Threading;
using Newtonsoft.Json;
using RabbitMQData.Models;
using RabbitMQExample.Consumer;
using RabbitMQExample.Producer;

namespace RabbitMQExample
{
    class Program
    {
        static void Main(string[] args)
        {
            RabbitMQMessageModel rabbitMQMessageModel = new RabbitMQMessageModel()
            {
                Id = 1,
                Message = "RABBITMQEXAMPLE"
            };

            RabbitMQProducer.Instance.Publish("Queue1", JsonConvert.SerializeObject(rabbitMQMessageModel));

            ThreadPool.QueueUserWorkItem(delegate
            {
                RabbitMQConsumer.Instance.Consumer("Queue1");
            });
            Console.ReadLine();
        }
    }
}
