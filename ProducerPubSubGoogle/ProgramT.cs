//using Google.Cloud.PubSub.V1;
//using Google.Cloud.Storage.V1;
//using Grpc.Core;
//using System;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace ProducerPubSubGoogle
//{
//    public class Program
//    {
//        static async Task Main(string[] args)
//        {
//            try
//            {
//                string projectId = "tag-traderepository-core-lab";
//                TopicName topicName = new TopicName(projectId, "TopicTest");

//                //Create Topic
//                PublisherServiceApiClient publisherService = await PublisherServiceApiClient.CreateAsync();
//                var topic = publisherService.GetTopic(topicName, null);
//                //Delete Topic
//                publisherService.DeleteTopic(topicName);
//                publisherService.CreateTopic(topicName);

                

//                //SubscriberServiceApiClient subscriber = SubscriberServiceApiClient.Create();

//                //SubscriptionName subscriptionName = new SubscriptionName(projectId,
//                //    "SubscriptionTest");
//                //var subscription = await subscriber.CreateSubscriptionAsync(
//                //    subscriptionName, topicName, pushConfig: null,
//                //    ackDeadlineSeconds: 60);

//                ////SubscriptionName subscriptionName = new SubscriptionName(projectId, "SubscriptionTest");
//                //SubscriberClient subscriberClient = await SubscriberClient.CreateAsync(
//                //    subscriptionName);

//                //string messageSubscription = string.Empty;

//                //var teste = subscriberClient.StartAsync(
//                //    async (PubsubMessage message, CancellationToken cancel) =>
//                //    {
//                //        string text = Encoding.UTF8.GetString(message.Data.ToArray());

//                //        messageSubscription = message.MessageId;
//                //        messageSubscription = text;

//                //        await Console.Out.WriteLineAsync($"Message {message.MessageId} : {text}");

//                //        Console.WriteLine(message.Data.ToStringUtf8());

//                //        return await Task.FromResult(SubscriberClient.Reply.Ack);

//                //        //return SubscriberClient.Reply.Ack;
//                //    });

//                //Console.WriteLine($"Subscriber message {messageSubscription} ");

//               await Test();

//            }
//            catch (RpcException ex)
//            {
//            }
//        }

//        private static async Task Test()
//        {
//            SubscriberServiceApiClient subscriber = SubscriberServiceApiClient.Create();

//            TopicName topicName = new TopicName("tag-traderepository-core-lab", "TopicTest");

//            //SubscriptionName subscriptionName = new SubscriptionName("tag-traderepository-core-lab",
//            //    "SubscriptionT");
//            //var subscription = await subscriber.CreateSubscriptionAsync(
//            //    subscriptionName, topicName, pushConfig: null,
//            //    ackDeadlineSeconds: 60);

//            SubscriptionName subscriptionName = new SubscriptionName("tag-traderepository-core-lab", "SubscriptionN");
            

//            PubsubMessage messageSubscription = new PubsubMessage();

//            //SubscriberServiceApiClient subscriberService = await SubscriberServiceApiClient.CreateAsync();
//            //SubscriptionName subscriptionName = new SubscriptionName([YOUR PROJECT_ID], "generalsubscription");
//            var teste = subscriber.CreateSubscription(subscriptionName, topicName, pushConfig: null, ackDeadlineSeconds: 60);

//            SubscriberClient subscriberClient = await SubscriberClient.CreateAsync(
//                subscriptionName);

//            var result = subscriberClient.StartAsync(
//                async (PubsubMessage message, CancellationToken cancel) =>
//                {
//                    string text = Encoding.UTF8.GetString(message.Data.ToArray());

//                    messageSubscription = message;

//                    await Console.Out.WriteLineAsync($"Message {message.MessageId} : {text}");

//                    Console.WriteLine(message);

//                    return await Task.FromResult(SubscriberClient.Reply.Ack);

//                });

//            await Task.Delay(3000);
//            await subscriberClient.StopAsync(CancellationToken.None);

//            Console.WriteLine($"Subscriber message {messageSubscription} ");
//        }
//    }
//}
