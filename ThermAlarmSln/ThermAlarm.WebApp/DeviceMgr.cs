using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThermAlarm.Common;
using System.Text;
using System.Threading;
using Microsoft.Azure.Devices;
using Newtonsoft.Json;


namespace ThermAlarm.WebApp
{
    public static class DeviceMgr
    {

        //await CallDeviceAction("LightTry1", eDeviceAction.Arm, serviceClient);
        //await CallDeviceAction("LightTry1", eDeviceAction.Disarm, serviceClient);
        //await CallDeviceAction("LightTry1", eDeviceAction.Alarm, serviceClient);

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

        public static async Task ReceiveFeedback(ServiceClient serviceClient)
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
}
