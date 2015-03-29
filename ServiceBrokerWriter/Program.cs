using ServiceBrokerCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ServiceBrokerWriter
{
    class Program
    {
        static void Main(string[] args)
        {
            Notification notification;
            notification.NotificationType = "ServiceBrokerWriter";
            notification.Payload = "Written by ServiceBrokerWriter on " + DateTime.Now.ToString();

            string message;
            using(StringWriter textWriter = new StringWriter())
            {
                new XmlSerializer(notification.GetType()).Serialize(textWriter, notification);
                message = textWriter.ToString();
            }

            using (SqlConnection connection = new SqlConnection(@"Data Source=localhost\sqlexpress;Initial Catalog=ServiceBrokerIntegration;Integrated Security=True"))
            {
                using (SqlCommand command = new SqlCommand("write_message", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("message", SqlDbType.NVarChar).Value = message;

                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
