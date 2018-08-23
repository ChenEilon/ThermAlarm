using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ThermAlarm.Common
{
    public class Alarm
    {
        public eDeviceAction status { get; set; }
        private Hashtable family;

        public Alarm()
        {
            this.status = eDeviceAction.Disarm;
            if(DatabaseMgr.isFamily())
            {
                this.family = DatabaseMgr.GetFamily();
            }
            else
            {
                this.family = new Hashtable();
            }
        }

        /**************************** Family *****************************/

        public void addFamilyMember(Person p)
        {
            this.family.Add(p.BTid, p);
            DatabaseMgr.addPersonToFamily(p);
        }

        public void removeFamilyMember(Person p)
        {
            this.family.Remove(p.BTid);
            DatabaseMgr.removePersonFromFamily(p);
        }

        public bool isFamilyMember(String BTid)
        {
            return family.ContainsKey(BTid);
        }

        



    }
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