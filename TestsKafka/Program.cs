//using KafkaNet;
//using KafkaNet.Model;
//using KafkaNet.Protocol;
//using Microsoft.Extensions.Options;
//using System;
//using System.Collections.Generic;

//namespace ProducerKafka
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            try
//            {
//                string payload = "Welcome to Kafka!";
//                string topic = "TagTopic";
//                Message msg = new Message(payload);
//                Uri uri = new Uri("http://localhost:9021");
//                var options = new KafkaOptions(uri);
//                var router = new BrokerRouter(options);
//                var client = new Producer(router);
//                client.SendMessageAsync(topic, new List<Message> { msg }).Wait();
//                Console.ReadLine();
//            }
//            catch (Exception ex)
//            {

//            }
//        }
//    }
//}
