using System;
using System.Collections.Generic;
using System.Text;

namespace ThermAlarm.Common
{
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
