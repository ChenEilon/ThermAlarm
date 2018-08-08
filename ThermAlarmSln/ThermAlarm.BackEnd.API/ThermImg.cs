using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThermAlarm.BackEnd.API
{
    public class ThermImg
    {
        private int[,] img = new int[8, 8];//TODO might need to change the data type all along code

        int [,] get_img()
        {
            return this.img;
        }

        void determine_object_size()
        {

        }

        void determine_object_shape()
        {

        }
    }
}
