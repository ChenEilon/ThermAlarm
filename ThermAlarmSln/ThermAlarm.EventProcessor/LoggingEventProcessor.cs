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

                var msg = JsonConvert.DeserializeObject<MsgObj>(payload);
                msgType type = msg.mType;
                switch (type)
                {
                    case msgType.Meausurements:
                        Console.WriteLine($"Got Measurement msg from device ID: '{deviceId}'");
                        break;
                    case msgType.BTscan:
                        Console.WriteLine($"Got BT scan msg from device ID: '{deviceId}'");
                        break;
                    case msgType.MeasurementsAndBT:
                        Console.WriteLine($"Got Measurement and BT scan msg from device ID: '{deviceId}'");
                        break;
                    default:
                        Console.WriteLine("ERROR - message is of unknown Type");
                        break;
                }

            }
            return context.CheckpointAsync();
        }

        ////TODO - Gal move to backend
        //const int HEAT_MEAS_RES = 8;
        //public static void measurementsCallback(int pirValue, int[] thermValue)
        //{
        //    Console.WriteLine("Entered Measurements callback:");
        //    Console.WriteLine("PIR sensor value is: {}",pirValue);
        //    Console.WriteLine("Therm camera sensor value is: {}", thermValue);
        //}
        //public static void BTCallback()
        //{
        //    Console.WriteLine("Entered BT callback...");
        //}
        //public static int[,] Make2DArray(int[] input, int rowCount, int colCount)
        //{
        //        int[,] output = new int[rowCount, colCount];
        //        if (rowCount * colCount <= input.Length)
        //        {
        //            for (int i = 0; i < rowCount; i++)
        //            {
        //                for (int j = 0; j < colCount; j++)
        //                {
        //                    output[i, j] = input[i * colCount + j];
        //                }
        //            }
        //        }
        //        return output;
        //}
        //public static void fillData(int[] input)
        //{
        //    int[,] data = Make2DArray(input, HEAT_MEAS_RES, HEAT_MEAS_RES);
        //    int maxRow = HEAT_MEAS_RES;
        //    int maxCol = HEAT_MEAS_RES;
        //    double factor = 1.0;
        //    DataGridView DGV;

        //    DGV.RowHeadersVisible = false;
        //    DGV.ColumnHeadersVisible = false;
        //    DGV.AllowUserToAddRows = false;
        //    DGV.AllowUserToOrderColumns = false;
        //    DGV.CellBorderStyle = DataGridViewCellBorderStyle.None;
        //    //..

        //    int rowHeight = DGV.ClientSize.Height / maxRow - 1;
        //    int colWidth = DGV.ClientSize.Width / maxCol - 1;

        //    for (int c = 0; c < maxRow; c++) DGV.Columns.Add(c.ToString(), "");
        //    for (int c = 0; c < maxRow; c++) DGV.Columns[c].Width = colWidth;
        //    DGV.Rows.Add(maxRow);
        //    for (int r = 0; r < maxRow; r++) DGV.Rows[r].Height = rowHeight;

        //    List<Color> baseColors = new List<Color>();  // create a color list
        //    baseColors.Add(Color.RoyalBlue);
        //    baseColors.Add(Color.LightSkyBlue);
        //    baseColors.Add(Color.LightGreen);
        //    baseColors.Add(Color.Yellow);
        //    baseColors.Add(Color.Orange);
        //    baseColors.Add(Color.Red);
        //    List<Color> colors = interpolateColors(baseColors, 1000);

        //    for (int r = 0; r < maxRow; r++)
        //    {
        //        for (int c = 0; c < maxRow; c++)
        //        {
        //            DGV[r, c].Style.BackColor =
        //                           colors[Convert.ToInt16(data[r][c].Item2 * factor)];

        //        }
        //    }

        //}
    }
}
