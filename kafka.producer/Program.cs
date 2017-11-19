using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;

namespace kafka.producer
{
    class Program
    {
        static void Main(string[] args)
        {
            string kafkaEndpoint = "127.0.0.1:9092";
            string kafkaTopic = "testtopic";
            int i = 0;

            var producerConfig = new Dictionary<string, object>{ { "bootstrap.servers", kafkaEndpoint}};

            Console.WriteLine("Starting Producer");
            using(var producer = new Producer<Null, string>(producerConfig, null, new StringSerializer(Encoding.UTF8)))
            {
                var cancelled = false;
                Console.CancelKeyPress += (_, e) => {
                    e.Cancel = true;
                    cancelled = true;
                };

                while (!cancelled)
                {
                    DateTime now = DateTime.Now;
                    var message = $"Event: {now}";
                    //var message = $"Event {i}";
                    var result = producer.ProduceAsync(kafkaTopic, null, message).GetAwaiter().GetResult();

                    Console.WriteLine($"{now} Event sent on Partition: {result.Partition}, with Offset: {result.Offset}");
                    Thread.Sleep(1000);
                }
            }
        }
    }
}

