using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBrokerCommon
{
    public struct Notification
    {
        public string NotificationType;
        public string Payload;

        public override string ToString()
        {
            System.Text.StringBuilder strbuf = new System.Text.StringBuilder("Notification: " + NotificationType + "\n");
            strbuf.Append("\tPayload: " + Payload + "\n");
            return strbuf.ToString();
        }
    }
}
