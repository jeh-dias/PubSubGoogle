using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QueuePubSub
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var manager = new Manager("tag-traderepository-core-lab");

                var topicName = manager.CreateTopic();

                var messageProducer = await manager.CreatePublisher(topicName);
                Console.WriteLine($"Publisher message {messageProducer.ToString()} ");

                await manager.CreateSubscriber(topicName);

                Console.ReadLine();

            }
            catch (Exception exception)
            {

            }
            
        }
    }
}
