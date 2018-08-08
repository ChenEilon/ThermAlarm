using System;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;


namespace ThermAlarm.EventProcessor
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var hubName = "iothub-ehub-iothubligh-621748-5b7250a98c";
            var iotHubConnctionString = "Endpoint=sb://ihsuprodbyres067dednamespace.servicebus.windows.net/;SharedAccessKeyName=iothubowner;SharedAccessKey=6iyC4LYT9cRhU8fLbO8uCsJdemq1yCh4OoXw+zFBk1I=";
            var storgaeConnectionString = "DefaultEndpointsProtocol=https;AccountName=lighttrystorage;AccountKey=yDgACrPnyIl4F1lCjIV/+6Sq+njDSS5mD9gaBvyc76CvpNtZq2E3MfewPKLxu8dduQGpvhcVttYOd4/4tj8VaQ==;EndpointSuffix=core.windows.net";
            var storageContainerName = "msg-processor-host";
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
