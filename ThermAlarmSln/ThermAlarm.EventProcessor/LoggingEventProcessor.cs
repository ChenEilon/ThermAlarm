using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
using Newtonsoft.Json;
using ThermAlarm.Common;

namespace ThermAlarm.EventProcessor
{
    

    class LoggingEventProcessor : IEventProcessor
    {
        public void msgReceivedHandler(MsgObj msg, String deviceId)
        {
            //DatabaseMgr.LogInDB(msg); /*Log msg in DB*/ TODO figure where to save in DB
            eMsgType type = msg.mType;
            switch (type)
            {
                case eMsgType.Meausurements:
                    Console.WriteLine($"Got Measurement msg from device ID: '{deviceId}'");
                    break;
                case eMsgType.BTscan:
                    Console.WriteLine($"Got BT scan msg from device ID: '{deviceId}'");
                    break;
                case eMsgType.MeasurementsAndBT:
                    Console.WriteLine($"Got Measurement and BT scan msg from device ID: '{deviceId}'");
                    break;
                default:
                    Console.WriteLine("ERROR - message is of unknown Type");
                    break;
            }

            MsgReceivedEvent.OnMsgReceived(msg); // Raise event msgReceived
        }

        

        public Task OpenAsync(PartitionContext context)
        {
            Console.WriteLine("LoggingEventProcessor opened, processing partition: " +
                              $"'{context.PartitionId}'");
            return Task.CompletedTask;
        }

        public Task CloseAsync(PartitionContext context, CloseReason reason)
        {
            Console.WriteLine("LoggingEventProcessor closing, partition: " +
                              $"'{context.PartitionId}', reason: '{reason}'.");
            return Task.CompletedTask;
        }

        public Task ProcessErrorAsync(PartitionContext context, Exception error)
        {
            Console.WriteLine("LoggingEventProcessor error, partition: " +
                              $"{context.PartitionId}, error: {error.Message}");
            return Task.CompletedTask;
        }

        public Task ProcessEventsAsync(PartitionContext context, IEnumerable<EventData> messages)
        {
            Console.WriteLine($"Batch of events received on partition '{context.PartitionId}'.");

            foreach (var eventData in messages)
            {
                var payload = Encoding.ASCII.GetString(eventData.Body.Array,
                    eventData.Body.Offset,
                    eventData.Body.Count);

                var deviceId = eventData.SystemProperties["iothub-connection-device-id"];

                Console.WriteLine($"Message received on partition '{context.PartitionId}', " +
                                  $"device ID: '{deviceId}', " +
                                  $"payload: '{payload}'");

                MsgObj msg = JsonConvert.DeserializeObject<MsgObj>(payload);
                msgReceivedHandler(msg, (String)deviceId);
            }
            return context.CheckpointAsync();
        }

        
    }


}
