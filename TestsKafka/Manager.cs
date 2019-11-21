using Google.Cloud.PubSub.V1;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Producer
{
    public class Manager
    {
        private readonly string _projectId;
        public Manager(string projectId)
        {
            _projectId = projectId;
        }

        public TopicName CreateTopic()
        {
            PublisherServiceApiClient publisher = PublisherServiceApiClient.Create();

            Topic topic;
            try
            {
                var topicName = new TopicName(_projectId, "topicId");
                topic = publisher.CreateTopic(topicName.ToString());
                return topicName;
            }
            catch (RpcException e)
            when (e.Status.StatusCode == StatusCode.AlreadyExists)
            {
                return null;
            }

        }

        public async Task CreateProducer(TopicName topic)
        {
            PublisherClient producer;
            try
            {

                producer = await PublisherClient.CreateAsync(topic);
                var messageProduter = await producer.PublishAsync("Hello, Pubsub Google");
                Console.WriteLine($"Producer message {messageProduter.ToString()} ");
            }
            catch (RpcException exception)
            {
            }
        }

        public async Task CreateConsumer(TopicName topic)
        {
            SubscriberServiceApiClient subscriber = SubscriberServiceApiClient.Create();

            SubscriptionName subscriptionName = new SubscriptionName(_projectId, "SubscriptionN");

            PubsubMessage messageSubscription = new PubsubMessage();

            var subscriptionCreated = subscriber.CreateSubscription(subscriptionName, topic, pushConfig: null, ackDeadlineSeconds: 60);

            SubscriberClient subscriberClient = await SubscriberClient.CreateAsync(
                subscriptionName);

            var result = subscriberClient.StartAsync(
               async (PubsubMessage message, CancellationToken cancel) =>
               {
                   string text = Encoding.UTF8.GetString(message.Data.ToArray());

                   messageSubscription = message;

                   await Console.Out.WriteLineAsync($"Message {message.MessageId} : {text}");

                   Console.WriteLine(message);

                   return await Task.FromResult(SubscriberClient.Reply.Ack);

               });

            await Task.Delay(3000);
            await subscriberClient.StopAsync(CancellationToken.None);

            Console.WriteLine($"Subscriber message {messageSubscription} ");
        }
    }
}

