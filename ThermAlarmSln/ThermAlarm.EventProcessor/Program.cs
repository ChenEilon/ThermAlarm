using System;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
using ThermAlarm.Common;


namespace ThermAlarm.EventProcessor
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var hubName = Configs.HUB_NAME;
            var iotHubConnctionString = Configs.IOT_HUB_CONNECTION_STRING;
            var storgaeConnectionString = Configs.STORAGE_CONNECTION_STRING;
            var storageContainerName = Configs.STORAGE_CONTAINER_NAME;
            var consumerGroupName = PartitionReceiver.DefaultConsumerGroupName;

            var processor = new EventProcessorHost(
                hubName,
                consumerGroupName,
                iotHubConnctionString,
                storgaeConnectionString,
                storageContainerName);
            await processor.RegisterEventProcessorAsync<LoggingEventProcessor>();

            Console.WriteLine("Event processor started, press enter to exit...");
            Console.ReadLine();

            await processor.UnregisterEventProcessorAsync();


        }
    }
}
