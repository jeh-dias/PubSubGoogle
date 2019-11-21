using Google.Cloud.PubSub.V1;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QueuePubSub
{
    public class Manager
    {
        private readonly string _projectId;
        public Manager(string projectId)
        {
            _projectId = projectId;
        }

        public Manager()
        {
        }

        public TopicName CreateTopic()
        {
            PublisherServiceApiClient publisher = PublisherServiceApiClient.Create();
            var topicName = new TopicName(_projectId, "topicId");
            try
            {
                
                return publisher.CreateTopic(topicName).TopicName;
            }
            catch (RpcException e)
            when (e.Status.StatusCode == StatusCode.AlreadyExists)
            {
                return publisher.GetTopic(topicName).TopicName;
            }

        }

        public async Task<string> CreatePublisher(TopicName topicName)
        {
            PublisherClient publisher;
            try
            {
                publisher = await PublisherClient.CreateAsync(topicName);
                var messageProducer = await publisher.PublishAsync("Hello, Pubsub Google");

                return messageProducer;
            }
            catch (RpcException exception)
            {
                return exception.Message;
            }
        }

        public async Task<object> CreateSubscriber(TopicName topic)
        {
            
            SubscriberServiceApiClient subscriber = SubscriberServiceApiClient.Create();

            SubscriptionName subscriptionName = new SubscriptionName(_projectId, "SubscriptionJ");

            var subscriptionCreated = subscriber.CreateSubscription(subscriptionName, topic, pushConfig: null, ackDeadlineSeconds: 60);

            SubscriberClient subscriberClient = await SubscriberClient.CreateAsync(subscriptionName);

            try
            {
                _= subscriberClient.StartAsync(
                   async (PubsubMessage message, CancellationToken cancel) =>
                   {
                       string text = Encoding.UTF8.GetString(message.Data.ToArray());

                       await Console.Out.WriteLineAsync($"Consumer {message.MessageId} => Message:{text}");

                       Console.WriteLine(message);

                       return await Task.FromResult(SubscriberClient.Reply.Ack);

                   });

                await Task.Delay(3000);
                await subscriberClient.StopAsync(CancellationToken.None);

            }
            catch (RpcException exception)
            {
            }

            return 0;
        }
    }
}

