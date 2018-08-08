using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Newtonsoft.Json;
using ThermAlarm.Common;


namespace ThermAlarm.DeviceMgr
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //init
            string serviceConnectionString = Configs.serviceConnectionString;
            ServiceClient serviceClient = ServiceClient.CreateFromConnectionString(serviceConnectionString);
            var registryManager = RegistryManager.CreateFromConnectionString(serviceConnectionString);
            var feedbackTask = ReceiveFeedback(serviceClient);

            await CallDeviceAction("LightTry1", eDeviceAction.Arm, serviceClient);
            await CallDeviceAction("LightTry1", eDeviceAction.Disarm, serviceClient);
            await CallDeviceAction("LightTry1", eDeviceAction.Alarm, serviceClient);


        }

        public static async Task CallDeviceAction(string deviceId, eDeviceAction action, ServiceClient serviceClient)
        {
            
            //msg C2D to activate action:
            deviceAction act = new deviceAction(action.ToString(), null);
            string Payload = JsonConvert.SerializeObject(act);//, Formatting.Indented);
            await SendCloudToDeviceMessage(serviceClient, deviceId, Payload);

        }

        private static async Task SendCloudToDeviceMessage(
            ServiceClient serviceClient,
            string deviceId,
            string payload)
        {
            var commandMessage = new Message(Encoding.ASCII.GetBytes(payload));
            commandMessage.MessageId = Guid.NewGuid().ToString();
            commandMessage.Ack = DeliveryAcknowledgement.Full;
            commandMessage.ExpiryTimeUtc = DateTime.UtcNow.AddSeconds(10);

            await serviceClient.SendAsync(deviceId, commandMessage);
        }

        private static async Task ReceiveFeedback(ServiceClient serviceClient)
        {
            var feedbackReceiver = serviceClient.GetFeedbackReceiver();

            while (true)
            {
                var feedbackBatch = await feedbackReceiver.ReceiveAsync();

                if (feedbackBatch == null)
                {
                    continue;
                }

                foreach (var record in feedbackBatch.Records)
                {
                    var messageId = record.OriginalMessageId;
                    var statusCode = record.StatusCode;

                    Console.WriteLine($"Feedback for message '{messageId}', status code: {statusCode}.");

                }

                await feedbackReceiver.CompleteAsync(feedbackBatch);
            }
        }

    }

    public class deviceAction
    {
        public string Name;
        public String Parameters;
        public deviceAction(string Name, String parameters)
        {
            this.Name = Name;
            if (this.Parameters==null)
            {
                this.Parameters = "{}";
            }
            else
            {
                this.Parameters = parameters;
            }
            
        }
    }
}
