using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ThermAlarm.Common
{
    //TODO - delete class
    public delegate void msgReceivedHandler(MsgObj msg);
    public static class MsgReceivedEvent
    {
        public static event msgReceivedHandler MsgReceived;
        /*Raise event function*/
        public static void OnMsgReceived(MsgObj msg)
        {
            if (MsgReceived != null)
            {
                MsgReceived(msg);
            }
        }

        
    }
}
