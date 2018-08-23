using System;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.ServiceBus; //?
using ThermAlarm.Common;


namespace ThermAlarm.EventProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            var hubName = Configs.HUB_NAME;
            var iotHubConnctionString = Configs.IOT_HUB_ENDPOINT_CONNECTION_STRING;
            var storgaeConnectionString = Configs.STORAGE_CONNECTION_STRING;
            var storageContainerName = Configs.STORAGE_CONTAINER_NAME;
            var consumerGroupName = PartitionReceiver.DefaultConsumerGroupName;

            var processor = new EventProcessorHost(
                hubName,
                consumerGroupName,
                iotHubConnctionString,
                storgaeConnectionString,
                storageContainerName);
            processor.RegisterEventProcessorAsync<LoggingEventProcessor>().Wait();

            var eventHubConfig = new EventHubConfiguration();
            eventHubConfig.AddEventProcessorHost(hubName,processor);

            var configuration = new JobHostConfiguration(storgaeConnectionString);
            configuration.UseEventHub(eventHubConfig);

            Console.WriteLine("Starting job host (event processor)...");
            var host = new JobHost(configuration);
            host.RunAndBlock();

        }
    }
}
