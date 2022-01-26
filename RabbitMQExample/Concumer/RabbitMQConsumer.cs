using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQData.Interfaces;
using RabbitMQExample.Connection;

namespace RabbitMQExample.Consumer
{
    public class RabbitMQConsumer : IRabbitMQConsumer
    {
        private RabbitMQConsumer()
        {
        }

        private static readonly Lazy<RabbitMQConsumer>
            instance = new Lazy<RabbitMQConsumer>(() => new RabbitMQConsumer());

        private RabbitMQConnection rabbitMqServices; //rabbitmq getirmek için oluşturulan sınıf

        private EventingBasicConsumer eventingBasicConsumer; //modeli dinlemek için oluşturulan sınıf

        public static RabbitMQConsumer Instance => instance.Value; //bu class a dışardan erişimi sağlar

        public RabbitMQConnection RabbitMqServices //rabbitmq bağlantısını getirmek için oluşturuldu
        {
            get
            {
                if (rabbitMqServices == null || !rabbitMqServices.IsConnected)
                {
                    rabbitMqServices = new RabbitMQConnection();
                }

                return rabbitMqServices;
            }
            set => rabbitMqServices = value;
        }

        public void Consumer(string queue) //gelen kuyruktaki mesajları okur
        {
            try
            {
                IModel channel = RabbitMqServices.GetChannel(queue);
                eventingBasicConsumer = new EventingBasicConsumer(channel);
                eventingBasicConsumer.Received += EventingBasicConsumerOnReceived;
                channel.BasicConsume(queue, true, eventingBasicConsumer);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex + "RabbitmqConcumer"}");
            }
        }
        private void EventingBasicConsumerOnReceived(object sender, BasicDeliverEventArgs e) //kuyruktaki mesaj düştükten sonra çalışan metot
        {
            string jsonData = Encoding.UTF8.GetString(e.Body.ToArray());
            Console.WriteLine(jsonData);
        }
    }
}
