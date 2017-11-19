using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;

namespace kafka.pubsub.console
{
    class Program
    {
        static void Main(string[] args)
        {
            var group = args[0];
            var name = args[1];
            string kafkaEndpoint = "127.0.0.1:9092";
            string kafkaTopic = "testtopic";

            var consumerConfig = new Dictionary<string, object>
            {
                {"group.id", group},
                {"metadata.max.age.ms", 1000},
                {"bootstrap.servers", kafkaEndpoint},
            };

            Console.WriteLine($"Starting Consumer {group}:{name}");
            var consumer = new Consumer<Null, string>(consumerConfig, null, new StringDeserializer(Encoding.UTF8));
            consumer.OnMessage += (obj, msg) =>
                {
                    Console.WriteLine($"{group}:{name} Received: {msg.Value}");
                };

            var cancelled = false;
            Console.CancelKeyPress += (_, e) => {
                e.Cancel = true;
                cancelled = true;
            };

            consumer.Subscribe(new List<string>() { kafkaTopic });

            while (!cancelled)
            {
                consumer.Poll(10);
            }
        }
    }
}
